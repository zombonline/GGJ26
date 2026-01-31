using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIComboDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI rimText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Settings")] 
    [SerializeField] private float decayDuration;
    [SerializeField] private AnimationCurve decayCurve;
    [SerializeField] private float scaleDuration;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float randomAngleFrom;
    [SerializeField] private float randomAngleTo;
    [SerializeField] private List<Color> textColors;

    private int _lastValue = 0;
    private float _lastChangeTime = float.NegativeInfinity;
    
    private void Awake()
    {
        canvasGroup.alpha = 0;
        
        player.ComboChanged += OnComboChanged;
        OnComboChanged();
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.ComboChanged -= OnComboChanged;
        }
    }

    private void Update()
    {
        ResolveAppearance();
    }

    private void OnComboChanged()
    {
        if (player.Combo > _lastValue)
        {
            rimText.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(randomAngleFrom, randomAngleTo));
            comboText.text = player.Combo.ToString();
            rimText.text = player.Combo.ToString();
            Color selectedColor = textColors[Random.Range(0, textColors.Count)];
            comboText.color = selectedColor;
            rimText.color = selectedColor * 0.25f + Color.black * 0.75f;
            _lastChangeTime = Time.time;
        }
        _lastValue = player.Combo;
    }

    private void ResolveAppearance()
    {
        if (Time.time - _lastChangeTime < Mathf.Max(decayDuration, scaleDuration))
        {
            canvasGroup.alpha = decayCurve.Evaluate((Time.time - _lastChangeTime) / decayDuration);
            rimText.transform.localScale = Vector3.one * scaleCurve.Evaluate((Time.time - _lastChangeTime) / scaleDuration);
        }
        else
        {
            canvasGroup.alpha = 0;
            rimText.transform.localScale = Vector3.one;
        }
    }
}
