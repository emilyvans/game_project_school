using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{

    [SerializeReference]
    TMP_Text GameOverText;

    public static GameOverCanvas Instance;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver(string Enemy)
    {
        Time.timeScale = 0;
        GameOverText.text = Enemy;
        GetComponent<Canvas>().enabled = true;
    }
}
