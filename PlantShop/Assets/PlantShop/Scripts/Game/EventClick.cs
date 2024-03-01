using UnityEngine;
using UnityEngine.EventSystems;

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
        if (field.requireWateringTool && PlayerToolManager.Instance.assignedTool == Tool.WateringTool && field.promptImages[0].enabled == true)
        {
            Debug.Log("Watering Tool Used");
            field.ClosePromptImage(0);
        }
        if (field.requirePruningTool && PlayerToolManager.Instance.assignedTool == Tool.PruningTool && field.promptImages[1].enabled == true)
        {
            Debug.Log("Pruning Tool Used");
            field.ClosePromptImage(1);
        }
        if (field.requirePestControlTool && PlayerToolManager.Instance.assignedTool == Tool.PestControlTool && field.promptImages[2].enabled == true)
        {
            Debug.Log("Pest Control Tool Used");
            field.ClosePromptImage(2);
        }
        else
        {
            return;
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
