using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    
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
        Time.timeScale = 1;
        SceneLoader.Instance.ChangeToMenuScene();
    }
}
