using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public float delayAmount = 1f;

    public AudioSource teleportation;
    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Todo: Update player position
        UpdatePosition();
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

    public void Teleport()
    {
        teleportation.Play();
        image.SetActive(true);
        
    }

    private void UpdatePosition()
    {
        // FORMAT:
        // Format = (Final time (in s) | Max time (1 hr in s) + ((1 | 0) * 100) - currentWaypoint - (distance/1000)
        // If not complete, set to max time in seconds, 1 * 100 if the racer still has a lap to go (0 means no), current waypoint, distance from next waypoint
        SortedList<float, GameObject> positions = new();

        // Go through all racers
        GameObject[] racers = GameObject.FindGameObjectsWithTag("Enemy").Concat(GameObject.FindGameObjectsWithTag("Player")).ToArray();
        foreach (GameObject racer in racers)
        {
            float n = 0;
            n = racer.CompareTag("Player") ? racer.GetComponent<PlayerController>().final : racer.GetComponent<EnemyController>().final;

            // Check if player/enemy has finished
            if (n < 7200)
            {
                // Player has finished, push result to positions
                positions.Add(n, racer);
                continue;
            }
            
            // Check if player has finished lap 1
            bool finishedLap1 = racer.CompareTag("Player") ? racer.GetComponent<PlayerController>().lap1 != 0 : racer.GetComponent<EnemyController>().lap1 != 0;
            if (!finishedLap1)
            {
                n += 100;
            }

            // Get current waypoint and subtract it from n
            n -= racer.GetComponent<WayPoints>().currentWaypoint;

            // Get distance from next waypoint and subtract it from n
            n -= (racer.GetComponent<WayPoints>().distanceFromWaypoint / 1000);

            positions.Add(n, racer);
        }

        // Update positions using sorted dictionary
        foreach (var position in positions.OrderBy(p => p.Key))
        {
            // work with pair.Key and pair.Value
            GameObject gameObject = position.Value;
            Debug.Log("Racer: " + gameObject.name + " Position: " + position.Key);
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<PlayerController>().position = positions.IndexOfKey(position.Key) + 1;
            } else
            {
                gameObject.GetComponent<EnemyController>().position = positions.IndexOfKey(position.Key) + 1;
            }
        }
    }
}
