using TMPro;
using UnityEngine;

public class Hitman : MonoBehaviour
{
    [SerializeReference]
    Clock clock;

    [SerializeReference]
    Door door;

    bool first = true;

    float count = 0f;

    [SerializeField]
    int percent = 5;

    float delay = 30f;

    void Start()
    {

    }

    void FixedUpdate()
    {

        if (first)
        {
            delay = 60f;
            first = false;
        }

        if (!(count > delay))
        {
            count += Time.deltaTime;
            return;
        }
        if (Random.Range(0, 101) < percent)
        {
            if (!door.isLocked())
            {
                GameOverCanvas.Instance.GameOver("A Hitman");
                return;
            }
        }
        count = 0f;
    }
}