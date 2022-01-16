using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �^�X���
    /// </summary>
    public Turn turn = Turn.My;

    [Header("�Ĥ�^�X�ƥ�")]
    public UnityEvent onEnemyTurn;
    [Header("�Ǫ��}�C")]
    public GameObject[] goEnemys; 
    [Header("�u�]")]
    public GameObject goMarble;
    [Header("�ѽL�s��")]
    public Transform tracheckboard;
    [Header("�ͦ��ƶq���̤p�̤j��")]
    public Vector2Int v2RandomEnemyCount = new Vector2Int(1, 7);
    [SerializeField]
    private Transform[] tracheckboards;
    /// <summary>
    /// �ĤG�C : �ͦ��Ǫ����ѽL
    /// </summary>
    private Transform[] traColumnSecond;
    /// <summary>
    /// �ѽL���ƶq
    /// </summary>
    private int countRow = 8;
    /// <summary>
    /// �ĤG�C�����ޭ� : �B�z�Ǫ��ͦ�������
    /// </summary>
    [SerializeField]
    private List<int> indexColumnSecond = new List<int>();
    private ControlSystem controlSystem;

    private void Awake()
    {
        //�ѽL�}�C = �ѽL�s��.�Ҽw�l���󪺭��<�ܧΤ���>()
        tracheckboards = tracheckboard.GetComponentsInChildren<Transform>();

        //��l�ĤG�C���ƶq
        traColumnSecond = new Transform[countRow];
        //���o�ĤG�C���ѽL
        for (int i = 9; i < 9+countRow; i++)
        {
            traColumnSecond[i - countRow - 1] = tracheckboards[i];
        }

        controlSystem = FindObjectOfType<ControlSystem>();

        SpawnEnemy();
    }

    /// <summary>
    /// �ͦ��ĤH : �H���ƶq v2RandomEnemyCount
    /// </summary>
    private void SpawnEnemy()
    {
        int countEnemy = Random.Range(v2RandomEnemyCount.x, v2RandomEnemyCount.y);

        indexColumnSecond.Clear();                                                                  //�M���W���Ѿl�����

        for (int i = 0; i < 8; i++) indexColumnSecond.Add(i);                                       //��l�Ʀr0 ~ 7

        for (int i = 0; i < countEnemy; i++)                                                        
        {
            int randomEnemy = Random.Range(0, goEnemys.Length);                                     //0 ~ 2 - �H�� 0 �� 1

            int radomColumnSecond = Random.Range(0, indexColumnSecond.Count);                       //�H���ĤG�C�����ޭ� : �Ĥ@�� 0 ~ 7 �A�ĤG����Ѿl���ƶq�H����

            Instantiate(goEnemys[randomEnemy], traColumnSecond[indexColumnSecond[radomColumnSecond]].position, Quaternion.identity);

            indexColumnSecond.RemoveAt(radomColumnSecond);                                          // �R���w�g��m�Ǫ����ĤG�C�ѽL
        }

        int randomMarble = Random.Range(0, indexColumnSecond.Count);
        Instantiate(goMarble, traColumnSecond[indexColumnSecond[randomMarble]].position+Vector3.up, Quaternion.identity);
    }

    private bool canSpawn = true;

    /// <summary>
    /// �����^�X
    /// </summary>
    /// <param name="isMyTurn"></param>
    public void SwitchTurn(bool isMyTurn)
    {
        if (isMyTurn)
        {
            turn = Turn.My;
            controlSystem.canShoot = true;
            RecycleMarble.recycleMarbles = 0;       //�^�����ƶq�k�s
            if (canSpawn)                           //�p�G �i�H�ͦ�
            {
                canSpawn = false;                       //����ͦ�
                Invoke("SpawnEnemy", 0.8f);             //�I�s�ͦ��ĤH
            }
        }
        else
        {
            canSpawn = true;
            turn = Turn.Enumy;
            onEnemyTurn.Invoke();
        }
    }
}
/// <summary>
/// �^�X : �ڤ�P�Ĥ�
/// </summary>
public enum Turn
{
    My, Enumy
}
