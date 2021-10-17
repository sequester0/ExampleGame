using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Constants

    private const float SizeScalel = 0.28f;
    private const float CheckerRadius = 0.18f;
    private const float Offset = 0.05f;

    #endregion

    #region SerializeField

    [SerializeField]
    private Vector3 defaultSize = new Vector3(1, 1, 1);

    [SerializeField]
    private LayerMask cylinderLayer;

    [SerializeField]
    private AudioClip clickSound, deathSound;

    [HideInInspector]
    public bool canCollectable = false;

    public float health = 10.0f;

    #endregion
    
    #region Private Variables

    private Camera _mCamera;

    private Transform _cyl;
    private float _cylRadius;

    #endregion
    
    #region Unity
    
    private void Awake()
    {
        _mCamera = Camera.main;
    }

    private void Update()
    {
        //Define Cylinder and Radius
        if (Physics.OverlapSphere(transform.position, CheckerRadius, cylinderLayer).Length > 0)
        {
            _cyl = Physics.OverlapSphere(transform.position, CheckerRadius, cylinderLayer)[0].transform;
            _cylRadius = _cyl.localScale.x * SizeScalel;
        }
        
        
        //Check Death Situations
        if (health <= 0)
        {
            Death();
        }
        if (_cylRadius > transform.localScale.y)
        {
            Death();
        }
        //Check Death Situations and Collectability
        if (_cylRadius + Offset > transform.localScale.y)
        {
            canCollectable = true;
            if (_cyl.CompareTag("Enemy"))
            {
                Death();
            }
        }
        else
        {
            canCollectable = false;
        }
        
        ChangeCircleRadius(_cylRadius);
        HealthCounter();
    }

    #endregion

    #region Functions

    private void Death()
    {
        //Stop Camera Controller
        if (_mCamera != null)
        {
            _mCamera.GetComponent<CameraController>().enabled = false;
        }

        //Open GameOverUI
        UIManager.uiM.OpenGameOverUI();
        
        //isPlayerAlive to false
        GameManager.gm.isPlayerAlive = false;
        
        //Play Death Sound Effect
        _mCamera.GetComponent<AudioSource>().PlayOneShot(deathSound, 0.5f);
        
        //Save High Score
        if (GameManager.gm.distance > PlayerPrefs.GetFloat("Highscore"))
        {
            PlayerPrefs.SetFloat("Highscore", GameManager.gm.distance);
        }
        //Set High Score Text
        UIManager.uiM.SetHighScoreText();

        //Destroy Player GameObject
        Destroy(gameObject);
    }

    private void ChangeCircleRadius(float cylRadius) 
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //Play sound effect
            if (touch.phase == TouchPhase.Began)
            {
                _mCamera.GetComponent<AudioSource>().PlayOneShot(clickSound, 0.1f);
            }
        
            //When touched to screen
            if (touch.phase == TouchPhase.Stationary)
            {
                //Set size of the circle
                var targetVector = new Vector3(defaultSize.x, cylRadius, cylRadius);
                transform.localScale = Vector3.Slerp(transform.localScale, targetVector, 0.125f);
            }
        }
        else
        {
            transform.localScale = Vector3.Slerp(transform.localScale, defaultSize, 0.125f);
        }
    }

    private void HealthCounter()
    {
        health = Mathf.Clamp(health, -1f, 10.0f);
        if (health >= 0)
        {
            health -= Time.deltaTime;
            UIManager.uiM.SetPlayerHealth(health);
        }
    }

    public void IncreaseHealth(float value)
    {
        health += value;
    }

    #endregion
}
