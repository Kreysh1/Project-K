using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBarr(){
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
    }

    [MenuItem("GameObject/UI/Radial Progress Bar")]
    public static void AddRadialProgressBarr(){
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Radial Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
    }
#endif

    public int minValue;
    public int maxValue;
    public int currentValue;
    public Image mask;
    public Image fill;
    public Color color;

    private void Update() {
        GetCurrentFill();
    }

    private void GetCurrentFill(){
        float currentOffset = currentValue - minValue;
        float maxOffset = maxValue - minValue;
        float fillAmount = currentOffset/maxOffset;
        mask.fillAmount = fillAmount;

        fill.color = color;

        // float fillAmount = (float)currentValue/(float)maxValue;
        // mask.fillAmount = fillAmount;
    }
}
