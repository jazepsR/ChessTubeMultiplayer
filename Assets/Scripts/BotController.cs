using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BotType { Random, SimpleEvaluation, MinMax}
public class BotController : MonoBehaviour
{
    public static BotController Instance;
    public static BotType botType = BotType.MinMax;
    Dictionary<FigureType, int> figValues;
    ChessFigure[,] tempBoard;
    ChessFigure removedFigure = null;
    ChessFigure movedFigure = null;
    Vector2Int lastMove;
    bool isWhite = false;
    ChessFigure bestFigure = null;
    Vector2 bestMove = new Vector2(-1, -1);

    private void Awake()
    {
        Instance = this;
        GenerateFigValueTable();
    }

    private List<ChessFigure> GetFigureList(ChessFigure[,] boardState, bool isWhite)
    {
        List<ChessFigure> figures = new List<ChessFigure>();
        for(int i= 0;i<8;i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (boardState[i,j]!= null)
                {
                    if (boardState[i, j].data.isWhite == isWhite)
                    {
                        boardState[i, j].PossibleMove(boardState);
                        if (boardState[i, j].moveCount > 0)
                        {
                            figures.Add(boardState[i, j]);
                        }
                    }
                }
            }
        }
        return figures;
    }

    public Vector2 GetMove(bool isWhite, ChessFigure[,] boardState)
    {
        var figures = GetFigureList(boardState,isWhite);

        switch(botType)
        {
            case BotType.Random:
                FigureMover.Instance.selectedFigure = figures[Random.Range(0, figures.Count)];
                Debug.Log("Board state evaluation: " + EvaluateBoardState(boardState, isWhite));
                return FigureMover.Instance.selectedFigure.MoveCoordinateList(boardState)[Random.Range(0, FigureMover.Instance.selectedFigure.moveCount)];
            case BotType.SimpleEvaluation:
                float bestEvaluation = -1000;
                for(int i= 0;i<figures.Count;i++)
                {
                    var moves = figures[i].MoveCoordinateList(boardState);
                    for(int j=0;j< moves.Length;j++)
                    {
                        tempBoard = MakeMove(boardState, figures[i], moves[j], isWhite);
                        float evaluation = EvaluateBoardState(tempBoard, isWhite);
                        if(bestEvaluation< evaluation)
                        {
                            bestFigure = figures[i];
                            bestMove = moves[j];
                            bestEvaluation = evaluation;
                        }
                      //  boardState = ReverseLastMove(tempBoard);
                    }
                }
                FigureMover.Instance.selectedFigure = bestFigure;
                ResetPieces();
                return bestMove;
            case BotType.MinMax:
                tempBoard = boardState;
                Minmax(boardState, 3, true);
                FigureMover.Instance.selectedFigure = bestFigure;
                ResetPieces();
                return bestMove;




            default:
                return new Vector2Int(0,0);
        }  
    }
    



    public float Minmax(ChessFigure[,] position, int depth, bool maximizingPlayer)
    {     
        if (depth == 0)
        {
           return EvaluateBoardState(tempBoard, false);
        };

        if (maximizingPlayer)
        {
            float maxEval = -10000;
            var figlist = GetFigureList(position,false);
            if (figlist.Count == 0)
            {
                return EvaluateBoardState(tempBoard, false);
            }
            else
            {
                for (int i = 0; i < figlist.Count; i++)
                {
                    var moves = figlist[i].MoveCoordinateList(position);
                    for (int j = 0; j < moves.Length; j++)
                    {
                        tempBoard = MakeMove(position, figlist[i], moves[j], isWhite);
                        float evaluation = Minmax(tempBoard, depth - 1, false);
                         //Debug.Log("Fig: " + figlist[i] + " move: " + moves[j] + " depth: " + depth + " maximizing player: " + maximizingPlayer + " eval " + evaluation);
                        if (evaluation > maxEval)
                        {
                            if (depth ==3)
                            {
                                bestFigure = figlist[i];
                                bestMove = moves[j];
                                Debug.Log("Fig: " + figlist[i] + " move: " + moves[j] + " depth: " + depth + " maximizing player: " + maximizingPlayer + " eval " + evaluation);
                            }
                            maxEval = evaluation;
                        }
                       // Debug.Log("Fig: " + figlist[i] + " move: " + moves[j] + " depth: " + depth + " maximizing player: " + maximizingPlayer + " eval " + evaluation);
                    }
                }
            }
            return maxEval;


        }
        else
        {
            float minEval = +10000; 
            var figlist = GetFigureList(position,true); 
            if (figlist.Count == 0)
            {
                return EvaluateBoardState(tempBoard, false);
            }
            else
            {
                for (int i = 0; i < figlist.Count; i++)
                {
                    var moves = figlist[i].MoveCoordinateList(position);
                    for (int j = 0; j < moves.Length; j++)
                    {
                        tempBoard = MakeMove(position, figlist[i], moves[j], !isWhite);
                        float evaluation = Minmax(tempBoard, depth - 1, true);
                       // Debug.Log("Fig: " + figlist[i] + " move: " + moves[j] + " depth: " + depth + " maximizing player: " + maximizingPlayer + " eval " + evaluation);
                        if (evaluation < minEval)
                        {
                            minEval = evaluation;
                        }
                    }
                }
            }
            return minEval;
        }

    }

    /*function minimax(position, depth, maximizingPlayer)
	if depth == 0 or game over in position
		return static evaluation of position
 
	if maximizingPlayer
		maxEval = -infinity
		for each child of position
			eval = minimax(child, depth - 1, false)
			maxEval = max(maxEval, eval)
		return maxEval
 
	else
		minEval = +infinity
		for each child of position
			eval = minimax(child, depth - 1, true)
			minEval = min(minEval, eval)
		return minEval


    */
    private void ResetPieces()
    {
        var board = FigureSpawner.Instance.ChessFigurePositions;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if(board[i,j] != null)
                {
                    board[i, j].SetPosition(i, j);
                }
            }
        }
    }

    private ChessFigure[,] MakeMove(ChessFigure[,] boardState, ChessFigure figure, Vector2 move, bool isWhiteMove)
    {
        ChessFigure[,] tempState = new ChessFigure[8, 8];
        for(int i=0;i<8;i++)
        {
            for (int j = 0; j< 8; j++)
            {
                if(boardState[i,j] != null)
                {
                    tempState[i, j] = boardState[i, j];
                }
            }
        }
        ChessFigure figToKill = tempState[(int)move.x, (int)move.y];
        if (figToKill != null && figToKill.data.isWhite != isWhiteMove)
        {
            tempState[(int)move.x, (int)move.y] = null;
        }
        for(int i = 0;i<8;i++)
        {
            for(int j = 0; j<8;j++)
            {
                if(tempState[i,j]!= null)
                {
                    tempState[i, j].SetPosition(i, j);
                }
                if(tempState[i,j] == figure)
                {
                    tempState[i, j] = null;
                }
            }

        }

       // tempState[figure.CurrentX, figure.CurrentY] = null;
        tempState[(int)move.x, (int)move.y] = figure;
        figure.SetPosition((int)move.x, (int)move.y);
        return tempState;
    }

    private float EvaluateBoardState(ChessFigure[,] boardState, bool isWhite)
    {
        float evaluation = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if(boardState[i,j]!= null)
                {
                    int figVal = 0;
                    figValues.TryGetValue(boardState[i, j].data.type, out figVal);
                    if( boardState[i,j].data.isWhite != isWhite)
                    {
                        figVal = -figVal;
                    }
                    evaluation += figVal;
                }
            }
        }

        return evaluation;

    }

    private void GenerateFigValueTable()
    {
        if (figValues != null)
            return;
        figValues = new Dictionary<FigureType, int>();
        figValues.Add(FigureType.Pawn, 10);
        figValues.Add(FigureType.Bishop, 30);
        figValues.Add(FigureType.Knight, 30);
        figValues.Add(FigureType.Rook, 50);
        figValues.Add(FigureType.Queen, 90);
        figValues.Add(FigureType.King, 900);
    }
}
