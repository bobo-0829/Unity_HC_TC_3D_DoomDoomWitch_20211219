using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 回合資料
    /// </summary>
    public Turn turn = Turn.My;

    [Header("敵方回合事件")]
    public UnityEvent onEnemyTurn;
    [Header("怪物陣列")]
    public GameObject[] goEnemys; 
    [Header("彈珠")]
    public GameObject goMarble;
    [Header("棋盤群組")]
    public Transform tracheckboard;
    [Header("生成數量的最小最大值")]
    public Vector2Int v2RandomEnemyCount = new Vector2Int(1, 7);
    [SerializeField]
    private Transform[] tracheckboards;
    /// <summary>
    /// 第二列 : 生成怪物的棋盤
    /// </summary>
    private Transform[] traColumnSecond;
    /// <summary>
    /// 棋盤欄位數量
    /// </summary>
    private int countRow = 8;
    /// <summary>
    /// 第二列的索引值 : 處理怪物生成不重複
    /// </summary>
    [SerializeField]
    private List<int> indexColumnSecond = new List<int>();
    private ControlSystem controlSystem;

    private void Awake()
    {
        //棋盤陣列 = 棋盤群組.啟德子物件的原件<變形元件>()
        tracheckboards = tracheckboard.GetComponentsInChildren<Transform>();

        //初始第二列的數量
        traColumnSecond = new Transform[countRow];
        //取得第二列的棋盤
        for (int i = 9; i < 9+countRow; i++)
        {
            traColumnSecond[i - countRow - 1] = tracheckboards[i];
        }

        controlSystem = FindObjectOfType<ControlSystem>();

        SpawnEnemy();
    }

    /// <summary>
    /// 生成敵人 : 隨機數量 v2RandomEnemyCount
    /// </summary>
    private void SpawnEnemy()
    {
        int countEnemy = Random.Range(v2RandomEnemyCount.x, v2RandomEnemyCount.y);

        indexColumnSecond.Clear();                                                                  //清除上次剩餘的資料

        for (int i = 0; i < 8; i++) indexColumnSecond.Add(i);                                       //初始數字0 ~ 7

        for (int i = 0; i < countEnemy; i++)                                                        
        {
            int randomEnemy = Random.Range(0, goEnemys.Length);                                     //0 ~ 2 - 隨機 0 或 1

            int radomColumnSecond = Random.Range(0, indexColumnSecond.Count);                       //隨機第二列的索引值 : 第一次 0 ~ 7 ，第二次抓剩餘的數量隨機值

            Instantiate(goEnemys[randomEnemy], traColumnSecond[indexColumnSecond[radomColumnSecond]].position, Quaternion.identity);

            indexColumnSecond.RemoveAt(radomColumnSecond);                                          // 刪除已經放置怪物的第二列棋盤
        }

        int randomMarble = Random.Range(0, indexColumnSecond.Count);
        Instantiate(goMarble, traColumnSecond[indexColumnSecond[randomMarble]].position+Vector3.up, Quaternion.identity);
    }

    private bool canSpawn = true;

    /// <summary>
    /// 切換回合
    /// </summary>
    /// <param name="isMyTurn"></param>
    public void SwitchTurn(bool isMyTurn)
    {
        if (isMyTurn)
        {
            turn = Turn.My;
            controlSystem.canShoot = true;
            RecycleMarble.recycleMarbles = 0;       //回收的數量歸零
            if (canSpawn)                           //如果 可以生成
            {
                canSpawn = false;                       //不能生成
                Invoke("SpawnEnemy", 0.8f);             //呼叫生成敵人
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
/// 回合 : 我方與敵方
/// </summary>
public enum Turn
{
    My, Enumy
}
