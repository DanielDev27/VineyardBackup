using System.Collections;
using System.Collections.Generic;
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
    void Update()
    {

    }
    public void AssignWateringTool()
    {
        assignedTool = Tool.WateringTool;
        //playerImage = wateringToolImage;
    }
    public void AssignPruningTool()
    {
        assignedTool = Tool.PruningTool;
        //playerImage = pruningToolImage;
    }
    public void AssignPestControlTool()
    {
        assignedTool = Tool.PestControlTool;
        //playerImage = pestControlToolImage;
    }
}
public enum Tool
{
    WateringTool,
    PruningTool,
    PestControlTool,
}
