using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MineSweeperButton : MonoBehaviour
{

    [SerializeReference]
    public TMP_Text text;

    [SerializeReference]
    Image image;

    bool hasBeenClicked = false;
    bool isMine;
    public int number;

    Vector2Int position;
    // Start is called before the first frame update
    void Start()
    {
        image.enabled = false;
        GetComponent<Button>().onClick.AddListener(onclick);
    }

    public void initButton(int content, int x, int y)
    {
        number = content;
        hasBeenClicked = false;
        isMine = content == -1;
        position = new Vector2Int(x, y);
        text.text = "";
    }

    public void reveal()
    {
        text.text = isMine ? "X" : number.ToString();
        GetComponent<Button>().enabled = false;
    }

    void onclick()
    {
        if (number == 0)
        {
            text.text = isMine ? "X" : number.ToString();
            minesweeper.Instance.revealZero(position.x, position.y);
        }
        else
        {
            if (!minesweeper.Instance.checkComplete(position.x, position.y, isMine | text.text == "F"))
            {
                if (hasBeenClicked)
                {
                    text.text = isMine ? "X" : number.ToString();
                    GetComponent<Button>().enabled = false;
                    if (isMine)
                    {
                        Invoke("fail", 0.5f);
                    }
                }
                else
                {
                    text.text = "F";
                    hasBeenClicked = true;
                }

            }
            else
            {
            }
        }
    }

    void fail()
    {
        minesweeper.Instance.fail();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
