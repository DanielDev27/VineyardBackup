using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;
    [Header("Pause Menu")]
    [SerializeField] bool isMenuActive;
    public bool pause;
    bool isPauseRoutineRunning;
    [SerializeField] int menuScene;
    [SerializeField] CanvasGroup pauseGroup;
    [SerializeField] CanvasGroup loadingGroup;

    [Header("Tutorial")]
    [SerializeField] Canvas tutorialCanvas;
    [Header("GameComplete")]
    [SerializeField] Canvas gameComplete;
    [Header("Game Lost")]
    [SerializeField] Canvas gameLost;
    private void Awake()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Pause(InputAction.CallbackContext context)//Player input function to pause the game
    {
        if (context.performed)
        {
            pause = true;
            PauseMenu(pause);
        }
    }
    public void PauseMenu(bool isPaused)//Open the pause menu
    {
        isMenuActive = isPaused;
        if (isPaused)
        {
            pauseGroup.GetComponent<Canvas>().enabled = true;
            Cursor.lockState = CursorLockMode.Confined;
            isMenuActive = true;
            Time.timeScale = 0f;
        }
    }
    public void ResumeGame()//Resume the game
    {
        if (isPauseRoutineRunning)
        {
            StopCoroutine(WaitForPause(0f));
        }
        Time.timeScale = 1f;
        pauseGroup.GetComponent<Canvas>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        isMenuActive = false;
        pause = false;
    }
    public void ReturnToMenu()//Go to the main menu
    {
        pauseGroup.DOFade(0, 0.3f);
        loadingGroup.DOFade(1, 0.3f);
        SceneLoaderManager.Instance.LoadScene(menuScene);
    }
    IEnumerator WaitForPause(float delay)
    {
        isPauseRoutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        pauseGroup.GetComponent<Canvas>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        isMenuActive = true;
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f;
        yield return new WaitForSeconds(0.01f);
        isPauseRoutineRunning = false;
    }

    public void GameComplete()
    {
        gameComplete.enabled = true;
    }
    public void GameLost()
    {
        gameLost.enabled = true;
    }
}
