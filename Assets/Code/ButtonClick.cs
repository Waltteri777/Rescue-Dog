using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonClick : MonoBehaviour
{
    //public RingMenuButton[] ringMenuButton;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private RingMenuSpawn ringMenuSpawn;
    [SerializeField] private Mover mover;

    // Start is called before the first frame update
    private void Start()
    {
        RingMenuButton.ButtonClicked += ProsessButtonClick;
    }

    private void OnDestroy()
    {
        RingMenuButton.ButtonClicked -= ProsessButtonClick;
    }

    /*public void Click(RingMenuButton ringMenuButton)
    {
        if (ringMenuSpawn.menuIsActive == false)
        {
            ringMenuSpawn.SpawnRingMenu();
        }

        if (ringMenuSpawn.menuIsActive == true && ringMenuButton.isClicked == true)
        {
            Debug.Log("DESTRUCTION!!");
            ringMenuSpawn.menuIsActive = false;
            ringMenuButton.isClicked = false;
        }
     */

    public void ProsessButtonClick(RingMenuButton button)
    {
        switch (button.ID)
        {
            //Walk here
            case 0:
                Debug.Log("Walk here works!");
                mover.StartMoveCoroutine();
                ringMenuSpawn.DisableMenu();
                break;

            //Sniff
            case 1:
                Debug.Log("Sniff");
                ringMenuSpawn.DisableMenu();
                break;

            //Pick up
            case 2:
                Debug.Log("Item picked up!");
                ringMenuSpawn.DisableMenu();
                break;

            //Interact
            case 3:
                Debug.Log("Interacting with...");
                ringMenuSpawn.DisableMenu();
                break;

            //Drop
            case 4:
                Debug.Log("Dropping an item");
                break;

            //Dig
            case 5:
                Debug.Log("Digging hole");
                break;

            //Bark
            case 6:
                Debug.Log("HAUHAUHAUHAU!!");
                break;

            default:
                Debug.Log("Ei löydy :(");
                break;
        } 
    }
}
