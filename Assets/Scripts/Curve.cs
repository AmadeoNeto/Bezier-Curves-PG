using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour{
    List<GameObject> controlPoints  = new List<GameObject>();
    List<GameObject> polygonalLines = new List<GameObject>();
    LineRenderer polygonalLine;
    LineRenderer bezierLine;
    
    private void Awake() {
        polygonalLine = GetComponent<LineRenderer>();

        GameObject child = transform.GetChild(0).gameObject;
        bezierLine = child.GetComponent<LineRenderer>();

        polygonalLine.positionCount = 0;
        polygonalLine.startWidth = 0.23f;
        polygonalLine.endWidth = 0.23f;
        
        bezierLine.positionCount = 0;
        bezierLine.startWidth = 0.23f;
        bezierLine.endWidth = 0.23f;
        setColor(0,232,232);
    }

    public void setColor(int r, int g, int b){
        bezierLine.startColor = new Color(r,g,b);
        bezierLine.endColor = new Color(r,g,b);
    }

    public void AddPoint(GameObject point) {
        polygonalLine.positionCount++;
        controlPoints.Add(point);
    }

    private void Update() {
        if (controlPoints.Count >= 2) {
            for (int i = 0; i < controlPoints.Count; i++) {
                polygonalLine.SetPosition(i, controlPoints[i].transform.position);
            }
        }
        if (controlPoints.Count >= 3) {
            List<Vector2> pointsOnCurve = DeCastlejau();
            bezierLine.positionCount = pointsOnCurve.Count;
            for (int i = 0; i < pointsOnCurve.Count; i++){
                bezierLine.SetPosition(i, pointsOnCurve[i]);
            }
        } 
    }

    private List<Vector2> GetControlPointPositions() {
        List<Vector2> controlPointPos = new List<Vector2>();
        for (int i = 0; i < controlPoints.Count; i++) {
            controlPointPos.Add(controlPoints[i].transform.position);
        }
        return controlPointPos;
    }

    private List<Vector2> DeCastlejau(){
        List<Vector2> curvePoints = new List<Vector2>();
        List<Vector2> controlPointPos = GetControlPointPositions();
        
        curvePoints.Add(controlPointPos[0]);
        
        for (int i = 0; i <= 40; i++) {
            curvePoints.Add(GetDeCastlejauPoint((float)i/40f, controlPointPos));
        }
        
        curvePoints.Add(controlPointPos[controlPointPos.Count-1]);

        return curvePoints;
    }
    
    private Vector2 GetDeCastlejauPoint(float t, List<Vector2> points){
        if (points.Count == 1) {
            return points[0];
        } else {
            List<Vector2> points_curr = new List<Vector2>();
            for (int i = 0; i < points.Count - 1; i++) {
                points_curr.Add(LinearInterpolation(t,points[i],points[i+1]));
            }
            return GetDeCastlejauPoint(t, points_curr);
        }
    }

    private Vector2 LinearInterpolation(float t, Vector2 A, Vector2 B){
        return new Vector2((1-t)*A.x + t*B.x, (1-t)*A.y + t*B.y);
    }
}