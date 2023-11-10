using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController
{
    private int lineSegment;
    private LineRenderer lineVisual;

    
    public PathController(int lineSegment, LineRenderer lineVisual)
    {
        this.lineSegment = lineSegment;
        this.lineVisual = lineVisual;
    }

    public void VisualizePath(Vector3 initialVelocity, Vector3 startPosition)
    {
        Clear();
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePositionInTime(initialVelocity, i / (float)lineSegment, startPosition);
            lineVisual.SetPosition(i, pos);
        }
    }
    private Vector3 CalculatePositionInTime(Vector3 initialVelocity, float time, Vector3 startPosition)
    {
        //  yatay mesafe= (baslangic hizi X zaman + ilk mesafe)  
        Vector3 distance = startPosition + initialVelocity * time;

        //yukseklik = (-1/2 * ivme * zamanin karesi) + (ilk hiz * zaman)+ ilk yukseklik   

        float verticalDistance = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (initialVelocity.y * time) + startPosition.y;

        distance.y = verticalDistance;

        return distance;

    }


    public void Clear()
    {
        for (int i = 0; i < lineSegment; i++)
        {

            lineVisual.SetPosition(i, Vector3.zero);
        }
    }

}
