using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;

    [Header("Components")] 
    [SerializeField] private Animator animator;
    [SerializeField] private Button resumeButton;
    
    private static readonly int Showing = Animator.StringToHash("Showing");

    private Coroutine _resumeCoroutine;
    
    private void Awake()
    {
        Show(false);
    }

    public void Resume()
    {
        if (_resumeCoroutine != null)
            return;
        
        _resumeCoroutine = StartCoroutine(ResumeSequence());
    }

    private IEnumerator ResumeSequence()
    {
        Show(false);
        yield return new WaitForSecondsRealtime(0.5f);
        gameManager.ResumeGame();

        _resumeCoroutine = null;
    }

    public void BackToTitle()
    {
        if (_resumeCoroutine != null)
            return;
        
        gameManager.ResumeAndChangeToMenu();
    }

    public void Show(bool show)
    {
        animator.SetBool(Showing, show);
        if (show)
        {
            resumeButton.Select();
        }
    }
}
