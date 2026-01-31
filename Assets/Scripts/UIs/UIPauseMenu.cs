using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    
    [Header("Components")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button resumeButton;


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

        if (show)
        {
            resumeButton.Select();
        }
    }
}
