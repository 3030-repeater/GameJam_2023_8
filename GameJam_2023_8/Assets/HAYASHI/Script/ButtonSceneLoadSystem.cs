using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSceneLoadSystem : MonoBehaviour
{
    [SerializeField, Header("�{�^���̖��O")]
    private string m_SceneName = "";

    public void ButtonSceneChange()
    {
        //�V�[�����[�h
        SceneManager.LoadScene(m_SceneName);
    }
}
