using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICompleteScreen : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Player player;
    
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private List<TextMeshProUGUI> comboTexts;
    [SerializeField] private Button retryButton;
    
    private static readonly int Showing = Animator.StringToHash("Showing");
    
    private void Awake()
    {
        Show(false);
    }
    
    public void Show(bool show)
    {
        animator.SetBool(Showing, show);
        if (show)
        {
            retryButton.Select();
            StartCoroutine(ShowComboAnimation());
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
    
    public void Retry()
    {
        gameManager.Restart();
    }

    public void BackToTitle()
    {
        gameManager.ChangeToMenu();
    }
}
