using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public float delayAmount = 1f;
    public float stopwatch;
    public int countdown = 3;

    public TMP_Text countdownTxt;
    public TMP_Text positionTxt;
    public TMP_Text lapTxt;
    public TMP_Text stopwatchTxt;

    public WayPointManager wayPointManager;

    public AudioSource teleportation;
    public GameObject image;

    float pastTime = 9999;
    public string playerPrefHighScoreKey = "playerBestTime";

    private const string highScoreKey = "best_time";

    private bool racingEnabled;

    // Start is called before the first frame update
    void Start()
    {
        // Disable Text
        positionTxt.enabled = false;
        stopwatchTxt.enabled = false;
        lapTxt.enabled = false;

        stopwatch = 0;
        racingEnabled = true;
        ToggleRacingScripts();
        StartCoroutine(RaceCountdown());

        if (PlayerPrefs.HasKey(highScoreKey))   // key exists
        {
            pastTime = PlayerPrefs.GetFloat(highScoreKey);
        }
        else
        {
            PlayerPrefs.SetFloat(highScoreKey, stopwatch);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Updates player position
        if (countdown <= 0)
        {
            stopwatch += Time.deltaTime;
            DisplayTime();
            UpdatePosition();
        }
    }

    public void PlayAgain()
    {
        StartCoroutine(LoadSplash());
    }

    public void GameOver()
    {
        StartCoroutine(LoadGameOver());
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
        SortedList<double, GameObject> positions = new();

        // Go through all racers
        GameObject[] racers = GetAllRacers();
        foreach (GameObject racer in racers)
        {
            double n = 0;
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
            if (racer.GetComponent<WayPoints>().currentWaypoint == 0 && racer.GetComponent<WayPoints>().wayPointsCrossed > 0)
            {
                // This means that the current waypoint is not actually 0 but the length of waypoints
                n -= wayPointManager.GetLength(racer.GetComponent<WayPoints>());
            } else
            {
                n -= racer.GetComponent<WayPoints>().currentWaypoint;
            }

            // Get distance from next waypoint and subtract it from n
            n -= (racer.GetComponent<WayPoints>().distanceFromWaypoint / 10000);

            if (!positions.ContainsKey(n))
            {
                positions.Add(n, racer);
            }
        }

        // Update positions using sorted dictionary
        foreach (var position in positions.OrderBy(p => p.Key))
        {
            GameObject gameObject = position.Value;
            if (gameObject.CompareTag("Player"))
            {
                int newPosition = positions.IndexOfKey(position.Key) + 1;
                gameObject.GetComponent<PlayerController>().position = newPosition;
                positionTxt.text = newPosition + " / " + racers.Length;
            } else
            {
                gameObject.GetComponent<EnemyController>().position = positions.IndexOfKey(position.Key) + 1;
            }
        }
    }

    private GameObject[] GetAllRacers()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Concat(GameObject.FindGameObjectsWithTag("Player")).ToArray();
    }
    
    private void ToggleRacingScripts()
    {
        GameObject[] racers = GetAllRacers();
        if (racingEnabled)
        {
            foreach (GameObject racer in racers)
            {
                // Check if player or enemy
                if (racer.CompareTag("Player"))
                {
                    racer.GetComponent<PlayerController>().enabled = false;
                } else
                {
                    racer.GetComponent<EnemyController>().enabled = false;
                }
            }
            racingEnabled = false;
        } else
        {
            foreach (GameObject racer in racers)
            {
                // Check if player or enemy
                if (racer.CompareTag("Player"))
                {
                    racer.GetComponent<PlayerController>().enabled = true;
                }
                else
                {
                    racer.GetComponent<EnemyController>().enabled = true;
                }
            }
            racingEnabled = true;
        }
    }

    private void DisplayTime()
    {
        int ms = (int) stopwatch * 1000;
        System.TimeSpan ts = System.TimeSpan.FromMilliseconds(ms);
        string rem = ((stopwatch % 1) * 100).ToString("##");
        stopwatchTxt.text = ts.ToString(@"hh\:mm\:ss") + "." + rem;
    }

    IEnumerator RaceCountdown()
    {
        countdownTxt.text = countdown.ToString();
        yield return new WaitForSeconds(1);
        countdown--;
        if (countdown > 0)
        {
            StartCoroutine(RaceCountdown());
        } else
        {
            ToggleRacingScripts();
            StartCoroutine(CountDownDisplay());
        }
    }

    IEnumerator CountDownDisplay()
    {
        countdownTxt.text = "GO";
        yield return new WaitForSeconds(0.5f);
        countdownTxt.enabled = false;
        // Enable other texts
        positionTxt.enabled = true;
        stopwatchTxt.enabled = true;
        lapTxt.enabled = true;
    }

    IEnumerator LoadSplash()
    {
        yield return new WaitForSeconds(delayAmount);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScene");
    }

    IEnumerator LoadGameOver()
    {
        if (stopwatch < pastTime && pastTime != 0)
        {
            PlayerPrefs.SetFloat(highScoreKey, stopwatch);
        }
        Constants.C.timeCount = stopwatch;
        Constants.C.HighTime = PlayerPrefs.GetFloat(highScoreKey);
        // Wait for delayAmount before going to game over scene
        yield return new WaitForSeconds(delayAmount);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }
}
