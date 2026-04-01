using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using UnityEngine.U2D;

public class levelSelect : MonoBehaviour
{
    [SerializeReference]
    List<PuzzleScreen> puzzles = new List<PuzzleScreen>();

    [SerializeField]
    List<lvlButton> levels = new List<lvlButton>();

    [SerializeField]
    List<int> levelsPerStage = new List<int>();

    [SerializeReference]
    Canvas WonScreen;

    [SerializeField]
    int stage = 0;

    int currentLevelIndex = 0;

    int completedLevel = 0;

    lvlButton currentLvl;

    // Start is called before the first frame update
    void Start()
    {
        foreach (PuzzleScreen puzzle in puzzles)
        {
            puzzle.onComplete = onLevelComplete;
            puzzle.Deactivate();
        }
        for (int i = 0; i < levels.Count; i++)
        {
            lvlButton level = levels[i];
            level.Deactivate();
        }
        for (int i = 0; i < levelsPerStage[stage]; i++)
        {
            lvlButton level = levels[i];
            level.Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onLevelComplete(bool won, int difficulty)
    {
        if (!won) return;
        completedLevel += 1;
        Money.instance.Add(Random.Range(0, 5 * difficulty));
        currentLvl.Complete();
        if (completedLevel == levelsPerStage[stage])
        {
            currentLevelIndex += levelsPerStage[stage];
            stage += 1;
            completedLevel = 0;
            if (stage >= levelsPerStage.Count)
            {
                WonScreen.enabled = true;
                return;
            }
            for (int i = 0; i < levelsPerStage[stage]; i++)
            {
                lvlButton level = levels[currentLevelIndex + i];
                level.Activate();
            }
        }

    }

    public void startPuzzle(int difficulty, lvlButton level)
    {
        int index = level.GetPuzzleIndex(puzzles.Count);
        PuzzleScreen puzzle = puzzles[index];
        puzzle.StartPuzzle(difficulty);
        currentLvl = level;
    }
}
