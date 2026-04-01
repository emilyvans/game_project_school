using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Clock instance { get; private set; } = null;

    float time = 6 * 60;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
    }

    public float getHour()
    {
        float localTime = time % 1440;
        return localTime / 60;
    }

    public string GetTimeText()
    {
        float localTime = (time) % 1440;
        int Hour = Mathf.FloorToInt(localTime / 60);
        int Min = Mathf.CeilToInt(localTime - (Hour * 60));
        return String.Format("{0}:{1:00}", Hour, Min);
    }
}
