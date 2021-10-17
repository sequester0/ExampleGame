using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private Vector3 axis = new Vector3(0, 0, 0);

    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField] 
    private Color collectableColor, nonCollectableColor;

    [SerializeField]
    private AudioClip pickupSound;

    private Camera _mCamera;

    private PlayerManager _pm;

    private void Awake()
    {
        _pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        _mCamera = Camera.main;
    }

    private void Update()
    {
        Transform goTransform;
        (goTransform = transform).Rotate(axis * Time.deltaTime);

        //Collect Point
        if (_pm.canCollectable)
        {
            //Color and Rotation Speed
            axis.y = 270;
            GetComponent<MeshRenderer>().material.color = collectableColor;
            
            var touchingToPlayer = Physics.CheckSphere(goTransform.position, 0.2f, playerLayer);
            if (touchingToPlayer)
            {
                _pm.IncreaseHealth(2.0f);
                _mCamera.GetComponent<AudioSource>().PlayOneShot(pickupSound, 0.5f);
                Destroy(this.gameObject);
            }
        }
        else
        {
            //Color and Rotation Speed
            axis.y = 45;
            GetComponent<MeshRenderer>().material.color = nonCollectableColor;
        }
    }
}
