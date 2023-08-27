using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

using UnityEngine.XR.Interaction.Toolkit;


public class ObjectSelector : MonoBehaviour
{
    public GameObject ControllerObject;
    public Material HighlightMaterial;
    

    public static GameObject selectedObject;

    private static string selectableTag = "Selectable";
    public static Material highlightMaterial;
    private static Material defaultMaterial;
    private static Transform _selection;
    private static XRRayInteractor rayInteractor;
    private static ActionBasedController controller; 
    
    private static bool isSelecting = false;

    public void Start()
    {
        controller = ControllerObject.GetComponent<ActionBasedController>();
        rayInteractor = ControllerObject.GetComponent<XRRayInteractor>();
        highlightMaterial = HighlightMaterial;
        //StartCoroutine(SelectObject());
        
    }

    public static IEnumerator SelectObject()
    {
        isSelecting = true;
        while (isSelecting)
        {
            if (_selection != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                selectionRenderer.material = defaultMaterial;
                _selection = null;
            }
            
            RaycastHit hit;
            if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag(selectableTag))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null)
                    {
                        if (selectionRenderer.material!=highlightMaterial)
                        {
                            defaultMaterial = selectionRenderer.material;
                        }
                        selectionRenderer.material = highlightMaterial;
                    }

                    _selection = selection;

                    if (controller.activateAction.action.phase == InputActionPhase.Performed)//check trigger isPressed
                    {
                        isSelecting = false;
                        selectedObject = hit.collider.gameObject;
                    }
                }
            }
            yield return null;
        }
        var _selectionRenderer = _selection.GetComponent<Renderer>();
        _selectionRenderer.material = defaultMaterial;
        _selection = null;
    }




}
