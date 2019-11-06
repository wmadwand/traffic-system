using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DraggedDirection
{
    Up,
    Down,
    Right,
    Left
}

public class Crossroads2 : MonoBehaviour, IEndDragHandler,IDragHandler
{
    Crossroads crossRoadsBrain;

    private void Awake()
    {
        crossRoadsBrain = GetComponent<Crossroads>();


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        GetDragDirection(dragVectorDirection);

        var result = GetDragDirection(dragVectorDirection);
        crossRoadsBrain.CallTrafficLights(result);
    }



    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        Debug.Log(draggedDir);
        return draggedDir;
    }

    private Vector2 GetDragDirectionVector(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);

        Vector2 vecDir;

        if (positiveX > positiveY)
        {
            vecDir = (dragVector.x > 0) ? Vector2.right : Vector2.left;
        }
        else
        {
            vecDir = (dragVector.y > 0) ? Vector2.up : Vector2.down;

        }

        Debug.Log(vecDir);
        return vecDir;
    }

    private Vector2 GetDragDirectionVectorInverse(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);

        Vector2 vecDir;

        if (positiveX > positiveY)
        {
            vecDir = (dragVector.x > 0) ? Vector2.left : Vector2.right;
        }
        else
        {
            vecDir = (dragVector.y > 0) ? Vector2.down : Vector2.up;

        }

        Debug.Log(vecDir);
        return vecDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}
