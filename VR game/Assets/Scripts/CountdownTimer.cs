using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60f; // Set your countdown time in seconds
    public Text countdownText; // Reference to the Text component for displaying the countdown
    public Text msg;
    GameObject[] Targets;
    private float currentTime;

    void Start()
    {
        currentTime = countdownTime;
    }

    void Update()
    {
        Targets = GameObject.FindGameObjectsWithTag("Target");
        UpdateTimer();
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            // The countdown has reached zero, you can handle what happens next here
            Debug.Log("Countdown has reached zero!");
            msg.text = "You Lost";
        }
        else
        {
            if (Targets.Length == 0)
            {
                if (SceneManager.GetActiveScene().buildIndex !=3)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
                
            }
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        // Update the UI text to display the current time
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timerText = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(currentTime / 60), seconds);
        countdownText.text = timerText;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
