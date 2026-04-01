using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour, IUsable
{

    public static Screen Instantace;

    [SerializeReference]
    public Camera FirstPersonCam;

    [SerializeReference]
    public Camera ScreenCam;

    [SerializeReference]
    Canvas firstPersonCanvas;

    [SerializeReference]
    public Canvas ScreenCanvas;


    [SerializeReference]
    RenderTexture renderTexture;

    private void Awake()
    {
        Instantace = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ScreenCam.targetTexture = renderTexture;
        ScreenCam.GetComponent<AudioListener>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Use()
    {
        firstPersonCanvas.enabled = false;
        ScreenCam.targetTexture = null;
        ScreenCam.GetComponent<AudioListener>().enabled = true;
        FirstPersonCam.enabled = false;
        FirstPersonCam.GetComponent<AudioListener>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void exit()
    {
        firstPersonCanvas.enabled = true;
        ScreenCam.targetTexture = renderTexture;
        ScreenCam.GetComponent<AudioListener>().enabled = false;
        FirstPersonCam.enabled = true;
        FirstPersonCam.GetComponent<AudioListener>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
