using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public float delayAmount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Todo: Update player position
        //UpdatePosition();
    }

    public void PlayAgain()
    {
        StartCoroutine(LoadSplash());
    }

    IEnumerator LoadSplash()
    {
        yield return new WaitForSeconds(delayAmount);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScene");
    }

    private void UpdatePosition()
    {
        // FORMAT:
        // Format = (Final time (in s) | Max time (1 hr in s) + ((1 | 0) * 100) - currentWaypoint - (distance/1000)
        // Still has a lap to go (0 means no), current waypoint, distance from next waypoint
        SortedDictionary<float, GameObject> positions = new SortedDictionary<float, GameObject>();

        // Go through all racers
        GameObject[] racers = GameObject.FindGameObjectsWithTag("Enemy").Concat(GameObject.FindGameObjectsWithTag("Player")).ToArray();
        foreach (GameObject racer in racers)
        {
            // Check if player/enemy has finished lap 1
            int hasFinishedFirstLap = 1;
            // Get index of current waypoint
            int currentWaypoint = 1;
            // Calculate distance from next waypoint
            int d = 2;
            float n = (hasFinishedFirstLap * 100) + currentWaypoint + (d / 1000); 
            positions.Add(n, racer);
        }

        // Update positions using sorted dictionary
    }
}
