using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using FMODUnity;

public class Mover : MonoBehaviour
{
    private InputReader inputReader;
    private Rigidbody rb;
    private Coroutine coroutine;
    private Vector3 targetPosition;
    [HideInInspector] public bool isDiggingEnabled = false;
    private bool isPauseEnabled = false;
    private bool isTeleporting = false;
    [SerializeField] private RingMenuSpawn ringMenuSpawn;
    [SerializeField] private ButtonClick buttonClick;
    private float clickInput;
    private NavMeshAgent agent;
    private GameObject further = null;
    private MeshRenderer meshRenderer;
    private RaycastHit hit;
    private Vector3 clickHitPos;
    private Vector3 offSet;
    public UIDocument document;
    private Camera mainCamera;
    public Transform camPos1;
    public Transform camPos2;
    private float timerRandom;
    public Sounds sounds;
     

    private void Awake()
    {
        mainCamera = Camera.main;
        offSet = new Vector3(transform.position.x, (transform.position.y + 5), transform.position.z);
        inputReader = GetComponent<InputReader>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
        timerRandom = Random.Range(7.0f, 30.0f);
        RuntimeManager.PlayOneShotAttached(sounds.wind1, gameObject);
    }

    private void FixedUpdate()
    {
        clickInput = inputReader.GetClickInput();
        /*if(isTeleporting)
        {
            meshRenderer.enabled = false;

            if (Vector3.Distance(further.transform.position, transform.position) < 0.2f)
            {
                //meshRenderer.enabled = false;
                isTeleporting = false;
            }

            //StopAllCoroutines();
            targetPosition = further.transform.position;
            agent.speed = 1000f;
            agent.acceleration = 1000f;
        }*/
        Debug.Log("click Input " +  clickInput);

        if (!isTeleporting && clickInput == 1)
        {
            Click();
        }

        if(inputReader.GetEscape() == 1)
        {
            
            document.gameObject.SetActive(true);
        }

        if (inputReader.GetRightClick() == 1)
        {
            ringMenuSpawn.DisableMenu();
        }

        if(inputReader.GetCamRotate() == 1)
        {
            mainCamera.transform.SetPositionAndRotation(camPos2.position, camPos2.rotation);
        }
        else
        {
            mainCamera.transform.SetPositionAndRotation(camPos1.position, camPos1.rotation);
        }

        timerRandom -= Time.deltaTime;
        if(timerRandom <= 0)
        {
            
            RuntimeManager.PlayOneShotAttached(sounds.birdSing, gameObject);
            timerRandom = timerRandom = Random.Range(7.0f, 30.0f);
        }
    }

    public void Click()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray: ray, hitInfo: out hit) && hit.collider && clickInput == 1 && ringMenuSpawn.menuIsActive == false)
        {
            clickHitPos = hit.transform.position;
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
        Debug.Log("ClickMove Works!");

        if (buttonClick.barkEnabled == true)
        {
            yield return StartCoroutine(Bark());
        }

        while (Vector3.Distance(transform.position, target) > 0.1f)
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
            if(buttonClick.pullEnabled == true)
            {
                yield return StartCoroutine(Pull());
            }
            yield break;
        }
        yield break;
    }

    public IEnumerator PickUp()
    {
        Debug.Log("PickUp");
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

    public IEnumerator Bark()
    {
        RuntimeManager.PlayOneShotAttached(sounds.dogBark, gameObject);
        Debug.Log("Bark");
        buttonClick.barkEnabled = false;
        yield break;
    }

    public IEnumerator Interact()
    {
        GameObject[] foundList = GameObject.FindGameObjectsWithTag("InteractButton");

        Vector3 pos = transform.position;
        GameObject nearest = null;
        foreach (GameObject gameObject in foundList)
        {
            if (gameObject.CompareTag("InteractButton"))
            {
                nearest = gameObject;
            }
        }
        if(nearest != null)
        {
            yield return new WaitForSeconds(2);
            Debug.Log("Interacting with: " + nearest.gameObject.name);
        }
        yield return null;
    }

    public IEnumerator Dig()
    {
        if (buttonClick.digEnabled == true)
        {
            GameObject[] foundList = GameObject.FindGameObjectsWithTag("Dig");

            //assuming this code is running on the gameobject to find closest to
            Vector3 pos = transform.position;
            float dist = 1f;
            //Distance to other side in tunnel is about 47 units
            float distToOther = 50f;
            GameObject nearest = null;

            foreach(GameObject gameObject in foundList)
            {
                if(gameObject.CompareTag("Dig"))
                {
                    nearest = gameObject;
                }
            }
            if(nearest != null)
            {
                //TODO: ADD ANIMATION
                //TODO: ADD SOUND
                //TODO:CHANGE SCENE

            }

            /*foreach (GameObject go in foundList)
            {
                if ((pos - go.transform.position).sqrMagnitude < dist && (go.transform.name == "Out" || go.transform.name == "In"))
                {
                    Debug.Log(go.transform.position + " nearest");
                    nearest = go;
                }
                //Finding the other side of the tunnel, so it doesn't take the current position
                if (((pos - go.transform.position).sqrMagnitude < distToOther && (pos - go.transform.position).sqrMagnitude > dist) 
                    && (go.transform.name == "Out" || go.transform.name == "In") && go.transform != nearest)
                {
                    Debug.Log(go.transform.position + " further 2");
                    further = go;
                }
            }
        }
        if(further != null)
        {
            RuntimeManager.PlayOneShotAttached(diggingSound1, gameObject);
            Debug.Log(further.transform.position + " furhter 1");
            (gameObject.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
            //Teleport(further.transform.position);
            //isTeleporting = true;
            //transform.position = further.transform.position;
            //yield return new WaitForSeconds(5);
            targetPosition = transform.position;
            //transform.position = further.transform.position;
            
            (gameObject.GetComponent(typeof(Collider)) as Collider).isTrigger = false;
            //Debug.Log(Vector3.Distance(further.transform.position, transform.position) + " dist further and player");
        } */
            buttonClick.digEnabled = false;
            yield break;
        }
    }

    public IEnumerator Pull()
    {
        //TODO: rope physics!
        //TODO: Add Animation
        //TODO: After animation destroy obstacle
        //Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        GameObject[] foundList = GameObject.FindGameObjectsWithTag("Obstacle");

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
        if (nearest != null)
        {
            //Move pickup item to player's child object "hands" (CHECK THE ORDER IN HIERARCHY)
            nearest.transform.position = this.gameObject.transform.GetChild(0).transform.position;
            //Sets the object to be child object of player
            nearest.transform.SetParent(this.gameObject.transform.GetChild(0).gameObject.transform);
            buttonClick.pickUpEnabled = false;
        }
        Debug.Log("Pulling animation playing");
        Debug.Log(nearest.name);
        yield return new WaitForSeconds(1);
        Collider collider1 = nearest.GetComponent<Collider>();
        collider1.gameObject.SetActive(false);
        foreach (Transform child in transform)
        {
            if (child.name == "hands")
            {
                GameObject childHand = child.gameObject;
                foreach (Transform child2 in childHand.transform)
                {
                    if (child2.tag == "Obstacle")
                    {
                        child2.transform.position = new Vector3(transform.position.x, 0.15f, transform.position.z);
                        child2.transform.SetParent(null, true);
                        buttonClick.dropEnabled = false;
                    }
                }
            }
            if (Vector3.Distance(transform.position, clickHitPos) < 1.5f)
            {
                if (hit.collider.gameObject.CompareTag("Obstacle"))
                {

                }
            }
            //Debug.Log("pulling!");
            buttonClick.pullEnabled = false;
            yield break;
        }
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
        if(collider.gameObject.CompareTag("Interactable") && inputReader.GetInteractionInput() == 1)
        {
            Debug.Log("F is pressed and interacts with object");
        }

        if(collider.gameObject.CompareTag("Pickup") && inputReader.GetInteractionInput() == 1)
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