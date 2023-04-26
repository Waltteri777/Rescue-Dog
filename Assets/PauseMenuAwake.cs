using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuAwake : MonoBehaviour
{
    public UIDocument document;
    public InputReader inputReader;

    void Update()
    {
        Debug.Log(inputReader.GetEscape());
        if(inputReader.GetEscape() == 1)
        {
            Debug.Log("toimii");
            document.enabled = true;
        }
    }
}
