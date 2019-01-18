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
    public float repeatTime = 0.5f;

    [Header("X coordinates")]
    public float xMin = -20f;
    public float xMax = 20f;

    [Header("Y coordinates")]
    public float yMin = 5f;
    public float yMax = 20f;
    
    [Header("Random Gravity")]
    public float minGravity = -15f;
    public float maxGravity = 15f;

    [Header("Random Start Speed")]
    public float minStartSpeed = -20f;
    public float maxStartSpeed = 20f;

    #endregion

    #region Methods

    private void Start()
    {
        StartSpawnig();
    }

    public void StartSpawnig()
    {
        StopSpawnig();
        StartCoroutine(InstObj());
    }

    public void StopSpawnig()
    {
        StopAllCoroutines();
    }

    IEnumerator InstObj()
    {
        yield return new WaitForSeconds(repeatTime);
        GeneratePrimitive();
        StartCoroutine(InstObj());
    }

    private PrimitiveType GetRandomPrimitive()
    {
        int randomPrimitive = Random.Range(0, 4);
        PrimitiveType primitive;

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
            default:
                primitive = PrimitiveType.Sphere;
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
        primitiveObject.AddComponent<SimplePhysics>().gravity = useRandomGravity ? Random.Range(minGravity, maxGravity) : gravity;
        primitiveObject.GetComponent<SimplePhysics>().startSpeed = useRandomStartSpeed ? Random.Range(minStartSpeed, maxStartSpeed) : startSpeed;
    }

    #endregion
}
