using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMenu : MonoBehaviour
{
    private Vector2 mousePosition;
    private InputReader inputReader;
    public RingMenuButton buttonPrefab;
    public RingMenuButton selected;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
    }
    // Start is called before the first frame update
    void Start()
    {
        RingMenuButton newButton = Instantiate(buttonPrefab) as RingMenuButton;
        newButton.transform.SetParent(transform, false);
        newButton.transform.localPosition = new Vector3(0f, 100f, 0f);
    }

    private void SpawnButtons (UIInteractable button)
    {
        for (int i = 0; i < button.options.Length;  i++)
        {
            RingMenuButton newButton = Instantiate(buttonPrefab) as RingMenuButton;
            newButton.transform.SetParent(transform, false);
            newButton.transform.localPosition = new Vector3(0f, 100f, 0f);
        }
    }
}
