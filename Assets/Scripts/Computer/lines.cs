using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lines : PuzzleScreen
{
    private bool running = false;

    [SerializeField]
    bool used;

    Rect PlayAreaRect;

    [SerializeField]
    GameObject linePrefab;

    [SerializeReference]
    Button button;

    [SerializeField]
    List<GameObject> lineList = new List<GameObject>();

    int CurrentIndex = 0;
    int score;

    private int WinningScore;
    // Start is called before the first frame update
    void Start()
    {
        PlayAreaRect = PlayArea.rectTransform.rect;
        button.onClick.AddListener(click);
        score = 0;
    }

    void FixedUpdate()
    {

    }

    void click()
    {
        if (running)
        {
            int state = lineList[CurrentIndex].GetComponent<line>().StopCursor() ? 1 : 0;
            score += state;
            CurrentIndex++;
            if (CurrentIndex == lineList.Count)
            {
                running = false;
                foreach (GameObject line in lineList)
                {
                    Destroy(line);
                }
                lineList.Clear();
                CurrentIndex = 0;
                gameObject.SetActive(false);
                onComplete(score >= WinningScore, difficulty);
                score = 0;
            }
            else
            {
                lineList[CurrentIndex].GetComponent<line>().start(difficulty);
            }
        }
    }

    public override void StartPuzzle(int difficulty)
    {
        running = true;
        CurrentIndex = 0;
        PlayAreaRect = PlayArea.rectTransform.rect;
        base.StartPuzzle(difficulty);
        for (int i = 1; i <= difficulty; i++)
        {
            GameObject line = Instantiate(linePrefab, PlayArea.transform);
            Vector3 modified = line.transform.localPosition;
            modified.y = (PlayAreaRect.height / 2f) - i * 100f;
            line.transform.localPosition = modified;
            line.GetComponent<line>().init();
            lineList.Add(line);
        }

        WinningScore = (int)MathF.Ceiling(lineList.Count / 2f);
        lineList[0].GetComponent<line>().start(difficulty);
    }
    public override void Reset()
    {
        base.Reset();
        lineList.Clear();
    }
}
