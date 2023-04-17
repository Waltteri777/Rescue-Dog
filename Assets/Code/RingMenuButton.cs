using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RingMenuButton : MonoBehaviour
{
    public static event Action<RingMenuButton> ButtonClicked;
    public TMP_Text buttonName;
    public Image button;
    //ID for button
    public int ID;
    public bool isClicked;
    

    public void ActivateButton()
    {
        if(ButtonClicked != null)
        {
            ButtonClicked.Invoke(this);
        }
    }
}
