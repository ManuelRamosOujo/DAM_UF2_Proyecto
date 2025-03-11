using System.Collections;
using UnityEngine;

public class Interaction : MonoBehaviour{
    void Start(){
        
    }

    void Update(){
        
    }

    public void StartInteraction(){
        bool destroy = true;
        switch (gameObject.name){
            case "Stick":
                GameObject.Find("Player").GetComponent<Inventory>().SetStick(true);
                break;
            case "HeartContainer1":
                GameObject.Find("Player").GetComponent<HealthUIManager>().IncreaseMaxHealth();
                break;
            case "HeartContainer2":
                GameObject.Find("Player").GetComponent<HealthUIManager>().IncreaseMaxHealth();
                break;
            case "HeartContainer3":
                GameObject.Find("Player").GetComponent<HealthUIManager>().IncreaseMaxHealth();
                break;
            case "BeamUpgrade":
                GameObject.Find("Player").GetComponent<Inventory>().SetBeamUpgrade(true);
                break;
            case "DefenseAltar":
                GameObject orb = gameObject.transform.GetChild(0).gameObject;
                Destroy(orb);
                destroy = false;
                GameObject.Find("Player").GetComponent<HealthUIManager>().SetDefenseUpgrade(true);
                gameObject.tag = "Untagged";
                break;
            case "Key":
                GameObject.Find("Player").GetComponent<Inventory>().AddKey();
                break;
            case "DoorLock":
                destroy = false;
                if(GameObject.Find("Player").GetComponent<Inventory>().UseKey()){
                    StartCoroutine(OpenDoorAnimation());
                }
                break;
            default:
                Debug.Log("Objeto no funciona");
                break;
        }
        if (destroy){
            Destroy(gameObject);
        }
    }

    private IEnumerator OpenDoorAnimation(){
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/puerta_candado");
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
