using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 관리 기능을 사용하려면 필요

public class TitleController : MonoBehaviour
{
    // Start 버튼을 눌렀을 때 호출되는 메서드
    public void OnStartButtonClicked()
    {
        // 게임 씬으로 전환 (씬 이름은 "GameScene"으로 설정)
        SceneManager.LoadScene("GameScene");
    }

    // Exit 버튼을 눌렀을 때 호출되는 메서드
    public void OnExitButtonClicked()
    {
        // 게임 종료
        Application.Quit();

        // 에디터에서 실행 중일 때는 종료가 되지 않으므로 아래 코드로 확인
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
