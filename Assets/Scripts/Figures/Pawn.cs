using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessFigure
{
    public override bool[,] PossibleMove(ChessFigure[,] position)
    {
        bool[,] r = new bool[8, 8];
        moveCount = 0;
        ChessFigure c, c2;

        if (data.isWhite)
        {
            // Diagonal Left
            if (data.CurrentX != 0 && data.CurrentY != 7)
            {
                c = position[data.CurrentX - 1, data.CurrentY + 1];
                if (c != null && !c.data.isWhite){ r[data.CurrentX - 1, data.CurrentY + 1] = true;
                moveCount++;
            }
        }

            // Diagonal Left (chess tube)
            if (data.CurrentX == 0 && data.CurrentY != 7)
            {
                c = position[7, data.CurrentY + 1];
                if (c != null && !c.data.isWhite){ r[7, data.CurrentY + 1] = true;
                moveCount++;
            }
        }


            // Diagonal Right
            if (data.CurrentX != 7 && data.CurrentY != 7)
            {
                c = position[data.CurrentX + 1, data.CurrentY + 1];
                if (c != null && !c.data.isWhite){ r[data.CurrentX + 1, data.CurrentY + 1] = true;
                moveCount++;
            }
        }

            // Diagonal Right (chess tube)
            if (data.CurrentX == 7 && data.CurrentY != 7)
            {
                c = position[0, data.CurrentY + 1];
                if (c != null && !c.data.isWhite){ r[0, data.CurrentY + 1] = true;
                moveCount++;
            }
        }

            // Forward
            if (data.CurrentY != 7)
            {
                c = position[data.CurrentX, data.CurrentY + 1];
                if (c == null){ r[data.CurrentX, data.CurrentY + 1] = true;
                moveCount++;
            }
        }
            // Two Steps Forward
            if (data.CurrentY == 1)
            {
                c = position[data.CurrentX, data.CurrentY + 1];
                c2 = position[data.CurrentX, data.CurrentY + 2];
                if (c == null && c2 == null){ r[data.CurrentX, data.CurrentY + 2] = true;
                moveCount++;
            }
        }
        }
        else
        {
            // Diagonal Left
            if (data.CurrentX != 0 && data.CurrentY != 0)
            {
                c = position[data.CurrentX - 1, data.CurrentY - 1];
                if (c != null && c.data.isWhite){ r[data.CurrentX - 1, data.CurrentY - 1] = true;
                moveCount++;
            }
        }

            // Diagonal Left (chess tube)
            if (data.CurrentX == 0 && data.CurrentY != 0)
            {
                c = position[7, data.CurrentY - 1];
                if (c != null && c.data.isWhite){ r[7, data.CurrentY - 1] = true;
                moveCount++;
            }
        }

            // Diagonal Right
            if (data.CurrentX != 7 && data.CurrentY != 0)
            {
                c = position[data.CurrentX + 1, data.CurrentY - 1];
                if (c != null && c.data.isWhite){ r[data.CurrentX + 1, data.CurrentY - 1] = true;
                moveCount++;
            }
        }

            // Diagonal Right (chess tube)
            if (data.CurrentX == 7 && data.CurrentY != 0)
            {
                c = position[0, data.CurrentY - 1];
                if (c != null && c.data.isWhite){ r[0, data.CurrentY - 1] = true;
                moveCount++;
            }
        }

            // Forward
            if (data.CurrentY != 0)
            {
                c = position[data.CurrentX, data.CurrentY - 1];
                if (c == null){ r[data.CurrentX, data.CurrentY - 1] = true;
                moveCount++;
            }
        }

            // Two Steps Forward
            if (data.CurrentY == 6)
            {
                c = position[data.CurrentX, data.CurrentY - 1];
                c2 = position[data.CurrentX, data.CurrentY - 2];
                if (c == null && c2 == null){ r[data.CurrentX, data.CurrentY - 2] = true;
                moveCount++;
            }
        }
        }

        return r;
    }
}
