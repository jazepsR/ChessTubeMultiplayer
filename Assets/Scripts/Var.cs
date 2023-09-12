using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameType { LocalMP, AI, OnlineMP}
public class Var
{
    public static float TILE_SIZE = 1f;
    public static float TILE_OFFSET = 0.5f;
    public static GameType gameType = GameType.OnlineMP;
    public static bool canMakeMove = true;
}
