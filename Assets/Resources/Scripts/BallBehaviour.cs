using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class BallBehaviour : MonoBehaviour
{
    #region Fields

    public float gravity = 10f;

    public float startYCoord = 0;
    public float currentYCoord = 0;

    public float startTime = 0;
    public float currentTime = 0;

    public float startSpeed = 0;
    public float currentSpeed = 0;

    #endregion

    #region Methods
    
    void Start()
    {
        startYCoord = transform.position.y;
        startTime = Time.time;
        startSpeed = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        startSpeed = -currentSpeed;
        startYCoord = transform.position.y;
        startTime = Time.time;
    }

    private void SimplePhysics()
    {
        Vector3 pos = transform.position;

        currentTime = Time.time - startTime;
        currentSpeed = startSpeed - gravity * currentTime;

        currentYCoord = startYCoord + startSpeed * currentTime - (gravity * currentTime * currentTime) / 2;

        pos.y = currentYCoord;
        transform.position = pos;
    }

    void FixedUpdate()
    {
        SimplePhysics();
    }

    #endregion
}
