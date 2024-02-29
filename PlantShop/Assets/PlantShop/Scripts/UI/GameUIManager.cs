using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;
    [SerializeField] bool isMenuActive;

    public bool pause;

    bool isPauseRoutineRunning;
    [SerializeField] int menuScene;
    [SerializeField] CanvasGroup pauseGroup;
    [SerializeField] CanvasGroup loadingGroup;
    private void Awake()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pause = true;
            PauseMenu(pause);
        }
    }

    public void PauseMenu(bool isPaused)
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
    public void ResumeGame()
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
    public void ReturnToMenu()
    {
        pauseGroup.DOFade(0, 0.3f);
        loadingGroup.DOFade(1, 0.3f);
        SceneLoaderManager.Instance.LoadScene(menuScene);
    }
}
