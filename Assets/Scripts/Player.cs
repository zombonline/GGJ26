using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private void OnInput1()
    {
        Debug.Log("OnInput1");
    }

    private void OnInput2()
    {
        Debug.Log("OnInput2");
    }

    private void OnPause()
    {
        gameManager.PauseGame();
    }
}
