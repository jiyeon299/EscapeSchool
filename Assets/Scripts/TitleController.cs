using UnityEngine;
using UnityEngine.SceneManagement;  // �� ���� ����� ����Ϸ��� �ʿ�

public class TitleController : MonoBehaviour
{
    // Start ��ư�� ������ �� ȣ��Ǵ� �޼���
    public void OnStartButtonClicked()
    {
        // ���� ������ ��ȯ (�� �̸��� "GameScene"���� ����)
        SceneManager.LoadScene("GameScene");
    }

    // Exit ��ư�� ������ �� ȣ��Ǵ� �޼���
    public void OnExitButtonClicked()
    {
        // ���� ����
        Application.Quit();

        // �����Ϳ��� ���� ���� ���� ���ᰡ ���� �����Ƿ� �Ʒ� �ڵ�� Ȯ��
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
