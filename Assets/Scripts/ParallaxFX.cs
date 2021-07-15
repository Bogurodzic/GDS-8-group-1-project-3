using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxFX : MonoBehaviour
{
    [SerializeField] private GameObject _cam;
    [SerializeField] private float _parallaxEffect;
    [SerializeField] private float _offset = 1.55f;

    private float _length;
    private float _startPos;

    private void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float _temp = (_cam.transform.position.x * (1 - _parallaxEffect));
        float _dist = (_cam.transform.position.x *_parallaxEffect);
        
        transform.position = new Vector3(_startPos + _dist, transform.position.y, transform.position.z);

        if (_temp > _startPos + _length)
        {
            _startPos += _length * _offset;
        } 
        else if (_temp < _startPos - _length)
        {
            _startPos -= _length * _offset;
        }
    }
}
