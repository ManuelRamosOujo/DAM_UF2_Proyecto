using System;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour{
    public float desplazamientoNegX = -20f;
    public float desplazamientoX = 20f;
    public float desplazamientoNegY = -11f;
    public float desplazamientoY = 11f;
    public float duracionDesplazamiento = 1f;
    private Vector3 posicionInicial;
    private bool desplazando = false;
    private Movimiento movimiento;
    private Coordenates coordenadas;

    void Start(){
        posicionInicial = transform.position;
        movimiento = FindObjectOfType<Movimiento>();
        coordenadas = gameObject.GetComponent<Coordenates>();
    }

    public void DesplazarCamara(String direction){
        if (!desplazando){
            switch (direction){
                case "x":
                    StartCoroutine(MoverCamara(desplazamientoX));
                    coordenadas.SetPositionX(coordenadas.GetPositionX() + 1);
                    break;
                case "-x":
                    StartCoroutine(MoverCamara(desplazamientoNegX));
                    coordenadas.SetPositionX(coordenadas.GetPositionX() - 1);
                    break;
                case "y":
                    StartCoroutine(MoverCamara(desplazamientoY));
                    coordenadas.SetPositionY(coordenadas.GetPositionY() + 1);
                    break;
                case "-y":
                    StartCoroutine(MoverCamara(desplazamientoNegY));
                    coordenadas.SetPositionY(coordenadas.GetPositionY() - 1);
                    break;
            }
        }
    }

    private IEnumerator MoverCamara(float desplazamiento){
        movimiento.SetFrozen(true);

        desplazando = true;
        Vector3 posicionObjetivo;

        coordenadas.DestroyEnemies();

        if (desplazamiento == 20f || desplazamiento == -20f){
            posicionObjetivo = posicionInicial + new Vector3(desplazamiento, 0, 0);

            if (desplazamiento > 0){
                movimiento.CameraAutoMove("x");
            } else {
                movimiento.CameraAutoMove("-x");
            }
        } else {
            posicionObjetivo = posicionInicial + new Vector3(0, desplazamiento, 0);

            if (desplazamiento > 0){
                movimiento.CameraAutoMove("y");
            } else {
                movimiento.CameraAutoMove("-y");
            }
        }

        float tiempoTranscurrido = 0;

        while (tiempoTranscurrido < duracionDesplazamiento){
            transform.position = Vector3.Lerp(posicionInicial, posicionObjetivo, tiempoTranscurrido / duracionDesplazamiento);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        transform.position = posicionObjetivo; // Asegura que llegue a la posición final
        posicionInicial = posicionObjetivo;

        desplazando = false;

        Vector3 playerPosition = GameObject.Find("Player").GetComponent<Transform>().position;
        GameObject.Find("Main Camera").GetComponent<Coordenates>().SetPlayerSpawnPosition(playerPosition);

        movimiento.SetFrozen(false);
        
        coordenadas.SpawnEnemies();
    }

    public void SetPosicionInicial(Vector3 newPosition){
        posicionInicial = newPosition;
    }
}
