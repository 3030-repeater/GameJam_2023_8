using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField, Header("加速度のレート")]
    private float m_AccelerationRate = 5f;
    [SerializeField, Header("最大スピード")]
    private float m_MaxSpeed = 50f;
    [SerializeField, Header("ジャンプ力")]
    private float m_JumpForce = 10f;
    [SerializeField, Header("ボタン押したときの回転値")]
    private float m_RotationSpeed = 90f;
    [SerializeField, Header("現在の速度")]
    private float m_CurrentSpeed = 50f;
    [SerializeField]
    private bool isAccelerating = true;
    private bool isGrounded = true;
   

    // Update is called once per frame
    void Update()
    {
    
        Physics.Raycast(transform.position, Vector3.down, 0.10f);


        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.10f);
        if (Input.GetKey(KeyCode.A) ||
     Input.GetKey(KeyCode.S) ||
     Input.GetKey(KeyCode.D))
        {
            isAccelerating = true;
            m_CurrentSpeed = Mathf.Max(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, 10f);
        }
        else
        {
            isAccelerating = true;
            m_CurrentSpeed = Mathf.Max(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, 10f);

        }
        if (isAccelerating)
        {
                  m_CurrentSpeed = Mathf.Min(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, m_MaxSpeed);
        }
        else
        {
            m_CurrentSpeed = Mathf.Min(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, m_MaxSpeed);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) *
            m_CurrentSpeed * Time.deltaTime;
        transform.Translate(movement);
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -m_RotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime);
        }

    }

}
