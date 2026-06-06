using UnityEngine;

public class QuitButton : MonoBehaviour
{
    void OnMouseDown()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            //Application.Quit();
            #endif
        }
}