using UnityEngine;

/// <summary>
/// �u�]�t��
/// </summary>
public class Marble : MonoBehaviour
{
    /// <summary>
    /// �����O
    /// </summary>
    public float attack;
    private void Awake()
    {
        //���z.�����ϼh�I��(A �ϼh �A B�ϼh) ���� A B �ϼh�I��
        Physics.IgnoreLayerCollision(7, 7);
    }

}
