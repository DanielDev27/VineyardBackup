using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
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

    [Header("Timers")]
    [SerializeField] public bool wateringTimerActive;
    [SerializeField] public float wateringPromptTimer;
    [SerializeField] public float wateringTimerBase;
    [SerializeField] public float wateringTimer;
    [SerializeField] public bool pestControlTimerActive;
    [SerializeField] public float pestControlPromptTimer;
    [SerializeField] public float pestControlTimerBase;
    [SerializeField] public float pestControlTimer;
    [SerializeField] public bool pruningTimerActive;
    [SerializeField] public float pruningPromptTimer;
    [SerializeField] public float pruningTimerBase;
    [SerializeField] public float pruningTimer;

    private void Awake()
    {
        GenerateField();
    }
    void Start()
    {
        wateringTimer = wateringTimerBase * GameManager.Instance.currentSeason.waterModifier;
        pruningTimer = pruningTimerBase * GameManager.Instance.currentSeason.pruningModifier;
        pestControlTimer = pestControlTimerBase * GameManager.Instance.currentSeason.pestModifier;
        fieldHealth = fieldHealthBase;
        promptCanvas.transform.LookAt(new Vector3(-MousePosition.Instance.camera.transform.position.x, -MousePosition.Instance.camera.transform.position.y, -MousePosition.Instance.camera.transform.position.z));
        //promptCanvas.GetComponentInChildren<TMP_Text>().enabled = false;
        foreach (Image prompt in promptImages)
        {
            prompt.enabled = false;
        }
    }
    void Update()
    {
        if (GameManager.Instance.timerActive)
        {

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
        }
        if (wateringPromptTimer >= wateringTimer & wateringTimerActive)
        {
            wateringTimerActive = false;
            foreach (GameObject _field in GameManager.Instance.fields)
            {
                _field.GetComponent<FieldManager>().TriggerPromptImage(Tool.WateringTool);
            }
        }
        if (pruningPromptTimer >= pruningTimer & pruningTimerActive)
        {
            foreach (GameObject _field in GameManager.Instance.fields)
            {
                _field.GetComponent<FieldManager>().TriggerPromptImage(Tool.PruningTool);
            }
            pruningTimerActive = false;
        }
        if (pestControlPromptTimer >= pestControlTimer & pestControlTimerActive)
        {
            foreach (GameObject _field in GameManager.Instance.fields)
            {
                _field.GetComponent<FieldManager>().TriggerPromptImage(Tool.PestControlTool);
            }
            pestControlTimerActive = false;
        }
    }

    public void fieldHealthModify(int healthChange)
    {
        fieldHealth += healthChange;
    }

    [Button]
    public void GenerateField()
    {
        GameObject vine = Instantiate(vineHolder);
        vine.transform.position = this.transform.position;
        vine.transform.parent = this.transform;
        vineAssets.Add(vine);
        UpdateField(0);
    }
    [Button]
    public void UpdateField(int vineIndex)
    {
        foreach (GameObject _vine in vineAssets)
        {
            if (_vine.TryGetComponent(out Vine component))
            {
                if (_vine.GetComponent<Vine>().vinePrefab != null)
                {
                    GameObject _vineExisting = _vine.GetComponent<Vine>().vinePrefab.gameObject;
                    DestroyImmediate(_vineExisting.gameObject, true);
                }
                //Create new Model instance
                _vine.GetComponent<Vine>().vinePrefab = Instantiate(vinePrefabs[vineIndex], _vine.transform.position + Vector3.up * vinesOffset, Quaternion.identity);
                _vine.GetComponent<Vine>().vinePrefab.transform.parent = _vine.transform;
                _vine.GetComponent<Vine>().vinePrefab.transform.forward = _vine.transform.up;

            }
        }
    }
    [Button]
    public void TriggerPromptImage(Tool _requiredTool)
    {
        switch (_requiredTool)
        {
            case Tool.WateringTool:
                requireWateringTool = true;
                //promptCanvas.GetComponentInChildren<TMP_Text>().text = "Watering Tool";
                promptImages[0].enabled = true;
                break;
            case Tool.PruningTool:
                requirePruningTool = true;
                //promptCanvas.GetComponentInChildren<TMP_Text>().text = "Pruning Tool";
                promptImages[1].enabled = true;
                break;
            case Tool.PestControlTool:
                requirePestControlTool = true;
                //promptCanvas.GetComponentInChildren<TMP_Text>().text = "Pest Control Tool";
                promptImages[2].enabled = true;
                break;
            default:
                break;

        }
        //promptCanvas.GetComponentInChildren<TMP_Text>().enabled = true;
    }
    public void ClosePromptImage(int promptIndex)
    {
        //promptCanvas.GetComponentInChildren<TMP_Text>().enabled = false;
        promptImages[promptIndex].enabled = false;
        switch (promptIndex)
        {
            case 0:
                requireWateringTool = false;
                ResetWateringPrompt();
                break;
            case 1:
                requirePruningTool = false;
                ResetPruningPrompt();
                break;
            case 2:
                requirePestControlTool = false;
                ResetPestControlPrompt();
                break;
            default:
                break;
        }
    }

    internal void ResetPromptTimers(SeasonsSO currentSeason)
    {
        wateringTimer = wateringTimerBase * currentSeason.waterModifier;
        wateringPromptTimer = 0;
        pruningTimer = pruningTimerBase * currentSeason.pruningModifier;
        pruningPromptTimer = 0;
        pestControlTimer = pestControlTimerBase * currentSeason.pestModifier;
        pestControlPromptTimer = 0;
    }

    //Reset Timers
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
