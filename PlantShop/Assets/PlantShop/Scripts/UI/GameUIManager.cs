using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    [Header("Tool Buttons")]
    [SerializeField] Color selectedColor;
    [SerializeField] Image waterBackground;
    [SerializeField] Image pruningBackground;
    [SerializeField] Image pestBackground;

    [Header("Tutorial")]
    [SerializeField] Canvas tutorialCanvas;
    [Header("GradingUI")]
    [SerializeField] Canvas gradingUI;
    [SerializeField] Animator gradingAnimator;
    [SerializeField] float zeroStarMax = 0;
    [SerializeField] float oneStarMax = 0;
    [SerializeField] float twoStarMax = 0;

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

    public void GradeUI(float score)
    {
        gradingUI.enabled = true;
        switch (score)
        {
            case float i when i <= zeroStarMax://Zero stars - Didn't lose the game, but didn't make a good wine
                gradingAnimator.SetBool("Idle", true);
                break;
            case float i when i > zeroStarMax && i <= oneStarMax://One Star - Managed to make a wine, but still not great
                gradingAnimator.SetBool("1Star", true);
                break;
            case float i when i > oneStarMax && i <= twoStarMax://Two Stars - Did fairly well
                gradingAnimator.SetBool("2Star", true);
                break;
            case float i when i > twoStarMax://Three stars - Did very well
                gradingAnimator.SetBool("3Star", true);
                break;
            default:
                break;
        }

    }
    public void GameComplete()
    {
        gameComplete.enabled = true;
    }
    public void GameLost()
    {
        gameLost.enabled = true;
    }

    //ToolButtons

    public void SetWaterSelected()
    {
        waterBackground.color = selectedColor;
        pruningBackground.color = Color.white;
        pestBackground.color = Color.white;
    }
    public void SetPruningSelected()
    {
        waterBackground.color = Color.white;
        pruningBackground.color = selectedColor;
        pestBackground.color = Color.white;
    }
    public void SetPestSelected()
    {
        waterBackground.color = Color.white;
        pruningBackground.color = Color.white;
        pestBackground.color = selectedColor;
    }

}
