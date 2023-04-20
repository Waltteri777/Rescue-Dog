using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class RingMenuSpawn : MonoBehaviour
{
    public static RingMenuSpawn ringMenuSpawn;
    private ButtonClick buttonClick;
    //Pie menu y-axis offset from click spot
    [SerializeField] private float offSet = 100f;
    public InputReader inputReader;
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

    public void SpawnRingMenu(String tag)
    {
        gameObject.SetActive(true);

        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).tag is "BarkButton" or "MoveButton"
                or "SniffButton" || (buttonClick.dropEnabled == true && transform.GetChild(i).tag is "DropButton"))
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        /*ringMenuClone = Instantiate(ringMenuPrefab[0]);
        ringMenuClone.transform.SetParent(transform, false);
        mousePosition = inputReader.GetMousePos();
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePosition);
        ringMenuClone.transform.position = new Vector3(mousePosition.x, (mousePosition.y + offSet), 0f);
        Debug.Log(ringMenuClone.transform.position);
        menuIsActive = true;
        //menuIsActive = false; */
        /*for(int i = 0; i < ringMenuPrefab.Length; i++)
        {
            ringMenuPrefab[i].SetActive(true);
        } */
        
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
