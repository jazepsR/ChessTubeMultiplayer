using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChessFigure : MonoBehaviour
{
    [SerializeField]
    public ChessFigureData data = new ChessFigureData();
    public int moveCount = 0;
    

   
    public void SetPosition(int x, int y)
    {
        data.CurrentX = x;
        data.CurrentY = y;
    }

    public virtual bool[,] PossibleMove(ChessFigure[,] position)
    {
        return new bool[8, 8];
    }

    public virtual Vector2[] MoveCoordinateList(ChessFigure[,] position)
    {
        var allowedMoves = PossibleMove(position);
        Vector2[] moveCoordinateList = new Vector2[moveCount];
        int movesAdded = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (allowedMoves[i, j])
                {
                    moveCoordinateList[movesAdded] = new Vector2(i, j);
                    movesAdded++;
                }
            }
        }
        return moveCoordinateList;
    }
}
[System.Serializable]
public class ChessFigureData
{
    public int CurrentX { get; set; } 
    public int CurrentY { get; set; }
    public bool isWhite;
    public FigureType type = FigureType.Pawn;

}
