using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ����t��
/// ���V�ƹ���m
/// �o�g�u�]
/// �^�X����
/// </summary>
public class ControlSystem : MonoBehaviour
{
    #region ���
    [Header("�b�Y")]
    public GameObject goArrow;
    [Header("�ͦ��u�]��m")]
    public Transform traSpawnPoint;
    [Header("�u�]�w�s��")]
    public GameObject goMarbles;
    [Header("�o�g�t��"), Range(0, 5000)]
    public float speedShoot = 750;
    [Header("�g�u�n�I�����ϼh")]
    public LayerMask layerToHit;
    [Header("���շƹ���m")]
    public Transform traTestMousePosition;
    #endregion



    #region �ƥ�
    private void Update()
    {
        MouseControl();
    }
    #endregion


    #region ��k

    /// <summary>
    /// �ƹ�����
    /// </summary>
    private void MouseControl()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 v3Mouse = Input.mousePosition;

            //print("�ƹ��y�� : " + v3Mouse);

            //�g�u - �D�n��v��.�ù��y����g�u(�ƹ��y��)
            Ray rayMouse = Camera.main.ScreenPointToRay(v3Mouse);
            //�g�u�I����T
            RaycastHit hit;

            //�p�G �g�u���쪫��N�B�z
            //���z �g�u�I��(�g�u�A�Z��)
            if (Physics.Raycast(rayMouse, out hit, 100, layerToHit))
            {
                print("�ƹ��g�����쪫��~~" + hit.collider.name);

                Vector3 hitPosition = hit.point;                // ���o�I����T���y��
                hitPosition.y = 0.5f;                           //�վ㰪�׶b�V
                traTestMousePosition.position = hitPosition;    //��s���ժ���y��
            }
        }
    }
    #endregion
}
