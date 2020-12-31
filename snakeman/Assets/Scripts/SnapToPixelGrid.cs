﻿using UnityEngine;

public class SnapToPixelGrid : MonoBehaviour
{
    [SerializeField]
    private int pixelsPerUnit = 16;

    private Transform parent;

    private void Start()
    {
        parent = transform.parent;
    }

    private void LateUpdate()
    {
        Vector3 newLocalPosition = Vector3.zero;

        newLocalPosition.x = (Mathf.Round(parent.position.x * pixelsPerUnit) / pixelsPerUnit) - parent.position.x;
        newLocalPosition.y = (Mathf.Round(parent.position.y * pixelsPerUnit) / pixelsPerUnit) - parent.position.y;

        transform.localPosition = newLocalPosition;
    }
}