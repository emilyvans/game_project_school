using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using static Block;

public enum Block
{
    CURSOR,
    EMPTY,
    NORMAL,
    FILEBLOCK,
    FILEFINISH,
    DEATH
}

public class Files : PuzzleScreen
{

    [SerializeField]
    private GameObject CursorBlock;

    [SerializeField]
    private GameObject fileBlock;

    [SerializeField]
    private GameObject fileFinishBlock;

    [SerializeField]
    private GameObject deathBlock;

    [SerializeField]
    private GameObject normalBlock;

    int finishes;

    private List<Block[]> layouts = new List<Block[]>{
        new Block[13*13]{
            FILEBLOCK,EMPTY,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,NORMAL,DEATH,FILEBLOCK,
            NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,NORMAL,FILEBLOCK,EMPTY,EMPTY,NORMAL,EMPTY,DEATH,
            EMPTY,NORMAL,EMPTY,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,DEATH,EMPTY,EMPTY,NORMAL,NORMAL,
            EMPTY,EMPTY,EMPTY,EMPTY,DEATH,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,
            EMPTY,EMPTY,DEATH,EMPTY,EMPTY,FILEBLOCK,EMPTY,DEATH,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,
            NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,DEATH,EMPTY,EMPTY,EMPTY,
            EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,FILEFINISH,EMPTY,EMPTY,EMPTY,
            EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,DEATH,EMPTY,NORMAL,EMPTY,CURSOR,EMPTY,EMPTY,EMPTY,
            DEATH,NORMAL,EMPTY,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,NORMAL,EMPTY,
            EMPTY,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,
            EMPTY,NORMAL,EMPTY,NORMAL,EMPTY,NORMAL,DEATH,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,
            FILEBLOCK,NORMAL,EMPTY,DEATH,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,
            NORMAL,FILEFINISH,NORMAL,EMPTY,EMPTY,FILEFINISH,NORMAL,FILEFINISH,FILEFINISH,EMPTY,EMPTY,NORMAL,EMPTY
        },
        new Block[13*13]{
            FILEBLOCK,EMPTY,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,NORMAL,DEATH,FILEBLOCK,
            NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,NORMAL,FILEBLOCK,EMPTY,EMPTY,NORMAL,EMPTY,DEATH,
            FILEFINISH,NORMAL,EMPTY,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,DEATH,EMPTY,EMPTY,NORMAL,NORMAL,
            EMPTY,EMPTY,EMPTY,EMPTY,DEATH,NORMAL,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,
            EMPTY,NORMAL,DEATH,EMPTY,EMPTY,FILEBLOCK,EMPTY,DEATH,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,
            NORMAL,EMPTY,EMPTY,NORMAL,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,DEATH,EMPTY,EMPTY,FILEBLOCK,
            EMPTY,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,
            EMPTY,EMPTY,NORMAL,EMPTY,FILEFINISH,DEATH,EMPTY,NORMAL,FILEFINISH,NORMAL,EMPTY,EMPTY,FILEBLOCK,
            DEATH,NORMAL,EMPTY,NORMAL,NORMAL,EMPTY,CURSOR,EMPTY,NORMAL,NORMAL,EMPTY,NORMAL,EMPTY,
            EMPTY,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,NORMAL,EMPTY,
            EMPTY,NORMAL,EMPTY,NORMAL,EMPTY,NORMAL,DEATH,EMPTY,NORMAL,EMPTY,EMPTY,EMPTY,EMPTY,
            FILEBLOCK,NORMAL,EMPTY,DEATH,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,EMPTY,
            NORMAL,FILEFINISH,NORMAL,EMPTY,EMPTY,FILEFINISH,NORMAL,FILEFINISH,FILEFINISH,EMPTY,EMPTY,NORMAL,EMPTY
        },
    };

    private Block[] field = new Block[13 * 13];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayArea.transform.childCount; i++)
        {
            Transform child = PlayArea.transform.GetChild(i);
            ItemSlot slot = child.GetComponent<ItemSlot>();
            slot.location = i;
            slot.handleChild = handleChild;
            slot.move = move;
            slot.canAccess = canAccess;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (finishes == 0)
        {
            onComplete(true, difficulty);
            Reset();
            gameObject.SetActive(false);
            finishes = -1;
        }
    }

    void move(int location, int prevLocation, Block item)
    {
        field[prevLocation] = EMPTY;
        field[location] = item;
    }

    public override void StartPuzzle(int difficulty)
    {
        base.StartPuzzle(difficulty);
        field = (Block[])layouts[Random.Range(0, layouts.Count)].Clone();
        finishes = System.Array.FindAll(field, (item) => item == FILEFINISH).Length;

        for (int i = 0; i < PlayArea.transform.childCount; i++)
        {
            Transform child = PlayArea.transform.GetChild(i);
            GameObject prefab = field[i] switch
            {
                CURSOR => Instantiate(CursorBlock, child),
                EMPTY => null,
                NORMAL => Instantiate(normalBlock, child),
                FILEBLOCK => Instantiate(fileBlock, child),
                FILEFINISH => Instantiate(fileFinishBlock, child),
                DEATH => Instantiate(deathBlock, child),
                _ => throw new System.NotImplementedException(),
            };
            if (prefab != null)
            {
                Item item = prefab.GetComponent<Item>();
                item.location = i;
                if (item is SemiDragableItem)
                {
                    SemiDragableItem semiItem = item as SemiDragableItem;
                    semiItem.canMove += canAccess;
                }
            }
        }
    }

    private bool canAccess(int location)
    {
        int cursorLocation = System.Array.IndexOf(field, CURSOR);

        for (int i = Mathf.Max(cursorLocation - 13, 0); i <= Mathf.Min(cursorLocation + 13, field.Length); i += 13)
        {
            for (int j = Mathf.Max(i - 1, 0); j <= Mathf.Min(i + 1, field.Length); j++)
            {
                if (location == j) return true;
            }
        }

        return false;
    }

    void handleChild(Transform newItem, Transform oldItem)
    {
        if (oldItem.tag == "Death")
        {
            Destroy(newItem.gameObject);
            fail();
        }
        else if (oldItem.tag == "Finish" && newItem.tag == "File")
        {
            Destroy(newItem.gameObject);
            Destroy(oldItem.gameObject);
            finishes -= 1;
        }
    }

    void fail()
    {
        gameObject.SetActive(false);
        Reset();
        onComplete(false, difficulty);
    }

    public override void Reset()
    {
        foreach (Transform parent in PlayArea.transform)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }
        field = new Block[13 * 13];
        return;
    }
}
