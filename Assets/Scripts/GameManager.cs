using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //Classe criada para intermediar as operações entre os
    //outros scripts entre si e entre a GUI

    [SerializeField] ScreenRenderer screenRenderer;
    [SerializeField] Text operationText;
    [SerializeField] Text evaluationNumberText;

    public Operations operation {get; private set;}
    [HideInInspector] public Curve currCurve;

    public void ChangeOperation(string op){
        //Função chamada para mudar a operação atual
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
            if(currCurve){
                currCurve.setBezierColor(0,0,0);
            }
            operation = Operations.NewCurve;
            operationText.text = "New Curve";
        }
        else if(op == "NextCurve"){
            NextCurve();
            operation = Operations.AddPoint;
            operationText.text = "Next Curve";
        }
        else if(op == "ChangeNumberEvaluations"){
            int newNumber = int.Parse(evaluationNumberText.text);
            currCurve.setNumberEvaluations(newNumber);
        }
        else if(op == "DeleteCurve"){
            operationText.text = "Delete Curve";
            DeleteCurve();
        }
    }

    public void NextCurve(){
        List<Curve> curves = screenRenderer.getCurveList();
        int currCurvIndex = curves.IndexOf(currCurve);

        if(curves.Count > 1){
            currCurve.setBezierColor(0,0,0);
            currCurve = curves[(currCurvIndex+1) % curves.Count];
        } else if(curves.Count == 1){
            currCurve = curves[0];
        } else{
            currCurve = null;
            return;
        }
        currCurve.setBezierColor(0,232,232);
    }

    public void DeleteCurve(){
        List<Curve> curves = screenRenderer.getCurveList();
        int currCurvIndex = curves.IndexOf(currCurve);

        curves.RemoveAt(currCurvIndex);
        Destroy(currCurve.gameObject);
        if(curves.Count >= 1){
            ChangeOperation("NextCurve");
        }
        else {
            currCurve = null;
            ChangeOperation("NewCurve");
        }
    }
}