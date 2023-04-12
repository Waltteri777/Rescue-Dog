using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMenuSpawn : MonoBehaviour
{
    public static RingMenuSpawn ringMenuSpawn;
    public RingMenu ringMenuPrefab;
    //public GameObject ringMenuUI;
    private float cameraDist = 5.0f;
    private Vector2 mousePosition;
    private InputReader inputReader;


    private void Awake()
    {
        ringMenuSpawn = this;
        inputReader = GetComponent<InputReader>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRingMenu()
    {
        RingMenu newRingMenu = Instantiate(ringMenuPrefab) as RingMenu;
        newRingMenu.transform.SetParent(transform, false);
        //newRingMenu.transform.position = Camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cameraDist), Camera.MonoOrStereoscopicEye.Mono);
    }
}
