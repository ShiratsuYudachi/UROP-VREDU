using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPositionSelector : MonoBehaviour
{
    public static Vector3 selectedPosition;

    private static GameObject indicator;
    private static Vector3 initialSelectorPosition;
    private static Vector3 initialIndicatorPosition;
    private static float scaleMultiplier;

    void Update()
    {
    }

    public static IEnumerator DragPositionFor(GameObject targetObject)
    {
        setDragSelector(targetObject);
        PositionSelector.isSelecting = true;
        initialSelectorPosition = PositionSelector.getSelectedPosition();
        initialIndicatorPosition = indicator.transform.position;
        while (PositionSelector.isSelecting)
        {
            Vector3 deltaSelector = PositionSelector.getSelectedPosition()-initialSelectorPosition;
            indicator.transform.position = initialIndicatorPosition + scaleMultiplier*deltaSelector;

            yield return null;
        }
        selectedPosition = indicator.transform.position;
        PositionSelector.reset();
        Destroy(indicator);
    }



    public static void setDragSelector(GameObject targetObject)
    {
        indicator = Instantiate(targetObject);
        indicator.transform.position = targetObject.transform.position;
        var renderer = indicator.GetComponent<Renderer>();
        renderer.material = ObjectSelector.highlightMaterial;

        GameObject MainCamera = PositionSelector.MainCamera;
        //ratio of dist(TGT,CAM)/dist(Dragger,CMA)
        scaleMultiplier = Vector3.Distance(targetObject.transform.position,MainCamera.transform.position)
        /Vector3.Distance(MainCamera.transform.position +  MainCamera.transform.rotation * Vector3.forward/2,MainCamera.transform.position);

        PositionSelector.setSelector();
    }

}
