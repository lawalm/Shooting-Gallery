using UnityEngine;
using UnityEngine.SceneManagement;

//Handles the main menu button and controls
public class MenuCtrl : MonoBehaviour
{
   public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
