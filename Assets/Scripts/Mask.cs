using UnityEngine;

public class Mask : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameManager gameManager;
    
    [Header("Settings (Energy)")]
    [SerializeField] private float maxEnergy; 
    [SerializeField] private float energyDecay;

    [Header("Settings (Failure)")]
    [SerializeField] private float timeTillFailure;
    
    private float _currentEnergy;
    private float _failureTimer;
    
    private void Awake()
    {
        _currentEnergy = maxEnergy;
        _failureTimer = float.PositiveInfinity;
    }

    private void Update()
    {
        DecayEnergy();
        DecreaseFailureTimer();
    }

    private void DecayEnergy()
    {
        if (Time.timeScale == 0f)
            return;
        
        if (!(_currentEnergy > 0))
            return;
        
        _currentEnergy -= energyDecay * Time.deltaTime;
        if (_currentEnergy <= 0f)
        {
            Debug.Log("Energy decayed");
            _failureTimer = timeTillFailure;
            _currentEnergy = 0f;
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
            Debug.Log("Timeout");
            gameManager.FailGame();
        }
    }

    public void AddEnergy(float value)
    {
        maxEnergy += value;
        _failureTimer = float.PositiveInfinity;
    }
}
