using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{

    [SerializeField] private Transform targetTransform;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float fieldofViewMax = 60f;
    [SerializeField] private float fieldofViewMin = 10f;

    float followOffsetMax = 20f;
    float followOffsetMin = 0f;
    float verticalRotation = 5f;
    float rotateSpeed = 50f;
    float targetFieldofView = 60f;
    bool dragRotationFlag = false;
    Vector2 lastMousePosition;
    CinemachineTransposer cinemachineTransposer;

    private void Start() {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Update() {
        FollowTargetPosition();
        HandleCameraRotation();
        HandleCameraZoom();
    }

    private void HandleCameraRotation(){
        float horizontalRotation = 0f;
        verticalRotation = 0f;

        if(transform.rotation.y <= -360) {
            transform.eulerAngles += new Vector3(0, transform.rotation.y + 360f, 0);
        }
        if(transform.rotation.y >= 360) {
            transform.eulerAngles += new Vector3(0, transform.rotation.y - 360f, 0);
        }

        if(Input.GetMouseButtonDown(1)){
            dragRotationFlag = true;
            lastMousePosition = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(1)){
            dragRotationFlag = false;
        }

        if(dragRotationFlag){
            Vector2 mouseRotationDelta = (Vector2)Input.mousePosition - lastMousePosition;

            // GET HORIZONTAL MOUSE INPUT
            horizontalRotation = mouseRotationDelta.x * rotateSpeed;

            // GET VERTICAL MOUSE INPUT
            if(Mathf.Abs(mouseRotationDelta.y) > 1){
                verticalRotation = (-mouseRotationDelta.y * rotateSpeed)/10; 
            }

            lastMousePosition = Input.mousePosition;
        }

        // 
        transform.eulerAngles += new Vector3(0, horizontalRotation * Time.deltaTime, 0);

        // verticalRotation = Mathf.Clamp(verticalRotation,followOffsetMin, followOffsetMax);

        // cinemachineTransposer.m_FollowOffset.y += verticalRotation;
        cinemachineTransposer.m_FollowOffset.y += verticalRotation * Time.deltaTime;
        cinemachineTransposer.m_FollowOffset.y = Mathf.Clamp(cinemachineTransposer.m_FollowOffset.y, followOffsetMin, followOffsetMax);
    }

    private void FollowTargetPosition(){
        transform.position = targetTransform.position;
    }

    // ? I'm manipulating the POV value to simulate the Zoom, should i implement this changing the cinemachineTransposer.m_FollowOffset.z value instead?
    private void HandleCameraZoom(){
        if(Input.mouseScrollDelta.y < 0){
            targetFieldofView += 2;
        }
        if(Input.mouseScrollDelta.y > 0){
            targetFieldofView -= 2;
        }

        targetFieldofView = Mathf.Clamp(targetFieldofView, fieldofViewMin, fieldofViewMax);
        
        cinemachineVirtualCamera.m_Lens.FieldOfView = targetFieldofView;
    }
}
