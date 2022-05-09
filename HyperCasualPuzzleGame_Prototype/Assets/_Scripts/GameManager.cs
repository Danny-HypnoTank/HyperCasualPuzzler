using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int collectablesNeeded = 10;
    [SerializeField]
    private int currentCollectables;
    [SerializeField]
    private GameObject[] collectables;

    [SerializeField]
    private TMP_Text collectablesCounter;
    [SerializeField]
    private GameObject restartButton;

    [SerializeField]
    private bool exit;
    [SerializeField]
    private GameObject objectiveText;
    [SerializeField]
    private GameObject completionText;
    [SerializeField]
    private TMP_Text totalGemsCollectedText;
    [SerializeField]
    private SpriteRenderer arrowSprite;
    [SerializeField]
    private Sprite arrowOn;


    private bool levelComplete;
    [SerializeField]
    private bool gameOver;
    private bool smashed;

    public bool LevelComplete { get => levelComplete;}
    public bool Smashed { get => smashed;}
    public int CurrentCollectables { get => currentCollectables; set => currentCollectables = value; }
    public bool GameOver { get => gameOver; set => gameOver = value; }
    public bool Exit { get => exit; set => exit = value; }

    private void Update()
    {
        collectablesCounter.text = currentCollectables + "/" + collectablesNeeded;

        CompletionCheck();

        if (gameOver == true)
        {
            restartButton.SetActive(true);
            if (Input.GetKey(KeyCode.R))
            {
                Restart();
            }
        }
        DestroyCheck();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void CompletionCheck()
    {
        if (currentCollectables == collectablesNeeded && gameOver == false)
        {
            levelComplete = true;
            smashed = true;
            arrowSprite.sprite = arrowOn;
            objectiveText.SetActive(true);
            // have a for loop that destorys all remaining collectables
            //for (int i = 0; i < collectables.Length; i++)
            //{
            //    if (collectables[i] != null)
            //    {
            //        collectables[i].SetActive(false);
            //        //Get animator then set bool for shrink anim to true
            //    }
            //}
        }
    }

    private void DestroyCheck()
    {
        if (exit == true)
        {
            objectiveText.SetActive(false);
            totalGemsCollectedText.text = "Gems Collected: " + currentCollectables + "/" + "11";
            completionText.SetActive(true);
            Time.timeScale = 0;
            //pause game here
        }
    }
}
