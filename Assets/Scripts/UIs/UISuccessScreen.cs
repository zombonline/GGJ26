using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UISuccessScreen : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameManager gameManager;
    
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Image previousMask;
    [SerializeField] private Image nextMask;
    [SerializeField] private CanvasGroup nextMaskBlinker;
    [SerializeField] private Button continueButton;
    
    private static readonly int Showing = Animator.StringToHash("Showing");

    private Coroutine _continueCoroutine;
    private Coroutine _blinkMaskCoroutine;
    
    private void Awake()
    {
        Show(false);
    }
    
    public void Continue()
    {
        if (_continueCoroutine != null)
            return;
        
        _continueCoroutine = StartCoroutine(ContinueSequence());
    }
    
    private IEnumerator ContinueSequence()
    {
        Show(false);
        yield return new WaitForSecondsRealtime(0.5f);

        if (_blinkMaskCoroutine != null)
        {
            StopCoroutine(_blinkMaskCoroutine);
            _blinkMaskCoroutine = null;
        }

        gameManager.StartLevel();
        _continueCoroutine = null;
    }
    
    public void Show(bool show)
    {
        animator.SetBool(Showing, show);
        if (show)
        {
            continueButton.Select();

            if (_blinkMaskCoroutine != null)
            {
                StopCoroutine(_blinkMaskCoroutine);
                _blinkMaskCoroutine = null;
            }
            _blinkMaskCoroutine = StartCoroutine(BlinkNextMaskSequence());
        }
    }

    private IEnumerator BlinkNextMaskSequence()
    {
        while (true)
        {
            nextMaskBlinker.alpha = 0f;
            
            float startTime = Time.time;
            float duration = 0.1f;
            while (Time.time - startTime < duration)
            {
                nextMaskBlinker.alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / duration);
                yield return null;
            }
            startTime = Time.time;
            while (Time.time - startTime < duration)
            {
                nextMaskBlinker.alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / duration);
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
