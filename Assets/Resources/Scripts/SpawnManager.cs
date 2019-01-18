using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Fields

    [Header("Primitive Settings")]
    public PrimitiveType buildinPrimitive = PrimitiveType.Sphere;
    public float gravity = 9.81f;
    public float startSpeed = 0f;

    [Header("Random Primitive Settings")]
    public bool useRandomGravity = false;
    public bool useRandomPrimitive = false;
    public bool useRandomStartSpeed = false;    

    [Header("Invoke Time")]
    public float startCall = 2f;
    public float repeatTime = 2f;

    [Header("X coordinates")]
    public float xMin = -20f;
    public float xMax = 20f;

    [Header("Y coordinates")]
    public float yMin = 5f;
    public float yMax = 20f;

    #endregion

    #region Methods

    private void OnEnable()
    {
        StartCoroutine(instObj());
    }

    private void OnDisable()
    {
        StopCoroutine(instObj());
    }

    IEnumerator instObj()
    {
        GeneratePrimitive();
        yield return new WaitForSeconds(repeatTime);
        StartCoroutine(instObj());
    }

    private PrimitiveType GetRandomPrimitive()
    {
        int randomPrimitive = Random.Range(0, 4);
        PrimitiveType primitive = PrimitiveType.Sphere;

        switch(randomPrimitive)
        {
            case 0:
                primitive = PrimitiveType.Sphere;
                break;
            case 1:
                primitive = PrimitiveType.Capsule;
                break;
            case 2:
                primitive = PrimitiveType.Cylinder;
                break;
            case 3:
                primitive = PrimitiveType.Cube;
                break;
        }

        return primitive;
    }

    private void GeneratePrimitive()
    {
        Vector3 position = Vector3.zero;
        position.x = Random.Range(xMin, xMax);
        position.y = Random.Range(yMin, yMax);

        GameObject primitiveObject = GameObject.CreatePrimitive(useRandomPrimitive ? GetRandomPrimitive() : buildinPrimitive);
        primitiveObject.transform.position = position;
        primitiveObject.AddComponent<Rigidbody>().useGravity = false;
        primitiveObject.AddComponent<SimplePhysics>().gravity = useRandomGravity ? Random.Range(-10,10) : gravity;
        primitiveObject.GetComponent<SimplePhysics>().startSpeed = useRandomStartSpeed ? Random.Range(-20, 20) : startSpeed;
    }

    #endregion
}
