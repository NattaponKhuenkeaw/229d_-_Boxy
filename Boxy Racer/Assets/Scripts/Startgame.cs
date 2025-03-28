using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void Restart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
