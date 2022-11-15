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
        GUI.Label(new Rect(Screen.width / 2 - 500, Screen.height / 2 - 300, 1000, 100), "�ö��� ���ٶ���");

        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2, 400, 100), "���ӽ���"))
        {
            SceneManager.LoadScene("GameScene");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 200, 400, 100), "���ӹ��"))
        {
            this.page = 1;
        }
        if (this.page == 1)
        {
            GUI.Box(new Rect(Screen.width / 2 - 600, Screen.height / 2 - 400, 1200, 800),
                "���۹� \n\n\n" +
                "���콺 ��Ŭ���� �Ʒ��� �ൿ�� �մϴ�.\n" +
                "----------------------------------------\n\n" +
                "�������� :  ����\n" +
                "�������� �ƴҶ� : ��������\n" +
                "----------------------------------------\n\n" +
                "�������� ���´� ���콺�� ���� ä�� �־�� �����˴ϴ�.\n" +
                "���� ��ܿ� �ִ� �������� �������� �� ������ ����� �� �����ϴ�.\n" +
                "�������� �������� �������� ���°� �ƴ� �� �������ϴ�." +
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
                "������ \n\n\n\n" +
                "���� : ���������� ������ŵ�ϴ�.                      \n\n" +
                "�ڼ� : �ֺ� ��� �������� ����մϴ�.             \n\n" +
                "���丮 : �������� �������� 1/3 ȸ����ŵ�ϴ�.\n\n" +
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
                "�Ͻ����� ��� \n" +
                "\n" +
                "���� �����߿� esc Ű�� ����Ͽ�\n" +
                "�Ͻ����� ���� / ������ �����մϴ�.\n" +  
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
                "�¸����� \n" +
                "\n" +
                "���� 3000�� �޼� (5��)\n\n\n" +
                "�й�����\n" +
                "\n" +
                "ü���� 0�� �� ��\n" +
                "���ۿ� ������ ��\n\n\n" +
                "ü�� ���� ����\n\n" +
                "��ֹ��� �������� �浹�Ͽ��� ��\n\n" +
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
