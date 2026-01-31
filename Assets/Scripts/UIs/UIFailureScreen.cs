using UnityEngine;
using UnityEngine.UI;

public class UIFailureScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    
    [Header("Components")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button retryButton;
    
    private void Awake()
    {
        Show(false);
    }
    
    public void Retry()
    {
        gameManager.ResumeAndRestart();
    }
    
    public void BackToTitle()
    {
        gameManager.ResumeAndChangeToMenu();
    }
    
    public void Show(bool show)
    {
        gameObject.SetActive(show);
        canvasGroup.alpha = show ? 1 : 0;
        
        if (show)
        {
            retryButton.Select();
        }
    }
}
