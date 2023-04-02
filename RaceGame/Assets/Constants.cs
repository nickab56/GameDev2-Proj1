using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public float timeCount = 0;
    public float HighTime;
    public bool RaceFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public Constants C;
    private void Awake()
    {
        C = this; // C now points to Constants for entire
                  // time the game runs
    }
}
