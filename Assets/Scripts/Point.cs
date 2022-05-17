using UnityEngine;

public class Point : MonoBehaviour {
    [SerializeField] GameManager gameManager;

    private void OnEnable() {
        GameObject managerObj = GameObject.FindWithTag("GameController");
        gameManager = managerObj.GetComponent<GameManager>();
    }

    private void OnMouseDown() {
        Drag();
    }

    private void OnMouseDrag() {
        Drag();
    }

    public void Drag(){
        if(gameManager.operation == Operations.DragPoint){
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.Set(mouseWorldPos.x, mouseWorldPos.y, 0);
            transform.position = mouseWorldPos;
        }
    }
}