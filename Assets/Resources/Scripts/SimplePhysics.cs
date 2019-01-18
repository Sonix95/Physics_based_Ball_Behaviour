using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class SimplePhysics : MonoBehaviour
{
    #region Fields

    public float gravity = 10f;

    Transform platform;

    private float _currentYCoord = 0;
    public float CurrentYCoord
    {
        get => _currentYCoord;
        set {
            if (value < platform.transform.position.y - 50 || value > platform.transform.position.y + 150)
            {
                Destroy(gameObject);
            }
            else
                _currentYCoord = value;
        }
    }

    private float _startYCoord = 0;

    public float startSpeed = 0;
    private float _currentSpeed = 0;

    private float _startTime = 0;
    private float _currentTime = 0;

    #endregion

    #region Methods

    void Start()
    {
        platform = GameObject.FindWithTag("Plane").transform;
        _startYCoord = transform.position.y;
        _startTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        startSpeed = -_currentSpeed;
        _startYCoord = transform.position.y;
        _startTime = Time.time;
    }

    private void ChangeCoordinate()
    {
        Vector3 pos = transform.position;

        _currentTime = Time.time - _startTime;
        _currentSpeed = startSpeed - gravity * _currentTime;

        CurrentYCoord = _startYCoord + startSpeed * _currentTime - (gravity * _currentTime * _currentTime) / 2;

        pos.y = CurrentYCoord;
        transform.position = pos;
    }

    void FixedUpdate()
    {
        ChangeCoordinate();
    }

    #endregion
}
