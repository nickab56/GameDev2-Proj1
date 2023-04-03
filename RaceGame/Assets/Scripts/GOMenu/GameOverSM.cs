using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSM : MonoBehaviour
{
    public float delayAmount = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
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
