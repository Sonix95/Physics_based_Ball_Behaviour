using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class BallBehaviour : MonoBehaviour
{

    public AnimationCurve plot = new AnimationCurve();

    public float g = 9.81f;
    public float h = 0f;
    public float vUp = 0;
    public float startFall;
    float curTimeFall;

    public float vOnCollision = 0f;

    public float hMax = 0;
    float curY;

    void Start()
    {
        h = transform.position.y;
        startFall = Time.time;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //    Debug.Log("Enter");
    //    //    transform.position = new Vector3(0, startH, 0);
    //    //    vUp = Mathf.Sqrt(2 * g * (startH - transform.position.y));
    //    fall = false;        
    //    startFall = Time.time;
    //    hMax = (Mathf.Pow(vOnCollision * Mathf.Sin(90f), 2) / (2 * g));
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    vOnCollision = Mathf.Sqrt(2 * g * (startH - transform.position.y));
    //}


    public bool fall = true;


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Теперь не падаем!");

        Debug.Log($"time: {curTimeFall}");
        vLast = Mathf.Sqrt(2 * g * (h - transform.position.y));
        Debug.Log($"vLast: {vLast}");

        startFall = Time.time;
        fall = false;
    }
    public float vLast = 0;
    private void FixedUpdate()
    {
        if (fall)
        {
            curTimeFall = Time.time - startFall;
            curY = h - ((g * Mathf.Pow(curTimeFall, 2)) / 2);           
        }

        if (!fall)
        {
            curTimeFall = Time.time - startFall;
            float vaa = vLast - g * curTimeFall;
            curY = vLast * curTimeFall - (g * Mathf.Pow(curTimeFall, 2) / 2);
            if (vaa < 0)
            {
                Debug.Log("Падаем!!");
                h = transform.position.y;
                startFall = Time.time;
                fall = true;
                return;

            }
            
        }
        plot.AddKey(Time.time, curY);

        transform.position = new Vector3(
               transform.position.x,
               curY,
               transform.position.z);


    }

}


/*
  изменение Y при падении ====> startH - (g * Time.time * Time.time) / 2
 
    





  
 */
