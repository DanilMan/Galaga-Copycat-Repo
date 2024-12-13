using System;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    private ulong points = 0;
    [SerializeField] private PointUpdater pointText;

    public ulong getPoints()
    {
        return points;
    }

    public void addPoints(uint points)
    {
        this.points = this.points + points;
        pointText.setPointsText(this.points);
    }
}
