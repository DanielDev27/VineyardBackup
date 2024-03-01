using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using Sirenix.OdinInspector;

public class ScriptReader : MonoBehaviour
{
    [Header("Basic Inputs")]
    [SerializeField] private TextAsset _inkJsonAsset;
    private Story _story;
    [SerializeField] public Canvas tutorialCanvas;
    [SerializeField] TMP_Text tutorialText;

    [SerializeField] private bool _space;

    [Header("Debug Visable")]

    [SerializeField] private int seasonIndex;
    [SerializeField, ShowInInspector] TextAsset[] storyArray;
    private void Awake()
    {
        tutorialText.text = "";
        _inkJsonAsset = storyArray[0];
        LoadStory();
        DisplayNextLine();
    }
    void LoadStory()
    {
        _story = new Story(_inkJsonAsset.text);
    }
    public void DisplayNextLine()
    {
        if (_story.canContinue) //checking that there is more content to display
        {
            string text = _story.Continue();//Gets next line
            text = text?.Trim();//Removes white space
            tutorialText.text = text;//Displays text}
        }
        else
        {
            tutorialCanvas.enabled = false;
        }
    }

    public void NextLine()
    {
        DisplayNextLine();
        //Debug.Log("Next line");
    }
    public void LoadTutorial(int storyIndex)
    {
        _inkJsonAsset = storyArray[storyIndex];
        LoadStory();
        DisplayNextLine();
    }

}
