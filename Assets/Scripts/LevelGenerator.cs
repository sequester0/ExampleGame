using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{

    #region SerializeField
    [Header("Cylinder Attributes")]
    [Tooltip("Default cylinder prefab for instantiate")]
    [SerializeField]
    private GameObject cylinder;
    
    [Tooltip("Minimum radius for cylinder size")]
    [SerializeField]
    private float minRadius;

    [Tooltip("Maximum radius for cylinder size")]
    [SerializeField] 
    private float maxRadius;
    
    [Header("Enemy Cylinder Attributes")]
    [SerializeField]
    private Color enemyCylinderColor;
    
    [SerializeField]
    private Material enemyMaterial;
    #endregion

    #region Private Variables
    private GameObject _previousCylinder;
    #endregion

    #region Functions
    private float FindRadius(float minR, float maxR)
    {
        var radius = Random.Range(minR, maxR);

        if (_previousCylinder != null)
        {
            while (Mathf.Abs(radius - _previousCylinder.transform.localScale.x) < 0.4f)
            {
                radius = Random.Range(minR, maxR);
            }
        }

        return radius;
    }

    public void SpawnCylinder()
    {
        //Set a random radius and height
        var radius = FindRadius(minRadius, maxRadius);
        var height = Random.Range(2f, 6f);
        
        // Apply radius and height to prefab
        cylinder.transform.localScale = new Vector3(radius, height, radius);
        
        //Instantiate First cylinder
        if (_previousCylinder == null)
        {
            _previousCylinder = Instantiate(cylinder, Vector3.zero, Quaternion.identity);
            
        }
        //Instantiate Other cylinders
        else
        {
            var spawnPoint = _previousCylinder.transform.position.z + _previousCylinder.transform.localScale.y + cylinder.transform.localScale.y;
            _previousCylinder = Instantiate(cylinder, new Vector3(0, 0, spawnPoint), Quaternion.identity);
            
            //Create Enemy Cylinders
            if (Random.value < 0.1f)
            {
                _previousCylinder.GetComponent<Renderer>().material = enemyMaterial;
                _previousCylinder.tag = "Enemy";
            }
        }
        
        //Rotate
        _previousCylinder.transform.Rotate(90, 0, 0);
    }
    #endregion
    
}
