
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Wifi : MonoBehaviour
{

    int VPNCount = 0;

    int ActiveVPNS = 0;

    float TimeOnNetwork = 0f;

    float MaxTimeOnNetwork = 5 * 60;

    string[] WifiNames = {
        "DowntownCafeWiFi",
        "UrbanNestNetwork",
        "MetroMingleNet",
        "HomelyHarbor",
        "CityViewConnections"
    };

    [SerializeReference]
    Button[] buttons = new Button[5];

    [SerializeReference]
    Button DisconnectButton;

    string CurrentWifi;

    [SerializeReference]
    Slider slider;

    [SerializeReference]
    Canvas NoWifi;

    // Start is called before the first frame update
    void Start()
    {
        CurrentWifi = "";
        DisconnectButton.interactable = false;
        slider.maxValue = VPNCount;
        slider.onValueChanged.AddListener((e) => ActiveVPNS = Mathf.CeilToInt(e));
        for (int i = 0; i < buttons.Length; i++)
        {
            string name = WifiNames[i];
            Button currentButton = buttons[i];
            currentButton.GetComponentInChildren<TMP_Text>().text = name;
            currentButton.onClick.AddListener(() =>
            {
                if (CurrentWifi != name)
                {
                    CurrentWifi = name;
                    TimeOnNetwork = 0f;
                    currentButton.interactable = false;
                    for (int j = 0; j < buttons.Length; j++)
                        if (buttons[j] != currentButton)
                            buttons[j].interactable = true;
                }
                DisconnectButton.interactable = true;
            });
        }
        DisconnectButton.onClick.AddListener(() =>
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].interactable = true;
            CurrentWifi = "";
            TimeOnNetwork = 0f;
            DisconnectButton.interactable = false;
        });
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CurrentWifi == "")
        {
            NoWifi.enabled = true;
            return;
        }
        NoWifi.enabled = false;
        TimeOnNetwork += Time.deltaTime;
        if (TimeOnNetwork > MaxTimeOnNetwork + (30 * ActiveVPNS))
        {
            GameOverCanvas.Instance.GameOver("The Police");
        }
    }

    public void AddVPN()
    {
        VPNCount += 1;
        slider.maxValue = VPNCount;
    }
}
