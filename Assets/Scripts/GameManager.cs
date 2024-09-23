using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // To manage scene loading

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;  // Reference to the Game Over panel
    public GameObject winPanel;        // Reference to the Win panel
    public Timer timer;                // Reference to the Timer script
    public PlayerController player;     // Reference to the PlayerController script

    void Start()
    {
        // Ensure both panels are inactive at the start
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    void Update()
    {
        CheckWinCondition();
        CheckGameOverCondition();
    }

    void CheckWinCondition()
    {
        if (player.GetCount() >= player.totalCoins) // Assuming totalCoins is the total number of coins
        {
            Win();
        }
    }

    void CheckGameOverCondition()
    {
        if (timer.GetRemainingTime() <= 0) // Assuming GetRemainingTime() returns the remaining time
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);  // Show the game over panel
    }

    void Win()
    {
        winPanel.SetActive(true);        // Show the win panel
    }

    // Methods for button functionality
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu"); // Load the MainMenu scene
    }
}


