using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    // ����� ������ ��Ÿ���� ����ü.
    public enum TYPE
    {
        NONE = -1, // ����.
        FLOOR = 0, // ����.
        HOLE, // ����.
        NUM, // ����� �� �������� ��Ÿ����(��2).
    };
};

public class MapCreator : MonoBehaviour
{
    public static float BLOCK_WIDTH = 1.0f; // ����� ��.
    public static float BLOCK_HEIGHT = 0.2f; // ����� ����.
    public static int BLOCK_NUM_IN_SCREEN = 24; // ȭ�� ���� ���� ����� ����.
    private LevelControl level_control = null;

    private struct FloorBlock
    { // ��Ͽ� ���� ������ ��Ƽ� �����ϴ� ����ü (���� ���� ������ �ϳ��� ���� �� ���).
        public bool is_created; // ����� ��������°�.
        public Vector3 position; // ����� ��ġ.
    };
    private FloorBlock last_block; // �������� ������ ���.
    private PlayerControl player = null; // ������ Player�� ����.
    private BlockCreator block_creator; // BlockCreator�� ����.

    public TextAsset level_data_text = null;

    private GameRoot game_root = null;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        this.last_block.is_created = false;
        this.block_creator = this.gameObject.GetComponent<BlockCreator>();
        
        this.level_control = new LevelControl();
        this.level_control.initialize();

        this.level_control.loadLevelData(this.level_data_text);

        this.game_root = this.gameObject.GetComponent<GameRoot>();
        this.player.level_control = this.level_control;
    }
    void Update()
    {
        // �÷��̾��� X��ġ�� �����´�.
        float block_generate_x = this.player.transform.position.x;
        // �׸��� �뷫 �� ȭ�鸸ŭ ���������� �̵�. 
        // �� ��ġ�� ����� �����ϴ� ���� ���� �ȴ�.
        block_generate_x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) * 3.0f / 4.0f;
        // �������� ���� ����� ��ġ�� ���� ������ ������.
        while (this.last_block.position.x < block_generate_x)
        {
            // ����� �����.
            this.create_floor_block();
        }
    }

    private void create_floor_block()
    {
        Vector3 block_position; // �������� ���� ����� ��ġ.
        if (!this.last_block.is_created)
        { // last_block�� �������� ���� ���.
          // ����� ��ġ�� �ϴ� Player�� ���� �Ѵ�.
            block_position = this.player.transform.position;
            // �׷��� ���� ����� X ��ġ�� ȭ�� 1/4��ŭ �������� �̵�.
            block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 4.0f);
            // ����� Y��ġ�� 0����.
            block_position.y = 0.0f;
        }
        else
        { // last_block�� ������ ���
          // �̹��� ���� ����� ��ġ�� ������ ���� ��ϰ� ����.
            block_position = this.last_block.position;
        }
        block_position.x += BLOCK_WIDTH; // ����� 1����ŭ ���������� �̵�.

        // LevelControl�� ����.
        this.level_control.update(this.game_root.getPlayTime());

        // level_control�� ����� current_block(���� ���� ��� ����)�� height(����)�� �� ���� ��ǥ�� ��ȯ.
        block_position.y = level_control.current_block.height * BLOCK_HEIGHT;
        // ���� ���� ��Ͽ� ���� ������ ���� current�� �ִ´�.
        LevelControl.CreationInfo current = this.level_control.current_block;
        // ���� ���� ����� �ٴ��̸� (���� ���� ����� �����̶��) 
        if (current.block_type == Block.TYPE.FLOOR)
        {
            // block_position�� ��ġ�� ����� ������ ����.
            this.block_creator.createBlock(block_position);
            this.block_creator.createObstacle(block_position, this.level_control);
            this.block_creator.createCoin(block_position, this.level_control);
            this.block_creator.createMagnet(block_position, this.level_control);
            this.block_creator.createAcorn(block_position, this.level_control);
        }
        this.last_block.position = block_position; // last_block�� ��ġ�� �̹� ��ġ�� ����.
        this.last_block.is_created = true; // ����� �����Ǿ����Ƿ� last_block�� is_created�� true�� ����.
    }

    public bool isDelete(GameObject block_object)
    {
        bool ret = false; // ��ȯ��.
                          // Player�κ��� �� ȭ�鸸ŭ ���ʿ� ��ġ, �� ��ġ�� ��������� �����ĸ� �����ϴ� ���� ���� ��.
        float left_limit = this.player.transform.position.x - BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN * 1.0f / 4.0f);
        // ����� ��ġ�� ���� ������ ������(����),
        if (block_object.transform.position.x < left_limit)
        {
            ret = true; // ��ȯ���� true(������� ����)��
        }
        return (ret); // ���� ����� ������.
    }


}
