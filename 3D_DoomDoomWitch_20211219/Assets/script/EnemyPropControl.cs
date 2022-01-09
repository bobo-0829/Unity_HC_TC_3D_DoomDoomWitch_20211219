using UnityEngine;

public class EnemyPropControl : MonoBehaviour
{
    public GameManager gm;

    [Header("�C�����ʪ��Z��")]
    public float moveDistance = 2; 
    [Header("���ʪ��y�Щ��u")]
    public float moveUnderLine = 2;


    /// <summary>
    /// ����
    /// </summary>
    private void Move()
    {
        transform.position += Vector3.forward * moveDistance;

        if (transform.position.z <= moveUnderLine) DestroyObject();
    }

    /// <summary>
    /// �R������
    /// </summary>
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
