using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public float deadwait = 3f;
    public void playerDied()
    {
        Invoke("goToGameOver", deadwait);
    }

    private void goToGameOver()
    {
        SceneManager.LoadScene(2);
    }
}
