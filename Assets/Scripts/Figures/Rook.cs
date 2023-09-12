using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessFigure
{
    public override bool[,] PossibleMove(ChessFigure[,] position)
    {
        bool[,] r = new bool[8, 8];
        moveCount = 0;
        ChessFigure c;
        int i;
        bool reachedEnd = true;
        // Left
        i = data.CurrentX;
        while (true)
        {
            i--;
            if (i < 0) break;
            c = position[i, data.CurrentY];
            if (c == null)
            {
                r[i, data.CurrentY] = true;
                moveCount++;
            }
            else
            {
                if (c.data.isWhite != data.isWhite){ r[i, data.CurrentY] = true;
                moveCount++;
            }
                reachedEnd = false;
                break;
            }
        }

        // Left (Chess tube)
        if (reachedEnd)
        {
            i = 8;
            while (true)
            {
                i--;
                if (i < data.CurrentX) break;
                c = position[i, data.CurrentY];
                if (c == null){
                    r[i, data.CurrentY] = true;
                moveCount++;
            }
                else
                {
                    if (c.data.isWhite != data.isWhite)
                    {
                        r[i, data.CurrentY] = true;
                        moveCount++;
                    }
                    break;
                }
            }
        }
        reachedEnd = true;
        // Right
        i = data.CurrentX;
        while (true)
        {
            i++;
            if (i >= 8) break;
            c = position[i, data.CurrentY];
            if (c == null){ r[i, data.CurrentY] = true;
            moveCount++;
        }
            else
            {
                if (c.data.isWhite != data.isWhite)
                {
                    r[i, data.CurrentY] = true;
                    moveCount++;
                }
                reachedEnd = false;
                break;
            }
        }

        if (reachedEnd)
        {
            // Right (chess tube)
            i = -1;
            while (true)
            {
                i++;
                if (i >= data.CurrentX) break;
                c = position[i, data.CurrentY];
                if (c == null){ r[i, data.CurrentY] = true;
                moveCount++;
            }
                else
                {
                    if (c.data.isWhite != data.isWhite)
                    {
                        r[i, data.CurrentY] = true;
                        moveCount++;
                    }
                    break;
                }
            }
        }

        // Forward
        i = data.CurrentY;
        while (true)
        {
            i++;
            if (i >= 8) break;
            c = position[data.CurrentX, i];
            if (c == null) {r[data.CurrentX, i] = true;
            moveCount++;
        }
            else
            {
                if (c.data.isWhite != data.isWhite)
                {
                    r[data.CurrentX, i] = true;
                    moveCount++;
                }
                break;
            }
        }

        // Back
        i = data.CurrentY;
        while (true)
        {
            i--;
            if (i < 0) break;
            c = position[data.CurrentX, i];
            if (c == null){ r[data.CurrentX, i] = true;
            moveCount++;
        }
            else
            {
                if (c.data.isWhite != data.isWhite)
                {
                    r[data.CurrentX, i] = true;
                    moveCount++;
                }
                break;
            }
        }

        return r;
    }
}
