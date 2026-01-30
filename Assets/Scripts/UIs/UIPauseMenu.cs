using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        Show(false);
    }
    
    public void Resume()
    {
        gameManager.ResumeGame();
    }

    public void BackToTitle()
    {
        gameManager.ResumeAndChangeToMenu();
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
        canvasGroup.alpha = show ? 1 : 0;
    }
}
