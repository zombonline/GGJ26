using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Mask mask;
    [SerializeField] private TextMeshProUGUI debugInputText;

   public event Action<BeatHitType> onActionPerformed;
    
    [Header("Settings")]
    [SerializeField] private float heavyAttackInputInterval;

    private bool _jumpInput;
    private bool _attackInput;
    private float _lastInputTime;
    
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
    
    public int MaxCombo { get; private set; }
    
    // ======== Unity Messages ========
    
    private void Update()
    {
        // debug
        FadeDebugText();
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            IncreaseCombo();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ResetCombo();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mask.AddEnergy(50f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            mask.CollectMask(Mask.MaskType.Special);
        }
    }
    
    // ======== Input Messages ========
    
    private void OnJump(InputValue value)
    {
        _jumpInput = value.isPressed;
        _lastInputTime = Time.unscaledTime;
        
        if (Time.timeScale == 0)
            return;

        if (value.isPressed)
        {
            if (_attackInput && _lastInputTime > Time.unscaledTime - heavyAttackInputInterval)
            {
                PerformHeavyAttack();
            }
            else
            {
                PerformJump();
            }
        }
    }

    private void OnAttack(InputValue value)
    {
        _attackInput = value.isPressed;
        _lastInputTime = Time.unscaledTime;
        
        if (Time.timeScale == 0)
            return;
        
        if (value.isPressed)
        {
            if (_jumpInput && _lastInputTime > Time.unscaledTime - heavyAttackInputInterval)
            {
                PerformHeavyAttack();
            }
            else
            {
                PerformLightAttack();
            }
        }
    }
    
    private void OnPause()
    {
        if (Time.timeScale == 0)
            return;
        
        gameManager.PauseGame();
    }
    
    // ======== Attacks ========

    private void PerformLightAttack()
    {
        debugInputText.text = "Light Attack";
        debugInputText.color = Color.white;
        onActionPerformed?.Invoke(BeatHitType.Attack);
    }

    private void PerformHeavyAttack()
    {
        debugInputText.text = "Heavy Attack";
        debugInputText.color = Color.white;
        onActionPerformed?.Invoke(BeatHitType.HeavyAttack);
    }

    private void PerformJump()
    {
        debugInputText.text = "Jump";
        debugInputText.color = Color.white;
        onActionPerformed?.Invoke(BeatHitType.Jump);
    }

    private void FadeDebugText()
    {
        Color c = debugInputText.color;
        c.a = Mathf.Max(0, c.a - 2f * Time.unscaledDeltaTime);
        debugInputText.color = c;
    }
    
    // ======== Combo ========

    private void IncreaseCombo()
    {
        Combo++;
        MaxCombo = Mathf.Max(Combo, MaxCombo);
    }

    private void ResetCombo()
    {
        Combo = 0;
    }
}
