using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {
    public static SFXManager Instance;
    [SerializeField] AudioSource wateringClip;
    [SerializeField] AudioSource pruningClip;
    [SerializeField] AudioSource pestClip;
    [SerializeField] AudioSource timePassingClip;
    private void Awake () {
        Instance = this;
    }

    public void PlayWateringClip () {
        wateringClip.Play ();
    }
    public void PlayPruningClip () {
        pruningClip.Play ();
    }
    public void PlayPestControlClip () {
        pestClip.Play ();
    }
    public void PlayTimePassingClip () {
        timePassingClip.Play ();
    }

}