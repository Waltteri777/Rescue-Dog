using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;
    private Rigidbody rb;
    [SerializeField] private Transform playerPosition;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerControls.KBM.Enable();
    }

    private void OnDisable()
    {
        playerControls.KBM.Disable();
    }
}
