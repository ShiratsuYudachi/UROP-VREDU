using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PositionSelector : MonoBehaviour
{
    public static GameObject selector;
    public static GameObject MainCamera;
    public static GameObject selectorUI;
    public static bool isSelecting;



    void Awake()
    {
        //get the selector, i.e. a "pointer" object used to point a position
        selector = GameObject.Find("PositionSelector");
        //create a selector?
        MainCamera = GameObject.FindWithTag("MainCamera");
        selectorUI = GameObject.FindWithTag("PositionSelectorUI");
        reset();
    }

    public static IEnumerator selectPosition()
    {
        setSelector();
        isSelecting = true;
        while (isSelecting)
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

    public static void reset()
    {
        selector.SetActive(false);
        selectorUI.SetActive(false);
        
    }

    public static void doneSelect()
    {
        isSelecting = false;
    }

    public static Vector3 getSelectedPosition()
    {
        return selector.transform.position;
    }

    

}
