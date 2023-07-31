using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSelector : MonoBehaviour
{
    public GameObject arrow;
    public GameObject dragger;

    public static GameObject Arrow;
    public static GameObject Dragger;
    public static GameObject Selector;
    public static Vector3 SelectedAxis;

    void Start()
    {
        Selector = this.gameObject;
        Arrow = arrow;
        Dragger = dragger;
        this.gameObject.SetActive(false);
    }

    public static IEnumerator SelectAxis()
    {
        setSelector();
        PositionSelector.isSelecting = true;
        while (PositionSelector.isSelecting)
        {
            SelectedAxis = Dragger.transform.position - Arrow.transform.position;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, SelectedAxis);
            Arrow.transform.rotation = rotation;
            yield return null;
        }
        
        PositionSelector.hideUI();
        Selector.SetActive(false);
    }

    public static void setSelector()
    {
        GameObject MainCamera = PositionSelector.MainCamera;
        Selector.transform.position = MainCamera.transform.position +  MainCamera.transform.rotation * Vector3.forward/2;
        Selector.SetActive(true);
        PositionSelector.showUI();
    }



}
