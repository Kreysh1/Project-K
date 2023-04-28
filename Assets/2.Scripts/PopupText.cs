using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    private Vector3 offset = new Vector3(0,2,0);
    public Transform mainCamera;

    private void Awake() {
        mainCamera = GameObject.Find("MainCamera").transform;
    }

    private void Start() {
        Destroy(gameObject, lifeTime);
        transform.localPosition += offset;
    }

    private void LateUpdate() {
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0,180,0);
    }
}
