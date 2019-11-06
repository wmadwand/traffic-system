using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum LightColor
{
    red,
    green,
    yellow
}

public class TrafficLight : MonoBehaviour, IPointerClickHandler
{
    public static event Action<int, LightColor> OnLightChange;

    public LightColor currentLight;
    public Material mat;
    public MeshRenderer mesh;

    public LightColor prevLight;

    public int id;

    Coroutine cor;

    public bool IsGreenLight()
    {
        return currentLight == LightColor.green;
    }

    private void Awake()
    {
        currentLight = LightColor.red;

        mat = mesh.materials[2];
        mat.color = Color.red;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");

        if (cor != null)
        {
            return;
        }

        ChangeColor();
    }

    public void ChangeColor()
    {
        cor = StartCoroutine(ChangeCo(() => { OnLightChange(id, currentLight); }));
    }

    IEnumerator ChangeCo(Action callback)
    {
        prevLight = currentLight;

        yield return new WaitForSeconds(.5f);

        mat.color = Color.yellow;

        yield return new WaitForSeconds(.5f);

        if (prevLight == LightColor.red)
        {
            currentLight = LightColor.green;
            mat.color = Color.green;
        }
        else
        {
            currentLight = LightColor.red;
            mat.color = Color.red;
        }

        Debug.Log($"curr light:{currentLight}");

        cor = null;
        callback();
    }

}
