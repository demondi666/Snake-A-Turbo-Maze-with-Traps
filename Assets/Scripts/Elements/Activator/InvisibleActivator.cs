using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleActivator : Trigger
{
    private BoxCollider boxCollider;

    private void OnValidate()
    {
         boxCollider = GetComponent<BoxCollider>();
    }

    private void OnDrawGizmos()
    {
        if (boxCollider == null)
            return;

        Color originalColor = Gizmos.color;
        Gizmos.color = Color.red;

        Vector3 worldCenter = transform.TransformPoint(boxCollider.center);

        Vector3 worldSize = boxCollider.size;
        worldSize.Scale(transform.lossyScale);

        Gizmos.DrawWireCube(worldCenter, worldSize);

        Gizmos.color = originalColor;
    }

    protected override void Animate()
    {

    }
}
