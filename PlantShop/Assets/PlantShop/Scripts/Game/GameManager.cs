using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] float timer;
    [SerializeField] float seasonTimerMax;
    [SerializeField] bool timerActive;
    [SerializeField] bool wateringTimerActive;
    [SerializeField] float wateringPromptTimer;
    [SerializeField] float wateringTimerBase;
    [SerializeField] float wateringTimer;
    [SerializeField] bool pestControlTimerActive;
    [SerializeField] float pestControlPromptTimer;
    [SerializeField] float pestControlTimerBase;
    [SerializeField] float pestControlTimer;
    [SerializeField] bool pruningTimerActive;
    [SerializeField] float pruningPromptTimer;
    [SerializeField] float pruningTimerBase;
    [SerializeField] float pruningTimer;
    [Header("Scene Settings")]
    [SerializeField] List<GameObject> fields;
    [SerializeField] List<SeasonsSO> seasons;
    [Header("Seasons Tracker")]
    [SerializeField] int totalSeasons;
    [SerializeField] int seasonsCount;
    [SerializeField] SeasonsSO currentSeason;
    void Start()
    {
        currentSeason = seasons[0];
        ChangeFieldAssets();
        SeasonUIManager.Instance.UpdateUIContent(currentSeason.name, seasonsCount);
        wateringTimer = wateringTimerBase * currentSeason.waterModifier;
        pruningTimer = pruningTimerBase * currentSeason.pruningModifier;
        pestControlTimer = pestControlTimerBase * currentSeason.pestModifier;
    }
    void Update()
    {
        if (timer <= seasonTimerMax && timerActive)
        {
            timer += Time.deltaTime;
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
            foreach (GameObject _field in fields)
            {
                _field.GetComponent<FieldManager>().TriggerPromptImage(Tool.WateringTool);
            }
        }
        if (pruningPromptTimer >= pruningTimer & pruningTimerActive)
        {
            foreach (GameObject _field in fields)
            {
                _field.GetComponent<FieldManager>().TriggerPromptImage(Tool.PruningTool);
            }
            pruningTimerActive = false;
        }
        if (pestControlPromptTimer >= pestControlTimer & pestControlTimerActive)
        {
            foreach (GameObject _field in fields)
            {
                _field.GetComponent<FieldManager>().TriggerPromptImage(Tool.PestControlTool);
            }
            pestControlTimerActive = false;
        }
        if (timer > seasonTimerMax)
        {
            timer = 0;
            ChangeSeasons();
        }
    }
    [Button]
    public void ChangeSeasons()
    {
        //Debug.Log(seasons.Count);

        //Seasons
        if (currentSeason == seasons[seasons.Count - 1])
        {
            currentSeason = seasons[3];
        }
        else
        {
            SeasonsSO nextSeason = seasons[seasons.IndexOf(currentSeason) + 1];
            currentSeason = nextSeason;
        }
        //Timers
        if (currentSeason == seasons[1] || currentSeason == seasons[2])
        {
            timerActive = false;
        }
        else
        {
            timerActive = true;
            wateringTimerActive = true;
            pruningTimerActive = true;
            pestControlTimerActive = true;
        }
        wateringTimer = wateringTimerBase * currentSeason.waterModifier;
        pruningTimer = pruningTimerBase * currentSeason.pruningModifier;
        pestControlTimer = pestControlTimerBase * currentSeason.pestModifier;
        seasonsCount++;

        ChangeFieldAssets();
        SeasonUIManager.Instance.UpdateUIContent(currentSeason.name, seasonsCount);
    }

    public void ChangeFieldAssets()
    {
        List<FieldManager> fieldManagers = new List<FieldManager>(FindObjectsOfType<FieldManager>());
        foreach (FieldManager field in fieldManagers)
        {
            field.UpdateField(seasonsCount);
        }
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
        pestControlTimerActive = false;
    }
}
