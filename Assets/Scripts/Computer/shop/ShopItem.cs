using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    string Name;

    [SerializeField]
    float cost;

    [SerializeField]
    int stock;

    [SerializeField]
    TMP_Text NameText;

    [SerializeField]
    Button BuyButton;

    [SerializeReference]
    Shop shop;

    // Start is called before the first frame update
    void Start()
    {
        NameText.text = Name;
        BuyButton.GetComponentInChildren<TMP_Text>().text = "BUY " + cost.ToString() + "PTC";
        BuyButton.onClick.AddListener(() =>
        {
            if (stock == -1)
            {
                shop.Buy(Name, cost);
            }
            else if (stock != 0)
            {
                shop.Buy(Name, cost);
                stock -= 1;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
