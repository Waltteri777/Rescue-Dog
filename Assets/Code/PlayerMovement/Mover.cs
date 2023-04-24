using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class Mover : MonoBehaviour
{
    private InputReader inputReader;
    private Rigidbody rb;
    private Coroutine coroutine;
    private Vector3 targetPosition;
    [HideInInspector] public bool isDiggingEnabled = false;
    private bool isTeleporting = false; 
    [SerializeField] private RingMenuSpawn ringMenuSpawn;
    [SerializeField] private ButtonClick buttonClick;
    private float clickInput;
    private NavMeshAgent agent;
    private GameObject further = null;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        clickInput = inputReader.GetClickInput();
        if(!isTeleporting)
        {
            Click();
        }
        
    }

    public void Click()
    {
    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
    if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider && clickInput == 1 && ringMenuSpawn.menuIsActive == false)
    {
        ringMenuSpawn.SpawnRingMenu(hit.collider.gameObject.tag);
        targetPosition = hit.point;
        }
    }

    public void StartMoveCoroutine()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(ClickMove(targetPosition));
    }
    
    public IEnumerator ClickMove(Vector3 target)
    {
        while(Vector3.Distance(transform.position, target) > 0.1f)
        {

            agent.SetDestination(target);

            if(buttonClick.pickUpEnabled == true)
            {
                yield return StartCoroutine(PickUp());
            }
            if(buttonClick.interactEnabled == true)
            {
                yield return StartCoroutine(Interact());
            }
            if(buttonClick.digEnabled == true)
            {
                yield return StartCoroutine(Dig());
            }
            yield break;
            
        }
        yield break;
    }

    public IEnumerator PickUp()
    {
            GameObject[] foundList = GameObject.FindGameObjectsWithTag("PickupButton");

            //Sets the closest found gameobject tagged pickupButton to nearest

            Vector3 pos = transform.position;            
            float radius = 2f;
            GameObject nearest = null;
            foreach (GameObject gameObject in foundList)
            {
            float d = (gameObject.transform.position - pos).sqrMagnitude;
                if (d < radius)
                {
                    nearest = gameObject;
                    radius = d;
                }
            }
            if(nearest != null)
            {
            //Move pickup item to player's child object "hands" (CHECK THE ORDER IN HIERARCHY)
            nearest.transform.position = this.gameObject.transform.GetChild(0).transform.position;
            //Sets the object to be child object of player
            nearest.transform.SetParent(this.gameObject.transform.GetChild(0).gameObject.transform);
            buttonClick.pickUpEnabled = false;
        }
        yield return null;
    }

    public void Bark()
    {
        //TODO: ADD AUDIO FOR BARKING
        Debug.Log("HAUHAUHUHUAUAUAUHAU INTENSIFIES!!!!");
        buttonClick.barkEnabled = false;
    }

    public IEnumerator Interact()
    {
        GameObject[] foundList = GameObject.FindGameObjectsWithTag("InteractButton");

        Vector3 pos = transform.position;
        float dist = 2f;
        GameObject nearest = null;
        foreach (GameObject gameObject in foundList)
        {
            //use sqr magnitude as its faster
            float d = (gameObject.transform.position - pos).sqrMagnitude;
            if (d < dist)
            {
                nearest = gameObject;
                dist = d;
            }
        }
        if(nearest != null)
        {
            Debug.Log("Interacting with: " + nearest.name);
        }
        yield return null;
    }

    public IEnumerator Dig()
    {
        if(buttonClick.digEnabled == true && isDiggingEnabled == true)
        {
            GameObject[] foundList = GameObject.FindGameObjectsWithTag("Dig");

            //assuming this code is running on the gameobject you want to find closest to
            Vector3 pos = transform.position;
            float dist = 1f;
            //Distance to other side in tunnel is about 47 units
            float distToOther = 50f;
            GameObject nearest = null;
            
            foreach (GameObject go in foundList)
            {
                if ((pos - go.transform.position).sqrMagnitude < dist && (go.transform.name == "Out" || go.transform.name == "In"))
                {
                    nearest = go;
                }
                //Finding the other side of the tunnel, so it doesn't take the current position
                if (((pos - go.transform.position).sqrMagnitude < distToOther && (pos - go.transform.position).sqrMagnitude > dist) 
                    && (go.transform.name == "Out" || go.transform.name == "In") && go.transform != nearest)
                {
                    further = go;
                }
            }
        }
        if(further != null)
        {
            Debug.Log((gameObject.GetComponent(typeof(Collider)) as Collider).name);
            (gameObject.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
            //Teleport(further.transform.position);
            isTeleporting = true;
            transform.position = further.transform.position;
            yield return new WaitForSeconds(5f);
            (gameObject.GetComponent(typeof(Collider)) as Collider).isTrigger = false;
        }
        buttonClick.digEnabled = false;
        yield break;
    }

    private void Teleport(Vector3 position)
    {
        isTeleporting = true;
        if(isTeleporting) 
        {
            StopCoroutine(ClickMove(position));
            targetPosition = position;
            isTeleporting = false;
        }
    }

    public void Drop()
    {

        foreach (Transform child in transform)
        {
            if (child.name == "hands")
            {
                GameObject childHand = child.gameObject;
                foreach (Transform child2 in childHand.transform)
                {
                    if (child2.tag == "PickupButton")
                    {
                        child2.transform.position = new Vector3(transform.position.x, 0.15f, transform.position.z);
                        child2.transform.SetParent(null, true);
                        buttonClick.dropEnabled = false;
                    }
                }
            }
        }
    }

    //When player is in interactable object's interact area (set as a trigger collider)
    //Player can interact with object by pressing F as default KBM controls
    private void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == "Interactable" && inputReader.GetInteractionInput() == 1)
        {
            Debug.Log("F is pressed and interacts with object");
        }

        if(collider.gameObject.tag == "Pickup" && inputReader.GetInteractionInput()== 1)
        {
            //TODO: drop item
            
            var foundList = GameObject.FindGameObjectsWithTag("Pickup");

            //assuming this code is running on the gameobject you want to find closest to
            var pos = transform.position; 
 
            float dist = float.PositiveInfinity;
            GameObject nearest = null;
            foreach(var gameObject in foundList)
            {
                //use sqr magnitude as its faster
                var d = (gameObject.transform.position - pos).sqrMagnitude;
                if (d < dist)
                {
                    nearest = gameObject;
                    dist = d;
                }
                //Disable pickup item collider (ONLY FIRST ONE so check the order) to prevent player getting stuck
                nearest.GetComponent<Collider>().enabled = false;

                //Move pickup item to player's child object "hands" (CHECK THE ORDER IN HIERARCHY)
                nearest.transform.position = this.gameObject.transform.GetChild(0).transform.position;

                //Sets the object to be child object of player
                nearest.transform.SetParent(this.gameObject.transform.GetChild(0).gameObject.transform);
            }
        }

        if(collider.gameObject.tag == "Dig")
        {
            //TODO: Dig animation
            //TODO: Make player wait until dig animation is over and teleport to other side of the tunnel
            isDiggingEnabled = true;
        }
    }
}