using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public enum MaskType
    {
        Normal,
        Special
    }

    [System.Serializable]
    public class MaskInfo
    {
        public MaskType type;
        public float totalEnergy;
        public float energyDecay;
    }
    
    [Header("References")] 
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI debugMaskInfoText;

    
    [Header("Settings (Energy)")]
    [SerializeField] private List<MaskInfo> maskInfos;

    [Header("Settings (Failure)")]
    [SerializeField] private float timeTillFailure;
    
    private readonly Dictionary<MaskType, MaskInfo> _maskInfoLookup =  new Dictionary<MaskType, MaskInfo>();

    private MaskType _currentMaskType;
    private float _currentEnergy;
    private float _failureTimer;
    
    private MaskInfo _currentMaskInfo => _maskInfoLookup[_currentMaskType];
    
    private void Awake()
    {
        ConstructMaskInfoLookup();
        
        _currentMaskType =  MaskType.Normal;
        _currentEnergy = _maskInfoLookup[MaskType.Normal].totalEnergy;
        _failureTimer = float.PositiveInfinity;
    }

    private void Update()
    {
        DecayEnergy();
        DecreaseFailureTimer();
        
        // debug
        if (float.IsPositiveInfinity(_failureTimer))
        {
            debugMaskInfoText.text = $"Mask energy: {_currentEnergy:F0}/{_currentMaskInfo.totalEnergy:F0}";
        }
        else
        {
            debugMaskInfoText.text = $"Time till failure: {(int)_failureTimer:D2}:{(int)(_failureTimer * 100 % 100):D2}";
        }
    }

    private void ConstructMaskInfoLookup()
    {
        foreach (MaskInfo maskInfo in maskInfos)
        {
            _maskInfoLookup[maskInfo.type] = maskInfo;
        }
    }

    private void DecayEnergy()
    {
        if (Time.timeScale == 0f)
            return;
        
        if (!(_currentEnergy > 0))
            return;
        
        _currentEnergy -= _currentMaskInfo.energyDecay * Time.deltaTime;
        if (_currentEnergy <= 0f)
        {
            if (_currentMaskType == MaskType.Normal)
            {
                _failureTimer = timeTillFailure;
                _currentEnergy = 0f;
            }
            else
            {
                _currentMaskType = MaskType.Normal;
                _currentEnergy = _currentMaskInfo.totalEnergy;
            }
        }
    }

    private void DecreaseFailureTimer()
    {
        if (Time.timeScale == 0f)
            return;
        
        if (float.IsPositiveInfinity(_failureTimer))
            return;
        
        _failureTimer -= Time.deltaTime;
        if (_failureTimer <= 0f)
        {
            gameManager.FailGame();
        }
    }

    public void AddEnergy(float value)
    {
        _currentEnergy = Mathf.Min(_currentEnergy + value, _currentMaskInfo.totalEnergy);
        _failureTimer = float.PositiveInfinity;
    }

    public void CollectMask(MaskType maskType)
    {
        _currentMaskType = maskType;
        _currentEnergy = _currentMaskInfo.totalEnergy;
    }
}
