using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PositionSelector : MonoBehaviour
{
    private static GameObject selector;
    private static GameObject MainCamera;
    private static GameObject selectorUI;
    private static bool isDone;



    void Awake()
    {
        //get the selector, i.e. a "pointer" object used to point a position
        selector = GameObject.Find("Position_Selector");
        //create a selector?
        MainCamera = GameObject.FindWithTag("MainCamera");
        selectorUI = GameObject.FindWithTag("PositionSelectorUI");
        reset();
    }

    public static IEnumerator selectPosition()
    {
        setSelector();
        while (isDone == false)
        {
            yield return null;
        }
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
        isDone = false;
    }

    public static void doneSelect()
    {
        isDone = true;
    }

    public static Vector3 getSelectedPosition()
    {
        return selector.transform.position;
    }

    

}
