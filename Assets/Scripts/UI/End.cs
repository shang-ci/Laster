using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public void ReStart()
    {
        SceneManager.LoadScene("GameScenes");
    }
}