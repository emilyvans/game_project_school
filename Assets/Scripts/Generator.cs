using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public static Generator instance { get; private set; } = null;

    [SerializeReference]
    Canvas nofuel;

    [SerializeReference]
    TMP_Text lowFuel;

    [SerializeField]
    float fuel = 100f;

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
        if (fuel > 25)
        {
            lowFuel.enabled = false;
            nofuel.enabled = false;
        }
        else if (fuel > 0)
        {
            lowFuel.enabled = true;
            nofuel.enabled = false;
        }
        else
        {
            nofuel.enabled = true;
            return;
        }
        fuel -= 0.35f * Time.deltaTime;
    }

    public void addFuel()
    {
        fuel += 25;
    }
}
