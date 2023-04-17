using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RingMenuSpawn : MonoBehaviour
{
    public static RingMenuSpawn ringMenuSpawn;
    public GameObject[] ringMenuPrefab;
    private GameObject[] ringMenuClone;
    //public GameObject ringMenuUI;
    //Pie menu y-axis offset from click spot
    [SerializeField] private float offSet = 100f;
    private Vector2 mousePosition;
    public InputReader inputReader;
    public Camera cam;
    public bool menuIsActive = false;


    public void Awake()
    {
        ringMenuSpawn = this;
    }

    public void SpawnRingMenu()
    {

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
        gameObject.transform.position = Mouse.current.position.ReadValue();
        gameObject.SetActive(true);
    }

    public void DisableMenu()
    {
        gameObject.SetActive(false);
    }
}
