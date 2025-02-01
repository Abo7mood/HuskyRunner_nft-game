using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _lengthX, _startPosX;// start position and length for parallax
    [SerializeField] GameObject _cam;//cam
    [SerializeField] float _parallaxEffect;//the effect
    [SerializeField] SpriteRenderer _objectLength;//the objects length for parallax
    public bool showLength;
    private void Start()
    {
        _startPosX = transform.position.x;//est the start position to 0
        _lengthX = _objectLength.bounds.size.x;//est the length to the object sprite length
        if(!showLength)
        _objectLength.color = new Color(0, 0, 0, 0);

    }
    private void Update()
    {

        float tempx = (_cam.transform.position.x * (1 - _parallaxEffect));
        float distx = (_cam.transform.position.x * _parallaxEffect);
        transform.position = new Vector3(_startPosX + distx, transform.position.y, transform.position.z);

        if (tempx > _startPosX + _lengthX) _startPosX += _lengthX;
        else if (tempx < _startPosX - _lengthX) _startPosX -= _lengthX;


    }
}

