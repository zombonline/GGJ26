using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public UnityEvent OnGameFailed;

    public void PauseGame()
    {
        Time.timeScale = 0;
        OnPause.Invoke();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        OnResume.Invoke();
    }

    public void ResumeAndChangeToMenu()
    {
        SceneLoader.Instance.ChangeToMenuScene();
    }

    public void FailGame()
    {
        Time.timeScale = 0;
        OnGameFailed.Invoke();
    }

    public void ResumeAndRestart()
    {
        SceneLoader.Instance.ChangeToLevelScene();
    }
}
