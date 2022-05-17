using System.Collections.Generic;
using UnityEngine;

public class ScreenRenderer : MonoBehaviour {
    
    [SerializeField] GameObject  point;
    [SerializeField] GameObject  curveObj;
    [SerializeField] GameManager gameManager;

    List<Curve> curves = new List<Curve>();
    Curve currCurve ;

    public void OnMouseDown(){
        if(gameManager.operation == Operations.NewCurve || currCurve == null){
            GameObject currCurveObj = Instantiate(curveObj, Vector3.zero, Quaternion.identity);
            currCurve = currCurveObj.GetComponent<Curve>();
            curves.Add(currCurve);
            gameManager.ChangeOperation("AddPoint");
        }

        if(gameManager.operation == Operations.AddPoint){
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.Set(mouseWorldPos.x, mouseWorldPos.y, 0);

            point = Instantiate(point, mouseWorldPos, Quaternion.identity);
            currCurve.AddPoint(point);
        }
    }
}