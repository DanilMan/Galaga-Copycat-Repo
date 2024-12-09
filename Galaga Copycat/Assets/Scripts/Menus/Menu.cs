using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PLay()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
