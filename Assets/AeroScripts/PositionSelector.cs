using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PositionSelector : MonoBehaviour
{
    private static GameObject selector;
    private static GameObject MainCamera;
    private static GameObject selectorUI;
    public static Vector3 storedVector;


    void Awake()
    {
        //get the selector, i.e. a "pointer" object used to point a position
        selector = GameObject.Find("Position_Selector");
        MainCamera = GameObject.FindWithTag("MainCamera");
        selectorUI = GameObject.FindWithTag("PositionSelectorUI");
        reset();
    }

    public static void setSelector()
    {
        selector.SetActive(true);
        selectorUI.SetActive(true);
        //teleport selector to the front of MainCamera
        selector.transform.SetPositionAndRotation(
            MainCamera.transform.position +  MainCamera.transform.rotation * Vector3.forward/2,
            Quaternion.identity);
    }

    private static void reset()
    {
        selector.SetActive(false);
        selectorUI.SetActive(false);
    }

    public void doneSelect()
    {
        reset();
        storedVector = selector.transform.position;
    }
}