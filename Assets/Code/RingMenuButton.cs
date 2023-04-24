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
    
    //Buttons cannot return float value so this should fix it
    public void ActivateButton()
    {
        ButtonClicked?.Invoke(this);
    }
}
