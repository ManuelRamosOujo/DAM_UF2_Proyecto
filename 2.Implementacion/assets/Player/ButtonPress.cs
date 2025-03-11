using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour{
    private PlayerInputActions input;
    private void Awake() {
        input = new PlayerInputActions();

        input.Player.APress.performed += APress;
        input.Player.BPress.performed += BPress;
    }

    private void OnEnable(){
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void APress(InputAction.CallbackContext context){
        Sprite AButton = GameObject.Find("AButton").GetComponent<Image>().sprite;

        if (AButton.name == "Botones_0"){
            if (gameObject.GetComponent<Inventory>().GetWeapon() && !gameObject.GetComponent<Movimiento>().GetFrozen()){
                Melee_colision colision = GameObject.Find("MeleeAttack").GetComponent<Melee_colision>();
                StartCoroutine(colision.EnableColision());

                if (gameObject.GetComponent<Inventory>().GetBeamUpgrade()){
                    StartCoroutine(colision.ShootEnergyBeam());
                }
            }
        }

        if (AButton.name == "Botones_2"){
            Interaction();
        }
    }

    private void BPress(InputAction.CallbackContext context){
        GameObject bomb = Instantiate(Resources.Load<GameObject>("Prefabs/Bomb"),
        GameObject.Find("MeleeAttack").transform.position, new Quaternion());
        bomb.name = "Bomb";
    }

    private void Interaction(){
        string name = GameObject.Find("Player")
        .GetComponent<DetectarColision>().GetColisionName();

        GameObject.Find(name).GetComponent<Interaction>().StartInteraction();
    }
}