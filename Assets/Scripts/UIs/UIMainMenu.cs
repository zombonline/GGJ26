using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
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

    // ======== General ========

    private void HideAllPages()
    {
        mainPage.SetActive(false);
        settingsPage.SetActive(false);
        creditsPage.SetActive(false);
    }
    
    // ======== Main Page ========
    
    public void ShowMainPage()
    {
        HideAllPages();
        mainPage.SetActive(true);
        mainPageFirstSelectable.Select();
    }
    
    public void StartGame()
    {
        SceneLoader.Instance.ChangeToLevelScene();
    }
    
    // ======== Settings Page ========

    public void ShowSettingsPage()
    {
        HideAllPages();
        settingsPage.SetActive(true);
        settingsPageFirstSelectable.Select();
    }
    
    public void ChangeSoundVolume(float volume)
    {
        
    }

    public void ChangeMusicVolume(float volume)
    {
        
    }
    
    // ======== Settings Page ========

    public void ShowCreditsPage()
    {
        HideAllPages();
        creditsPage.SetActive(true);
        creditsPageFirstSelectable.Select();
    }
}
