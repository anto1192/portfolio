using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasFadeAnimation : MonoBehaviour
{
    private List<CanvasRenderer> canvasRendererList = new List<CanvasRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        FindAlphaElements(transform);

        //Debug.Log(gameObject.name + ": " + canvasRendererList.Count + " elements");

        foreach (CanvasRenderer canvasRendererElement in canvasRendererList)
        {
            canvasRendererElement.SetAlpha(0);
        }
    }

    private void FindAlphaElements(Transform parent)
    {
        //find the elements in the hierarchy of this object who has a renderer; if the object has a renderer we can fade the element through the alpha of its color

        //first check the element itself
        CheckSingleElement(parent.gameObject);
        //Then check its childrens
        FindAlphaElementsInChildren(parent);
    }

    private void FindAlphaElementsInChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            //get the all the childs of the element
            GameObject child = parent.GetChild(i).gameObject;
            //check the current child
            CheckSingleElement(child);

            //if this child also has childs check its childs in a recoursive way
            if (child.transform.childCount > 0)
            {
                FindAlphaElementsInChildren(child.transform);
            }
        }
    }

    private void CheckSingleElement(GameObject element)
    {
        //check if this element has a renderer; if so it is added to the list
        CanvasRenderer renderer = element.GetComponent<CanvasRenderer>();
        if (renderer != null)
        {
            canvasRendererList.Add(renderer);
        }
    }

    public void FadeInCanvas()
    {
        StopAllCoroutines();
        foreach(CanvasRenderer canvasRendererElement in canvasRendererList)
        {
            StartCoroutine(FadeInElement(canvasRendererElement));
        }
    }

    public void FadeOutCanvas()
    {
        StopAllCoroutines();
        foreach (CanvasRenderer canvasRendererElement in canvasRendererList)
        {
            StartCoroutine(FadeOutElement(canvasRendererElement));
        }
    }

    private IEnumerator FadeInElement(CanvasRenderer canvasRendererElement)
    {
        float currentAlpha = canvasRendererElement.GetColor().a;
        for (float i = 0; i < Constant.FADE_TIME; i += Time.deltaTime)
        {
            canvasRendererElement.SetAlpha(i * (1 - currentAlpha) + currentAlpha);            
            yield return null;
        }
        canvasRendererElement.SetAlpha(1);
    }

    private IEnumerator FadeOutElement(CanvasRenderer canvasRendererElement)
    {
        float currentAlpha = canvasRendererElement.GetColor().a;
        for (float i = Constant.FADE_TIME; i > 0; i -= Time.deltaTime)
        {
            canvasRendererElement.SetAlpha(i * currentAlpha);
            yield return null;
        }
        canvasRendererElement.SetAlpha(0);
    }
}
