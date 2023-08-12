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
        //�X�^�[�g�t���O
        [SerializeField]
        private bool isStart = false;
        //�^�C���J�����[
        [SerializeField]
        private float m_timecal;
        //Booth�t���O
        [SerializeField]
        private bool isBooth = false;

        private void Start()
        {
            //����Scale
            m_motoScale = new Vector3(1, 1, 1);
            //���݂�Scale
            m_Scale = transform.localScale;
            m_CarMoveSpeed = 0;
            MoveToDestination(m_CurrentDestinationIndex);
            m_timecal = 0;
        }
        private void Update()
        {
            //���݂�Scale�l�擾
            m_Scale = transform.localScale;
            m_Timer += Time.deltaTime;
            if (!isStart)
            {
                if (m_StartTimer < m_Timer)
                {
                    m_CarMoveSpeed = 25;
                    isStart = true;
                }
            }
            //����Scale��茻�݂�Scale���傫�����
            if (m_motoScale.x < m_Scale.x)
            {
                //���񂾂�Scale�����̑傫���ɖ߂�
                //transform.localScale = new Vector3(m_Scale.x - Time.deltaTime * 0.1f, m_Scale.y - Time.deltaTime * 0.1f, m_Scale.z - Time.deltaTime * 0.1f);
                
                //Space�L�[������
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Scale����i�K������
                    transform.localScale = new Vector3(m_Scale.x - 1.0f, m_Scale.y - 1.0f, m_Scale.z - 1.0f);
                    //�u�[�X�g�t���O��ON
                    isBooth = true;
                }
            }
            //�u�[�X�g�t���O��ON��������
            if (isBooth)
            {
                m_timecal += Time.deltaTime;
                //�E�̎��Ԃ܂Ńu�[�X�g
                if (m_timecal <= 1.0f)//if (m_timecal <= 1.0f)//if (m_timecal <= 2.0f)
                {
                    //�����x
                    m_CarMoveSpeed += 0.3f;//m_CarMoveSpeed += 0.2f;//m_CarMoveSpeed += 0.1f;
                }
                else
                {
                    //�^�C���J�����[��0�ɖ߂�
                    m_timecal = 0;
                    //�u�[�X�g�t���O��OFF
                    isBooth = false;
                }
            }
            else
            {
                //�X�^�[�g�J�E���g���I�������
                if (m_StartTimer < m_Timer)
                {
                    //Scale�l��4�{�̎��̉����x
                    if (m_motoScale.x * 4 == m_Scale.x) {
                        m_CarMoveSpeed = 10;

                    }
                    //Scale�l��3�{�̎��̉����x
                    else if (m_motoScale.x * 3 == m_Scale.x)
                    {
                        m_CarMoveSpeed = 15;
                    }
                    //Scale�l��2�{�̎��̉����x
                    else if (m_motoScale.x * 2 == m_Scale.x)
                    {
                        m_CarMoveSpeed = 20;
                    }
                    //Scale�l�����{�̎��̉����x
                    else
                    {
                        m_CarMoveSpeed = 25;
                    }
                }
                //�����Ȃ��̏ꍇ
                //m_CarMoveSpeed = 25;
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
            
            //Scale�`�F���W�p
            //Collider�ɐڐG�����Ƃ�
            if(other.CompareTag("Cal"))
            {
                Destroy(other.gameObject);
                //Cal��ChangeCal�֐�����l���擾����
                switch (m_cal.ChangeCal())
                {
                    case 1:
                        //Scale�l2�{
                        transform.localScale = new Vector3(m_Scale.x * 2, m_Scale.y * 2, m_Scale.z * 2);
                        break;
                    case 2:
                        //Scale�l3�{
                        transform.localScale = new Vector3(m_Scale.x * 3, m_Scale.y * 3, m_Scale.z * 3);
                        break;
                    case 3:
                        //Scale�l4�{
                        transform.localScale = new Vector3(m_Scale.x * 4, m_Scale.y * 4, m_Scale.z * 4);
                        break;
                }

            }
        }
    }

}