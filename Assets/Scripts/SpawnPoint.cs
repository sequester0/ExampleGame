using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject point;

    private void Start()
    {
        if (!this.gameObject.CompareTag("Enemy"))
        {
            CreatePoint();
        }
    }

    private void CreatePoint()
    {
        var cubeTransform = transform;
        var cubePosition = cubeTransform.position;
        var cubeLocalScale = cubeTransform.localScale;
        
        var cylRadius = cubeLocalScale.x / 2;
        var cubeRadius = point.transform.localScale.x / 2;

        var height = cylRadius + cubeRadius;

        var minRange = cubePosition.z - cubeLocalScale.y;
        var maxRange = cubePosition.z + cubeLocalScale.y;
        var pos = new Vector3(cubePosition.x, cubePosition.y + height, Random.Range(minRange, maxRange));

        
        Instantiate(point, pos, Quaternion.identity);
    }
}
