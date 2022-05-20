using System.Collections.Generic;
using UnityEngine;

public class ScreenRenderer : MonoBehaviour {
//Classe para renderizar e manipular os pontos e curvas da tela

    [Header("Prefabs")]
    [SerializeField] GameObject  point; //Prefab do ponto
    [SerializeField] GameObject  curveObj; //Prefab da curva
    
    [Header("GameManager")]
    [SerializeField] GameManager gameManager;

    List<Curve> curves = new List<Curve>();

    public List<Curve> getCurveList(){
        return curves;
    }
    
    //A área que dá pra clicar é o plano verde de fundo
    public void OnMouseDown(){        
        if(gameManager.operation == Operations.NewCurve || gameManager.currCurve == null){
            //Cria o objeto de uma nova curva na tela
            GameObject currCurveObj = Instantiate(curveObj, Vector3.zero, Quaternion.identity);
            //Faz essa curva ser a atual
            gameManager.currCurve = currCurveObj.GetComponent<Curve>();
            curves.Add(gameManager.currCurve);
            gameManager.ChangeOperation("AddPoint");
        }

        if(gameManager.operation == Operations.AddPoint){
            //Pega as coordenas do mouse em coordenada de mundo, mas com z=0
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.Set(mouseWorldPos.x, mouseWorldPos.y, 0);

            //Adiciona um novo objeto de ponto à curva
            GameObject pointObj = Instantiate(point, mouseWorldPos, Quaternion.identity, gameManager.currCurve.gameObject.transform);
            gameManager.currCurve.AddPoint(pointObj);
        }
    }
}