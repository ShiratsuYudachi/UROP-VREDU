using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private GameObject MainCamera;
    private Vector3 _cameraPosition;
    //private Vector3 initial_relative_position;
    void Start()
    {
        MainCamera = GameObject.FindWithTag("MainCamera");
        _cameraPosition = MainCamera.transform.position;
        //initial_relative_position = _cameraPosition - this.gameObject.transform.position;
    }

    // follow position with MainCamera
    void Update()
    {
        this.gameObject.transform.position += MainCamera.transform.position - _cameraPosition;
        _cameraPosition = MainCamera.transform.position;
    }

    /*
    public void resetPosition()
    {
        this.transform.position = MainCamera.transform.position -  MainCamera.transform.rotation * Vector3.forward/2;
    }
    */
}