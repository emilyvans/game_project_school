using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class minesweeper : PuzzleScreen
{
    public static minesweeper Instance;
    List<List<int>> rawfield;
    List<bool> rawfieldcompleted = new List<bool>();
    List<MineSweeperButton> buttons = new List<MineSweeperButton>();

    [SerializeField]
    private int minePercentage = 3;

    [SerializeField]
    private float minePercentageDificultyScale = 0.5f;
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    List<List<int>> generate(int difficulty)
    {
        List<List<int>> field = new List<List<int>>();
        for (int y = 0; y < 14; y++)
        {
            List<int> row = new List<int>();
            for (int x = 0; x < 14; x++)
            {
                row.Add(0);
                rawfieldcompleted.Add(false);
            }
            field.Add(row);
        }

        for (int y = 0; y < field.Count; y++)
        {
            for (int x = 0; x < field[y].Count; x++)
            {
                if (Random.Range(0, 100) <= minePercentage + difficulty * minePercentageDificultyScale)
                {
                    field[y][x] = -1;
                }
            }
        }

        for (int cellY = 0; cellY < field.Count; cellY++)
        {
            for (int cellX = 0; cellX < field[cellY].Count; cellX++)
            {

                if (field[cellY][cellX] == -1)
                {
                    continue;
                }
                int minX = cellX - 1;
                int maxX = cellX + 1;
                int minY = cellY - 1;
                int maxY = cellY + 1;

                if (minX < 0)
                {
                    minX = 0;
                }
                if (maxX > field[cellY].Count - 1)
                {
                    maxX = field[cellY].Count - 1;
                }

                if (minY < 0)
                {
                    minY = 0;
                }
                if (maxY > field.Count - 1)
                {
                    maxY = field.Count - 1;
                }


                int Count = 0;

                for (int y = minY; y <= maxY; y++)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        if (!(x == cellX && y == cellY))
                        {
                            if (field[y][x] == -1)
                                Count += 1;
                        }

                    }
                }
                field[cellY][cellX] = Count;
            }
        }

        return field;
    }

    public void revealZero(int x, int y)
    {
        List<int> indices = checkZeros(x - 1, y - 1);

        foreach (int index in indices)
        {
            rawfieldcompleted[index] = true;
            buttons[index].reveal();
        }

    }

    List<int> checkZeros(int GameCellX, int GameCellY, List<int> Indices = null, List<Vector2Int> Visited = null)
    {
        int CellX = GameCellX;
        int CellY = GameCellY;
        List<int> indices = Indices ?? new List<int>();
        List<Vector2Int> visited = Visited ?? new List<Vector2Int>();
        int minX = CellX - 1;
        int maxX = CellX + 1;
        int minY = CellY - 1;
        int maxY = CellY + 1;

        if (minX < 0)
        {
            minX = 0;
        }
        if (maxX > rawfield[CellY].Count - 1)
        {
            maxX = rawfield[CellY].Count - 1;
        }
        if (minY < 0)
        {
            minY = 0;
        }
        if (maxY > rawfield.Count - 1)
        {
            maxY = rawfield.Count - 1;
        }

        List<Vector2Int> zeros = new List<Vector2Int>();

        int length = rawfield[0].Count;
        indices.Add((length * CellY) + CellX);
        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                if (!(x == CellX && y == CellY))
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    if (rawfield[y][x] == 0 && !visited.Contains(pos))
                    {
                        indices.Add((length * y) + x);
                        zeros.Add(pos);
                    }
                    else if (rawfield[y][x] == -1)
                    {
                    }
                    else if (!visited.Contains(pos))
                    {
                        indices.Add((length * y) + x);
                    }
                    visited.Add(pos);
                }

            }
        }

        foreach (Vector2Int position in zeros)
        {
            checkZeros(position.x, position.y, indices, visited);
        }
        return indices;
    }

    public void fail()
    {
        gameObject.SetActive(false);
        onComplete(false, difficulty);
    }
    public override void StartPuzzle(int difficulty)
    {
        base.StartPuzzle(difficulty);
        if (buttons.Count == 0)
        {
            foreach (Transform child in PlayArea.transform.GetChild(0))
            {
                buttons.Add(child.gameObject.GetComponent<MineSweeperButton>());
            }
        }
        rawfieldcompleted = new List<bool>();
        List<int> field = new List<int>();
        rawfield = generate(difficulty);
        foreach (List<int> row in rawfield)
        {
            field.AddRange(row);
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            MineSweeperButton button = buttons[i];
            button.GetComponent<Button>().enabled = true;
            button.initButton(field[i], (i % rawfield[0].Count) + 1, (i / rawfield.Count) + 1);
        }
    }
    public override void Reset()
    {
        return;
    }

    public bool checkComplete(int x, int y, bool complete)
    {
        rawfieldcompleted[rawfield.Count * (y - 1) + (x - 1)] = complete;
        for (int i = 0; i < rawfieldcompleted.Count; i++)
        {
            if (rawfieldcompleted[i] == false)
            {
                return false;
            }
        }
        gameObject.SetActive(false);
        onComplete(true, difficulty);
        return true;
    }
}

