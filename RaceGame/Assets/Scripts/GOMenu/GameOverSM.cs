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
        PlayerTimeTxt.text = "Time   " + Constants.C.timeCount.ToString("0.00");
        HighTimeTxt.text = "Best Time   " + Constants.C.HighTime.ToString("0.00");
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
}
