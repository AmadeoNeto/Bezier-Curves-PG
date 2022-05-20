using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour{
    List<GameObject> controlPoints  = new List<GameObject>();
    List<GameObject> polygonalLines = new List<GameObject>();
    LineRenderer polygonalLine;
    LineRenderer bezierLine;
    int numeroAvaliacoes = 40;

    private void Awake() {
        polygonalLine = GetComponent<LineRenderer>();

        GameObject child = transform.GetChild(0).gameObject;
        bezierLine = child.GetComponent<LineRenderer>();

        //Zera a qt de pontos que a linha da poligonal liga
        polygonalLine.positionCount = 0;
        //Inicializa a largura da poligonal
        polygonalLine.startWidth = 0.23f;
        polygonalLine.endWidth = 0.23f;

        //Inicaliza os mesmo valores que os acima mas para as linhas da Curva de Bezier
        bezierLine.positionCount = 0;
        bezierLine.startWidth = 0.23f;
        bezierLine.endWidth = 0.23f;
        setBezierColor(0,232,232); //Mudar a cor da da curva
    }

    public void setBezierColor(int r, int g, int b){
        //Muda a cor das linhas que compoem a curva de Bezier
        bezierLine.startColor = new Color(r,g,b);
        bezierLine.endColor = new Color(r,g,b);
    }

    public void setNumberEvaluations(int numberEvaluations){
        numeroAvaliacoes = numberEvaluations;
    }

    public void AddPoint(GameObject point) {
        polygonalLine.positionCount++; //Abre espaço para mais uma linha na poligonal
        controlPoints.Add(point); //Adiciona o objeto do ponto para a lista de pontos de controle 
    }

    private void Update() {
        //Se houerem 2 ou mais pontos de controle, faz com que uma linha seja criada entre eles
        if (controlPoints.Count >= 2) {
            //Para cada ponto ...
            for (int i = 0; i < controlPoints.Count; i++) {
                //Cria uma linha do último ponto até o atual
                polygonalLine.SetPosition(i, controlPoints[i].transform.position);
            }
        }

        //Se houverem 3 ou mais pontos de controle, desenha a curva
        if (controlPoints.Count >= 3) {
            //Usa DeCastlejau para pegar a lista de pontos da curva
            List<Vector2> pointsOnCurve = DeCastlejau();
            //Faz o numero de pontos da linha que une os pontos da curva
            //ser o mesmo que os computados no algoritmo
            bezierLine.positionCount = pointsOnCurve.Count;

            //Dá a cada ponto calculado da cura, um ponto invisível 
            //na linha que será renderizada
            for (int i = 0; i < pointsOnCurve.Count; i++){
                bezierLine.SetPosition(i, pointsOnCurve[i]);
            }
        } 
    }

    private List<Vector2> GetControlPointPositions() {
        //Retorna uma lista com a posição dos pontos de controle
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
        
        for (int i = 0; i <= numeroAvaliacoes; i++) {
            curvePoints.Add(GetDeCastlejauPoint((float)i/(float)numeroAvaliacoes, controlPointPos));
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