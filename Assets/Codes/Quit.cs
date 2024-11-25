using UnityEngine;

public class ExitGameManager : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Game is exiting...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 실행 중지
#else
        Application.Quit(); // 게임 종료
#endif
    }
}