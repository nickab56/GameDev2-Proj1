using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverSM : MonoBehaviour
{
    public float delayAmount = 0.75f;
    public TMP_Text PlayerTimeTxt;
    public TMP_Text HighTimeTxt;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTimeTxt.text = "Time   ";
        HighTimeTxt.text = "Best Time   ";
        DisplayTime(PlayerTimeTxt, Constants.C.timeCount);
        DisplayTime(HighTimeTxt, Constants.C.HighTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu()
    {
        StartCoroutine(DelayLoad());
    }

    IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(delayAmount);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScene");
    }

    private void DisplayTime(TMP_Text txt, float time)
    {
        int ms = (int) time * 1000;
        System.TimeSpan ts = System.TimeSpan.FromMilliseconds(ms);
        string rem = ((time % 1) * 100).ToString("##");
        txt.text = txt.text + ts.ToString(@"hh\:mm\:ss") + "." + rem;
    }
}
