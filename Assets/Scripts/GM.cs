using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // UI 관련 네임스페이스

public class GM : MonoBehaviour
{
    public Image gameOverImage;  // 게임 오버 텍스트 UI
    public Image gameClearImage; // 게임 클리어 텍스트 UI
    public GameObject gameUI;  // 게임 UI (게임 오버/클리어 UI를 띄울 때 사용)
    public GameObject player;  // 플레이어 오브젝트

    private bool isGameOver = false;
    private bool isGameClear = false;

    private void Start()
    {
        gameClearImage.gameObject.SetActive(false);  // 게임 클리어 텍스트 숨기기
        gameOverImage.gameObject.SetActive(false);   // 게임 오버 텍스트 숨기기
        gameUI.SetActive(false);                    // 게임 UI 숨기기
    }

    private void Update()
    {
        // 게임 클리어 조건 (문과 상호작용)
        if (isGameClear && !gameUI.activeSelf)
        {
            ShowGameClear();
        }

        // 게임 오버 조건 (적과 충돌)
        if (isGameOver && !gameUI.activeSelf)
        {
            ShowGameOver();
        }
    }


    // 게임 오버 처리
    public void SetGameOver()
    {
        isGameOver = true;
    }

    // 게임 클리어 처리
    public void SetGameClear()
    {
        isGameClear = true;
    }

    // 게임 오버 UI 활성화
    private void ShowGameOver()
    {
        gameUI.SetActive(true); // 게임 UI 활성화
        gameOverImage.gameObject.SetActive(true); // "GAME OVER" 텍스트 활성화
    }

    // 게임 클리어 UI 활성화
    private void ShowGameClear()
    {
        gameUI.SetActive(true); // 게임 UI 활성화
        gameClearImage.gameObject.SetActive(true); // "GAME CLEAR" 텍스트 활성화
    }

    // 타이틀 씬으로 이동
    public void GoToTitle()
    {
        SceneManager.LoadScene("Title"); 
    }

    // 현재 씬을 처음부터 다시 시작
    public void RetryLevel()
    {
        // 현재 씬을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}