using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ListTest
{
    public List<Vector2> positions = new List<Vector2>();
}
public class FigureMover : NetworkBehaviour
{
    [field: SyncVar]
    public bool isWhiteMove { get; [ServerRpc(RequireOwnership = false)]set; } = true;
    public int id1;
    public int id2;
    public ChessFigure selectedFigure = null;
    public ListTest positions = new ListTest();
    private bool[,] allowedMoves = new bool[8, 8];
    public static FigureMover Instance;
    private FigureSpawner spawner;
    [SerializeField] private AnimationCurve heightCurve, moveLerpCurve;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        spawner = FigureSpawner.Instance;
       Var.canMakeMove = true;
    }
/*
    public override void OnStartClient()
    {
        base.OnStartClient();
    }
*/
    // Update is called once per frame
    void Update()
    {
        if (BoardManager.selectionX >= 0 && BoardManager.selectionY >= 0 &&
            BoardManager.selectionX <8  && BoardManager.selectionY <8)
        {

            if (Input.GetMouseButtonDown(0))
            {

                if (selectedFigure == null)
                {
                    SelectChessFigure(BoardManager.selectionX, BoardManager.selectionY);
                    if (selectedFigure == null)
                    {
                        BoardHighlighting.Instance.HideHighlights();
                    }
                    else
                    {
                        BoardHighlighting.Instance.HighlightAllowedMoves(allowedMoves);
                    }
                }
                else
                {
                    MoveChessFigure(BoardManager.selectionX, BoardManager.selectionY);
                }
            }
        }
    }

    private IEnumerator MovePiece(GameObject toMove, Vector3 target)
    {
        float moveHeight = 0.5f;
        float moveTime = 0.5f;
        float t = 0;
        Vector3 startPos = toMove.transform.position;
        Vector3 currentPos;
        while (t <1)
        {
            currentPos = Vector3.Lerp(startPos, target, moveLerpCurve.Evaluate(t));
            currentPos.y = heightCurve.Evaluate(t) * moveHeight + currentPos.y;
            t += Time.deltaTime/moveTime;
            toMove.transform.position = currentPos;
            yield return null;
        }       
        toMove.transform.position = target;
        //BoardHighlighting.Instance.HighlightAllowedMoves(selectedFigMoveList.positions);
    }
   


    public void SelectChessFigure(int x, int y)
    {
      //  Debug.LogError("SelectChessFigure");
        if (!Var.canMakeMove)
        {
            Debug.Log("Falied 1");
            return;
        }
        if (!FigureSpawner.Instance.blackSpawned)
        {
            Debug.Log("Failed 5");
            return;
        }
        if (spawner.ChessFigurePositions[x, y] == null)
        {
            Debug.Log("Falied 2");
            return;
        }
        if (spawner.ChessFigurePositions[x, y].data.isWhite != isWhiteMove)
        {
            Debug.Log("Falied 3");
            return;
        }
        if(isWhiteMove != BoardManager.instance.isPlayerWhite)
        {
            Debug.Log("Failed 4");
            return;
        }


        bool hasAtLeastOneMove = false;
        allowedMoves = spawner.ChessFigurePositions[x, y].PossibleMove(spawner.ChessFigurePositions);

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (allowedMoves[i, j])
                {
                    hasAtLeastOneMove = true;
                    i = 7;
                    break;
                }
            }
        }

        if (!hasAtLeastOneMove) return;
        selectedFigure = spawner.ChessFigurePositions[x, y];
        if (Var.gameType == GameType.OnlineMP)
        {
            positions = new ListTest();
            var movelist = selectedFigure.MoveCoordinateList(spawner.ChessFigurePositions);
            foreach (Vector2 move in movelist)
            {
                positions.positions.Add(new Vector2(move.x, move.y));
            }
        }
    }

    private void MoveChessFigure(int x, int y)
    {
        //Debug.Log("making move. is white: " + isWhiteMove +" " + selectedFigure.name + " move: x " + x + " y " + y);
        if (allowedMoves[x, y])
        {         
            ChessFigure c = spawner.ChessFigurePositions[x, y];
            if (c != null && c.data.isWhite != isWhiteMove)
            {
                DestroyFigureServer(x,y);
            }
            SwapFigurePositionsServer(x, y,selectedFigure.data.CurrentX, selectedFigure.data.CurrentY, selectedFigure.data);



            isWhiteMove = !isWhiteMove;

            // LogBoardState(spawner.ChessFigurePositions);
            if (isWhiteMove != BoardManager.instance.isPlayerWhite && Var.gameType == GameType.AI)
            {
                Var.canMakeMove = false;
                Vector2 move = BotController.Instance.GetMove(isWhiteMove, spawner.ChessFigurePositions);
                ExecuteAIMove(move);
            }
        }
        else
        {
           // Debug.Log("move failed! Selected figure: " + selectedFigure.name + " move: x " +x+ " y " + y);
        }


        // LogBoardState(spawner.ChessFigurePositions);
        BoardHighlighting.Instance.HideHighlights();
        selectedFigure = null;
    }
    [ServerRpc(RequireOwnership = false)]
    private void DestroyFigureServer(int x, int y)
    {
        DestroyFigureObserver(x,y);
    }
    [ObserversRpc]
    private void DestroyFigureObserver(int x, int y)
    {
        ChessFigure c = spawner.ChessFigurePositions[x, y];
        if (c != null)
        {
            spawner.activeFigures.Remove(c);
            Destroy(c.transform.gameObject);

            if (c.data.type == FigureType.King)
            {
                EndGame(c.data.isWhite != BoardManager.instance.isPlayerWhite);
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SwapFigurePositionsServer(int x, int y, int oldX, int oldY, ChessFigureData figure)
    {
        SwapFigurePositionObserver(x, y, oldX, oldY, figure);
    }

    [ObserversRpc]
    private void SwapFigurePositionObserver(int x, int y, int oldX, int oldY, ChessFigureData figure)
    {
        ChessFigure fig = spawner.ChessFigurePositions[oldX, oldY];
        fig.data = figure;
        fig.SetPosition(x, y);
        StartCoroutine(MovePiece(fig.gameObject, spawner.GetTileCenter(x, y)));
        //fig.transform.position = ;
        spawner.ChessFigurePositions[oldX, oldY] = null;
        spawner.ChessFigurePositions[x, y] = fig;
        //ResetPieces();
    }
    public void LogBoardState(ChessFigure[,] boardState)
    {
        for (int i = 0; i < 8; i++)
        {
            string line = "line " + i + " ";
            for (int j = 0; j < 8; j++)
            {
                if (boardState[j, i] != null)
                {
                    line += boardState[j, i].name + " ";
                }
                else
                {
                    line += "- ";
                }
            }
            Debug.Log(line);
        }
    }


    public void ExecuteAIMove(Vector2 moveCoordinates)
    {
        allowedMoves = selectedFigure.PossibleMove(spawner.ChessFigurePositions);
        BoardHighlighting.Instance.ShowLastMove(selectedFigure.data.CurrentX, selectedFigure.data.CurrentY);
        MoveChessFigure((int)moveCoordinates.x, (int)moveCoordinates.y);
        Var.canMakeMove = true;
    }


    private void EndGame(bool playerWon)
    {
        UiManager.instance.ShowGameOver(playerWon);
        Var.canMakeMove = false;
       // resetText.SetActive(true);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
