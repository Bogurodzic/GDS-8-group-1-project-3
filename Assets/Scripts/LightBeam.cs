using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _platformLayers;
    [SerializeField] private float _distanceRay = 100f;


    private void FixedUpdate()
    {
        CastLight();
    }

    private void CastLight()
    {
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, transform.right, _distanceRay, _platformLayers);
        if (_hit)
        {
            DrawBeam(transform.position, _hit.point);
            _lineRenderer.SetPosition(2, Vector2.Reflect((_hit.point - new Vector2(transform.position.x, transform.position.y)).normalized * _distanceRay, _hit.normal));
        }
        else
        {
            DrawBeam(transform.position, transform.transform.right * _distanceRay);
            _lineRenderer.SetPosition(2, _lineRenderer.GetPosition(1));
        } 
    }

    private void DrawBeam(Vector2 startPosition, Vector2 endPosition)
    {
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);
    }
}
