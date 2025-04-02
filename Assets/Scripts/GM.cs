using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // UI ���� ���ӽ����̽�

public class GM : MonoBehaviour
{
    public Image gameOverImage;  // ���� ���� �ؽ�Ʈ UI
    public Image gameClearImage; // ���� Ŭ���� �ؽ�Ʈ UI
    public GameObject gameUI;  // ���� UI (���� ����/Ŭ���� UI�� ��� �� ���)
    public GameObject player;  // �÷��̾� ������Ʈ

    private bool isGameOver = false;
    private bool isGameClear = false;

    private void Start()
    {
        gameClearImage.gameObject.SetActive(false);  // ���� Ŭ���� �ؽ�Ʈ �����
        gameOverImage.gameObject.SetActive(false);   // ���� ���� �ؽ�Ʈ �����
        gameUI.SetActive(false);                    // ���� UI �����
    }

    private void Update()
    {
        // ���� Ŭ���� ���� (���� ��ȣ�ۿ�)
        if (isGameClear && !gameUI.activeSelf)
        {
            ShowGameClear();
        }

        // ���� ���� ���� (���� �浹)
        if (isGameOver && !gameUI.activeSelf)
        {
            ShowGameOver();
        }
    }


    // ���� ���� ó��
    public void SetGameOver()
    {
        isGameOver = true;
    }

    // ���� Ŭ���� ó��
    public void SetGameClear()
    {
        isGameClear = true;
    }

    // ���� ���� UI Ȱ��ȭ
    private void ShowGameOver()
    {
        gameUI.SetActive(true); // ���� UI Ȱ��ȭ
        gameOverImage.gameObject.SetActive(true); // "GAME OVER" �ؽ�Ʈ Ȱ��ȭ
    }

    // ���� Ŭ���� UI Ȱ��ȭ
    private void ShowGameClear()
    {
        gameUI.SetActive(true); // ���� UI Ȱ��ȭ
        gameClearImage.gameObject.SetActive(true); // "GAME CLEAR" �ؽ�Ʈ Ȱ��ȭ
    }

    // Ÿ��Ʋ ������ �̵�
    public void GoToTitle()
    {
        SceneManager.LoadScene("Title"); 
    }

    // ���� ���� ó������ �ٽ� ����
    public void RetryLevel()
    {
        // ���� ���� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}