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
        UI,
        touchScreen
    }
    //stores move input of movement controls
    private Vector3 moveInput;
    private float clickInput;
    private Vector2 mousePos;
    private float worldClickPosition;
    private float interactionInput;
    private float escape;
    private float camRotate;
    private float rightClick;
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

    public void MousePosition(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentControlMethod = ControlMethod.KBM;
            mousePos = context.ReadValue<Vector2>();
        }
    }

    public void Escape(InputAction.CallbackContext context)
    {
        currentControlMethod = ControlMethod.KBM;
        escape = context.ReadValue<float>();
    }

    public void CameraRotate(InputAction.CallbackContext context)
    {
        currentControlMethod = ControlMethod.KBM;
        camRotate = context.ReadValue<float>();
    }
    public void CancelMenu(InputAction.CallbackContext context)
    {
        currentControlMethod = ControlMethod.KBM;
        rightClick = context.ReadValue<float>();
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

    public Vector2 GetMousePos() 
    {
        return mousePos;
    }

    public float GetEscape()
    {
        return escape;
    }

    public float GetCamRotate()
    {
        return camRotate;
    }

    public float GetRightClick()
    {
        return rightClick;
    }
}
