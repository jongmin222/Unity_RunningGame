using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // ������ �ʿ��� �������� ���� ����.
    public static float ACCELERATION = 10.0f; // ���ӵ�.
    public static float SPEED_MIN = 4.0f; // �ӵ��� �ּڰ�.
    public static float SPEED_MAX = 8.0f; // �ӵ��� �ִ�.
    public static float JUMP_HEIGHT_MAX = 7.0f; // ���� ����.
    public static float JUMP_KEY_RELEASE_REDUCE = 0.6f; // ���� ���� ���ӵ�.%
    public static float FLY_SPEED_MAX = 3.0f; // �������� ��� �ӵ�.
    public enum STEP
    { // Player�� ���� ���¸� ��Ÿ���� �ڷ��� (*����ü)
        NONE = -1, // �������� ����.
        RUN = 0, // �޸���.
        JUMP, // ����.
        FLY, //��������.
        MISS, // ����.
        NUM, // ���°� �� ���� �ִ��� �����ش�(=3).
    };
    public STEP step = STEP.NONE; // Player�� ���� ����.
    public STEP next_step = STEP.NONE; // Player�� ���� ����.
    public STEP previous_step = STEP.NONE;
    public float step_timer = 0.0f; // ��� �ð�.
    private bool is_landed = false; // �����ߴ°�.
    private bool is_colided = false; // ������ �浹�ߴ°�.
    private bool is_key_released = false; // ��ư�� �������°�.

    public static float NARAKU_HEIGHT = -5.0f;

    public float current_speed = 0.0f; // ���� �ӵ�.
    public LevelControl level_control = null; // LevelControl�� �����.
    public FlyBarScript flyBar = null;
    private float click_timer = -1.0f; // ��ư�� ���� ���� �ð�
    private float CLICK_GRACE_TIME = 0.5f; // �����ϰ� ���� �ǻ縦 �޾Ƶ��� �ð�

    public int health = 0;
    private float blink_timer = 0.0f;
    private float blinkTime = 0.0f;
    public bool setBlink = false;
    private AudioSource audioSource;
    private bool landingSoundPlayed;
    public AudioClip landingSound;

    void Start()
    {
        this.next_step = STEP.RUN;
        this.audioSource = this.gameObject.GetComponent<AudioSource>();
        this.health = 3;
    }

    void blink()
    {
        this.flyBar.currentGage += 150 * Time.deltaTime;

        this.blink_timer += Time.deltaTime;
        this.blinkTime += Time.deltaTime;
        if (this.blink_timer >= 0.1f)
        {
            this.blink_timer = 0.0f;

            if (this.GetComponent<Renderer>().enabled == false)
                this.GetComponent<Renderer>().enabled = true;
            else
                this.GetComponent<Renderer>().enabled = false;
        }
        return;
    }

    void Update()
    {
        Vector3 velocity = this.GetComponent<Rigidbody>().velocity; // �ӵ��� ����.
        // �Ʒ� ���� �ӵ��� �������� �޼��� ȣ�� �߰�
        this.current_speed = this.level_control.getPlayerSpeed();
        this.check_landed(); // ���� �������� üũ.
        if (this.setBlink) {

            this.blink();

            if (this.blinkTime > 3.0f) {
                this.blinkTime = 0.0f;
                this.GetComponent<Renderer>().enabled = true;

                this.setBlink = false;
            }
        }
        switch (this.step)
        {
            case STEP.RUN:
            case STEP.FLY:
            case STEP.JUMP:
                if (velocity.x <= 0.0f)
                {
                    this.health--;
                    if (this.health > 0)
                    {
                        transform.position = new Vector3(transform.position.x, 4, transform.position.z);
                        if (this.setBlink)
                            this.blinkTime = 0.0f;
                        else
                            this.setBlink = true;
                    }
                }

                // ���� ��ġ�� �Ѱ�ġ���� �Ʒ���.
                if (this.transform.position.y < NARAKU_HEIGHT || this.health <= 0)
                {
                    this.health = 0;
                    this.next_step = STEP.MISS;
                }
                    break;
        }

        this.step_timer += Time.deltaTime;


        if (Time.timeScale == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.click_timer = 0.0f;
            }
            else
            {
                if (this.click_timer >= 0.0f)
                {
                    this.click_timer += Time.deltaTime;
                }
            }
            // ���� ���°� ������ ���� ������ ������ ��ȭ�� �����Ѵ�.
            if (this.next_step == STEP.NONE)
            {
                switch (this.step)
                {
                    case STEP.RUN:
                        if (this.is_landed && Input.GetMouseButtonDown(0))
                        {
                            this.next_step = STEP.JUMP;
                        }
                        else if (!this.is_landed && Input.GetMouseButtonDown(0))
                        {
                            this.next_step = STEP.FLY;
                        }
                        break;
                    case STEP.JUMP:
                        if (this.is_landed && step_timer >= CLICK_GRACE_TIME)
                        {
                            this.next_step = STEP.RUN;
                        }
                        else
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                this.next_step = STEP.FLY;
                            }
                        }
                        break;
                    case STEP.FLY:
                        if (this.is_landed && step_timer >= CLICK_GRACE_TIME)
                        {
                            this.next_step = STEP.RUN;
                        }
                        if (Input.GetMouseButtonUp(0))
                        {
                            this.next_step = STEP.RUN;
                        }
                        break;
                    case STEP.MISS:
                        // ���ӵ�(ACCELERATION)�� ���� Player�� �ӵ��� ������ �� ����.
                        velocity.x -= PlayerControl.ACCELERATION * Time.deltaTime;
                        if (velocity.x < 0.0f)
                        { // Player�� �ӵ��� ���̳ʽ���.
                            velocity.x = 0.0f; // 0���� �Ѵ�.
                        }
                        break;
                }
            }
        }

        // '���� ����'�� '���� ���� ����'�� �ƴ� ����(���°� ���� ����).
        while (this.next_step != STEP.NONE)
        {
            this.previous_step = this.step;           
            this.step = this.next_step; // '���� ����'�� '���� ����'�� ����.
            this.next_step = STEP.NONE; // '���� ����'�� '���� ����'���� ����.
            switch (this.step)
            { // ���ŵ� '���� ����'��.
                case STEP.RUN: // '�޸���'�� ��.
                    this.step_timer = 0.0f;
                    is_key_released = false;
                    break;
                case STEP.JUMP: // '����'�� ��.
                                // �ְ� ������ ����(JUMP_HEIGHT_MAX)���� ������ �� �ִ� �ӵ��� ���.                                
                    velocity.y = Mathf.Sqrt(2.0f * 9.8f * PlayerControl.JUMP_HEIGHT_MAX);
                    this.step_timer = 0.0f;
                    is_key_released = false;
                    break;
                case STEP.FLY: // '��������'�� ��.
                    this.step_timer = 0.0f;
                    is_key_released = false;
                    break;
            }

        }
        // ���º��� �� ������ ���� ó��.
        switch (this.step)
        {
            case STEP.RUN: // �޸��� ���� ��.
                if (this.previous_step == STEP.FLY)
                {
                    if (this.step_timer >= 0.5f)
                        this.flyBar.currentGage += 30 * Time.deltaTime;
                }
                else
                    this.flyBar.currentGage += 30 * Time.deltaTime;

                if (velocity.x < this.current_speed)
                {
                    velocity.x = this.current_speed;
                }
                velocity.x += PlayerControl.ACCELERATION * Time.deltaTime;
                //velocity.x = this.current_speed;
                // ������� ���� �ӵ��� �����ؾ� �� �ӵ��� ������.
                if (Mathf.Abs(velocity.x) > this.current_speed)
                {
                    // ���� �ʰ� �����Ѵ�.
                    velocity.x *= this.current_speed / Mathf.Abs(velocity.x);
                }

                if (this.is_key_released)
                {
                    break; // �ƹ��͵� ���� �ʰ� ������ ����������.
                }
                if (velocity.y <= 0.0f)
                {
                    break; // �ƹ��͵� ���� �ʰ� ������ ����������.
                }

                // ��ư�� ������ �ְ� ��� ���̶�� ���� ����.
                // ������ ����� ���⼭ ��.
                velocity.y *= JUMP_KEY_RELEASE_REDUCE;
                this.is_key_released = true;
                break;
            case STEP.JUMP: // ���� ���� ��.
                if (velocity.x < this.current_speed)
                {
                    velocity.x = this.current_speed;
                }
                velocity.x += PlayerControl.ACCELERATION * Time.deltaTime;
                //velocity.x = this.current_speed;
                // ������� ���� �ӵ��� �����ؾ� �� �ӵ��� ������.
                if (Mathf.Abs(velocity.x) > this.current_speed)
                {
                    // ���� �ʰ� �����Ѵ�.
                    velocity.x *= this.current_speed / Mathf.Abs(velocity.x);
                }

                do
                {
                    if (this.previous_step == STEP.FLY) {
                        if (this.step_timer >= 0.5f)
                            this.flyBar.currentGage += 30 * Time.deltaTime;
                    }
                    else
                        this.flyBar.currentGage += 30 * Time.deltaTime;
                    // '��ư�� ������ ����'�� �ƴϸ�.
                    //if (!Input.GetMouseButtonUp(0))
                    //{
                    //    break; // �ƹ��͵� ���� �ʰ� ������ ����������.
                    //}
                    // �̹� ���ӵ� ���¸�(�� ���̻� �������� �ʵ���).
                    if (this.is_key_released)
                    {
                        break; // �ƹ��͵� ���� �ʰ� ������ ����������.
                    }
                    // ���Ϲ��� �ӵ��� 0 ���ϸ�(�ϰ� ���̶��).
                    if (velocity.y <= 0.0f)
                    {
                        break; // �ƹ��͵� ���� �ʰ� ������ ����������.
                    }
                    // ��ư�� ������ �ְ� ��� ���̶�� ���� ����.
                    // ������ ����� ���⼭ ��.
                    velocity.y *= JUMP_KEY_RELEASE_REDUCE;
                    this.is_key_released = true;
                } while (false);
                break;
            case STEP.FLY: // �������� ���� ��.
                if (velocity.x < this.current_speed)
                {
                    velocity.x = this.current_speed;
                }
                velocity.x += PlayerControl.ACCELERATION * Time.deltaTime;
                //velocity.x = this.current_speed;
                // ������� ���� �ӵ��� �����ؾ� �� �ӵ��� ������.
                if (Mathf.Abs(velocity.x) > this.current_speed)
                {
                    // ���� �ʰ� �����Ѵ�.
                    velocity.x *= this.current_speed / Mathf.Abs(velocity.x);
                }

                do
                {
                    // '��ư�� ���� ����' ��.
                    if (Input.GetMouseButton(0) && this.flyBar.currentGage > 0)
                    {
                        velocity.y = Mathf.Lerp(velocity.y, FLY_SPEED_MAX, Mathf.Min(this.step_timer * 50.0f * Time.deltaTime, 1));
                        this.flyBar.currentGage -= 300 * Time.deltaTime;
                        break;
                    }
                    // �̹� ���ӵ� ���¸�(�� ���̻� �������� �ʵ���).
                    if (this.is_key_released)
                    {
                        break; // �ƹ��͵� ���� �ʰ� ������ ����������.
                    }
                    // ���Ϲ��� �ӵ��� 0 ���ϸ�(�ϰ� ���̶��).
                    if (velocity.y <= 0.0f)
                    {
                        break; // �ƹ��͵� ���� �ʰ� ������ ����������.
                    }
                    // ��ư�� ������ �ְ� ��� ���̶�� ���� ����.
                    // ������ ����� ���⼭ ��.
                    velocity.y *= JUMP_KEY_RELEASE_REDUCE;
                    this.is_key_released = true;
                } while (false);
                break;
        }
        // Rigidbody�� �ӵ��� ������ ���� �ӵ��� ����.
        // (�� ���� ���¿� ������� �Ź� ����ȴ�).
        this.GetComponent<Rigidbody>().velocity = velocity;
    }



    private void check_landed() // �����ߴ��� ����
    {
        this.is_landed = false; // �ϴ� false�� ����.

        do
        {
            Vector3 s = this.transform.position; // Player�� ���� ��ġ.
            Vector3 e = s + Vector3.down * 1.0f; // s���� �Ʒ��� 1.0f�� �̵��� ��ġ.
            RaycastHit hit;

            // s���� e ���̿� �ƹ��͵� ���� ��. *out: method ������ ������ ���� ��ȯ�� ���.
            if (setBlink)
            {
                if (!Physics.Linecast(s, e, out hit, 1 << LayerMask.NameToLayer("Floor")))
                {
                    break;
                }
            }
            else
            {
                if (!Physics.Linecast(s, e, out hit))
                {
                    break; // �ƹ��͵� ���� �ʰ� do~while ������ ��������(Ż�ⱸ��).
                }
            }
            // s���� e ���̿� ���� ���� �� �Ʒ��� ó���� ����.
            if(this.step == STEP.JUMP || this.step == STEP.FLY) { // ����, ����,�������� ���¶��.
                if(this.step_timer < Time.deltaTime * 3.0f) { // ��� �ð��� 3.0f �̸��̶��.
                    break; // �ƹ��͵� ���� �ʰ� do~while ������ ��������(Ż�ⱸ��).
                }
            }
            // s���� e ���̿� ���� �ְ� JUMP ���İ� �ƴ� ���� �Ʒ��� ����.

            if (this.landingSoundPlayed == false && this.step == STEP.RUN)
            {
                this.audioSource.clip = landingSound;
                this.audioSource.Play();
                this.landingSoundPlayed = true;
            }
            this.is_landed = true;
        } while (false);
        // ������ Ż�ⱸ.

        if(this.is_landed == false)
            this.landingSoundPlayed = false;
    }


    public bool isPlayEnd() // ������ �������� ����.
    {
        bool ret = false;
        switch (this.step)
        {
            case STEP.MISS: // MISS ���¶��.
                ret = true; // '�׾����'(true)��� �˷���.
                break;
        }
        return (ret);
    }
}
