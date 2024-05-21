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
    private float jumpSpeed;//�޸���Ծ�߶�

    [SerializeField]
    private float jumpingBufferTime; //������Ծ���ʱ��

    [SerializeField]
    private Transform cameraTransform; //���������λ�����

    [SerializeField]
    private float jumpHorizontalSpeed;//����Ծ������ˮƽ�����Ĳ���


    private GameObject pauseControlelr;//��ͣ��Ϸ����ȡ����

    private CharacterController characterController;
    private Animator animator;
    private float originalStepOffset; //��Ծǰ��ƫ��
    private float ySpeed;
    private float? lastGroundedTime; //��һ�νӴ������ʱ�䣬?��ʾ����Ϊ��ֵ(null)
    private float? lastJumpPressedTime; //�ϴε����Ծ��ʱ��
    private bool isJumping;//��Ծ״̬
    private bool isGrounded;//���״̬


    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;// �洢ԭʼƫ��
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        inputMagnitude /= 2; // Ĭ������

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.JoystickButton4))
        {
            inputMagnitude *= 2; //����
        }

        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);//���ö������ƶ��ٶ�(0-1)

        // ���ƶ�����ת��Ϊ����ķ���
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        // ����y�߶�
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // ÿ��ˢ�£��������ʱ�����Ծ��ťʱ��
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        if(Input.GetButtonDown("Jump"))
        {
            lastJumpPressedTime = Time.time;
        }

        // ���ʱ���ڣ������Ծ��ť�����£�����Ծ
        if (Time.time - lastGroundedTime <= jumpingBufferTime)
        {
            characterController.stepOffset = originalStepOffset; // �ָ�ԭʼƫ��
            ySpeed = -0.5f;// ��Ծ���y�̶�һ���ȶ���ֵ

            // �ڵ���
            isJumping = false;
            isGrounded = true;
            animator.SetBool("IsGrounded",true);
            animator.SetBool("IsJumping",false);
            animator.SetBool("IsFalling",false);

            if (Time.time - lastJumpPressedTime <= jumpingBufferTime)
            {
                // ��Ծ��
                animator.SetBool("IsJumping",true);
                isJumping = true;

                //�����ֵ�������ظ���Ծ
                ySpeed = jumpSpeed;
                lastJumpPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            // ���ڵ���
            animator.SetBool("IsGrounded", false);
            isGrounded = false;
            // �ж��Ƿ�������
            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("IsFalling", true);
            }

            characterController.stepOffset = 0; // ��Ծʱ��ԭʼƫ������Ϊ0
        }

        if (movementDirection != Vector3.zero)
        {
            // �ƶ���
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // ֹͣ�ƶ�
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
            // ͨ���������������ٶ��������ƶ�
            Vector3 velocity = animator.deltaPosition;
            velocity.y = ySpeed * Time.deltaTime;

            characterController.Move(velocity);
        }
    }

    // �������㡢���ع��
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
