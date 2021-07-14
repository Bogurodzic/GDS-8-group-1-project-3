using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxFX : MonoBehaviour
{
    [SerializeField] private GameObject _cam;
    [SerializeField] private float _parallaxEffect;

    private float _length;
    private float _startPos;

    private void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float _temp = (_cam.transform.position.x * (1 - _parallaxEffect));
        float _dist = (_cam.transform.position.x *_parallaxEffect);
        
        transform.position = new Vector3(_startPos + _dist, transform.position.y, transform.position.z);

        if (_temp > _startPos + _length)
        {
            _startPos += _length*1.55f;
        } 
        else if (_temp < _startPos - _length)
        {
            _startPos -= _length*1.55f;
        }
    }
    //[SerializeField] private Transform[] _bgLayers;
    //[SerializeField] private float _smoothing = 1f;

    //private Transform _cam;
    //private Vector3 _previousCamPos;
    //private float[] _parallaxScales;

    //private void Awake()
    //{
    //    _cam = Camera.main.transform;
    //}

    //private void Start()
    //{
    //    _previousCamPos = _cam.position;
    //    _parallaxScales = new float [_bgLayers.Length];

    //    for (int i = 0; i < _bgLayers.Length; i++)
    //    {
    //        _parallaxScales[i] = _bgLayers[i].position.z;// * -1;
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    for (int i = 0; i < _bgLayers.Length; i++)
    //    {
    //        float _parallax = (_previousCamPos.x - _cam.position.x) * _parallaxScales[i];
    //        float _backgroundTargetPosX = _bgLayers[i].position.x + _parallax;

    //        Vector3 backgroundTargetPos = new Vector3(_backgroundTargetPosX, _bgLayers[i].position.y, _bgLayers[i].position.z);

    //        _bgLayers[i].position = Vector3.Lerp(_bgLayers[i].position, backgroundTargetPos, _smoothing * Time.deltaTime);
    //    }
    //    _previousCamPos = _cam.position;
    //}

}
