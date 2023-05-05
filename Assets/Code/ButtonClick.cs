using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private RingMenuSpawn ringMenuSpawn;
    [SerializeField] private Mover mover;
    [HideInInspector] public bool pickUpEnabled = false;
    [HideInInspector] public bool sniffEnabled = false;
    [HideInInspector] public bool interactEnabled = false;
    [HideInInspector] public bool dropEnabled = false;
    [HideInInspector] public bool barkEnabled = false;
    [HideInInspector] public bool digEnabled = false;
    [HideInInspector] public bool pullEnabled = false;

    private void Start()
    {
        RingMenuButton.ButtonClicked += ProsessButtonClick;
    }

    private void OnDestroy()
    {
        RingMenuButton.ButtonClicked -= ProsessButtonClick;
    }

    public void ProsessButtonClick(RingMenuButton button)
    {
        switch (button.ID)
        {
            //Walk here
            case 0:
                mover.StartMoveCoroutine();
                ringMenuSpawn.DisableMenu();
                break;

            //Sniff
            case 1:
                Debug.Log("Sniff");
                mover.StartMoveCoroutine();
                ringMenuSpawn.DisableMenu();
                break;

            //Pick up
            case 2:
                Debug.Log("Item picked up!");
                pickUpEnabled = true;
                mover.StartMoveCoroutine();
                ringMenuSpawn.DisableMenu();
                dropEnabled = true;
                break;

            //Interact
            case 3:
                Debug.Log("Interacting with...");
                interactEnabled = true;
                mover.StartMoveCoroutine();
                ringMenuSpawn.DisableMenu();
                break;

            //Drop
            case 4:
                Debug.Log("Dropping an item");               
                mover.Drop();
                ringMenuSpawn.DisableMenu();
                break;

            //Dig
            case 5:
                digEnabled = true;
                mover.StartMoveCoroutine();
                ringMenuSpawn.DisableMenu();
                break;

            //Bark
            case 6:
                Debug.Log("HAUHAUHAUHAU!!");
                barkEnabled = true;
                mover.Bark();
                break;

            /*//Drink
            case 7:
                mover.StartMoveCoroutine();
                ringMenuSpawn.DisableMenu();
                break;

            //Pull
            case 8:
                pullEnabled = true;
                mover.StartMoveCoroutine();
                ringMenuSpawn.DisableMenu();
                break;*/

            default:
                Debug.Log("Ei löydy :(");
                break;
        } 
    }
}
