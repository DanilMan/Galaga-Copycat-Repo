using System;
using TMPro;
using UnityEngine;

public class PointUpdater : MonoBehaviour
{
    private TMP_Text pointsText;

    private void Awake()
    {
        pointsText = GetComponent<TMP_Text>();
        setPointsText(0);
    }

    public void setPointsText(ulong points)
    {
        pointsText.SetText(points.ToString());
    }
}
