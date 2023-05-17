using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionCollision : MonoBehaviour
{
    public Collider playerCol;
    public bool isSceneChange = false;
    public Sounds sounds;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider == playerCol)
        {
            RuntimeManager.PlayOneShotAttached(sounds.diggingSound1, gameObject);
            isSceneChange = true;
        }
    }
}
