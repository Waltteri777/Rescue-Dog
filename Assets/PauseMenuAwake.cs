using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuAwake : MonoBehaviour
{
    public UIDocument document;
    public InputReader inputReader;
    private float escape;

    void Update()
    {
        escape = inputReader.GetEscape();
        Debug.Log(escape);
        if (escape == 1)
        {
            PauseMenu();
        }
        Debug.Log(inputReader.GetEscape());
    }

    public void PauseMenu()
    {
        document.enabled = true;
    }
}
