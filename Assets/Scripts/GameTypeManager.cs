
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameTypeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetGameMode(GameType.LocalMP);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetGameMode(GameType.AI);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetGameMode(GameType.OnlineMP);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetBotType(BotType.SimpleEvaluation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetBotType(BotType.MinMax);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void SetBotType(BotType type)
    {
        BotController.botType = type;
        Debug.Log("Setting bot type " + type.ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetGameMode(GameType gameType)
    {
        Var.gameType = gameType;
        Debug.Log("Starting " + gameType.ToString() + " game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
