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
    [SerializeField] public int seasonsCount;
    [SerializeField] int yearsCount;
    [SerializeField] public SeasonsSO currentSeason;
    [SerializeField] int tutorialTaskCount;
    private void Awake()
    {
        Instance = this;
        currentSeason = seasons[0];//Starts at first season
    }
    void Start()
    {
        ChangeFieldAssets();//Spawn Vines
        SeasonUIManager.Instance.UpdateYear(yearsCount);
        SeasonUIManager.Instance.UpdateSeason(currentSeason.season);
        foreach (GameObject _field in fields)
        {
            _field.GetComponent<FieldManager>().SetActionTimers(currentSeason);
        }
    }
    void Update()
    {
        //General Timer
        if (timer <= seasonTimerMax && timerActive)
        {
            timer += Time.deltaTime;
        }
        //Reset timer and Change Season
        if (timer > seasonTimerMax)
        {
            ChangeSeasons();
        }
        if (fields[0].GetComponent<FieldManager>().fieldHealth + fields[1].GetComponent<FieldManager>().fieldHealth + fields[2].GetComponent<FieldManager>().fieldHealth <= 0)
        {
            GameUIManager.Instance.GameLost();
            timerActive = false;
        }
    }
    [Button]
    public void ChangeSeasons()
    {
        //Debug.Log(seasons.Count);
        timer = 0;
        //Seasons
        if (currentSeason == seasons[seasons.Count - 1])
        {//Reset back to 4th season if you reach the end of the list
            currentSeason = seasons[3];
        }
        else
        {//Increase the season index and assign the new season
            SeasonsSO nextSeason = seasons[seasons.IndexOf(currentSeason) + 1];
            currentSeason = nextSeason;
        }
        SeasonUIManager.Instance.UpdateSeason(currentSeason.season);
        //Timers
        if (currentSeason == seasons[1] || currentSeason == seasons[2])
        {//Timer isn't active during tutorials
            timerActive = false;
        }
        else
        {//Activate all the field timers when the main timer is active
            timerActive = true;
            foreach (GameObject _field in fields)
            {
                _field.GetComponent<FieldManager>().wateringTimerActive = true;
                _field.GetComponent<FieldManager>().pruningTimerActive = true;
                _field.GetComponent<FieldManager>().pestControlTimerActive = true;
                _field.GetComponent<FieldManager>().SetActionTimers(currentSeason);
            }
        }
        seasonsCount++;
        if (seasonsCount % 4 == 0 && seasonsCount != 0)
        {//If 4 seasons pass, increase the year
            yearsCount++;
        }
        if (seasonsCount == 12)
        {
            GameUIManager.Instance.GameComplete();
            timerActive = false;
            return;
        }
        else
        {
            ScriptReader.Instance.LoadTutorial(seasonsCount);//Load the tutorial based on the season
            SeasonUIManager.Instance.UpdateYear(yearsCount);
            SeasonUIManager.Instance.UpdateSeason(currentSeason.season);
            ChangeFieldAssets();
        }
        foreach (GameObject _field in fields)
        {
            //Reset all the timers in the fields
            _field.GetComponent<FieldManager>().UpdateActionTimers(currentSeason);
            _field.GetComponent<FieldManager>().SetActionTimers(currentSeason);
        }
    }
    public void ChangeFieldAssets()//Call an update to the vine assets in each field
    {
        List<FieldManager> fieldManagers = new List<FieldManager>(FindObjectsOfType<FieldManager>());
        foreach (FieldManager field in fieldManagers)
        {
            field.UpdateField(seasonsCount);
        }
    }
    public void SetPrompts(int _tutorialIndex)//Activating the action prompt timers specifically for the tutorials
    {
        timerActive = true;
        switch (_tutorialIndex)
        {
            case 1:
                foreach (GameObject _field in fields)
                {
                    _field.GetComponent<FieldManager>().wateringTimerActive = true;
                }
                break;
            case 2:
                foreach (GameObject _field in fields)
                {
                    _field.GetComponent<FieldManager>().wateringTimerActive = true;
                    _field.GetComponent<FieldManager>().pestControlTimerActive = true;
                }
                break;
            case 3:
                foreach (GameObject _field in fields)
                {
                    _field.GetComponent<FieldManager>().wateringTimerActive = true;
                    _field.GetComponent<FieldManager>().pruningTimerActive = true;
                    _field.GetComponent<FieldManager>().pestControlTimerActive = true;
                }
                break;
            default:
                break;
        }
    }
    public void TutorialTaskComplete()//Tracking the completed tasks during the tutorial in order to change the season and progress
    {
        tutorialTaskCount++;
        if (seasonsCount == 0 && tutorialTaskCount == 3)
        {
            tutorialTaskCount = 0;
            ChangeSeasons();
        }
        if (seasonsCount == 1 && tutorialTaskCount == 6)
        {
            tutorialTaskCount = 0;
            ChangeSeasons();
        }
        if (seasonsCount == 2 && tutorialTaskCount == 9)
        {
            tutorialTaskCount = 0;
            ChangeSeasons();
        }
    }
}
