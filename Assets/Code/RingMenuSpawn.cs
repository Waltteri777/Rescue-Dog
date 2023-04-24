using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class RingMenuSpawn : MonoBehaviour
{
    public static RingMenuSpawn ringMenuSpawn;
    private ButtonClick buttonClick;
    public InputReader inputReader;
    public Mover mover;
    public Camera cam;
    public bool menuIsActive = false;


    public void Awake()
    {
        ringMenuSpawn = this;
        buttonClick = GetComponent<ButtonClick>();
    }

    GameObject FindChildWithTag(GameObject parent, String tag)
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                child.SetActive(true);
                break;
            }
        }
        return child;
    }

    //Activates the default buttons (BARK, MOVE, SNIFF) and checks if DROP and DIG should be activated
    public void SpawnRingMenu(String tag)
    {
        gameObject.SetActive(true);

        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).tag is "BarkButton" or "MoveButton"
                or "SniffButton" || (buttonClick.dropEnabled == true && transform.GetChild(i).tag is "DropButton")
                    || (buttonClick.digEnabled == true && transform.GetChild(i).tag is "Dig"))
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if(tag !=null) 
        {
            menuIsActive = true;
            gameObject.transform.position = Mouse.current.position.ReadValue();
            GameObject GO = FindChildWithTag(gameObject, tag);
            if(GO != null)
            {
                GO.SetActive(true);
            }
        }
    }

    public void DisableMenu()
    {
        gameObject.SetActive(false);
        menuIsActive = false;
    }
}
