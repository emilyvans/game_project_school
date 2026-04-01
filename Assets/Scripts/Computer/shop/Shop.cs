using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    [SerializeReference]
    Wifi wifi;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Buy(string name, float cost)
    {
        if (Money.instance.Sub(cost))
            switch (name)
            {
                case "VPN":
                    wifi.AddVPN();
                    break;
                case "Fuel":
                    Generator.instance.addFuel();
                    break;
                default:
                    Money.instance.Add(cost);
                    break;
            }
    }
}
