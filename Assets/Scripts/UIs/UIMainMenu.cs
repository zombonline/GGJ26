using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Animator animator;
    
    [Header("Components (Main Page)")]
    [SerializeField] private GameObject mainPage;
    [SerializeField] private Selectable mainPageFirstSelectable;
    
    [Header("Components (Settings Page)")]
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private Selectable settingsPageFirstSelectable;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    
    [Header("Components (Credits Page)")]
    [SerializeField] private GameObject creditsPage;
    [SerializeField] private Selectable creditsPageFirstSelectable;
    
    private static readonly int PageAnimationKey = Animator.StringToHash("Page");

    private Coroutine _changePageCoroutine;
    
    // ======== General ========

    private void ChangePage(int page, Action onChange)
    {
        if (_changePageCoroutine != null)
            return;
        
        animator.SetInteger(PageAnimationKey, page);
        _changePageCoroutine = StartCoroutine(ChangePageSequence(onChange));
    }

    private IEnumerator ChangePageSequence(Action onChange)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        onChange.Invoke();
        yield return new WaitForSecondsRealtime(0.25f);
        _changePageCoroutine = null;
    }
    
    // ======== Main Page ========
    
    public void ShowMainPage()
    {
        if (_changePageCoroutine != null)
            return;
        
        ChangePage(0, () =>
        {
            mainPageFirstSelectable.Select();
        });
    }
    
    public void StartGame()
    {
        if (_changePageCoroutine != null)
            return;
        
        SceneLoader.Instance.ChangeToLevelScene();
    }
    
    // ======== Settings Page ========

    public void ShowSettingsPage()
    {
        if (_changePageCoroutine != null)
            return;

        ChangePage(1, () =>
        {
            settingsPageFirstSelectable.Select();
        });
    }
    
    public void ChangeSoundVolume(float volume)
    {
        if (_changePageCoroutine != null)
            return;
        
        audioMixer.SetFloat("SoundVolume", LinearToDecibel((int)volume / 4f));
    }

    public void ChangeMusicVolume(float volume)
    {
        if (_changePageCoroutine != null)
            return;
        
        audioMixer.SetFloat("MusicVolume", LinearToDecibel(volume / 4f));
    }
    
    private static float LinearToDecibel(float linear)
    {
        return linear != 0 ? 20.0f * Mathf.Log10(linear) : -144.0f;
    }
    
    // ======== Credits Page ========

    public void ShowCreditsPage()
    {
        if (_changePageCoroutine != null)
            return;
        
        ChangePage(2, () =>
        {
            creditsPageFirstSelectable.Select();
        });
    }
}
