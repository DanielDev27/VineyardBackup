using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;
    [SerializeField] AudioSource Music1;
    [SerializeField] AudioSource Music2;
    [SerializeField] AudioSource Music3;

    [SerializeField] float music1Length;
    [SerializeField] float music2Length;
    [SerializeField] float music3Length;
    [SerializeField] float timer;
    //[SerializeField] AudioSource MainMenuAmbience;
    //[SerializeField] AudioSource GameOver;

    private void Start () {
        Instance = this;
        Music1.Play ();
        timer = 0;
        music1Length = Music1.clip.length;
        music2Length = Music2.clip.length;
        music3Length = Music3.clip.length;
    }
    private void Update () {
        timer += Time.deltaTime;
        if (timer >= music2Length && !Music1.isActiveAndEnabled && !Music3.isActiveAndEnabled) {
            Music1.enabled = false;
            Music2.enabled = false;
            Music3.enabled = true;
            Music1.Play ();
            timer = 0;
        }
        if (timer >= music1Length && !Music2.isActiveAndEnabled && !Music3.isActiveAndEnabled) {
            Music1.enabled = false;
            Music2.enabled = true;
            Music3.enabled = false;
            Music2.Play ();
            timer = 0;
        }
        if (timer >= music3Length && !Music1.isActiveAndEnabled && !Music2.isActiveAndEnabled) {
            Music1.enabled = true;
            Music2.enabled = false;
            Music3.enabled = false;
            Music3.Play ();
            timer = 0;
        }
    }

}