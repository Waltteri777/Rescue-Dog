using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "RingMenuButton", menuName = "RingMenu/RingButtons", order = 0)]
public class RingMenuButton : ScriptableObject
{
    public TMP_Text buttonName;
    public Button button;
}
