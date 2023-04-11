using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class Mover : MonoBehaviour
{
    private InputReader inputReader;
    private Rigidbody rb;
    private Coroutine coroutine;
    private Vector3 targetPosition;
    [SerializeField] private float playerSpeed = 1.0f;
    [SerializeField] private float playerRotation = 1.0f;
    [SerializeField] private float timer = 1.0f;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 moveInput = inputReader.GetMoveInput();
        float clickInput = inputReader.GetClickInput();
        Move(moveInput);
        Click(clickInput);
    }

    private void Update()
    {
        
    }

    //Player movement settings
    private void Move(Vector3 direction)
    {
        rb.velocity = direction * playerSpeed;
    }

    private void Click(float direction)
    {
        //TODO: Change hit.collider to layerCheck(?) and ground check (now player can fly(:D))
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider && direction == 1)
        {
            if(coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(ClickMove(hit.point));
            targetPosition = hit.point;
        }
    }

    private IEnumerator ClickMove(Vector3 target)
    {
        while(Vector3.Distance(transform.position, target) > 0.1f)
        {
            Vector3 destination = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
            transform.position = destination;
            transform.rotation = Quaternion.LookRotation(destination);
            yield return null;
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
}
