using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    public GUISkin guiskin;
    public Texture2D coinTexture, magnetTexture, acornTexture;

    private int page;
    // Start is called before the first frame update
    void Start()
    {
        this.page = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SceneManager.LoadScene("GameScene");
        //}
    }

    void OnGUI()
    {
        GUI.skin = this.guiskin;
        GUI.Label(new Rect(Screen.width / 2 - 500, Screen.height / 2 - 300, 1000, 100), "플라잉 날다람쥐");

        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2, 400, 100), "게임시작"))
        {
            SceneManager.LoadScene("GameScene");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 200, 400, 100), "게임방법"))
        {
            this.page = 1;
        }
        if (this.page == 1)
        {
            GUI.Box(new Rect(Screen.width / 2 - 600, Screen.height / 2 - 400, 1200, 800),
                "조작법 \n\n\n" +
                "마우스 좌클릭시 아래의 행동을 합니다.\n" +
                "----------------------------------------\n\n" +
                "착지상태 :  점프\n" +
                "착지상태 아닐때 : 공기저항\n" +
                "----------------------------------------\n\n" +
                "공기저항 상태는 마우스를 누른 채로 있어야 유지됩니다.\n" +
                "좌측 상단에 있는 공기저항 게이지가 다 닳으면 사용할 수 없습니다.\n" +
                "공기저항 게이지는 공기저항 상태가 아닐 때 차오릅니다." +
                "\n\n\n" +
                "1/4");

            if (GUI.Button(new Rect(Screen.width / 2 + 530, Screen.height / 2 - 380, 50, 50), "x"))
            {
                this.page = 0;
            }

            if (GUI.Button(new Rect(Screen.width / 2 + 530, Screen.height / 2 + 330, 50, 50), ">"))
            {
                this.page = 2;
            }
        }
        else if (this.page == 2)
        {
            GUI.Box(new Rect(Screen.width / 2 - 600, Screen.height / 2 - 400, 1200, 800),
                "아이템 \n\n\n\n" +
                "코인 : 최종점수를 증가시킵니다.                      \n\n" +
                "자석 : 주변 모든 아이템을 흡수합니다.             \n\n" +
                "도토리 : 공기저항 게이지를 1/3 회복시킵니다.\n\n" +
                "\n\n\n\n\n" +
                "2/4");

            GUI.Label(new Rect(Screen.width / 2 - 550, Screen.height / 2 - 220, 100, 100), this.coinTexture);
            GUI.Label(new Rect(Screen.width / 2 - 550, Screen.height / 2 - 120, 100, 100), this.magnetTexture);
            GUI.Label(new Rect(Screen.width / 2 - 550, Screen.height / 2 - 20, 100, 100), this.acornTexture);

            if (GUI.Button(new Rect(Screen.width / 2 + 530, Screen.height / 2 - 380, 50, 50), "x"))
            {
                this.page = 0;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 580, Screen.height / 2 + 330, 50, 50), "<"))
            {
                this.page = 1;
            }

            if (GUI.Button(new Rect(Screen.width / 2 + 530, Screen.height / 2 + 330, 50, 50), ">"))
            {
                this.page = 3;
            }
        }
        else if (this.page == 3)
        {
            GUI.Box(new Rect(Screen.width / 2 - 600, Screen.height / 2 - 400, 1200, 800),
                "\n\n\n\n\n\n" +
                "일시정지 기능 \n" +
                "\n" +
                "게임 진행중에 esc 키를 사용하여\n" +
                "일시정지 설정 / 해제가 가능합니다.\n" +  
                "\n\n\n\n\n"+
                "3/4");

            if (GUI.Button(new Rect(Screen.width / 2 + 530, Screen.height / 2 - 380, 50, 50), "x"))
            {
                this.page = 0;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 580, Screen.height / 2 + 330, 50, 50), "<"))
            {
                this.page = 2;
            }

            if (GUI.Button(new Rect(Screen.width / 2 + 530, Screen.height / 2 + 330, 50, 50), ">"))
            {
                this.page = 4;
            }
        }

        else if (this.page == 4)
        {
            GUI.Box(new Rect(Screen.width / 2 - 600, Screen.height / 2 - 400, 1200, 800),
                "승리조건 \n" +
                "\n" +
                "점수 3000점 달성 (5분)\n\n\n" +
                "패배조건\n" +
                "\n" +
                "체력이 0이 될 때\n" +
                "구멍에 빠졌을 때\n\n\n" +
                "체력 감소 조건\n\n" +
                "장애물에 정면으로 충돌하였을 때\n\n" +
                "4/4");

            if (GUI.Button(new Rect(Screen.width / 2 + 530, Screen.height / 2 - 380, 50, 50), "x"))
            {
                this.page = 0;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 580, Screen.height / 2 + 330, 50, 50), "<"))
            {
                this.page = 3;
            }
        }
    }

}
