using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    [SerializeField] private Transform LookAtTransform;

    private void Awake() {
        playerInputActions=  new PlayerInputActions();
        playerInputActions.Player.Enable();

        // Lock and hide the cursor.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        ToggleCursor();
        // ChangePointerPosition();
    }

    private void ToggleCursor(){
        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            if(Cursor.visible == false){
                // Unlock and show the cursor.
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else{
                // Lock and hide the cursor.
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    // ! IT NEEDS AN UPGRADE...
    private void ChangePointerPosition(){
        Vector2 inputVector = playerInputActions.Player.Pointer.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        LookAtTransform.transform.position += new Vector3(inputVector.x, inputVector.y, 0f);
    }
}
