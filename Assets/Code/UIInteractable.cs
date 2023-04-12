using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractable : MonoBehaviour
{
    private InputReader inputReader;
    [SerializeField]
    public class Action
    {
        private Color color;
        private string title;
        private Sprite image;
    }
    public Action[] options;

    private void Start()
    {
        inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        if (inputReader.GetClickInput() == 1)
        {
            Debug.Log("Toimii");
            RingMenuSpawn.ringMenuSpawn.SpawnRingMenu();
        }
    }
}
