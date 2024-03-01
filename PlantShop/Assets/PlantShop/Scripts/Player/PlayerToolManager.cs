using UnityEngine;
using UnityEngine.UI;

public class PlayerToolManager : MonoBehaviour
{
    public static PlayerToolManager Instance;
    [SerializeField] public Tool assignedTool;
    [SerializeField] public Image playerImage;
    void Start()
    {
        Instance = this;
    }
    //Assign the tool being used by the player
    public void AssignWateringTool()
    {
        assignedTool = Tool.WateringTool;
    }
    public void AssignPruningTool()
    {
        assignedTool = Tool.PruningTool;
    }
    public void AssignPestControlTool()
    {
        assignedTool = Tool.PestControlTool;
    }
}
public enum Tool //The tools the player can be assigned
{
    WateringTool,
    PruningTool,
    PestControlTool,
}
