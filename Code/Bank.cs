using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI displayBalance;
    [SerializeField] int finishGoal;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas gold;

    public int CurrentBalance { get { return currentBalance; } }

    private void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
        gameOverCanvas.enabled = false;
        gold.enabled = true;
    }



    private void Update()
    {
        if (currentBalance < 0)
        {
            Debug.Log("Reloading scene due to negative balance.");
            ReLoadScene();
        }
        else if (finishGoal < currentBalance)
        {
            Debug.Log("Loading next level due to reaching finish goal.");
            LoadNextLevel();
        }
    }


    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();


    }


    void UpdateDisplay()
    {
        displayBalance.text = "Gold  :  " + currentBalance;
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // You may want to add additional logic here, such as returning to the main menu or ending the game.
            Debug.LogWarning("No next scene available. Consider adding additional logic.");
        }
    }

    void ReLoadScene()
    {
        gameOverCanvas.enabled = true;
        gold.enabled = false;
    }
}
