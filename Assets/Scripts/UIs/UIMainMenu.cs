using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Components (Main Page)")]
    [SerializeField] private GameObject mainPage;
    [SerializeField] private Button startButton;
    
    [Header("Components (Settings Page)")]
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    // ======== Main Page ========
    
    public void ShowMainPage()
    {
        settingsPage.SetActive(false);
        mainPage.SetActive(true);
        
        startButton.Select();
    }
    
    public void StartGame()
    {
        SceneLoader.Instance.ChangeToLevelScene();
    }
    
    // ======== Settings Page ========

    public void ShowSettingsPage()
    {
        mainPage.SetActive(false);
        settingsPage.SetActive(true);
        
        soundSlider.Select();
    }
    
    public void ChangeSoundVolume(float volume)
    {
        
    }

    public void ChangeMusicVolume(float volume)
    {
        
    }
}
