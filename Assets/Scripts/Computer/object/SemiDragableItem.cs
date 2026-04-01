using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SemiDragableItem : Item, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    float deltaModifierX = 1f;

    [SerializeField]
    float deltaModifierY = 1f;

    float DeltaModifier = 1f;

    public delegate bool CanMove(int location);

    public CanMove canMove;

    [HideInInspector]
    public Transform ParentAfterDrop;

    bool moving = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        bool canmove = canMove(location);
        if (!canmove) return;
        ParentAfterDrop = transform.parent;
        transform.SetParent(transform.root, true);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        moving = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!moving) return;
        Vector2 delta = eventData.delta * DeltaModifier;
        transform.localPosition += new Vector3(delta.x * deltaModifierX, delta.y * deltaModifierY);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!moving) return;
        transform.SetParent(ParentAfterDrop);
        image.raycastTarget = true;
        moving = false;
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
