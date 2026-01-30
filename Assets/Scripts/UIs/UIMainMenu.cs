using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoader.Instance.ChangeToLevelScene();
    }
}
