using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isPlayerAlive = true;

    public static GameManager gm;

    private GameObject player;

    [SerializeField] 
    private Transform playerStartPoint;

    [SerializeField]
    private CameraController cc;

    [SerializeField]
    private float difficulty;

    public float distance;

    private void Awake()
    {
        gm = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //Load Scene
        if (!isPlayerAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        
        //Calculate Player Distance
        if (player != null)
        {
            distance = Vector3.Distance(player.transform.position, playerStartPoint.position);
            UIManager.uiM.SetDistanceValue(distance);
        }

        Mathf.Clamp(cc.speed, 1, 50);
        cc.speed += Time.timeSinceLevelLoad * difficulty / 10000;
        cc.speed = Mathf.Clamp(cc.speed, 1, 50);
    }
}
