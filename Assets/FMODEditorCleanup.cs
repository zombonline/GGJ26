using UnityEngine;
using FMODUnity;

[ExecuteAlways]
public class FMODEditorCleanup : MonoBehaviour
{
    private void OnDisable()
    {
        if (!Application.isPlaying) return;
        RuntimeManager.CoreSystem.release(); // release FMOD system
    }
}
