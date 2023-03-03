using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSM : MonoBehaviour
{
    public float delayAmount = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BeginPlay()
    {
        StartCoroutine(DelayLoad());
    }

    IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(delayAmount);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
