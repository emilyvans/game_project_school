using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;
using UnityEngine;
using UnityEngine.UI;

public abstract class PuzzleScreen : MonoBehaviour
{

    [SerializeReference]
    protected RawImage PlayArea;

    [SerializeReference]
    Tutorial tutorial;

    //void onComplete()
    public Action<bool, int> onComplete;

    protected int difficulty;

    bool first = true;

    public virtual void StartPuzzle(int difficulty)
    {
        if (first)
        {
            tutorial.Show();
            first = false;
        }
        gameObject.SetActive(true);
        this.difficulty = difficulty;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public virtual void Reset()
    {
        for (int i = 0; i < PlayArea.transform.childCount; i++)
            Destroy(PlayArea.transform.GetChild(i).gameObject);
    }
}