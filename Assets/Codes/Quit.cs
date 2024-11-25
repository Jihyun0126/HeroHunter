using UnityEngine;

public class ExitGameManager : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Game is exiting...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ������ ���� ����
#else
        Application.Quit(); // ���� ����
#endif
    }
}