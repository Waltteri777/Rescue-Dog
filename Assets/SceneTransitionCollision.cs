using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionCollision : MonoBehaviour
{
    public Collider playerCol;
    public bool isSceneChange = false;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider == playerCol)
        {
            isSceneChange = true;
        }
    }
}
