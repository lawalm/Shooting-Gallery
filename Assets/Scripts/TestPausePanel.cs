using UnityEngine;
/// <summary>
/// Using itween to show panel
/// </summary>
public class TestPausePanel : MonoBehaviour
{
  
    public void ShowPausePanel()
    {
        iTween.MoveBy(gameObject, iTween.Hash("y", 10,
                                               "easeType", "Spring",
                                                "time", 0.7
                                               ));
    }

    public void HidePausePanel()
    {
        iTween.MoveBy(gameObject, iTween.Hash("y", 10,
                                               "easeType", "Spring",
                                                "time", 0.7
                                               ));
    }
}
