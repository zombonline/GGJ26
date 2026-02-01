using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UITutorialScreen : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameManager gameManager;
    
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Button startButton;
    
    private static readonly int Showing = Animator.StringToHash("Showing");

    private Coroutine _startGameCoroutine;
    
    private void Awake()
    {
        Show(false);
    }
    
    public void StartGame()
    {
        if (_startGameCoroutine != null)
            return;
        
        _startGameCoroutine = StartCoroutine(StartGameSequence());
    }
    
    private IEnumerator StartGameSequence()
    {
        Show(false);
        yield return new WaitForSecondsRealtime(0.5f);
        
        gameManager.StartLevel();
        _startGameCoroutine = null;
    }
    
    public void Show(bool show)
    {
        animator.SetBool(Showing, show);
        if (show)
        {
            startButton.Select();
        }
    }
}
