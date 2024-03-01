using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Timer")]
    [SerializeField] float timer;
    [SerializeField] float seasonTimerMax;
    [SerializeField] public bool timerActive;
    [Header("Scene Settings")]
    [SerializeField] public List<GameObject> fields;
    [SerializeField] List<SeasonsSO> seasons;
    [Header("Seasons Tracker")]
    [SerializeField] int totalSeasons;
    [SerializeField] int seasonsCount;
    [SerializeField] int yearsCount;
    [SerializeField] public SeasonsSO currentSeason;
    private void Awake()
    {
        Instance = this;
        currentSeason = seasons[0];
    }
    void Start()
    {
        ChangeFieldAssets();
        SeasonUIManager.Instance.UpdateYear(yearsCount);
        SeasonUIManager.Instance.UpdateSeason(currentSeason.season);
    }
    void Update()
    {
        if (timer <= seasonTimerMax && timerActive)
        {
            timer += Time.deltaTime;
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
        SeasonUIManager.Instance.UpdateSeason(currentSeason.season);
        //Timers
        if (currentSeason == seasons[1] || currentSeason == seasons[2])
        {
            timerActive = false;
        }
        else
        {
            timerActive = true;
            foreach (GameObject _field in fields)
            {
                _field.GetComponent<FieldManager>().wateringTimerActive = true;
                _field.GetComponent<FieldManager>().pruningTimerActive = true;
                _field.GetComponent<FieldManager>().pestControlTimerActive = true;

            }
        }
        seasonsCount++;
        if (seasonsCount % 4 == 0 && seasonsCount != 0)
        {
            yearsCount++;
        }

        SeasonUIManager.Instance.UpdateYear(yearsCount);
        SeasonUIManager.Instance.UpdateSeason(currentSeason.season);
        ChangeFieldAssets();
        foreach (GameObject _field in fields)
        {
            _field.GetComponent<FieldManager>().ResetPromptTimers(currentSeason);
        }

    }

    public void ChangeFieldAssets()
    {
        List<FieldManager> fieldManagers = new List<FieldManager>(FindObjectsOfType<FieldManager>());
        foreach (FieldManager field in fieldManagers)
        {
            field.UpdateField(seasonsCount);
        }
    }
}
