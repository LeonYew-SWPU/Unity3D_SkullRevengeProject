using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float jumpSpeed;//修改跳跃高度

    [SerializeField]
    private float jumpingBufferTime; //设置跳跃误差时间

    [SerializeField]
    private Transform cameraTransform; //引入摄像机位置组件

    [SerializeField]
    private float jumpHorizontalSpeed;//对跳跃动画的水平向量的补充


    private GameObject pauseControlelr;//暂停游戏，获取参数

    private CharacterController characterController;
    private Animator animator;
    private float originalStepOffset; //跳跃前的偏倚
    private float ySpeed;
    private float? lastGroundedTime; //上一次接触地面的时间，?表示可以为空值(null)
    private float? lastJumpPressedTime; //上次点击跳跃的时间
    private bool isJumping;//跳跃状态
    private bool isGrounded;//落地状态


    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;// 存储原始偏移
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        inputMagnitude /= 2; // 默认行走

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.JoystickButton4))
        {
            inputMagnitude *= 2; //加速
        }

        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);//设置动画的移动速度(0-1)

        // 将移动方向转换为相机的方向
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        // 计算y高度
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // 每次刷新，更新落地时间和跳跃按钮时间
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        if(Input.GetButtonDown("Jump"))
        {
            lastJumpPressedTime = Time.time;
        }

        // 误差时间内，如果跳跃按钮被按下，则跳跃
        if (Time.time - lastGroundedTime <= jumpingBufferTime)
        {
            characterController.stepOffset = originalStepOffset; // 恢复原始偏移
            ySpeed = -0.5f;// 跳跃后给y固定一个稳定的值

            // 在地面
            isJumping = false;
            isGrounded = true;
            animator.SetBool("IsGrounded",true);
            animator.SetBool("IsJumping",false);
            animator.SetBool("IsFalling",false);

            if (Time.time - lastJumpPressedTime <= jumpingBufferTime)
            {
                // 跳跃中
                animator.SetBool("IsJumping",true);
                isJumping = true;

                //清空数值，以免重复跳跃
                ySpeed = jumpSpeed;
                lastJumpPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            // 不在地面
            animator.SetBool("IsGrounded", false);
            isGrounded = false;
            // 判断是否在下落
            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("IsFalling", true);
            }

            characterController.stepOffset = 0; // 跳跃时将原始偏移设置为0
        }

        if (movementDirection != Vector3.zero)
        {
            // 移动中
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // 停止移动
            animator.SetBool("IsMoving",false);
        }

        if (!isGrounded)
        {
            Vector3 velocity = movementDirection * inputMagnitude * jumpHorizontalSpeed;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);
        }
    }

    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            // 通过动画控制器的速度来控制移动
            Vector3 velocity = animator.deltaPosition;
            velocity.y = ySpeed * Time.deltaTime;

            characterController.Move(velocity);
        }
    }

    // 锁定焦点、隐藏光标
    //private void OnApplicationFocus(bool focus)
    //{
    //    if (focus)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //}



}
