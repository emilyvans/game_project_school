using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : Item, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    float deltaModifierX = 1f;

    [SerializeField]
    float deltaModifierY = 1f;

    [HideInInspector]
    public Transform ParentAfterDrop;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Middle)
        {
            deltaModifierX = 1 / (UnityEngine.Screen.width / 1920f);
            deltaModifierY = 1 / (UnityEngine.Screen.height / 1080f);
        }

        ParentAfterDrop = transform.parent;
        transform.SetParent(transform.root, true);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;
        transform.localPosition += new Vector3(delta.x * deltaModifierX, delta.y * deltaModifierY);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(ParentAfterDrop);
        image.raycastTarget = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        deltaModifierX = 1 / (UnityEngine.Screen.width / 1920f);
        deltaModifierY = 1 / (UnityEngine.Screen.height / 1080f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
