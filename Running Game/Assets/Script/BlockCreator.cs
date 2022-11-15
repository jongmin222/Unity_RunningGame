using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    public GameObject[] blockPrefabs; // 블록을 저장할 배열.
    private LevelControl level_control = null;
    private PlayerControl player_control = null;
    private int block_count = 0; // 생성한 블록의 개수.
    private int obstacle_count = 0; // 생성한 블록의 개수.
    private int magnet_count = 0;
    private int acorn_count = 0;
    private int create_count = 0;
    private int create_type = 0;
    private int tempLevel = -1;
    List<GameObject> obstacleList = new List<GameObject>(0);

    public void createBlock(Vector3 block_position)
    {
        // 만들어야 할 블록의 종류(흰색인가 빨간색인가)를 구한다.
        int next_block_type = this.block_count % 2/*this.blockPrefabs.Length*/; // % : 나머지를 구하는 연산자.
                                                                           // 블록을 생성하고 go에 보관한다.
        GameObject go = GameObject.Instantiate(this.blockPrefabs[next_block_type]) as GameObject;
        go.transform.position = block_position; // 블록의 위치를 이동.
        this.block_count++; // 블록의 개수를 증가.
    }

    public void createObstacle(Vector3 block_position, LevelControl level_control)
    {
        //Debug.Log("level : " + level_control.level);
        switch (level_control.level)
        {
            case 0:
                return;
            case 1:
                this.create_count = 11;
                this.create_type = 0;
                break;
            case 2:
                this.create_count = 8;
                this.create_type = 1;
                break;
            case 3:
                this.create_count = 9;
                this.create_type = 0;
                break;
            case 4:
                return;
            case 5:
                this.create_count = 11;
                this.create_type = 2;
                break;
            case 6:
                this.create_count = 11;
                this.create_type = 2;
                break;
            case 7:
                return;
            case 8:
                this.create_count = 9;
                this.create_type = 0;
                break;
            case 9:
                return;
            case 10:
                this.create_count = 11;
                this.create_type = 2;
                break;
            case 11:
                this.create_count = 11;
                this.create_type = 2;
                break;
            case 12:
                return;
            case 13:
                this.create_count = 9;
                this.create_type = 0;
                break;
            case 14:
                return;
            case 15:
                this.create_count = 11;
                this.create_type = 2;
                break;
            case 16:
                this.create_count = 11;
                this.create_type = 2;
                break;
        }


        if (this.obstacle_count % this.create_count == 0)
        {
            if (this.create_type == 0)
            {
                if (level_control.level < 12)
                    block_position.y += 0.75f;
                else
                    block_position.y += 0.75f + Random.Range(0, 2);

                GameObject go = GameObject.Instantiate(this.blockPrefabs[2]) as GameObject;
                go.transform.position = block_position;
                obstacleList.Add(go);
            }
            else if (this.create_type == 1)
            {
                if (level_control.level < 7)
                    block_position.y += 5.75f + Random.Range(0, 2);
                else
                    block_position.y += 5.75f;

                GameObject go = GameObject.Instantiate(this.blockPrefabs[3]) as GameObject;
                go.transform.position = block_position;
                obstacleList.Add(go);
            }
            else if (this.create_type == 2)
            {
                if (this.obstacle_count % 2 == 0)
                {
                    if (level_control.level < 12)
                        block_position.y += 0.75f;
                    else
                        block_position.y += 0.75f + Random.Range(0, 2);

                    GameObject go = GameObject.Instantiate(this.blockPrefabs[2]) as GameObject;
                    go.transform.position = block_position;
                    obstacleList.Add(go);
                }
                else
                {
                    if (level_control.level < 7)
                        block_position.y += 5.75f + Random.Range(0, 2);
                    else
                        block_position.y += 5.75f;

                    GameObject go = GameObject.Instantiate(this.blockPrefabs[3]) as GameObject;
                    go.transform.position = block_position;
                    obstacleList.Add(go);
                }
            }
        }

        this.obstacle_count++;
    }

    public void createCoin(Vector3 block_position, LevelControl level_control)
    {

        if (level_control.level == 7)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject go = GameObject.Instantiate(this.blockPrefabs[4]) as GameObject;
                block_position.y += 1.0f;
                go.transform.position = block_position; // 블록의 위치를 이동.
            }
        }
        else if (level_control.level == 12)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject go = GameObject.Instantiate(this.blockPrefabs[4]) as GameObject;
                block_position.y += 1.0f;
                go.transform.position = block_position; // 블록의 위치를 이동.
            }
        }
        else if (level_control.level != 0)
        {
            GameObject go = GameObject.Instantiate(this.blockPrefabs[4]) as GameObject;
            block_position.y += 1.0f;
            go.transform.position = block_position; // 블록의 위치를 이동.
        }
        
    }
    public void createMagnet(Vector3 block_position, LevelControl level_control)
    {
        if (level_control.level == 1 && this.magnet_count == 0)
        {
            this.magnet_count++;
            GameObject go = GameObject.Instantiate(this.blockPrefabs[5]) as GameObject;
            block_position.x -= 1.0f;
            block_position.y += 3.0f;
            go.transform.position = block_position; // 블록의 위치를 이동.            
        }
        else if (level_control.level == 2)
        {
            this.magnet_count = 0;

        }
        else if (level_control.level == 3 && this.magnet_count == 0)
        {
            this.magnet_count++;
            GameObject go = GameObject.Instantiate(this.blockPrefabs[5]) as GameObject;
            block_position.x -= 1.0f;
            block_position.y += 3.0f;
            go.transform.position = block_position; // 블록의 위치를 이동.            
        }
        else if (level_control.level == 6)
            this.magnet_count = 0;
        else if (level_control.level == 7 && this.magnet_count == 0)
        {
            this.magnet_count++;
            GameObject go = GameObject.Instantiate(this.blockPrefabs[5]) as GameObject;
            block_position.x -= 1.0f;
            block_position.y += 3.0f;
            go.transform.position = block_position; // 블록의 위치를 이동.          
        }
        else if (level_control.level == 11)
            this.magnet_count = 0;
        else if (level_control.level == 12 & this.magnet_count == 0)
        {
            this.magnet_count++;
            GameObject go = GameObject.Instantiate(this.blockPrefabs[5]) as GameObject;
            block_position.x -= 1.0f;
            block_position.y += 3.0f;
            go.transform.position = block_position; // 블록의 위치를 이동.            
        }        
    }

    public void createAcorn(Vector3 block_position, LevelControl level_control)
    {
        if (this.tempLevel < level_control.level) {
            this.acorn_count = 0;
        }
        this.tempLevel = level_control.level;

        if (level_control.level == 1 && this.acorn_count == 0)
        {
            this.acorn_count++;
            GameObject go = GameObject.Instantiate(this.blockPrefabs[6]) as GameObject;
            block_position.y += 2.0f;
            go.transform.position = block_position; // 블록의 위치를 이동.            
        }
        else if (level_control.level == 5 && this.acorn_count == 0)
        {
            this.acorn_count++;
            GameObject go = GameObject.Instantiate(this.blockPrefabs[6]) as GameObject;
            block_position.y += 2.0f;
            go.transform.position = block_position; // 블록의 위치를 이동.            
        }
        else if (level_control.level == 10 && this.acorn_count == 0)
        {
            this.acorn_count++;
            GameObject go = GameObject.Instantiate(this.blockPrefabs[6]) as GameObject;
            block_position.y += 2.0f;
            go.transform.position = block_position; // 블록의 위치를 이동.            
        }
        else if (level_control.level == 13 && this.acorn_count == 0)
        {
            this.acorn_count++;
            GameObject go = GameObject.Instantiate(this.blockPrefabs[6]) as GameObject;
            block_position.y += 2.0f;
            go.transform.position = block_position; // 블록의 위치를 이동.            
        }
        else if (level_control.level == 15 && this.acorn_count == 0)
        {
            this.acorn_count++;
            GameObject go = GameObject.Instantiate(this.blockPrefabs[6]) as GameObject;
             block_position.y += 2.0f;
            go.transform.position = block_position; // 블록의 위치를 이동.            
        }

        else if (level_control.level == 16 && this.acorn_count == 0)
        {
            this.acorn_count++;
            GameObject go = GameObject.Instantiate(this.blockPrefabs[6]) as GameObject;
            block_position.y += 2.0f;
            go.transform.position = block_position; // 블록의 위치를 이동.            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.player_control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < this.obstacleList.Count; i++)
        {
            if(this.obstacleList[i] == null)            
                this.obstacleList.RemoveAt(i);
        }
        if (this.player_control.setBlink)
        {
            for (int i = 0; i < this.obstacleList.Count; i++)
                this.obstacleList[i].GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            for (int i = 0; i < this.obstacleList.Count; i++)
                this.obstacleList[i].GetComponent<BoxCollider>().enabled = true;
        }
    }
}
