using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
struct Range
{
    public float min;
    public float max;
}
public class line : MonoBehaviour
{
    [SerializeField]
    bool running = false;
    bool reversed = false;

    [SerializeField]
    float CursorPos = 0.0f;
    Range range = new Range();

    [SerializeField]
    float BaseSpeed = 1;

    [SerializeField]
    float Speed = 1;

    [SerializeReference]
    public GameObject cursor;
    [SerializeReference]
    GameObject GoodZone;

    void Awake()
    {
        cursor.GetComponent<Image>().enabled = false;
    }

    void Start()
    {

    }

    public void init()
    {
        Rect rect = GetComponent<RectTransform>().rect;
        range.min = -(rect.width / 2);
        range.max = rect.width / 2;
        float length = GoodZone.GetComponent<RectTransform>().rect.width / 2;
        float GoodZonex = Mathf.Lerp(range.min + length, range.max - length, Random.value);
        GoodZone.transform.localPosition = new Vector3(GoodZonex, GoodZone.transform.localPosition.y, GoodZone.transform.localPosition.z);
    }

    public void start(int difficulty)
    {
        running = true;
        Speed += BaseSpeed + difficulty * 100;

    }

    private void FixedUpdate()
    {
        Vector2 current = cursor.GetComponent<RectTransform>().localPosition;
        current.x = Mathf.Lerp(range.min, range.max, CursorPos);
        cursor.GetComponent<RectTransform>().localPosition = current;

        if (!running) return;

        cursor.GetComponent<Image>().enabled = true;

        if (!reversed)
        {
            CursorPos += 0.00001f * Speed;
        }
        else
        {
            CursorPos -= 0.00001f * Speed;
        }
        if (CursorPos >= 1f)
        {
            CursorPos = 1f;
            reversed = true;
        }
        else if (CursorPos <= 0f)
        {
            CursorPos = 0f;
            reversed = false;
        }
    }

    public bool StopCursor()
    {
        if (running)
        {
            float cursorPos = cursor.GetComponent<RectTransform>().localPosition.x;
            float CursorWidth = cursor.GetComponent<RectTransform>().rect.width;
            float goodZonePos = GoodZone.GetComponent<RectTransform>().localPosition.x;
            float goodZoneWidth = GoodZone.GetComponent<RectTransform>().rect.width;
            bool result = checkOverlap(cursorPos, CursorWidth, goodZonePos, goodZoneWidth);
            running = false;
            cursor.GetComponent<Image>().enabled = false;
            GoodZone.GetComponent<Image>().enabled = false;
            GetComponent<Image>().color = result ? Color.green : Color.red;
            return result;
        }
        return false;
    }

    private bool checkOverlap(float cursorPos, float cursorWidth, float goodZonePos, float goodZoneWidth)
    {
        bool condition = (cursorPos - cursorWidth / 2) > (goodZonePos - goodZoneWidth / 2);
        condition &= (cursorPos + cursorWidth / 2) < (goodZonePos + goodZoneWidth / 2);
        return condition;
    }
}
