using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    
    [SerializeField] private string menuSceneName;
    [SerializeField] private string levelSceneName;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            gameObject.transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void ChangeToMenuScene()
    {
        SceneManager.LoadScene(menuSceneName);
    }
    
    public void ChangeToLevelScene()
    {
        SceneManager.LoadScene(levelSceneName);
    }
}
