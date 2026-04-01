using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VPNslider : MonoBehaviour
{
    [SerializeReference]
    TMP_Text text;

    [HideInInspector]
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        text.text = slider.value.ToString();
        slider.onValueChanged.AddListener((e) =>
        {
            text.text = e.ToString();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
