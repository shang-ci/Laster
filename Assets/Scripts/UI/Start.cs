using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScenes");
    }
}
