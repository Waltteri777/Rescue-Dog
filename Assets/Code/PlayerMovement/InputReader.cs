using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{

    //Different control methods to use to control the game (KeyBoard&Mouse, touchscreen, controller etc)
    public enum ControlMethod
    {
        KBM,
        touchScreen
    }
    //stores move input of movement controls
    private Vector3 moveInput;
    private float clickInput;
    private float worldClickPosition;
    private float interactionInput;
    private PlayerInput playerInput;
    //primary control method set to KBM, can be adjusted in code to use other methods
    private ControlMethod currentControlMethod = ControlMethod.KBM;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        currentControlMethod = ControlMethod.KBM;
        moveInput = context.ReadValue<Vector3>();
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        currentControlMethod = ControlMethod.KBM;
        interactionInput = context.ReadValue<float>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        currentControlMethod = ControlMethod.KBM;
        clickInput = context.ReadValue<float>();
    }

    public Vector3 GetMoveInput()
    {
        return this.moveInput;
    }

    public float GetClickInput() 
    { 
        return clickInput;
    }

    public float GetInteractionInput()
    {
        return interactionInput;
    }
}