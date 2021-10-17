using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class FpsDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _fpsText;
    private void Update()
    {
        var fps = 1 / Time.unscaledDeltaTime;
        _fpsText.text = fps.ToString(CultureInfo.InvariantCulture);
    }
}
