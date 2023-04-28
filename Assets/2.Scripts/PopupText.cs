using UnityEngine;

public class PopupText : MonoBehaviour
{
    [Range(0.1f, 5f)]
    [SerializeField] private float lifeTime;
    [Range(-3f, 3f)]
    [SerializeField] private float offset;

    [HideInInspector] public Transform mainCamera;

    private Vector3 offsetVector;

    private void Awake() {
        mainCamera = GameObject.Find("MainCamera").transform;
        offsetVector = new Vector3(0,offset,0);
    }

    private void Start() {
        // Initialize text life time.
        Destroy(gameObject, lifeTime);

        // Add an offset to the position.
        transform.localPosition += offsetVector;
    }

    private void LateUpdate() {
        // Face the text to the camera position.
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0,180,0);
    }
}
