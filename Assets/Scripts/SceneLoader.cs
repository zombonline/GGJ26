using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    
    [Header("UI Components")]
    [SerializeField] private CanvasGroup overlayCanvasGroup;

    [Header("Settings")]
    [SerializeField] private string menuSceneName;
    [SerializeField] private string levelSceneName;
    [SerializeField] private float fadeDuration;     
    
    private Coroutine _changeSceneCoroutine;
    
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
        ChangeToScene(menuSceneName);
    }
    
    public void ChangeToLevelScene()
    {
        ChangeToScene(levelSceneName);
    }

    private void ChangeToScene(string sceneName)
    {
        if (_changeSceneCoroutine != null)
        {
            StopCoroutine(_changeSceneCoroutine);
            _changeSceneCoroutine = null;
        }

        StartCoroutine(ChangeSceneSequence(sceneName));
    }
    
    private IEnumerator ChangeSceneSequence(string sceneName)
    {
        overlayCanvasGroup.blocksRaycasts = true;
        
        float startTime = Time.unscaledTime;
        while (Time.unscaledTime < startTime + fadeDuration)
        {
            overlayCanvasGroup.alpha = Mathf.Lerp(0f, 1f, (Time.unscaledTime - startTime) / fadeDuration);
            yield return null;
        }
        
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
        
        startTime = Time.unscaledTime;
        while (Time.unscaledTime < startTime + fadeDuration)
        {
            overlayCanvasGroup.alpha = Mathf.Lerp(1f, 0f, (Time.unscaledTime - startTime) / fadeDuration);
            yield return null;
        }
        
        overlayCanvasGroup.blocksRaycasts = false;
        _changeSceneCoroutine = null;
    }
}
