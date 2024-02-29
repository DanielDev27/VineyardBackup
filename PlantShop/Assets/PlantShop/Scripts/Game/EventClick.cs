using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

//Add this to the game objects you want to click on
public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] FieldManager field;
    void Start()
    {
        field = this.GetComponent<FieldManager>();
    }
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Interact with " + this.gameObject.name);
        if (field.promptCanvas.GetComponentInChildren<TMP_Text>().enabled == true)
        {
            if (field.requiredTool == Tool.WateringTool && PlayerToolManager.Instance.assignedTool == Tool.WateringTool)
            {
                Debug.Log("Watering Tool Used");
                field.ClosePromptImage();
            }
            if (field.requiredTool == Tool.PruningTool && PlayerToolManager.Instance.assignedTool == Tool.PruningTool)
            {
                Debug.Log("Pruning Tool Used");
                field.ClosePromptImage();
            }
            if (field.requiredTool == Tool.PestControlTool && PlayerToolManager.Instance.assignedTool == Tool.PestControlTool)
            {
                Debug.Log("Pest Control Tool Used");
                field.ClosePromptImage();
            }
            else
            {
                return;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Enter " + this.gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //
    }
}
