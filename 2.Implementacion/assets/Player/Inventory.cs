using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour{
    private bool stick = false;
    private bool sword = false;
    private bool beamUpgrade = false;
    private int keys = 0;
    private int bombs = 0;
    private float attackPower = 1;
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private GameObject keyCounter;

    void Start(){
        
    }

    void Update(){
        
    }

    public bool GetStick(){
        return stick;
    }

    public bool GetSword(){
        return sword;
    }

    public bool GetWeapon(){
        return GetStick() || GetSword();
    }

    public bool GetBeamUpgrade(){
        return beamUpgrade;
    }

    public void SetStick(bool option){
        stick = option;
    }
    
    public void SetSword(bool option){
        sword = option;
    }

    public void SetBeamUpgrade(bool option){
        beamUpgrade = option;
    }

    public int GetKeys(){
        return keys;
    }

    public void AddKey(){
        keys++;
        UpdateKeyCounter();
    }

    public bool UseKey(){
        if (keys > 0){    
            keys--;
            UpdateKeyCounter();
            return true;
        }
        
        return false;
    }

    public float GetAttackPower(){
        return attackPower;
    }

    private void UpdateKeyCounter(){
        if (keys > 0) {
            keyCounter.SetActive(true);
        } else {
            keyCounter.SetActive(false);
        }
        counterText.text = keys.ToString();
    }
}
