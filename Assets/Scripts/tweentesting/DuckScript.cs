using UnityEngine;

public class DuckScript : MonoBehaviour
{
    public void Start()
    {
         iTween.ShakePosition(gameObject, iTween.Hash("y", 0.3f,
                                                     "time", 0.8f,
                                                     "delay", 2.0f,
                                                      "looptype", "loop"
                                                      ));
            
                                                       
    }
    
}
