using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource Music1;
    [SerializeField] AudioSource Music2;
    //[SerializeField] AudioSource MainMenuAmbience;
    //[SerializeField] AudioSource GameOver;

    private void Start()
    {
        Instance = this;
        Music1.Play();
    }
    private void Update()
    {
        if (!Music1.isPlaying && !Music2.isActiveAndEnabled)
        {
            Music1.enabled = false;
            Music2.enabled = true;
            Music2.Play();
        }
        if (!Music2.isPlaying && !Music1.isActiveAndEnabled)
        {
            Music2.enabled = false;
            Music1.enabled = true;
            Music1.Play();
        }
    }

}
