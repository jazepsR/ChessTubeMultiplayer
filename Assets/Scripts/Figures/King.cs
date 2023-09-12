using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessFigure
{
    public override bool[,] PossibleMove(ChessFigure[,] position)
    {
        bool[,] r = new bool[8, 8];
        moveCount = 0;
        ChessFigure c;
        int i, j;

        // Top
        i = data.CurrentX - 1;
        j = data.CurrentY + 1;
        if (data.CurrentY < 7)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 && i < 8)
                {
                    c = position[i, j];
                    if (c == null){ r[i, j] = true;
                        moveCount++;
                    }
                    else if (c.data.isWhite != data.isWhite){ r[i, j] = true;
                    moveCount++;
                }
            }
                i++;
            }
        }

        // Bottom
        i = data.CurrentX - 1;
        j = data.CurrentY - 1;
        if (data.CurrentY > 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 && i < 8)
                {
                    c = position[i, j];
                    if (c == null){ r[i, j] = true;
                    moveCount++;
                }
                else if (c.data.isWhite != data.isWhite){ r[i, j] = true;
                    moveCount++;
                }
            }
                i++;
            }
        }

        // Left
        if (data.CurrentX > 0)
        {
            c = position[data.CurrentX - 1, data.CurrentY];
            if (c == null){ r[data.CurrentX - 1, data.CurrentY] = true;
            moveCount++;
        }
        else if (c.data.isWhite != data.isWhite){ r[data.CurrentX - 1, data.CurrentY] = true;
            moveCount++;
        }
    }

        // Left (chesstube)
        if (data.CurrentX == 0)
        {
            c = position[7, data.CurrentY];
            if (c == null){ r[7, data.CurrentY] = true;
            moveCount++;
        }
        else if (c.data.isWhite != data.isWhite){ r[7, data.CurrentY] = true;
            moveCount++;
        }
    }



        // Right
        if (data.CurrentX < 7)
        {
            c = position[data.CurrentX + 1, data.CurrentY];
            if (c == null){ r[data.CurrentX + 1, data.CurrentY] = true;
            moveCount++;
        }
        else if (c.data.isWhite != data.isWhite){ r[data.CurrentX + 1, data.CurrentY] = true;
            moveCount++;
        }
    }

        // Right (Chesstube)
        if (data.CurrentX == 7)
        {
            c = position[0, data.CurrentY];
            if (c == null){ r[0, data.CurrentY] = true;
            moveCount++;
        }
        else if (c.data.isWhite != data.isWhite){ r[0, data.CurrentY] = true;
            moveCount++;
        }
    }

        return r;
    }
}
