using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bounce : MonoBehaviour {
    public Vector3 relativeTargetPosition = Vector3.zero;
    public float duration = 2;
    public bool isInfinite = true;

    private Vector3 deltaVec; //movement expected per second
    private Vector3 initial_position;
    private Vector3 targetPosition;
    private bool isGoing = true;
    private bool isActive = true;


    // Use this for initialization
    public void Start () {
        isActive = relativeTargetPosition!=Vector3.zero;
        initial_position = this.transform.position;
        targetPosition = this.transform.position + relativeTargetPosition;
        deltaVec = targetPosition-this.transform.position;
        deltaVec = 2*deltaVec/duration;
    }

    // Update is called once per frame
    void Update () {
        if (isActive){
            Vector3 newPosition =  this.transform.position += deltaVec * Time.deltaTime;
            if (isValidNewPosition(newPosition)){
                this.transform.position = newPosition;
            }else{
                if (isGoing || isInfinite){
                    this.transform.position = isGoing ? targetPosition : initial_position;
                    deltaVec = -1*deltaVec;
                    isGoing = !isGoing;
                }else{
                    this.transform.position = initial_position;
                    deltaVec = Vector3.zero;
                    isActive = false;
                }

            };
        }
    }

    private bool isValidNewPosition(Vector3 newPosition) {
        //check if new position beyond target
        Vector3 vec1 = targetPosition - initial_position;
        vec1 = isGoing ? vec1 : -vec1;
        Vector3 vec2 = isGoing ? targetPosition - newPosition : initial_position - newPosition;
        vec1 = vec1.normalized;//seems there are some bugs for Vector3.Normalized
        vec2 = vec2.normalized;
        //Debug.Log("vec1: "+vec1+", vec2: "+vec2);
        return vec2==Vector3.zero || vec1==vec2;
    }



}
