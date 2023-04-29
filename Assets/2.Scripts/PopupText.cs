using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    [Header("Behaviour")]
    [Range(0.1f, 5f)]
    [SerializeField] private float lifeTime;
    [Range(-3f, 3f)]
    [SerializeField] private float offset;

    [Header("Customization")]
    public Color normalDamageColor;
    public Color criticalDamageColor;

    [HideInInspector] public Transform mainCamera;

    private TextMeshProUGUI textMesh;
    private Vector3 offsetVector;

    private void Awake() {
        mainCamera = GameObject.Find("MainCamera").transform;
        textMesh = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        offsetVector = new Vector3(0,offset,0);
        // Set text color.
        textMesh.color = normalDamageColor;
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

    public void Setup(int _damage, bool _isCritical){
        if(_isCritical){
            textMesh.color = criticalDamageColor;
        }

        textMesh.SetText(_damage.ToString());
    }
}
