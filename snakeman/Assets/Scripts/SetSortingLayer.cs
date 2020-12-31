using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSortingLayer : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 5000;

    [SerializeField]
    private int offset = 0;

    [SerializeField]
    private bool runOnlyOnce = false;

    private Renderer myRenderer;

    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        myRenderer.sortingOrder = (int)(sortingOrderBase - (transform.position.y*10) - offset);
        if (runOnlyOnce)
        {
            Destroy(this);
        }
    }
}
