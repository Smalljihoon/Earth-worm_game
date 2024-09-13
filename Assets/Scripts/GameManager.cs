using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임오버 , 플레이 타임,

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance
    { get { return instance; } }

    [SerializeField] TMP_Text tailcount = null;
    [SerializeField] TMP_Text PT = null;    

    public float playtime = 0;

    void Awake()
    {
        instance= this;
    }

    void Update()
    {
        playtime += Time.deltaTime;
        string time = string.Format("{0:F2}", playtime);
        PT.text = "Time : " + time.ToString() + "s";
        tailcount.text = "Tail : " + Worm.instance.count;
    }

    public void RetryClick()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
}
