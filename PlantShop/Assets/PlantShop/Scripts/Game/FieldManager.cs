using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
public class FieldManager : MonoBehaviour
{
    [SerializeField] public float fieldHealth;
    [SerializeField] float fieldHealthBase = 50;
    [SerializeField] float vinesOffset;
    [SerializeField] public List<GameObject> vineAssets;
    [SerializeField] GameObject vineHolder;
    [SerializeField] List<GameObject> vinePrefabs;
    [Header("UI Prompts")]
    [SerializeField] public bool requireWateringTool;
    [SerializeField] public bool requirePruningTool;
    [SerializeField] public bool requirePestControlTool;
    [SerializeField] public Canvas promptCanvas;
    [SerializeField] public List<Image> promptImages;
    [SerializeField] public List<Image> timerImages;

    [Header("Timers")]
    [Header("Water Timers")]
    [SerializeField] float waterActionTimer;
    [SerializeField] public bool wateringTimerActive;
    [SerializeField] public float wateringPromptTimer;
    [SerializeField] public float wateringTimerBase;
    [SerializeField] public float wateringTimer;
    [Header("Pest Control Timers")]
    [SerializeField] float pestControlActionTimer;
    [SerializeField] public bool pestControlTimerActive;
    [SerializeField] public float pestControlPromptTimer;
    [SerializeField] public float pestControlTimerBase;
    [SerializeField] public float pestControlTimer;
    [Header("Pruning Timers")]
    [SerializeField] float pruningActionTimer;
    [SerializeField] public bool pruningTimerActive;
    [SerializeField] public float pruningPromptTimer;
    [SerializeField] public float pruningTimerBase;
    [SerializeField] public float pruningTimer;

    private void Awake()
    {
        GenerateField();//Generate the empty vine holders in the field
    }
    void Start()
    {
        //Assign the water timers limits
        wateringTimer = wateringTimerBase * GameManager.Instance.currentSeason.waterModifier;
        pruningTimer = pruningTimerBase * GameManager.Instance.currentSeason.pruningModifier;
        pestControlTimer = pestControlTimerBase * GameManager.Instance.currentSeason.pestModifier;
        //Set the health of the field
        fieldHealth = fieldHealthBase;
        //Set the canvases to look at the camera
        promptCanvas.transform.LookAt(new Vector3(-MousePosition.Instance.camera.transform.position.x, -MousePosition.Instance.camera.transform.position.y, -MousePosition.Instance.camera.transform.position.z));
        //Turn off the prompt images and timer images
        foreach (Image prompt in promptImages)
        {
            prompt.enabled = false;
        }
        foreach (Image timer in timerImages)
        {
            timer.enabled = false;
        }
    }
    void Update()
    {
        if (GameManager.Instance.timerActive)
        {
            //Action Timers for waiting - Counting up
            if (wateringTimerActive)
            {
                wateringPromptTimer += Time.deltaTime;
            }
            if (pruningTimerActive)
            {
                pruningPromptTimer += Time.deltaTime;
            }
            if (pestControlTimerActive)
            {
                pestControlPromptTimer += Time.deltaTime;
            }
            //Action Timers for reaction priority - Count Down
            if (timerImages[0].isActiveAndEnabled)
            {
                timerImages[0].fillAmount -= Time.deltaTime / waterActionTimer;
                if (timerImages[0].fillAmount <= 0)
                {
                    ClosePromptImage(0, false);
                }
            }
            if (timerImages[1].isActiveAndEnabled)
            {
                timerImages[1].fillAmount -= Time.deltaTime / pruningActionTimer;
                if (timerImages[1].fillAmount <= 0)
                {
                    ClosePromptImage(1, false);
                }
            }
            if (timerImages[2].isActiveAndEnabled)
            {
                timerImages[2].fillAmount -= Time.deltaTime / pestControlActionTimer;
                if (timerImages[2].fillAmount <= 0)
                {
                    ClosePromptImage(2, false);
                }
            }
        }
        //Water Action Trigger
        if (wateringPromptTimer >= wateringTimer & wateringTimerActive)
        {
            wateringTimerActive = false;
            TriggerPromptImage(Tool.WateringTool);
        }
        //Pruning Action Trigger
        if (pruningPromptTimer >= pruningTimer & pruningTimerActive)
        {
            pruningTimerActive = false;
            TriggerPromptImage(Tool.PruningTool);
        }
        //Pest Control Trigger
        if (pestControlPromptTimer >= pestControlTimer & pestControlTimerActive)
        {
            TriggerPromptImage(Tool.PestControlTool);
            pestControlTimerActive = false;
        }
    }

    public void fieldHealthModify(int healthChange)//update the Field health
    {
        if (fieldHealth <= 0)
        {
            fieldHealth = 0;
        }
        else
        {
            fieldHealth += healthChange;
        }
    }

    [Button]
    public void GenerateField()//Make the vine holders
    {
        GameObject vine = Instantiate(vineHolder);
        vine.transform.position = this.transform.position;
        vine.transform.parent = this.transform;
        vineAssets.Add(vine);
        UpdateField(0);
    }
    [Button]
    public void UpdateField(int seasonIndex)//Change the vine prefabs in the vine holders
    {
        foreach (GameObject _vine in vineAssets)
        {
            if (_vine.TryGetComponent(out Vine component))
            {
                if (_vine.GetComponent<Vine>().vinePrefab != null)
                {//Destroy the old prefab
                    GameObject _vineExisting = _vine.GetComponent<Vine>().vinePrefab.gameObject;
                    DestroyImmediate(_vineExisting.gameObject, true);
                }
                //Create new Model instance
                _vine.GetComponent<Vine>().vinePrefab = Instantiate(vinePrefabs[seasonIndex], _vine.transform.position + Vector3.up * vinesOffset, Quaternion.identity);
                _vine.GetComponent<Vine>().vinePrefab.transform.parent = _vine.transform;
                _vine.GetComponent<Vine>().vinePrefab.transform.forward = _vine.transform.up;
            }
        }
    }
    [Button]
    public void TriggerPromptImage(Tool _requiredTool)//Activate the prompt Images and timers
    {
        switch (_requiredTool)
        {
            case Tool.WateringTool:
                requireWateringTool = true;
                promptImages[0].enabled = true;
                timerImages[0].enabled = true;
                timerImages[0].fillAmount = 1;
                break;
            case Tool.PruningTool:
                requirePruningTool = true;
                promptImages[1].enabled = true;
                timerImages[1].enabled = true;
                timerImages[1].fillAmount = 1;
                break;
            case Tool.PestControlTool:
                requirePestControlTool = true;
                promptImages[2].enabled = true;
                timerImages[2].enabled = true;
                timerImages[2].fillAmount = 1;
                break;
            default:
                break;
        }
    }
    public void SetActionTimers(SeasonsSO season)
    {
        waterActionTimer = season.waterActionModifier;
        pruningActionTimer = season.pruningActionModifier;
        pestControlActionTimer = season.pestActionModifier;
    }
    public void ClosePromptImage(int promptIndex, bool taskComplete)//Close and turn off the prompt images and timers
    {
        promptImages[promptIndex].enabled = false;
        timerImages[promptIndex].enabled = false;
        switch (promptIndex)
        {
            case 0:
                requireWateringTool = false;
                if (GameManager.Instance.seasonsCount == 0 || GameManager.Instance.seasonsCount == 1 || GameManager.Instance.seasonsCount == 2)
                {
                    GameManager.Instance.TutorialTaskComplete();
                }
                if (GameManager.Instance.seasonsCount != 0 && GameManager.Instance.seasonsCount != 1 && GameManager.Instance.seasonsCount != 2)
                {
                    ResetWateringPrompt();
                }
                break;
            case 1:
                requirePruningTool = false;
                if (GameManager.Instance.seasonsCount == 0 || GameManager.Instance.seasonsCount == 1 || GameManager.Instance.seasonsCount == 2)
                {
                    GameManager.Instance.TutorialTaskComplete();
                }
                if (GameManager.Instance.seasonsCount != 0 && GameManager.Instance.seasonsCount != 1 && GameManager.Instance.seasonsCount != 2)
                {
                    ResetWateringPrompt();
                }
                break;
            case 2:
                requirePestControlTool = false;
                if (GameManager.Instance.seasonsCount == 0 || GameManager.Instance.seasonsCount == 1 || GameManager.Instance.seasonsCount == 2)
                {
                    GameManager.Instance.TutorialTaskComplete();
                }
                if (GameManager.Instance.seasonsCount != 0 && GameManager.Instance.seasonsCount != 1 && GameManager.Instance.seasonsCount != 2)
                {
                    ResetWateringPrompt();
                }
                break;
            default:
                break;
        }
    }
    internal void ResetPromptTimers(SeasonsSO currentSeason)//Reset the prompt timers at the end of a season
    {
        wateringTimer = wateringTimerBase * currentSeason.waterModifier;
        wateringPromptTimer = 0;
        pruningTimer = pruningTimerBase * currentSeason.pruningModifier;
        pruningPromptTimer = 0;
        pestControlTimer = pestControlTimerBase * currentSeason.pestModifier;
        pestControlPromptTimer = 0;
    }

    //Reset Timers after completing the task
    public void ResetWateringPrompt()
    {
        wateringPromptTimer = 0;
        wateringTimerActive = true;
    }
    public void ResetPruningPrompt()
    {
        pruningPromptTimer = 0;
        pruningTimerActive = true;
    }
    public void ResetPestControlPrompt()
    {
        pestControlPromptTimer = 0;
        pestControlTimerActive = true;
    }
}
