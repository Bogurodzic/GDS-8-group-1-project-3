using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _platformLayers;
    [SerializeField] private float _distanceRay = 100f;


    private void Update()
    {
        CastLight();
    }

    private void CastLight()
    {
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, transform.right, _distanceRay, _platformLayers);
        if (_hit)
        {
            DrawBeam(transform.position, _hit.point);
            //Debug.Log(transform.position);
            //Debug.Log(_hit.point);
            //Debug.Log(_hit.collider.GetType());
        }
        else
        {
            DrawBeam(transform.position, transform.transform.right * _distanceRay);
        } 
    }

    private void DrawBeam(Vector2 startPosition, Vector2 endPosition)
    {
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);
    }
}
