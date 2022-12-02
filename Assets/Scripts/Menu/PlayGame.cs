using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void LaunchGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
