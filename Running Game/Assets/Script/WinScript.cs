using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public struct WinInfo
{
    public int score, coin, health, sum;
}

public class WinScript : MonoBehaviour
{
    public GUISkin guiskin1, guiskin2;

    private WinInfo high;
    private WinInfo current;

    private void ReadFile()
    {
        string fullpth = "./score_data.txt";
        StreamReader sr;
        sr = new StreamReader(fullpth);

        string[] line = { null, null };
        for (int i = 0; i < 2; i++)
            line[i] = sr.ReadLine();
        sr.Close();

        for (int i = 0; i < 2; i++)
        {
            string[] words = line[i].Split();
            if (i == 0)
            {
                this.current.score = int.Parse(words[0]);
                this.current.coin = int.Parse(words[1]);
                this.current.health = int.Parse(words[2]);
                this.current.sum = this.current.score + this.current.coin + this.current.health * 500;
            }
            else if (i == 1)
            {
                this.high.score = int.Parse(words[0]);
                this.high.coin = int.Parse(words[1]);
                this.high.health = int.Parse(words[2]);
                this.high.sum = this.high.score + this.high.coin + this.high.health * 500;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {                
        ReadFile();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.skin = this.guiskin2;

        if (this.current.sum == this.high.sum)
        {
            GUI.Label(new Rect(Screen.width / 2 + 50, Screen.height / 2 - 310, 1000, 100), "기록 갱신!");
        }
        GUI.TextField(new Rect(Screen.width / 2 - 600, Screen.height / 2 - 230, 1200, 350), "");

        GUI.Label(new Rect(Screen.width / 2 - 550, Screen.height / 2 - 230, 1000, 100), "시간 점수 : " + this.current.score + " * 1");
        GUI.Label(new Rect(Screen.width / 2 - 550, Screen.height / 2 - 150, 1000, 100), "코인 점수: " + this.current.coin + " * 1");
        GUI.Label(new Rect(Screen.width / 2 - 550, Screen.height / 2 - 70, 1000, 100), "목숨 점수 : " + this.current.health + " * 500");
        GUI.Label(new Rect(Screen.width / 2 - 550, Screen.height / 2 + 10, 1000, 100), "현재 전체 점수 : " + this.current.sum);

        GUI.Label(new Rect(Screen.width / 2 + 50, Screen.height / 2 - 230, 1000, 100), "시간 점수 : " + this.high.score + " * 1");
        GUI.Label(new Rect(Screen.width / 2 + 50, Screen.height / 2 - 150, 1000, 100), "코인 점수 : " + this.high.coin + " * 1");
        GUI.Label(new Rect(Screen.width / 2 + 50, Screen.height / 2 - 70, 1000, 100), "목숨 점수 : " + this.high.health + " * 500");
        GUI.Label(new Rect(Screen.width / 2 + 50, Screen.height / 2 + 10, 1000, 100), "최고 전체 점수 : " + this.high.sum);

        GUI.skin = this.guiskin1;
        GUI.Label(new Rect(Screen.width / 2 - 500, Screen.height / 2 - 400, 1000, 100), "성공!");
        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 150, 400, 100), "메인화면"))
        {
            SceneManager.LoadScene("TitleScene");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 300, 400, 100), "다시하기"))
        {
            SceneManager.LoadScene("GameScene");
        }

    }
}
