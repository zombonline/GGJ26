using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI attackDebugText;
    
    [Header("Settings")]
    [SerializeField] private float doubleInputInterval;
    [SerializeField] private float repeatingInputInterval;
    [SerializeField] private int repeatingInputMinCount;

    public int Combo
    {
        get => _combo;
        set
        {
            if (_combo == value)
                return;
            _combo = value;
            ComboChanged?.Invoke();
        }
    }
    private int _combo = 0;
    public event Action ComboChanged; 
    
    // ======== Unity Messages ========
    
    private void Update()
    {
        // debug
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Combo++;
            ComboChanged?.Invoke();   
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Combo = 0;
            ComboChanged?.Invoke();   
        }
    }
    
    // ======== Input Messages ========
    
    private void OnJump(InputValue value)
    {
        if (Time.timeScale == 0)
            return;
        
        // Perform jump here

    }

    private void OnAttack(InputValue value)
    {
        if (Time.timeScale == 0)
            return;
        
        // Perform attack here

    }
    
    private void OnPause()
    {
        if (Time.timeScale == 0)
            return;
        
        gameManager.PauseGame();
    }
}
