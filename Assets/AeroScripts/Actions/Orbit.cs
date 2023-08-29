using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Orbit : Action {
    //name = "Orbit";
    public GameObject centerObject;
    public bool isOrbitingObject = true;
    public Vector3 centerPosition;
    public float angularSpeedInDegree = 60;
    public Vector3 centerAxis = Vector3.up; // unit vector of angular velocity
    //public bool isInfinite = true;


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

    public override IEnumerator Initialize(GameObject attachedObject){
        
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
        yield return AxisSelector.SelectAxis();
        centerAxis = AxisSelector.SelectedAxis;
        isActive = true;
    }

    public override IEnumerator InitializeUpdater(ActionUpdater updater)
    {
        if (isOrbitingObject){
            yield return ObjectSelector.SelectObject();
            updater.param["centerObject"] = ObjectSelector.selectedObject;
        }else
        {
            yield return PositionSelector.selectPosition();
            updater.param["centerPosition"] = PositionSelector.getSelectedPosition();
        }
        yield return AxisSelector.SelectAxis();
        updater.param["centerAxis"] = AxisSelector.SelectedAxis;

        updater.doneCreate = true;
    }

    public override void InitializeWith(Dictionary<string,object> param)
    {
        if (isOrbitingObject){
            centerObject = (GameObject)param["centerObject"];
            this.transform.SetParent(centerObject.transform); //fix Aero bug
        }else
        {
            centerPosition = (Vector3)param["centerPosition"];
        }
        centerAxis = (Vector3)param["centerAxis"];
        isActive = true;
    }



}

