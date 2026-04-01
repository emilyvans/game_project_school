using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lvlButton : MonoBehaviour
{

    [SerializeField]
    private int difficulty = 0;

    int PuzzleIndex = -1;

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void Deactivate()
    {
        GetComponent<Button>().interactable = false;
    }


    public void Activate()
    {
        GetComponent<Image>().color = Color.red;
        GetComponent<Button>().interactable = true;
    }
    public void Complete()
    {
        GetComponent<Image>().color = Color.green;
        GetComponent<Button>().interactable = false;
    }

    public int GetPuzzleIndex(int count)
    {
        if (PuzzleIndex == -1)
        {
            PuzzleIndex = Random.Range(0, count);
        }
        return PuzzleIndex;
    }


    void OnClick()
    {
        transform.parent.gameObject.GetComponent<levelSelect>().startPuzzle(difficulty, this);
    }
}
