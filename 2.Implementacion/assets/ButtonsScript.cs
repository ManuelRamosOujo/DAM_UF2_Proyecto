using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public static void OnRestartClick()
    {
        SceneManager.LoadScene(1);
    }

    public static void OnCloseClick()
    {
        Application.Quit();
    }
} 
 