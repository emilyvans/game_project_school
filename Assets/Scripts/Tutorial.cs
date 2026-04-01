using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    [SerializeReference]
    Button close;
    // Start is called before the first frame update
    void Start()
    {
        Show();
        close.onClick.AddListener(() =>
        {
            Pause.Instance.isPaused = false;
            Time.timeScale = 1;
        });
    }

    public void Show()
    {
        Pause.Instance.isPaused = true;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
