using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private SoundMixerManager soundMixerManager;

    public void lerpMuteMaster()
    {
        soundMixerManager.setGameEnd();
    }
    public void playerDied(float deadwait = 0)
    {
        Invoke("goToGameOver", deadwait);
    }

    private void goToGameOver()
    {
        SceneManager.LoadScene(2);
    }
}
