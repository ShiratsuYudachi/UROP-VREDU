using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBounceComponent()
    {
        gameObject.AddComponent<Bounce>();
        Bounce bounce = gameObject.GetComponent<Bounce>();
        bounce.relativeTargetPosition = PositionSelector.storedVector - gameObject.transform.position;
        //TODO: create a panel to with slider to select duration and checkbox for infinite
        bounce.Start();
    }

    public void AddOrbitComponent()
    {

    }

    public void RemoveBounceComponent()
    {
        Destroy(gameObject.GetComponent<Bounce>());
    }
}
