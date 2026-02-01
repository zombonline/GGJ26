using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISuccessScreen : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Player player;
    [SerializeField] private SongPlayer songPlayer;
    
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Image previousMask;
    [SerializeField] private Image nextMask;
    [SerializeField] private CanvasGroup nextMaskBlinker;
    [SerializeField] private List<TextMeshProUGUI> comboTexts;
    [SerializeField] private Button continueButton;
    [SerializeField] private List<Sprite> maskIcons;
    
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

            previousMask.sprite = maskIcons[Mathf.Clamp(songPlayer.SongIndex - 1, 0, maskIcons.Count - 1)];
            nextMask.sprite = maskIcons[Mathf.Min(songPlayer.SongIndex, 0, maskIcons.Count - 1)];
            
            StartCoroutine(ShowComboAnimation());

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
    
    private IEnumerator ShowComboAnimation()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        float time = Time.unscaledTime;
        float duration = 1f;
        while (Time.unscaledTime - time < duration)
        {
            foreach (TextMeshProUGUI comboText in comboTexts)
            {
                comboText.text = $"{(int)Mathf.Lerp(0, player.MaxCombo, (Time.unscaledTime - time) / duration)}";
            }
            yield return null;
        }

        foreach (TextMeshProUGUI comboText in comboTexts)
        {
            comboText.text = $"{player.MaxCombo}";
        }
    }
}
