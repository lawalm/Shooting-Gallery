using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the pausing of the game
/// </summary>
public class PauseMenuBehaviour : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pausePanel;
    public string playGame = "Game";
    public string menu = "MainMenu";

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            // set Time.timeScale = 0
            Time.timeScale = 0;
        }
        else
        {
            // set Time.timeScale = 1
            Time.timeScale = 1;
        }
        Debug.Log("timescale = " + Time.timeScale);
    }

    void SetPause()
    {
        isPaused = true;
        
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        HidePausePanel();
    }

    public void ShowPausePanel()
    {
       
        iTween.MoveBy(pausePanel, iTween.Hash("y", 10,
                                               "easeType", "Spring",
                                                "time", 0.7,
                                                 "oncomplete", "SetPause",
                                                 "ignoretimescale", true
                                               ));
        isPaused = true;
    }

    public void HidePausePanel()
    {
        iTween.MoveBy(pausePanel, iTween.Hash("y", -10,
                                                "time", 0.7
                                               ));
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(playGame);
       
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(menu);
    }
}
