using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� Ʈ������
    public float smoothSpeed = 0.125f; // �ε巯�� �̵� �ӵ�
    public Vector3 offset; // ī�޶�� �÷��̾��� �Ÿ� ���� (X, Y, Z)

    void LateUpdate()
    {
        // ī�޶� ���� ��ǥ ��ġ (�÷��̾� ��ġ + offset)
        Vector3 desiredPosition = player.position + offset;

        // ī�޶� �ε巴�� �̵��ϵ���
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, 
            desiredPosition, smoothSpeed);

        // Y���� �����ϰ�, X���� ���󰡵��� ����
        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z); // Y�� ����, X�� ����
    }

}
