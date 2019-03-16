using System.Collections; //IEnumerator
using System.Collections.Generic; //List
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private int score;
    public float timeLeft = 20;
    private IEnumerator coroutine;
    [Header("UI Elements")]
    public Text timerTxt;
    public Text scoreTxt;
    public Text highScoreTxt;
    public Text displayHighScore;
    public GameObject gameOverPanel;
    bool isPaused;
    


    [HideInInspector] public List<TargetBehaviour> targets = new List<TargetBehaviour>();

    private void Awake()
    {
        instance = this;
        timeLeft = 20;
        timerTxt.text = timeLeft.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        isPaused = false;
        coroutine = SpawnTargets();
        StartCoroutine(coroutine);
        //StartTimer();
        Invoke("StartTimer", 2);
       

        highScoreTxt.text = "High Score: " + PlayerPrefs.GetInt("highScore").ToString();
    }

    public void StartTimer()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
                      "from", timeLeft,
                      "to", 0,
                      "time", timeLeft,
                      "onupdatetarget", gameObject,
                      "onupdate", "tweenUpdate",
                      "oncomplete", "GameComplete"
                      ));
    }

    public void IncreaseScore()
    {
        score++;
        scoreTxt.text = "Score: " + score.ToString();

        if(score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", score);
            highScoreTxt.text = "High Score: " + score.ToString();
        }
    }

    void GameComplete()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
       
        timerTxt.color = Color.black;
        GameOverPanel.goPanelInstance.ShowGameOverPanel();
        displayHighScore.text = PlayerPrefs.GetInt("highScore").ToString();
    }

    // Update is called once per frame
    void tweenUpdate(float newValue)
    {
        timeLeft = newValue; 

        if(timeLeft > 10)
        {
            timerTxt.text = timeLeft.ToString("#");
        }
        else
        {
            timerTxt.color = Color.red;
            timerTxt.text = timeLeft.ToString("#.0");
        }
    }

    void SpawnTarget()
    {
        int index = Random.Range(0, targets.Count);
        TargetBehaviour target = targets[index];

        target.ShowTarget();
       
    }

    IEnumerator SpawnTargets()
    {
        yield return new WaitForSeconds(1.0f);

        while(timeLeft > 0)
        {
            int numOfTargets = Random.Range(1, 4);

            for(int i = 0; i < numOfTargets; i++)
            {
                SpawnTarget();
            }
            yield return new WaitForSeconds(Random.Range(0.5f * numOfTargets, 2.5f));
        }
    }
}
