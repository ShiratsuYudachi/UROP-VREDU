using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Orbit : MonoBehaviour {
    public GameObject centerObject;
    public float angularSpeedInDegree = 60;
    //public float inclinationAngle = 0;
    //public bool isInfinite = true;
    private bool isActive = true;


    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (isActive){
            this.transform.RotateAround(centerObject.transform.position, Vector3.up, angularSpeedInDegree * Time.deltaTime);
        }
    }

}

