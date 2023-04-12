using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractable : MonoBehaviour
{
    public InputReader inputReader;
    [System.Serializable]
    public class Action
    {
        public string title;
        public Sprite image;
    }
    public Action[] options;


    private void Update()
    {
        if (inputReader.GetClickInput() == 1)
        {
            RingMenuSpawn.ringMenuSpawn.SpawnRingMenu();
            Debug.Log(inputReader.GetClickInput());
        }
    }
}
