using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    // �� ��ȯ �Լ�
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
