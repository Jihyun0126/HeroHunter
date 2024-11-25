using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    // 씬 전환 함수
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
