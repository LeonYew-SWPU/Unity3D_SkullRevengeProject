using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushObject : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude; //��һ���ƶ�����

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rigidBody = hit.collider.attachedRigidbody; //��ȡ�����Rigidbody���

        if (rigidBody != null)
        {
            var forceDirection = hit.gameObject.transform.position - transform.position; // �������ķ���
            forceDirection.y = 0; // �����ô�ֱ�������
            forceDirection.Normalize();

            rigidBody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse); // Impulse ���壬����ʩ��


        }
    }
}
