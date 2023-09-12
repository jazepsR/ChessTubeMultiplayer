using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject waitingForPlayerText;
    public GameObject logoObject;
    public GameObject otherPlayerMoveText;
    public GameObject winScreen;
    public GameObject loseScreen;

    public static UiManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        waitingForPlayerText.SetActive(!FigureSpawner.Instance.blackSpawned);
        logoObject.SetActive(!FigureSpawner.Instance.blackSpawned);
        otherPlayerMoveText.SetActive(FigureMover.Instance.isWhiteMove != BoardManager.instance.isPlayerWhite && Var.canMakeMove);
    }

    public void ShowGameOver(bool playerWon)
    {
        if (playerWon)
            winScreen.SetActive(true);
        else
            loseScreen.SetActive(true);
    }
}
