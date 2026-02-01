using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICountdownDisplay : MonoBehaviour
{
    [System.Serializable]
    public struct MessageInfo
    {
        public string message;
        public Color color;
    }

    [Header("References")] 
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SongPlayer songPlayer;

    [Header("Components")] 
    [SerializeField] private TextMeshProUGUI rimText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform textTransform;
    
    [Header("Settings")]
    [SerializeField] private float delayBeforeCountdown;
    [SerializeField] private List<MessageInfo> messageInfos;
    
    private Coroutine _textFadeCoroutine;
    
    private void Start()
    {
        canvasGroup.alpha = 0f;
        textTransform.localScale = Vector3.zero;
    }
    
    public void StartCountdown()
    {
        StartCoroutine(CountdownSequence());
    }
    
    private IEnumerator CountdownSequence()
    {
        yield return new WaitForSecondsRealtime(delayBeforeCountdown);

        float secondPerBeat = 60f / songPlayer.currentChart.bpm;
        for (int i = 0; i < messageInfos.Count - 1; i++)
        {
            ShowText(messageInfos[i], secondPerBeat * 2f);
            yield return new WaitForSecondsRealtime(secondPerBeat);
        }
        ShowText(messageInfos[^1], 1.5f);
        gameManager.CompleteCountdown();
        yield return null;
    }

    private void ShowText(MessageInfo messageInfo, float fullDuration)
    {
        if (_textFadeCoroutine != null)
        {
            StopCoroutine(_textFadeCoroutine);
            _textFadeCoroutine = null;
        }
        _textFadeCoroutine = StartCoroutine(ShowTextSequence(messageInfo, fullDuration));
    }

    private IEnumerator ShowTextSequence(MessageInfo messageInfo, float fullDuration)
    {
        rimText.text = messageInfo.message;
        countdownText.text = messageInfo.message;
        rimText.color = messageInfo.color * 0.25f + Color.black * 0.75f;
        countdownText.color = messageInfo.color;
        
        canvasGroup.alpha = 1f;
        textTransform.localScale = Vector3.one;
        
        yield return new WaitForSecondsRealtime(fullDuration * 0.25f);
        
        float startTime = Time.unscaledTime;
        float duration = fullDuration * 0.75f;
        while (Time.unscaledTime - startTime < duration)
        {
            canvasGroup.alpha = Sinerp(1f, 0f, (Time.unscaledTime - startTime) / duration);
            textTransform.localScale = Vector3.one * Sinerp(1f, 0f, (Time.unscaledTime - startTime) / duration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        textTransform.localScale = Vector3.zero;
        _textFadeCoroutine = null;
    }
    
    private static float Sinerp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
    }
}
