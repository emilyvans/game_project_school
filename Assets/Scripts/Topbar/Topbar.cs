using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Topbar : MonoBehaviour
{
    [Header("Text Boxes")]

    [SerializeReference]
    TMP_Text PC_Money;

    [SerializeReference]
    TMP_Text FirstPerson_Money;

    [SerializeReference]
    TMP_Text PC_Time;

    [SerializeReference]
    TMP_Text FirstPerson_Time;

    [Header("Buttons")]

    [SerializeReference]
    Button Wifi;

    [SerializeReference]
    Button shutdown;

    [SerializeReference]
    Player player;

    Clock clock;
    Money money;
    Wifi wifi;

    // Start is called before the first frame update
    void Start()
    {
        clock = Clock.instance;
        money = GetComponent<Money>();
        wifi = GetComponent<Wifi>();
        shutdown.onClick.AddListener(shutdownPC);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        string time = clock.GetTimeText();
        string moneyText = money.getMoneyText();
        PC_Time.text = time;
        PC_Money.text = moneyText;
        FirstPerson_Time.text = time;
        FirstPerson_Money.text = moneyText;

    }

    void shutdownPC()
    {
        player.OnExitComputer();
    }
}
