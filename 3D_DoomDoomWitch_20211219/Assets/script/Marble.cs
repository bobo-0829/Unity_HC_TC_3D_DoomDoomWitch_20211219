using UnityEngine;

/// <summary>
/// �u�]�t��
/// </summary>
public class Marble : MonoBehaviour
{
    private void Awake()
    {
        //���z.�����ϼh�I��(A �ϼh �A B�ϼh) ���� A B �ϼh�I��
        Physics.IgnoreLayerCollision(7, 7);
    }

}
