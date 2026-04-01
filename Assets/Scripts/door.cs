using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IUsable
{
    [SerializeField]
    bool locked = false;

    bool canChange = true;
    new AudioSource audio;

    bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int hour = (int)Clock.instance.getHour();
        if (hour % 3 == 0 && canChange)
        {
            StartCoroutine("lockDoor");
            canChange = false;
            if (!first)
            {
                audio.Play();
            }
        }
        else if (hour % 3 != 0)
        {
            canChange = true;
        }
        if (first) first = false;
    }

    public bool isLocked()
    {
        return locked;
    }

    public void Use()
    {
        locked = true;
        audio.Play();

    }

    IEnumerable lockDoor()
    {
        yield return new WaitForSeconds(10);
        locked = false;
    }
}
