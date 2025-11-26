using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // for TextMeshPro UI

public class GameLogic : MonoBehaviour
{
    public int Score1;
    public int Score2;
    public TMP_Text ScoreUI1;
    public TMP_Text ScoreUI2;
    public AudioSource ScoreAudio;

    private void Start()
    {
        // Initialize UI text
        if (ScoreUI1 != null)
            ScoreUI1.text = Score1.ToString();

        if (ScoreUI2 != null)
            ScoreUI2.text = Score2.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if collided with object tagged as "Waypoint1"
        if (other.CompareTag("Waypoint1"))
        {
            Destroy(other.gameObject); // destroy the waypoint
            Score1++; // increase score

            // Play sound if available
            if (ScoreAudio != null)
                ScoreAudio.Play();

            // Update UI
            if (ScoreUI1 != null)
                ScoreUI1.text = Score1.ToString();

            // Check if level should change
            if (Score1 >= 5)
            {
                LoadNextLevel();
            }
        }

        


        // Example: If you want to do similar for player 2
        if (other.CompareTag("Waypoint2"))
        {
            Destroy(other.gameObject);
            Score2++;

            if (ScoreAudio != null)
                ScoreAudio.Play();

            if (ScoreUI2 != null)
                ScoreUI2.text = Score2.ToString();

            if (Score2 >= 5)
            {
                LoadNextLevel();
            }
        }
    }

    private void LoadNextLevel()
    {
        // Load the next scene in Build Settings
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels! Game completed!");
        }
    }
}
