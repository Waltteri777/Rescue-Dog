using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Collider playerCol;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider == playerCol)
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
