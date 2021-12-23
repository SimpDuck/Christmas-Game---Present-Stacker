using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;
    void Start()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
        textDisplay.text = "00";
    }

    public void updateScoreText(int scoreAmount)
    {
        textDisplay.text = scoreAmount.ToString();
    }
}
