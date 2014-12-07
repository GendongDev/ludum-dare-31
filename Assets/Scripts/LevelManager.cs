using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public Text Clock;
    public int Seconds = 300;
    public Animator GameOver;
    public Animator CourseComplete;
    public AudioClip Music;
    public AudioClip GameOverClip;
    public AudioClip CourseCompleteClip;

    int secondsRemaining;
    AudioManager am;

    void Awake()
    {
        var g = FindObjectOfType<GameManager>();
        if (g == null)
        {
            Application.LoadLevel("Entry");
        }
    }

    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        am.PlayMusic(Music);

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        secondsRemaining = Seconds;
        while (secondsRemaining >= 0)
        {
            Clock.text = string.Format("{0:D3}", secondsRemaining);
            yield return new WaitForSeconds(1);
            --secondsRemaining;
        }
        StartCoroutine(GameOverProcess());
    }

    IEnumerator GameOverProcess()
    {
        BroadcastMessage("Freeze");
        GameOver.SetBool("isHidden", false);
        am.Play(GameOverClip);
        yield return new WaitForSeconds(4.0f);
        SendMessageUpwards("LevelEnded");
    }

    void HeroReachedGoal()
    {
        StartCoroutine(CourseCompleteProcess());
    }

    IEnumerator CourseCompleteProcess()
    {
        BroadcastMessage("Freeze");
        CourseComplete.SetBool("isHidden", false);
        am.Play(CourseCompleteClip);
        yield return new WaitForSeconds(4.0f);
        SendMessageUpwards("LevelEnded");
    }

    void HeroDied()
    {
        StartCoroutine(GameOverProcess());
    }

}
