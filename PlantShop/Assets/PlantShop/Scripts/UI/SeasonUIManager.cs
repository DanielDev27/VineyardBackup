using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeasonUIManager : MonoBehaviour
{
    public static SeasonUIManager Instance;
    [SerializeField] List<Image> seasonImage;
    [SerializeField] List<Image> backgroundSeason;
    [SerializeField] TextMeshProUGUI timeText;
    void Start()
    {
        Instance = this;
    }
    void Update()
    {

    }
    public void UpdateUIContent(string season, int seasonIndex)
    {
        timeText.text = season;
    }
}
