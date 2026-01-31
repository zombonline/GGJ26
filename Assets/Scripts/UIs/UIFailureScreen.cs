using UnityEngine;
using UnityEngine.UI;

public class UIFailureScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Button retryButton;
    
    private static readonly int Showing = Animator.StringToHash("Showing");
    
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
        animator.SetBool(Showing, show);
        if (show)
        {
            retryButton.Select();
        }
    }
}
