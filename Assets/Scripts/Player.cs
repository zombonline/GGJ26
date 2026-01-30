using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private enum InputType
    {
        None,
        Light,
        Heavy,
        Repeating
    }
    
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI attackDebugText;
    
    [Header("Settings")]
    [SerializeField] private float doubleInputInterval;
    [SerializeField] private float repeatingInputInterval;
    [SerializeField] private int repeatingInputMinCount;
    
    private InputType _currentInputType = InputType.None;
    
    private bool[] _inputValue =  new bool[2];
    private float _lastInputTime =  float.NegativeInfinity;
    private int _repeatingInputCount = 0;
    
    private void Update()
    {
        ParseInput();
    }
    
    private void OnInput1(InputValue value)
    {
        CollectInput(0, value.isPressed);
    }

    private void OnInput2(InputValue value)
    {
        CollectInput(1, value.isPressed);
    }
    private void OnPause()
    {
        if (Time.timeScale == 0)
            return;
        
        gameManager.PauseGame();
    }
    
    private void CollectInput(int inputIndex, bool isPressed)
    {
        _inputValue[inputIndex] = isPressed;

        if (isPressed)
        {
            int inputCount = 0;
            for (int i = 0; i < _inputValue.Length; i++)
            {
                inputCount += _inputValue[i] ?  1 : 0;
            }

            if (inputCount == 2 && _lastInputTime > Time.unscaledTime - doubleInputInterval)
            {
                _repeatingInputCount = 0;
                _currentInputType = InputType.Heavy;
            }
            else if (inputCount == 1)
            {
                if (_lastInputTime > Time.unscaledTime - repeatingInputInterval)
                {
                    _repeatingInputCount++;
                    if (_repeatingInputCount >= repeatingInputMinCount)
                    {
                        _currentInputType = InputType.Repeating;
                    }
                }
                else
                {
                    _repeatingInputCount = 0;
                    _currentInputType = InputType.Light;
                }
            }
            
            _lastInputTime = Time.unscaledTime;
        }
    }

    private void ParseInput()
    {
        attackDebugText.text = _currentInputType.ToString();
    }
}
