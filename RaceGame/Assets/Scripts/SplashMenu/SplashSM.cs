using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSM : MonoBehaviour
{
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
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
