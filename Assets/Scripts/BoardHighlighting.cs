using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlighting : MonoBehaviour
{

    public static BoardHighlighting Instance { get; set; }

    public GameObject highlightPrefab;
    public GameObject moveHighlightPrefab;
    private GameObject moveHighlight;
    private List<GameObject> highlights;

    private void Start()
    {
        Instance = this;
        moveHighlight = Instantiate(moveHighlightPrefab);
        moveHighlight.SetActive(false);
        highlights = new List<GameObject>();

    }
    private GameObject GetHighlightObject()
    {
        GameObject go = highlights.Find(g => !g.activeSelf);
        if (go == null)
        {
            go = Instantiate(highlightPrefab);
            highlights.Add(go);
        }
        return go;
    }

    public void HighlightAllowedMoves(bool[,] moves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = GetHighlightObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }
    public void HighlightAllowedMoves(List<Vector2> moves)
    {
        foreach(Vector2 move in moves)
        { 
            GameObject go = GetHighlightObject();
            go.SetActive(true);
            go.transform.position = new Vector3(move.x + 0.5f, 0, move.y + 0.5f);
        }
    }

    public void ShowLastMove(int x, int y)
    {
        GameObject go = moveHighlight;
        go.SetActive(true);
        go.transform.position = new Vector3(x + 0.5f, 0, y + 0.5f);

    }

    public void HideHighlights()
    {
        foreach (GameObject go in highlights) go.SetActive(false);
    }
}