using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �����x�̃��[�g
    [SerializeField]
    private float m_AccelerationRate = 10f;
    // �ő呬�x
    [SerializeField]
    private float m_MaxSpeed = 25f; 
    [SerializeField]
    private float m_CurrentSpeed = 0f; // ���݂̑��x
    [SerializeField]
    private bool isAccelerating = false; // �{�^���������Ă��邩�ǂ����̃t���O

    private void Update()
    {
        // �L�[���͂ɂ������E��������
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;
            m_CurrentSpeed = Mathf.Max(m_CurrentSpeed - m_AccelerationRate * Time.deltaTime, 5f); // �{�^���𗣂����猸��
        }

        // �����x�̌v�Z
        if (isAccelerating)
        {
            m_CurrentSpeed = Mathf.Min(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, m_MaxSpeed); // �ő呬�x�𒴂��Ȃ��悤�ɐ���
        }

        // �L�[���͂ɂ��ړ�����
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * m_CurrentSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
