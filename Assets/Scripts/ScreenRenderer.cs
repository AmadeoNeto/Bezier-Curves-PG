using System.Collections.Generic;
using UnityEngine;

public class ScreenRenderer : MonoBehaviour {
    
    [SerializeField] GameObject  point;
    [SerializeField] GameObject  curveObj;
    [SerializeField] GameManager gameManager;

    List<Curve> curves = new List<Curve>();

    public List<Curve> getCurveList(){
        return curves;
    }

    public void OnMouseDown(){        
        if(gameManager.operation == Operations.NewCurve || gameManager.currCurve == null){
            GameObject currCurveObj = Instantiate(curveObj, Vector3.zero, Quaternion.identity);
            gameManager.currCurve = currCurveObj.GetComponent<Curve>();
            curves.Add(gameManager.currCurve);
            gameManager.ChangeOperation("AddPoint");
        }

        if(gameManager.operation == Operations.AddPoint){
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.Set(mouseWorldPos.x, mouseWorldPos.y, 0);

            point = Instantiate(point, mouseWorldPos, Quaternion.identity, gameManager.currCurve.gameObject.transform);
            gameManager.currCurve.AddPoint(point);
        }
    }
}