using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class EndGameMenu : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerMessage;

    [Header("Last game stats")]
    public TextMeshProUGUI currentMonstersCountText;
    public TextMeshProUGUI currentItemsCollectedCountText;

    [Header("Global stats")]
    public TextMeshProUGUI monstersCountText;
    public TextMeshProUGUI itemsCollectedCountText;

    [Header("Record time texts")]
    public Text[] recordTexts;

    private GameController gameController;
    private ItemUnlockController itemUnlockController;

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        itemUnlockController = GameObject.FindWithTag("ItemUnlockController").GetComponent<ItemUnlockController>();

        currentMonstersCountText.text = itemUnlockController.currentEnemysKilled.ToString();
        currentItemsCollectedCountText.text = itemUnlockController.currentItemsBought.ToString();

        monstersCountText.text = itemUnlockController.enemysKilled.ToString();
        itemsCollectedCountText.text = itemUnlockController.itemsBought.ToString();

        timerText.text = String.Format("{0:00}", (int)gameController.timer / 3600) + ":" + String.Format("{0:00}", (int)(gameController.timer % 3600) / 60) + ":" + String.Format("{0:00}", (int)gameController.timer % 60);

        float[] tempRecordsArray = gameController.GetRecordsArray();

        int i = 0;
        int j = 0;
        while(i < tempRecordsArray.Length)
        {
            if(tempRecordsArray[i] != 0)
            {
                recordTexts[j].text = (j+1) + ". " + String.Format("{0:00}", (int)tempRecordsArray[i] / 3600) + ":" + String.Format("{0:00}", (int)(tempRecordsArray[i] % 3600) / 60) + ":" + String.Format("{0:00}", (int)tempRecordsArray[i] % 60);
                if(tempRecordsArray[i] == gameController.timer)
                {
                    recordTexts[j].GetComponentInParent<Animator>().Play("timeFinishedAtList");
                }
                j++;
            }
            i++;
        }

        if (gameController.IsCurrentTimeRecord())
        {
            timerText.GetComponentInParent<Animator>().Play("newRecord");
            timerMessage.text = "NOWY REKORD:";
        }
    }

    private void TrasitionBetweenScenes()
    {
        GameObject blackPanel = GameObject.Find("Black Panel");
        blackPanel.GetComponent<BlackPanel>().enabled = true;
        blackPanel.GetComponent<BlackPanel>().ShowBlackPanel();

        GameObject[] objects = new GameObject[]
        {
            GameObject.FindGameObjectWithTag("Player"),
            GameObject.FindGameObjectWithTag("GameController"),
            GameObject.FindGameObjectWithTag("AnimationController"),
            GameObject.FindGameObjectWithTag("ItemUnlockController"),
            GameObject.FindGameObjectWithTag("Manager")
        };

        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    public void GoBackToMainMenu()
    {
        TrasitionBetweenScenes();

        Invoke(nameof(LoadMenu), 1.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        TrasitionBetweenScenes();

        Invoke(nameof(LoadStartGameScene), 1.5f);
    }

    private void LoadMenu()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        SceneManager.LoadScene(0);

        Destroy(canvas);
    }

    private void LoadStartGameScene()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        SceneManager.LoadScene(1);

        Destroy(canvas);
    }
}
