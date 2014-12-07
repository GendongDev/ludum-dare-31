using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Animator ContentPanel;

    public Animator GameTitle;
    public Animator StartButton;

    public Animator BackButton;
    public Animator Level1Button;
    public Animator Level2Button;
    public Animator Level3Button;
    public Animator SelectLevelBanner;
    public Animator InstructionsPanel;

    public Animator[] Level1Items;
    public Animator[] Level2Items;
    public Animator[] Level3Items;

    public AudioClip ButtonClick;
    public AudioClip Music;

    LevelManager currentLevel;
    AudioManager am;

    void Start()
    {
        //PlayerPrefs.SetInt("Level1Item1", 0);
        //PlayerPrefs.SetInt("Level1Item2", 0);
        //PlayerPrefs.SetInt("Level1Item3", 0);
        //PlayerPrefs.SetInt("Level1Item4", 0);
        //PlayerPrefs.SetInt("Level1Item5", 0);

        //PlayerPrefs.SetInt("Level2Item1", 0);
        //PlayerPrefs.SetInt("Level2Item2", 0);
        //PlayerPrefs.SetInt("Level2Item3", 0);
        //PlayerPrefs.SetInt("Level2Item4", 0);
        //PlayerPrefs.SetInt("Level2Item5", 0);

        //PlayerPrefs.SetInt("Level3Item1", 0);
        //PlayerPrefs.SetInt("Level3Item2", 0);
        //PlayerPrefs.SetInt("Level3Item3", 0);
        //PlayerPrefs.SetInt("Level3Item4", 0);
        //PlayerPrefs.SetInt("Level3Item5", 0);

        am = GetComponent<AudioManager>();

        am.PlayMusic(Music);
    }

    public void OpenLevelSelect()
    {
        StartCoroutine(Blah());
    }

    public void CloseLevelSelect()
    {
        StartCoroutine(UnBlah());
    }

    IEnumerator Blah()
    {
        am.Play(ButtonClick);
        GameTitle.SetBool("isHidden", true);
        StartButton.SetBool("isHidden", true);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(UnhideLevelSelectWidgets());
    }

    IEnumerator UnBlah()
    {
        am.Play(ButtonClick);
        yield return StartCoroutine(HideLevelSelectWidgets());

        GameTitle.SetBool("isHidden", false);
        StartButton.SetBool("isHidden", false);
    }

    IEnumerator HideLevelSelectWidgets()
    {
        BackButton.SetBool("isHidden", true);
        Level1Button.SetBool("isHidden", true);
        Level2Button.SetBool("isHidden", true);
        Level3Button.SetBool("isHidden", true);
        SelectLevelBanner.SetBool("isHidden", true);
        InstructionsPanel.SetBool("isHidden", true);
        yield return new WaitForSeconds(0.75f);
    }

    IEnumerator UnhideLevelSelectWidgets()
    {
        SelectLevelBanner.SetBool("isHidden", false);
        Level1Button.SetBool("isHidden", false);
        yield return new WaitForSeconds(0.2f);
        Level2Button.SetBool("isHidden", false);
        yield return new WaitForSeconds(0.2f);
        Level3Button.SetBool("isHidden", false);
        yield return new WaitForSeconds(0.2f);
        BackButton.SetBool("isHidden", false);
        yield return new WaitForSeconds(0.2f);
        InstructionsPanel.SetBool("isHidden", false);

        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < Level1Items.Length; ++i)
        {
            int x = PlayerPrefs.GetInt("Level1Item" + (i + 1), 0);
            if (x > 0)
            {
                Level1Items[i].SetBool("isFilled", x > 0);
                yield return new WaitForSeconds(0.1f);
            }
        }
        for (int i = 0; i < Level2Items.Length; ++i)
        {
            int x = PlayerPrefs.GetInt("Level2Item" + (i + 1), 0);
            if (x > 0)
            {
                Level2Items[i].SetBool("isFilled", x > 0);
                yield return new WaitForSeconds(0.1f);
            }
        }
        for (int i = 0; i < Level3Items.Length; ++i)
        {
            int x = PlayerPrefs.GetInt("Level3Item" + (i + 1), 0);
            if (x > 0)
            {
                Level3Items[i].SetBool("isFilled", x > 0);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void LoadLevel(string name)
    {
        StartCoroutine(LoadLevelProcess(name));
    }

    IEnumerator LoadLevelProcess(string name)
    {
        am.Play(ButtonClick);

        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
        }

        yield return new WaitForEndOfFrame();

        Application.LoadLevelAdditive(name);

        yield return new WaitForEndOfFrame();

        currentLevel = FindObjectOfType<LevelManager>();
        currentLevel.gameObject.transform.parent = transform;

        yield return StartCoroutine(HideLevelSelectWidgets());
        yield return new WaitForSeconds(0.25f);

        InstructionsPanel.gameObject.SetActive(false);

        ContentPanel.SetBool("isHidden", true);
    }

    void LevelEnded()
    {
        StartCoroutine(LevelEndedProcess());
    }

    IEnumerator LevelEndedProcess()
    {
        am.PlayMusic(Music);
        ContentPanel.SetBool("isHidden", false);
        yield return new WaitForSeconds(0.5f);

        Destroy(currentLevel.gameObject);
        currentLevel = null;

        InstructionsPanel.gameObject.SetActive(true);

        yield return StartCoroutine(UnhideLevelSelectWidgets());
    }
}
