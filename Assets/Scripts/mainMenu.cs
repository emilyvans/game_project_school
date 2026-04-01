using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{

    [SerializeReference]
    Button play;

    [SerializeReference]
    Button exit;

    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        exit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
