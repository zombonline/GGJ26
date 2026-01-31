using TMPro;
using UnityEngine;

public class UIComboDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Settings")] 
    [SerializeField] private float decayDuration;
    [SerializeField] private AnimationCurve decayCurve;
    [SerializeField] private float scaleDuration;
    [SerializeField] private AnimationCurve scaleCurve;

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
            text.text = player.Combo.ToString();
            _lastChangeTime = Time.time;
        }
        _lastValue = player.Combo;
    }

    private void ResolveAppearance()
    {
        if (Time.time - _lastChangeTime < Mathf.Max(decayDuration, scaleDuration))
        {
            canvasGroup.alpha = decayCurve.Evaluate((Time.time - _lastChangeTime) / decayDuration);
            text.transform.localScale = Vector3.one * scaleCurve.Evaluate((Time.time - _lastChangeTime) / scaleDuration);
        }
        else
        {
            canvasGroup.alpha = 0;
            text.transform.localScale = Vector3.one;
        }
    }
}
