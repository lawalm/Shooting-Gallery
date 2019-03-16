using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public static GameOverPanel goPanelInstance;

    public void Awake()
    {
        goPanelInstance = this;
    }

    public void ShowGameOverPanel()
    {
        iTween.MoveBy(gameObject, iTween.Hash("y", 581,
                                              "easeType", "Spring",
                                               "time", 0.7
                                              ));
    }

    public void HideGameOverPanel()
    {
        iTween.MoveBy(gameObject, iTween.Hash("y", -10,
                                               "time", 0.7
                                              ));
    }
}
