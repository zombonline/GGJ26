using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnLevelStart;
    public UnityEvent OnCountdownCompleted;
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public UnityEvent OnGameFailed;

    private bool _canPause;
    
    private void Start()
    {
        StartLevel();
    }

    private void StartLevel()
    {
        OnLevelStart.Invoke();
    }

    public void CompleteCountdown()
    {
        _canPause = true;
        OnCountdownCompleted.Invoke();
    }
    
    public void PauseGame()
    {
        if (!_canPause)
            return;
        
        Time.timeScale = 0;
        OnPause.Invoke();
    }

    public void ResumeGame()
    {
        if (!_canPause)
            return;
        
        Time.timeScale = 1;
        OnResume.Invoke();
    }

    public void ChangeToMenu()
    {
        // Resuming is now done in scene loader
        SceneLoader.Instance.ChangeToMenuScene();
    }

    public void FailGame()
    {
        _canPause = false;
        
        Time.timeScale = 0;
        OnGameFailed.Invoke();
    }

    public void Restart()
    {
        // Resuming is now done in scene loader
        SceneLoader.Instance.ChangeToLevelScene();
    }
    
    public void CompleteLevel()
    {
        _canPause = false;
    }
}
