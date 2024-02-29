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
    [SerializeField] float wateringActionTimer;
    [SerializeField] bool pestControlTimerActive;
    [SerializeField] float pestControlPromptTimer;
    [SerializeField] float pestControlActionTimer;
    [SerializeField] bool pruningTimerActive;
    [SerializeField] float pruningPromptTimer;
    [SerializeField] float pruningActionTimer;
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
        //SeasonUIManager.Instance.UpdateUIContent(currentSeason.name, seasonsCount);
    }
    void Update()
    {
        if (timer <= seasonTimerMax && timerActive)
        {
            timer += Time.deltaTime;
        }
        if (timer >= wateringPromptTimer & wateringTimerActive)
        {
            wateringTimerActive = false;
        }
        if (timer >= pruningPromptTimer & pruningTimerActive)
        {
            pruningTimerActive = false;
        }
        if (timer >= pestControlPromptTimer & pestControlTimerActive)
        {
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
        if (currentSeason == seasons[seasons.Count - 1])
        {
            currentSeason = seasons[3];
        }
        else
        {
            SeasonsSO nextSeason = seasons[seasons.IndexOf(currentSeason) + 1];
            currentSeason = nextSeason;
        }
        if (currentSeason == seasons[1] || currentSeason == seasons[2])
        {
            timerActive = false;
        }
        else
        {
            timerActive = true;
        }
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
    public void SetWateringPrompt()
    {
        wateringTimerActive = true;
    }
    public void SetPruningPrompt()
    {
        pruningTimerActive = true;
    }
    public void SetPestControlPrompt()
    {
        pestControlTimerActive = false;
    }
}
