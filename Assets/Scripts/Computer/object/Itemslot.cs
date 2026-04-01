using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int location;

    public Action<Transform, Transform> handleChild;
    public Action<int, int, Block> move;

    public delegate bool CanAccess(int location);

    public CanAccess canAccess;
    public void OnDrop(PointerEventData eventData)
    {
        Transform dropped = eventData.pointerDrag.transform;
        if (transform.childCount != 0)
        {
            if (!canAccess(location)) return;
            handleChild(dropped, transform.GetChild(0));
            return;
        }
        else
        {
            DragableItem item = dropped.GetComponent<DragableItem>();
            SemiDragableItem semiItem = dropped.GetComponent<SemiDragableItem>();

            if (item != null)
            {
                item.ParentAfterDrop = transform;
                move(location, item.location, Block.CURSOR);
                item.location = location;
            }
            else if (semiItem != null)
                if (canAccess(location))
                {
                    semiItem.ParentAfterDrop = transform;
                    move(location, semiItem.location, Block.FILEBLOCK);
                    semiItem.location = location;
                }
        }
    }
}
