using UnityEngine;
using UnityEngine.EventSystems;

//Add this to the game objects you want to click on
//Camera also requires a raycaster
public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] FieldManager field;
    void Start()
    {
        field = this.GetComponent<FieldManager>();//Sets the field manager that the script is attached to
    }
    public void OnPointerDown(PointerEventData eventData)//Reactions to clicking
    {
        //Debug.Log("Interact with " + this.gameObject.name);
        if (field.requireWateringTool && PlayerToolManager.Instance.assignedTool == Tool.WateringTool && field.promptImages[0].enabled == true)
        {//Used the watering tool where it's needed
            Debug.Log("Watering Tool Used");
            field.ClosePromptImage(0, true);
            field.fieldHealthModify(GameManager.Instance.promptSuccess);
            SFXManager.Instance.PlayWateringClip();
        }
        if (field.requirePruningTool && PlayerToolManager.Instance.assignedTool == Tool.PruningTool && field.promptImages[1].enabled == true)
        {//Used the pruning tool where it's needed
            Debug.Log("Pruning Tool Used");
            field.ClosePromptImage(1, true);
            field.fieldHealthModify(GameManager.Instance.promptSuccess);
            SFXManager.Instance.PlayPruningClip();
        }
        if (field.requirePestControlTool && PlayerToolManager.Instance.assignedTool == Tool.PestControlTool && field.promptImages[2].enabled == true)
        {//Used the pest control tool where it's needed
            Debug.Log("Pest Control Tool Used");
            field.ClosePromptImage(2, true);
            field.fieldHealthModify(GameManager.Instance.promptSuccess);
            SFXManager.Instance.PlayPestControlClip();
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
