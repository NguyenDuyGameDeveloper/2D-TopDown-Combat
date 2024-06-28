using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update()
    {
        FaceMouse();
    }
    private void FaceMouse()
    {
        Vector3 mouPosition = Input.mousePosition;
        mouPosition = Camera.main.ScreenToWorldPoint(mouPosition);

        Vector2 direction = transform.position - mouPosition;
        transform.right = -direction;
    }
}
