using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : MonoBehaviour
{
    // public event EventHandler OnJumpAction;
    private PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions=  new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    
    public Vector2 GetMovementVectorNormalized(){

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public bool OnJump(){
        return playerInputActions.Player.Jump.triggered;
    }

    public bool OnRun(){
        return playerInputActions.Player.Run.IsInProgress();
    } 
}
