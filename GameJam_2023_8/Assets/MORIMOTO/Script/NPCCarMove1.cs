using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace HAYASHI_MORIMOTO.Script
{
    [RequireComponent(typeof(NPCCarMove1))]
    public class NPCCarMove1 : MonoBehaviour
    {
        // �ړI�n�̃I�u�W�F�N�g��z��ŕێ�
        [SerializeField]
        private Transform[] m_Destinations;
        // �Ԃ̈ړ����x
        [SerializeField]
        private float m_CarMoveSpeed = 25f;
        // ���݂̖ړI�n�̃C���f�b�N�X
        private int m_CurrentDestinationIndex = 0;
        [SerializeField]
        private float m_StartTimer= 7;
        [SerializeField]
        private float m_Timer;
        // Cal�̃X�N���v�g
        [SerializeField]
        public Cal m_cal;
        //�X�P�[��
        [SerializeField]
        Vector3 m_Scale;
        //���X�P�[��
        [SerializeField]
        Vector3 m_motoScale;

        private void Start()
        {
            m_motoScale = new Vector3(1, 1, 1);
            m_Scale = transform.localScale;
            m_CarMoveSpeed = 0;
            MoveToDestination(m_CurrentDestinationIndex);
            
        }
        private void Update()
        {
            m_Scale = transform.localScale;
            m_Timer += Time.deltaTime;
            if (m_StartTimer<m_Timer)
            {
                m_CarMoveSpeed = 25;
            }
            if (m_motoScale.x < m_Scale.x)
            {
                transform.localScale = new Vector3(m_Scale.x - Time.deltaTime * 0.5f, m_Scale.y - Time.deltaTime * 0.5f, m_Scale.z - Time.deltaTime * 0.5f);
                //m_Scale.x -= Time.deltaTime;
                //m_Scale.y -= Time.deltaTime;
                //m_Scale.z -= Time.deltaTime;
            }
        }
        private void MoveToDestination(int destinationIndex)
        {
            // �ړI�n�������ȃC���f�b�N�X�̏ꍇ�͏I��
            if (destinationIndex >= m_Destinations.Length || destinationIndex < 0)
                return;

            // �ړI�n�ւ̈ړ����J�n
            StartCoroutine(MoveCoroutine(m_Destinations[destinationIndex].position));
        }

        private IEnumerator MoveCoroutine(Vector3 destination)
        {
            //�ԗ����ړI�n�ɓ��B����܂Ń��[�v
            //�ԗ��̌��݈ʒu�ƖړI�n�̋������v�Z��0.05���ȏ�傫���ƃ��[�v����
            while (Vector3.Distance(transform.position, destination) > 0.05f)
            {
                //�ړI�n�̕������v�Z
                Vector3 lookDir = destination - transform.position;
                //�ԗ������������ɉ�]���Ȃ��悤�ɂ���
                //���ꂪ�Ȃ��Ǝԗ��̋������o�O��܂�
                lookDir.y = 0f;
                if (lookDir != Vector3.zero)
                {
                    // �ړI�n�̕���������
                    transform.rotation = Quaternion.LookRotation(lookDir);
                }

                // �ړI�n�ւ̈ړ�
                transform.position = Vector3.MoveTowards(transform.position, destination, m_CarMoveSpeed * Time.deltaTime);
                yield return null;
            }

            // �ړI�n�ɓ��������玟�̖ړI�n��
            m_CurrentDestinationIndex++;
            if (m_CurrentDestinationIndex >= m_Destinations.Length)
            {
                // �Ō�̖ړI�n�ɓ��B������ŏ��ɖ߂�
                m_CurrentDestinationIndex = 0;
            }
            // ���̖ړI�n�ֈړ�
            MoveToDestination(m_CurrentDestinationIndex);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                Destroy(other.gameObject);
                m_CarMoveSpeed *= 2;
            }
            
            if(other.CompareTag("Cal"))
            {
                Destroy(other.gameObject);
                switch (m_cal.ChangeCal())
                {
                    case 1:
                        transform.localScale = new Vector3(m_Scale.x * 2, m_Scale.y * 2, m_Scale.z * 2);
                        break;
                    case 2:
                        transform.localScale = new Vector3(m_Scale.x * 3, m_Scale.y * 3, m_Scale.z * 3);
                        break;
                    case 3:
                        transform.localScale = new Vector3(m_Scale.x * 4, m_Scale.y * 4, m_Scale.z * 4);
                        break;
                }

            }
        }
    }

}