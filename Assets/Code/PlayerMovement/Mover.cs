using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class Mover : MonoBehaviour
{
    private InputReader inputReader;
    private Rigidbody rb;
    private Coroutine coroutine;
    private Vector3 targetPosition;
    [SerializeField] private float timer = 1.0f;
    private float distToGround = 0.1f;
    [SerializeField] private RingMenuSpawn ringMenuSpawn;
    [SerializeField] private ButtonClick buttonClick;
    private LayerMask layerMaskUI = 5;
    private float clickInput;
    private NavMeshAgent agent;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        clickInput = inputReader.GetClickInput();
        Click();
    }

    //Player movement settings
    //direction is 0 or 1 so it doesnt matter if its multiplied or not


    public void Click()
    {
    //TODO: Change hit.collider to layerCheck(?) and ground check (now player can fly(:D))
    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
    if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider && clickInput == 1 && ringMenuSpawn.menuIsActive == false)
    {
        ringMenuSpawn.SpawnRingMenu(hit.collider.gameObject.tag);
        //coroutine = StartCoroutine(ClickMove(hit.point));
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
            //OLD MOVE SYSTEM WITHOUT NAVMESH
            /*Vector3 destination = Vector3.MoveTowards(transform.position, new Vector3(target.x, 1f, target.z), playerSpeed * Time.deltaTime);
            transform.position = destination;
            Quaternion rotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, playerRotationSpeed * Time.deltaTime); */
            //***
            //TODO: Make this return other task if needed after player has moved next to interaction
            //Replace null return with something like --> yield return StartCoroutine(**THING TO DO**);
            //***
            //Figure out how to recognize if pickup should start

            //NEW MOVE SYSTEM WITH NAVMESH
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

            yield return null;
        }
    }

    //TODO: STOP THIS FROM PLAYING INFINITELLY OR HOW TO SPELL IT IDK ;D
    public IEnumerator PickUp()
    {
            //TODO: drop item

            GameObject[] arr = GameObject.FindGameObjectsWithTag("PickupButton");

            //assuming this code is running on the gameobject you want to find closest to
            Vector3 pos = transform.position;            
            float dist = 2f;
            GameObject nearest = null;
            foreach (GameObject go in arr)
            {
            //use sqr magnitude as its faster
            float d = (go.transform.position - pos).sqrMagnitude;
                if (d < dist)
                {
                    nearest = go;
                    dist = d;
                }
            }
            if(nearest != null)
            {
            //Disable pickup item collider (ONLY FIRST ONE so check the order) to prevent player getting stuck
            //nearest.GetComponent<Collider>().enabled = false;
            //Move pickup item to player's child object "hands" (CHECK THE ORDER IN HIERARCHY)
            nearest.transform.position = this.gameObject.transform.GetChild(0).transform.position;
            //Sets the object to be child object of player
            nearest.transform.SetParent(this.gameObject.transform.GetChild(0).gameObject.transform);
            buttonClick.pickUpEnabled = false;
        }
        //buttonClick.pickUpEnabled = false;
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
        GameObject[] arr = GameObject.FindGameObjectsWithTag("InteractButton");

        //assuming this code is running on the gameobject you want to find closest to
        Vector3 pos = transform.position;
        float dist = 2f;
        GameObject nearest = null;
        foreach (GameObject go in arr)
        {
            //use sqr magnitude as its faster
            float d = (go.transform.position - pos).sqrMagnitude;
            if (d < dist)
            {
                nearest = go;
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
        GameObject[] arr = GameObject.FindGameObjectsWithTag("DigButton");

        //assuming this code is running on the gameobject you want to find closest to
        Vector3 pos = transform.position;
        float dist = 2f;
        GameObject nearest = null;
        foreach (GameObject go in arr)
        {
            //use sqr magnitude as its faster
            float d = (go.transform.position - pos).sqrMagnitude;
            if (d < dist)
            {
                nearest = go;
                dist = d;
            }
        }
        if (nearest != null)
        {
            Debug.Log("Diging near: " + nearest.name);
        }
        yield return null;
    }

    public void Drop()
    {
        bool execDrop = true;
        if(execDrop == true) 
        {
            foreach (Transform child in transform)
            {
                if (child.name == "hands")
                {
                    GameObject childHand = child.gameObject;
                    foreach (Transform child2 in childHand.transform)
                    {
                        //Debug.Log(child2.tag);
                        if (child2.tag == "PickupButton")
                        {
                            child2.transform.position = new Vector3(transform.position.x, 0.15f, transform.position.z);
                            child2.transform.SetParent(null, true);
                            execDrop = false;
                            buttonClick.dropEnabled = false;
                        }
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
            
            var arr = GameObject.FindGameObjectsWithTag("Pickup");

            //assuming this code is running on the gameobject you want to find closest to
            var pos = transform.position; 
 
            float dist = float.PositiveInfinity;
            GameObject nearest = null;
            foreach(var go in arr)
            {
                //use sqr magnitude as its faster
                var d = (go.transform.position - pos).sqrMagnitude;
                if (d < dist)
                {
                    nearest = go;
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

        if(collider.gameObject.tag == "Digin" && inputReader.GetInteractionInput() == 1)
        {
            //TODO: Dig animation
            //TODO: Make player wait until dig animation is over and teleport to other side of the tunnel
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }

            if (timer <= 0)
            {
                var arr = GameObject.FindGameObjectsWithTag("Digout");

                //assuming this code is running on the gameobject you want to find closest to
                var pos = transform.position;

                float dist = float.PositiveInfinity;
                GameObject nearest = null;
                foreach (var go in arr)
                {
                    //use sqr magnitude as its faster
                    var d = (go.transform.position - pos).sqrMagnitude;
                    if (d < dist)
                    {
                        nearest = go;
                        dist = d;
                    }
                }
                this.transform.position = nearest.transform.position;
                timer = 5.0f;
            }
        }
    }

    private bool GroundCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }
}
