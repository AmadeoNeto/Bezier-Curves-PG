using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField] ScreenRenderer screenRenderer;
    [SerializeField] Text operationText;

    public Operations operation {get; private set;}
    [HideInInspector] public Curve currCurve;

    public void ChangeOperation(string op){
        if(op == "AddPoint"){
            operation = Operations.AddPoint;
            operationText.text = "Add Point";
        }
        else if(op == "DragPoint"){
            operation = Operations.DragPoint;
            operationText.text = "Drag Point";
        }
        else if(op == "DeletePoint"){
            operation = Operations.DeletePoint;
            operationText.text = "Delete Point";
        }
        else if(op == "NewCurve"){
            currCurve.setColor(0,0,0);
            operation = Operations.NewCurve;
            operationText.text = "New Curve";
        }
        else if(op == "NextCurve"){
            NextCurve();
            operation = Operations.AddPoint;
            operationText.text = "Next Curve";
        }
    }

    public void NextCurve(){
        List<Curve> curves = screenRenderer.getCurveList();
        int currCurvIndex = curves.IndexOf(currCurve);

        if(curves.Count > 1){
            currCurve.setColor(0,0,0);
            currCurve = curves[(currCurvIndex+1) % curves.Count];
            currCurve.setColor(0,232,232);
        }
    }
}