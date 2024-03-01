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
    [SerializeField] Image yearImage;
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
    public void UpdateUIContent(int yearIndex)
    {
        yearImage.sprite = yearImages[yearIndex];
    }
}
