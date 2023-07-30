using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Orbit : Action {
    public GameObject centerObject;
    public bool isOrbitingObject = true;
    public Vector3 centerPosition;
    public float angularSpeedInDegree = 60;
    public Vector3 centerAxis = Vector3.up; // unit vector of angular velocity
    //public bool isInfinite = true;
    private bool isActive = false;


    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (isActive){
            centerPosition = isOrbitingObject ? centerObject.transform.position : centerPosition;
            this.transform.RotateAround(centerPosition, centerAxis, angularSpeedInDegree * Time.deltaTime);
        }
    }

    public override IEnumerator Initialize(){
        
        //Add UI to select whether to orbit object
        if (isOrbitingObject){
            yield return ObjectSelector.SelectObject();
            centerObject = ObjectSelector.selectedObject;
            this.transform.SetParent(centerObject.transform); //fix Aero bug
        }else
        {
            yield return PositionSelector.selectPosition();
            centerPosition = PositionSelector.getSelectedPosition();
        }
        isActive = true;
    }


}

