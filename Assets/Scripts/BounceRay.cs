using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceRay : MonoBehaviour
{
    public int RayCount = 2;

    void Update()
    {
        CastRay(transform.position, transform.forward);
    }

    void CastRay(Vector3 position, Vector3 direction)
    {
        for (int i = 0; i < RayCount; i++)
        {
            Ray ray = new Ray (position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10, 1))
            {
                Debug.DrawLine(position, hit.point, Color.red);
                position = hit.point;
                direction = hit.point;
            }
            else
            {
                Debug.DrawRay(position, direction * 10, Color.blue);
                break;
            }
                

    }
}

