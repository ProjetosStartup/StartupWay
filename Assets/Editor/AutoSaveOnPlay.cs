using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class AutoSaveOnPlay : MonoBehaviour
{
    static AutoSaveOnPlay()
    {
        EditorApplication.playModeStateChanged += SaveOnPlayModeChanged;
    }

    private static void SaveOnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            Debug.Log("Salvando modificações antes de iniciar o jogo...");
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
        }
    }
}
