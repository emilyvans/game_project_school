using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastScreen : MonoBehaviour
{
    [SerializeReference]
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
