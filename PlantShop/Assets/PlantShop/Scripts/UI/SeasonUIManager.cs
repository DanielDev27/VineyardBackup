using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SeasonUIManager : MonoBehaviour
{
    public static SeasonUIManager Instance;
    [SerializeField] List<Image> seasonImage;
    [SerializeField] Image yearImage;
    [SerializeField] int yearIndex;
    [SerializeField] List<Sprite> yearImages;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
    }
    void Update()
    {

    }
    public void UpdateYear(int _yearIndex)
    {
        yearIndex = _yearIndex;
        yearImage.sprite = yearImages[yearIndex];
    }
    public void UpdateSeason(Season season)
    {
        switch (season)
        {
            case Season.Summer:
                seasonImage[0].color = new Color(seasonImage[0].color.r, seasonImage[0].color.g, seasonImage[0].color.b, 0.5f);
                seasonImage[1].color = new Color(seasonImage[1].color.r, seasonImage[1].color.g, seasonImage[1].color.b, 1f);
                seasonImage[2].color = new Color(seasonImage[2].color.r, seasonImage[2].color.g, seasonImage[2].color.b, 0.5f);
                seasonImage[3].color = new Color(seasonImage[3].color.r, seasonImage[3].color.g, seasonImage[3].color.b, 0.5f);
                break;
            case Season.Autumn:
                seasonImage[0].color = new Color(seasonImage[0].color.r, seasonImage[0].color.g, seasonImage[0].color.b, 0.5f);
                seasonImage[1].color = new Color(seasonImage[1].color.r, seasonImage[1].color.g, seasonImage[1].color.b, 0.5f);
                seasonImage[2].color = new Color(seasonImage[2].color.r, seasonImage[2].color.g, seasonImage[2].color.b, 1f);
                seasonImage[3].color = new Color(seasonImage[3].color.r, seasonImage[3].color.g, seasonImage[3].color.b, 0.5f);
                break;
            case Season.Winter:
                seasonImage[0].color = new Color(seasonImage[0].color.r, seasonImage[0].color.g, seasonImage[0].color.b, 0.5f);
                seasonImage[1].color = new Color(seasonImage[1].color.r, seasonImage[1].color.g, seasonImage[1].color.b, 0.5f);
                seasonImage[2].color = new Color(seasonImage[2].color.r, seasonImage[2].color.g, seasonImage[2].color.b, 0.5f);
                seasonImage[3].color = new Color(seasonImage[3].color.r, seasonImage[3].color.g, seasonImage[3].color.b, 1f);
                break;
            case Season.Spring:
                seasonImage[0].color = new Color(seasonImage[0].color.r, seasonImage[0].color.g, seasonImage[0].color.b, 1f);
                seasonImage[1].color = new Color(seasonImage[1].color.r, seasonImage[1].color.g, seasonImage[1].color.b, 0.5f);
                seasonImage[2].color = new Color(seasonImage[2].color.r, seasonImage[2].color.g, seasonImage[2].color.b, 0.5f);
                seasonImage[3].color = new Color(seasonImage[3].color.r, seasonImage[3].color.g, seasonImage[3].color.b, 0.5f);
                break;
            default:
                break;
        }
    }
}
