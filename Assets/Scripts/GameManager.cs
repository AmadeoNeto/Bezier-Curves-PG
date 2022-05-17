using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] public Operations operation {get; private set;}

    public void ChangeOperation(string op){
        if(op == "AddPoint"){
            operation = Operations.AddPoint;
        }
        else if(op == "DragPoint"){
            operation = Operations.DragPoint;
        }
        else if(op == "DeletePoint"){
            operation = Operations.DeletePoint;
        }
        else if(op == "NewCurve"){
            operation = Operations.NewCurve;
        }
    }
}