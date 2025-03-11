using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour{
    public GameObject heartPrefab;
    public Transform heartsContainer;
    private float MaxHealth = 3f;
    private float currentHealth = 3f;
    private bool defenseUpgrade = false;
    private List<GameObject> hearts = new List<GameObject>();
    Sprite[] heartSprites;
    public float animationDuration = 2f;
    private float timeElapsed = 2f;
    private float progress = 0f;
    private Material material;
    private bool invencibility = false;
    [SerializeField] Sprite frontPlayer;

    private void Awake(){
        heartSprites = Resources.LoadAll<Sprite>("Images/UI/Vida");
        Shader defenseShader = Shader.Find("Custom/DefenseShader");
        material = new Material(defenseShader);
    }
    
    private void Start(){
        AddHearts();
        if (defenseUpgrade == true){
            GetComponent<SpriteRenderer>().material = material;
            progress = Mathf.Clamp01(timeElapsed / animationDuration);
            material.SetFloat("_Progress", progress);
        }
    }

    void Update(){
        if (timeElapsed < animationDuration && defenseUpgrade){
            timeElapsed += Time.deltaTime;
            progress = Mathf.Clamp01(timeElapsed / animationDuration);
            material.SetFloat("_Progress", progress);
        }
    }

    public void IncreaseMaxHealth(){
        MaxHealth++;
        FullHeal();
        UpdateHearts();
    }
    public void UpdateHearts(){
        foreach (Transform child in heartsContainer){
            Destroy(child.gameObject);
        }

        hearts.Clear();

        // Crea y añade corazones
        AddHearts();

        RefreshHearts();
    }

    public void AddHearts(){
        for (int i = 0; i < Mathf.CeilToInt(MaxHealth); i++){
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
            hearts.Add(newHeart);
        }

        RefreshHearts();
    }

    public void RefreshHearts(){
        for (int i = 0; i < hearts.Count; i++){
            float heartValue = Mathf.Clamp(currentHealth - i, 0f, 1f);

            if (heartValue >= 1f){
                hearts[i].GetComponent<Image>().sprite = defenseUpgrade ? heartSprites[0] : heartSprites[3];
                hearts[i].GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            }
            else if (heartValue >= 0.5f){
                hearts[i].GetComponent<Image>().sprite = defenseUpgrade ? heartSprites[1] : heartSprites[4];
                hearts[i].GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            } else{
                hearts[i].GetComponent<Image>().sprite = defenseUpgrade ? heartSprites[2] : heartSprites[5];
                hearts[i].GetComponent<Image>().color = new Color(255, 255, 255, 0.6f);
            }
        }
    }

    public void FullHeal(){
        currentHealth = MaxHealth;
        RefreshHearts();
    }

    public void Heal(float heal){
        currentHealth += heal;
        if (currentHealth > MaxHealth){
            currentHealth = MaxHealth;
        }
        RefreshHearts();
    }

    public void LoseHealth(float damage){
        if (!invencibility){
            currentHealth -= defenseUpgrade ? damage/2 : damage;
            StartCoroutine(InvencibilityFrames());
        }
        if (currentHealth <= 0) {
            currentHealth = 0;
            SceneManager.LoadScene(2);
        }
        RefreshHearts();
    }

    public bool GetDefenseUpgrade(){
        return defenseUpgrade;
    }

    public void SetDefenseUpgrade(bool defense){
        defenseUpgrade = defense;
        if (defense){
            timeElapsed = 0;
            GetComponent<SpriteRenderer>().material = material;
            StartCoroutine(DefenseAnimation());
        } else {
            timeElapsed = 0;
            Shader normalShader = Shader.Find("Sprites/Default");
            GetComponent<SpriteRenderer>().material = new Material(normalShader);
        }
    }

    private IEnumerator DefenseAnimation(){
        GetComponent<Movimiento>().SetFrozen(true);
        GetComponent<SpriteRenderer>().sprite = frontPlayer;
        yield return new WaitForSeconds(2);
        RefreshHearts();
        GetComponent<Movimiento>().SetFrozen(false);
    }

    private IEnumerator InvencibilityFrames(){
        invencibility = true;
        float timePassed = 0;
        while(timePassed < 1.6f){
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
            timePassed += 0.2f;
        }
        invencibility = false;
    }
}