using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFacingCamera : MonoBehaviour
{
    public Camera cameraToLookAt;

    private void Update()
    {
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
