using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public bool isPaused { get; set; } = false;

    public static Pause Instance { get; private set; }

    [SerializeReference]
    Canvas pauseScreen;

    [SerializeReference]
    Button ContinueButton;

    [SerializeReference]
    Button ExitButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pauseScreen.enabled = false;
        ContinueButton.onClick.AddListener(() =>
        {
            Set(false);
        });
        ExitButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    public void Set(bool isPaused)
    {
        this.isPaused = isPaused;
        Cursor.visible = isPaused;
        pauseScreen.enabled = isPaused;
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
