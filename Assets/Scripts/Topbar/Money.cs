using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public static Money instance { get; private set; } = null;

    float money = 200;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Add(float ToAdd)
    {
        money += ToAdd;
    }

    public bool Sub(float ToSub)
    {
        if ((money - ToSub / 4) > 0)
        {
            money -= ToSub;
            return true;
        }
        return false;
    }


    public string getMoneyText()
    {
        return String.Format("PTC: {0}", money);
    }
}
