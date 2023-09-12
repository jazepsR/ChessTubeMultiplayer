using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessFigure
{
    ChessFigure[,] position;
    public override bool[,] PossibleMove(ChessFigure[,] position)
    {
        bool[,] r = new bool[8, 8];
        this.position = position;
        moveCount = 0;

        // Up / Left
        KnightMove(data.CurrentX - 1, data.CurrentY + 2, ref r);
        KnightMove(data.CurrentX - 2, data.CurrentY + 1, ref r);

        // Up / Right
        KnightMove(data.CurrentX + 1, data.CurrentY + 2, ref r);
        KnightMove(data.CurrentX + 2, data.CurrentY + 1, ref r);

        // Down / Left
        KnightMove(data.CurrentX - 1, data.CurrentY - 2, ref r);
        KnightMove(data.CurrentX - 2, data.CurrentY - 1, ref r);

        // Down / Right
        KnightMove(data.CurrentX + 1, data.CurrentY - 2, ref r);
        KnightMove(data.CurrentX + 2, data.CurrentY - 1, ref r);

        return r;
    }

    public void KnightMove(int x, int y, ref bool[,] r)
    {
        ChessFigure c;
        if (x >= 8)
        {
            x -= 8;
        }
        if(x<0)
        {
            x += 8;
        }


        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = position[x, y];
            if (c == null){ r[x, y] = true;
            moveCount++;
        }
        else if (c.data.isWhite != data.isWhite){ r[x, y] = true;
            moveCount++;
        }
    }
    }
}
