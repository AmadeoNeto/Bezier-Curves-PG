using UnityEngine;

public class Point : MonoBehaviour {
    [SerializeField] GameManager gameManager;
    Curve curve;

    private void Start() {
        //Acha o gamerManager no jogo e o guarda na var.
        GameObject managerObj = GameObject.FindWithTag("GameController");
        gameManager = managerObj.GetComponent<GameManager>();
    }

    public void SetCurve(Curve curve){
        this.curve = curve;
    }

    private void OnMouseDown() {
        DecideOperation();
    }

    private void OnMouseDrag() {
        DecideOperation();
    }

    public void DecideOperation(){
        if(gameManager.operation == Operations.DragPoint){
            Drag();
        }
        else if(gameManager.operation == Operations.DeletePoint){
            DeletePoint();
        }
    }

    public void Drag(){
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.Set(mouseWorldPos.x, mouseWorldPos.y, 0);
        transform.position = mouseWorldPos;
    }

    public void DeletePoint(){
        curve.DeletePoint(this.gameObject);
    }
}