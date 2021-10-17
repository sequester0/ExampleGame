using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager uiM;
    
    [SerializeField] 
    private GameObject gameOverPanel;

    [SerializeField] 
    private TMP_Text distanceValue;

    [SerializeField]
    private RectTransform healthBar;

    [SerializeField]
    private TMP_Text highScoreText;
    
    private void Awake()
    {
        uiM = this;
    }

    public void SetPlayerHealth(float health)
    {
        healthBar.localScale = new Vector3(health / 10, 1f, 1f);
    }

    public void OpenGameOverUI()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void SetDistanceValue(float distance)
    {
        distanceValue.text = distance.ToString("F1");
    }

    public void SetHighScoreText()
    {
        highScoreText.text = PlayerPrefs.GetFloat("Highscore").ToString("F1");
    }
}
