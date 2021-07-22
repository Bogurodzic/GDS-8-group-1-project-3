using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _platformLayers;
    [SerializeField] private LayerMask _mirrorLayer;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _distanceRay = 100f;
    
    private void FixedUpdate()
    {
        CastLight();              
    }

    private void CastLight()
    {
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, transform.right, _distanceRay, _platformLayers);
        RaycastHit2D _hitPlayer = Physics2D.BoxCast(transform.position, new Vector2(0.7f, 0.7f), 0, transform.right, _distanceRay, _playerLayer);
        
        TouchPlayer(_hitPlayer);

        if (_hit)
        {
            DrawBeam(transform.position, _hit.point);
            if (_hit.collider.tag == "Mirror")
            {
                _lineRenderer.SetPosition(2, Vector2.Reflect((_hit.point - new Vector2(transform.position.x, transform.position.y)) * _distanceRay, _hit.normal));
                RaycastHit2D _hitPlayerMirrored = Physics2D.Raycast(_hit.point, _lineRenderer.GetPosition(2), _distanceRay, _playerLayer);
                TouchPlayer(_hitPlayerMirrored);
            }
            else
            {
                _lineRenderer.SetPosition(2, _lineRenderer.GetPosition(1));
            }
            
        }
        else
        {
            DrawBeam(transform.position, transform.transform.right * _distanceRay);
            _lineRenderer.SetPosition(2, _lineRenderer.GetPosition(1));
        }

    }

    private void TouchPlayer(RaycastHit2D hitPlayer)
    {
        if (hitPlayer)
        {
            hitPlayer.collider.gameObject.GetComponent<PlayerMovementController>().HandleSunEffect();
        }
    }

    private void DrawBeam(Vector2 startPosition, Vector2 endPosition)
    {
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.DrawRay(transform.position, transform.right);
    }
}
