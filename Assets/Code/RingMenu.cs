using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMenu : MonoBehaviour
{
    private Vector2 mousePosition;
    private InputReader inputReader;
    public GameObject buttonPrefab;
    public RingMenuButton selected;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        //SpawnButtons();
    }

   /* public void SpawnButtons ()
    {
            Debug.Log("SpawnButtons toimii!");
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(transform, false);
            newButton.transform.localPosition = new Vector3(100f, 100f, 0f);

    }  */
}
