using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameInput : MonoBehaviour
{
    // public event EventHandler OnJumpAction;
    private PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions=  new PlayerInputActions();
        playerInputActions.Player.Enable();

        // playerInputActions.Player.Jump.performed += Jump_performed;
    }

    // public void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
    //     Debug.Log(obj);
    //     OnJumpAction?.Invoke(this, EventArgs.Empty);
    // }
    
    public Vector2 GetMovementVectorNormalized(){

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
