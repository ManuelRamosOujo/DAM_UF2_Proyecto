using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update(){
        if (Input.anyKeyDown){
            SceneManager.LoadScene(1);
        }
    }
}
