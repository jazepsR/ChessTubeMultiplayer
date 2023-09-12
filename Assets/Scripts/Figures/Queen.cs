using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessFigure
{
    public override bool[,] PossibleMove(ChessFigure[,] position)
    {
        bool[,] r = new bool[8, 8];
        ChessFigure c;

        bool reachedEnd = true;
        moveCount = 0;

        int i = data.CurrentX;
        int j = data.CurrentY;

        while (true)
        {
            i--;
            if (i < 0) break;
            c = position[i, data.CurrentY];
            if (c == null){ r[i, data.CurrentY] = true;
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
                if (c == null){ r[i, data.CurrentY] = true;
                moveCount++;
            }
                else
                {
                    if (c.data.isWhite != data.isWhite){ r[i, data.CurrentY] = true;
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
                if (c.data.isWhite != data.isWhite){ r[i, data.CurrentY] = true;
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
                if (c == null) {r[i, data.CurrentY] = true;
                moveCount++;
            }
                else
                {
                    if (c.data.isWhite != data.isWhite){ r[i, data.CurrentY] = true;
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
            if (c == null){ r[data.CurrentX, i] = true;
            moveCount++;
        }
            else
            {
                if (c.data.isWhite != data.isWhite){ r[data.CurrentX, i] = true;
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
                if (c.data.isWhite != data.isWhite){ r[data.CurrentX, i] = true;
                moveCount++;
            }
            break;
            }
        }
        // Top Left
        i = data.CurrentX;
        j = data.CurrentY;


        while (true)
        {
            j++;
            i--;
            if (i < 0)
            {
                i = 7;
            }


            if (j >= 8) break;
            c = position[i, j];
            if (c == null)
            {
                r[i, j] = true;
                moveCount++;
            }
            else
            {
                if (c.data.isWhite != data.isWhite){ r[i, j] = true;
                moveCount++;
            }
                break;
            }
        }

        // Top Right
        i = data.CurrentX;
        j = data.CurrentY;
        while (true)
        {
            i++;
            j++;
            if (i == 8)
            {
                i = 0;
            }


            if (j >= 8) break;
            c = position[i, j];
            if (c == null)
            {
                r[i, j] = true;
                moveCount++;
            }
            else
            {
                if (c.data.isWhite != data.isWhite){ r[i, j] = true;
                moveCount++;
            }
                break;
            }
        }

        // Bottom Left
        i = data.CurrentX;
        j = data.CurrentY;
        while (true)
        {
            i--;
            j--;
            if (i < 0)
            {
                i = 7;
            }
            if (i < 0 || j < 0) break;
            c = position[i, j];
            if (c == null)
            {
                r[i, j] = true;
                moveCount++;
            }
            else
            {
                if (c.data.isWhite != data.isWhite) {r[i, j] = true;
                moveCount++;
            }
                break;
            }
        }

        // Bottom Right
        i = data.CurrentX;
        j = data.CurrentY;
        while (true)
        {
            i++;
            j--;
            if (i == 8)
            {
                i = 0;
            }
            if (j < 0) break;
            c = position[i, j];
            if (c == null){ r[i, j] = true;
            moveCount++;
        }
            else
            {
                if (c.data.isWhite != data.isWhite){ r[i, j] = true;
                moveCount++;
            }
            break;
            }
        }

        return r;




    }
}
