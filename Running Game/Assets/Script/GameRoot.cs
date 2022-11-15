using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public struct ResultInfo
{
    public int score, coin, health;
}

    public class GameRoot : MonoBehaviour
{

    private PlayerControl player = null;
    private ResultInfo high;
    private ResultInfo current;
    private UIControl uiControl = null;

    private void ReadFile()
    {
        string fullpth = "./score_data.txt";
        StreamReader sr;
        sr = new StreamReader(new FileStream(fullpth, FileMode.OpenOrCreate));

        string[] line = { null,null };
        for (int i = 0; i < 2; i++)
            line[i] = sr.ReadLine();
        sr.Close();

        if (line[1] == null)
        {
            this.high.score = 0;
            return;
        }
        else {
            string[] words = line[1].Split();
            this.high.score = int.Parse(words[0]);
            this.high.coin = int.Parse(words[1]);
            this.high.health = int.Parse(words[2]);
        }
    }


    private void WriteFile()
    {
        ReadFile();

        string fullpth = "./score_data.txt";
        StreamWriter sw;
        sw = new StreamWriter(fullpth);

        this.current.score = this.uiControl.score;
        this.current.coin = this.uiControl.playerItem.getCoinNum;
        this.current.health = this.uiControl.playerControl.health;

        if (false == File.Exists(fullpth))
        {
            sw.WriteLine(this.current.score + " " + this.current.coin + " " + this.current.health);
            sw.WriteLine(this.current.score + " " + this.current.coin + " " + this.current.health);
        }
        else
        {
            if (this.high.score < this.current.score)
            {
                sw.WriteLine(this.current.score + " " + this.current.coin + " " + this.current.health);
                sw.WriteLine(this.current.score + " " + this.current.coin + " " + this.current.health);
            }
            else
            {
                sw.WriteLine(this.current.score + " " + this.current.coin + " " + this.current.health);
                sw.WriteLine(this.high.score + " " + this.high.coin + " " + this.high.health);
            }
        }

        sw.Flush();
        sw.Close();
    }

    void Start()
    {                
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        this.uiControl = this.GetComponent<UIControl>();
    }

    public float step_timer = 0.0f; // 경과 시간을 유지한다.
    void Update()
    {
        this.step_timer += Time.deltaTime; // 경과 시간을 더해 간다.

        if (this.player.isPlayEnd())
        {
            WriteFile();
            SceneManager.LoadScene("LoseScene");
        }

        if (this.uiControl.score >= 3000)
        {
            WriteFile();
            SceneManager.LoadScene("WinScene");
        }

    }
    public float getPlayTime()
    {
        float time;
        time = this.step_timer;
        return (time); // 호출한 곳에 경과 시간을 알려준다.
    }
}
