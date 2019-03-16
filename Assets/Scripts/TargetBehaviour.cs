using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the target bahaviour
/// </summary>
public class TargetBehaviour : MonoBehaviour
{
    [SerializeField]private bool beenHit = false;
    private Animator anim;
    private bool activated;
    private Vector3 originalPos;
    private AudioSource audioSource;

    [Header("Movement")]
    [Tooltip("Speed on the x-axis")]public float moveSpeed = 1f;
    [Tooltip("Speed of sine movement")] public float frequency = 5f;
    [Tooltip("Size of sine movement")] public float magnitude = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //ShowTarget();
        originalPos = gameObject.transform.position;
        GameController.instance.targets.Add(this);
    }

    /// <summary>
    /// Called whenever the player clicks on the target with a collider
    /// </summary>
    private void OnMouseDown()
    {
        if(!beenHit && activated)
        {
            GameController.instance.IncreaseScore();
            beenHit = true;
            anim.Play("FlipTarget");
            audioSource.PlayOneShot(audioSource.clip);
            StopAllCoroutines();
            StartCoroutine(HideTarget());
        }
    }

    public void ShowTarget()
    {
        if(!activated)
        {
            activated = true;
            beenHit = false;
            anim.Play("Idle");

            iTween.MoveBy(gameObject, iTween.Hash("y", 1.4,
                                             "easeType", "easeInOutExpo",
                                             "time", 0.5,
                                             "oncomplete", "OnShown",
                                             "oncompletetarget", gameObject));
        }
    }

    void OnShown()
    {
        StartCoroutine("MoveTarget");
    }

    void OnHidden()
    {
        gameObject.transform.position = originalPos;
        activated = false;
    }

    public IEnumerator HideTarget()
    {
        yield return new WaitForSeconds(.5f);
        // Move down to the original spot
        iTween.MoveBy(gameObject, iTween.Hash(
        "y", (originalPos.y - gameObject.transform.position.y),
        "easeType", "easeOutQuad",
        "loopType", "none", 
        "time", 0.5, 
        "oncomplete", "OnHidden",
        "oncompletetarget", gameObject));
    }

    IEnumerator MoveTarget()
    {
        var relativeEndPos = gameObject.transform.position;

        //are we facing right or left
        if (transform.eulerAngles == Vector3.zero)
        {
            relativeEndPos.x = 8;
        }
        else
        {
            relativeEndPos.x = -8;
        }

        var movementTime = Vector3.Distance(gameObject.transform.position, relativeEndPos) * moveSpeed;

        var pos = gameObject.transform.position;
        var time = 0f;

        while (time < movementTime)
        {
            time += Time.deltaTime;

            pos += transform.right * Time.deltaTime * moveSpeed;
            gameObject.transform.position = pos + (transform.up * Mathf.Sin(Time.time * frequency) * magnitude);

            yield return new WaitForSeconds(0);
        }

        StartCoroutine(HideTarget());
    }  
}

