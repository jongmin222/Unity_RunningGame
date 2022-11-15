using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{

    private float currentTime;
    public int score;
    public GUISkin guiskin;
    public GetItem playerItem = null; // 씬상의 Player를 보관.
    public PlayerControl playerControl = null;
    private MapCreator mapCreator = null;
    private GameObject healthUI = null;
    private PauseScript pauseScript = null;
    private int tempHealth;
    // Start is called before the first frame update
    void Start()
    {
        this.currentTime = 0;
        this.playerItem = GameObject.FindGameObjectWithTag("Player").GetComponent<GetItem>();
        this.playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        this.mapCreator = this.GetComponent<MapCreator>();
        this.healthUI = GameObject.FindWithTag("Canvas").transform.Find("Health").gameObject;
        this.tempHealth = this.playerControl.health;
        this.pauseScript = this.GetComponent<PauseScript>();
    }
    
    // Update is called once per frame
    void Update()
    {
        this.currentTime += Time.deltaTime;
        this.score = (int)(this.currentTime * 10)/* + this.player.getCoinNum * 10*/;

        if (this.tempHealth != this.playerControl.health)
        {
            for (int i = 0; i < 3; i++)
            {
                this.healthUI.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
                this.healthUI.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
            }

            for (int i = 0; i < this.playerControl.health; i++)
            {
                this.healthUI.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
                this.healthUI.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(true);
            }
            this.tempHealth = this.playerControl.health;
        }
    }

    void OnGUI()
    {
        GUI.skin = this.guiskin;
        GUI.Label(new Rect(Screen.width/2 -500, 20, 1000, 100), "점수 : " + this.score);
        GUI.Label(new Rect(Screen.width - 800, 20, 1000, 100), "코인 : " + this.playerItem.getCoinNum);

        if (this.playerControl.level_control.level == 4 ||
            this.playerControl.level_control.level == 9 ||
            this.playerControl.level_control.level == 14)
            GUI.Label(new Rect(Screen.width / 2 - 500, 300, 1000, 100), "속도가 잠시 빨라집니다!");

        if (this.pauseScript.isPause)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 400, 100), "처음부터"))
            {
                SceneManager.LoadScene("GameScene");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 100, 400, 100), "메인메뉴"))
            {
                SceneManager.LoadScene("TitleScene");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 100), "진행하기"))
            {
                this.pauseScript.isPause = false;
            }

            GUI.TextField(new Rect(Screen.width / 2 - 600, Screen.height / 2 - 250, 1200, 500), "");

            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 400, 100), "처음부터"))
            {
                SceneManager.LoadScene("GameScene");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 100, 400, 100), "메인메뉴"))
            {
                SceneManager.LoadScene("TitleScene");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 100), "진행하기"))
            {
                this.pauseScript.isPause = false;
            }
        }
    }
}
