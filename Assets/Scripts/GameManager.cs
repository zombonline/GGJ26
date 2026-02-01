using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SongPlayer songPlayer;
    
    public UnityEvent OnGameStart;
    public UnityEvent OnLevelStart;
    public UnityEvent OnCountdownCompleted;
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public UnityEvent OnLevelCompleted;
    public UnityEvent OnGameFailed;
    public UnityEvent OnGameCompleted;

    private bool _canPause;
    
    private void Start()
    {
        Time.timeScale = 0;
        OnGameStart.Invoke();
    }

    public void StartLevel()
    {
        OnLevelStart.Invoke();
    }

    public void CompleteCountdown()
    {
        _canPause = true;
        Time.timeScale = 1;
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
        if (songPlayer.SongIndex == 2)
        {
            OnGameCompleted.Invoke();
        }
        else
        {
            OnLevelCompleted.Invoke();
        }

    }
}
