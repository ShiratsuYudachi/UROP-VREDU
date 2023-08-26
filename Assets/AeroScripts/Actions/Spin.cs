using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : Action
{
    public Vector3 spinAxis;
    public float spinDuration = 1;
    public bool isInfinite;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            float spinSpeed = 360 / spinDuration;
            Vector3 initialAngle = this.transform.rotation.eulerAngles;
            Quaternion newRotation = Quaternion.Euler(initialAngle + new Vector3(0, spinSpeed * Time.deltaTime, 0));
            this.transform.rotation = newRotation;
            //or
            //this.tranform.Rotate(0, 30, 0);
        }


    }

    public override IEnumerator Initialize(GameObject attachedObject)
    {
        isActive = true;
        yield return null;
    }

    
    public override void InitializeWith(Dictionary<string,object> param)
    {
        isActive = true;
    }

    
}
