using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushObject : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude; //给一个推动的力

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rigidBody = hit.collider.attachedRigidbody; //获取物体的Rigidbody组件

        if (rigidBody != null)
        {
            var forceDirection = hit.gameObject.transform.position - transform.position; // 计算力的方向
            forceDirection.y = 0; // 不设置垂直方向的力
            forceDirection.Normalize();

            rigidBody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse); // Impulse 脉冲，持续施加


        }
    }
}
