using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMenuSpawn : MonoBehaviour
{
    public static RingMenuSpawn ringMenuSpawn;
    public RingMenu ringMenuPrefab;
    //public GameObject ringMenuUI;
    private float cameraDist = 5.0f;
    //Pie menu y-axis offset from click spot
    [SerializeField] private float offSet = 100f;
    private Vector2 mousePosition;
    public InputReader inputReader;
    public Camera cam;


    private void Awake()
    {
        ringMenuSpawn = this;
    }

    public void SpawnRingMenu()
    {
        RingMenu newRingMenu = Instantiate(ringMenuPrefab) as RingMenu;
        newRingMenu.transform.SetParent(transform, false);
        mousePosition = inputReader.GetMousePos();
        //Vector3 worldPos = cam.ScreenToWorldPoint(mousePosition);
        newRingMenu.transform.position = new Vector3(mousePosition.x, (mousePosition.y + offSet), 0f);
        Debug.Log(newRingMenu.transform.position);
    }
}
