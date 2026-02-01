using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] private List<MessageInfo> messageInfos;
    
    private Coroutine _textFadeCoroutine;
    
    private void Start()
    {
        canvasGroup.alpha = 0f;
        textTransform.localScale = Vector3.zero;
        StartCountdown();
    }
    
    private void StartCountdown()
    {
        StartCoroutine(CountdownSequence());
    }
    
    private IEnumerator CountdownSequence()
    {
        float secondPerBeat = 60f / songPlayer.currentChart.bpm;
        for (int i = 0; i < messageInfos.Count - 1; i++)
        {
            ShowText(messageInfos[i], secondPerBeat * 1.5f);
            yield return new WaitForSeconds(secondPerBeat);
        }
        ShowText(messageInfos[^1], secondPerBeat * 1.5f);
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
        StartCoroutine(ShowTextSequence(messageInfo, fullDuration));
    }

    private IEnumerator ShowTextSequence(MessageInfo messageInfo, float fullDuration)
    {
        rimText.text = messageInfo.message;
        rimText.color = messageInfo.color;
        countdownText.text = messageInfo.message;
        countdownText.color = messageInfo.color * 0.25f + Color.black * 0.75f;
        
        canvasGroup.alpha = 1f;
        textTransform.localScale = Vector3.one;
        
        float startTime = Time.time;
        float duration = fullDuration / 2f;
        while (Time.time - startTime > duration)
        {
            canvasGroup.alpha = Sinerp(1f, 0f, (Time.time - startTime) / duration);
            textTransform.localScale = Vector3.one * Sinerp(1f, 0f, (Time.time - startTime) / duration);
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
