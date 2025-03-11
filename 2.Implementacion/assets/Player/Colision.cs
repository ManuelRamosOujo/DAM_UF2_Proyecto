using System;
using UnityEngine;
using UnityEngine.UI;

public class DetectarColision : MonoBehaviour{
    private CameraController camaraController;
    private Sprite[] buttons;
    private String colisionName;
    private bool falling = false;

    private void Awake() {
        buttons = Resources.LoadAll<Sprite>("Images/UI");
    }

    void Start(){
        camaraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update() {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    // Detecta colisión con un trigger
    void OnTriggerEnter2D(Collider2D other){
        if (!falling){
            if (other.gameObject.name == "TriggerLeft"){
                camaraController.DesplazarCamara("-x");
            }
            if (other.gameObject.name == "TriggerRight"){
                camaraController.DesplazarCamara("x");
            }
            if (other.gameObject.name == "TriggerTop"){
                camaraController.DesplazarCamara("y");
            }
            if (other.gameObject.name == "TriggerBottom"){
                camaraController.DesplazarCamara("-y");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Interactable")){
            GameObject.Find("AButton").GetComponent<Image>().sprite = buttons[2]; 
            GameObject.Find("AButtonImage").GetComponent<Image>().enabled = false;
            colisionName = other.gameObject.name;
        }
    }

    void OnCollisionStay2D(Collision2D other){
        if (!other.gameObject.CompareTag("Interactable")){
            GameObject.Find("AButton").GetComponent<Image>().sprite = buttons[0];
            if (GameObject.Find("Player").GetComponent<Inventory>().GetWeapon()){
                GameObject.Find("AButtonImage").GetComponent<Image>().enabled = true;
            }
            colisionName = null;
        } else {
            GameObject.Find("AButton").GetComponent<Image>().sprite = buttons[2]; 
            GameObject.Find("AButtonImage").GetComponent<Image>().enabled = false;
            colisionName = other.gameObject.name;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Interactable")){
            GameObject.Find("AButton").GetComponent<Image>().sprite = buttons[0];
            if (GameObject.Find("Player").GetComponent<Inventory>().GetWeapon()){
                GameObject.Find("AButtonImage").GetComponent<Image>().enabled = true;
            }
            colisionName = null;
        }
    }

    public String GetColisionName(){
        return colisionName;
    }

    public bool IsFalling(){
        return falling;
    }

    public void SetFalling(bool fall){
        falling = fall;
    }
}
