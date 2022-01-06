using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT
    }
    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    Rigidbody2D rb;
    float speed;

    bool isGround;
    SpriteRenderer spriteRenderer;

    bool isDead;

    [SerializeField] GameManager gm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 不定期に何度も呼ばれる
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        if (!isDead)
        {
            // 移動する方向を決める
            if (x == 0)
            {
                // 止まってる
                direction = DIRECTION_TYPE.STOP;
            }
            else if (x > 0)
            {
                // 右に移動
                direction = DIRECTION_TYPE.RIGHT;
            }
            else if (x < 0)
            {
                // 左に移動
                direction = DIRECTION_TYPE.LEFT;
            }

            // スペースを押したらジャンプする
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                // ジャンプする
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        // 向きによる速さを決めて速度を設定
        switch(direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;

            case DIRECTION_TYPE.RIGHT:
                speed = 3;
                transform.localScale = new Vector3(1, transform.localScale.y, 1);
                break;

            case DIRECTION_TYPE.LEFT:
                speed = -3;
                transform.localScale = new Vector3(-1, transform.localScale.y, 1);
                break;
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = Vector2.zero;
        // キャラを上下反転させる
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 1);

        // 重力を逆にする
        rb.gravityScale = -rb.gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // 地面に触れているかどうか
            isGround = true;
        }

        if (collision.gameObject.tag == "Trap")
        {
            isDead = true;
            StartCoroutine(GameOver());
           // Debug.Log("トラップだ！");
        }

        if (collision.gameObject.tag == "BounceBar")
        {
            Jump();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // 地面に触れているかどうか
            isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // 地面に触れているかどうか
            isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BounceBar")
        {
            Jump();
        }
    }

    IEnumerator GameOver()
    {
        // 動きを止める
        direction = DIRECTION_TYPE.STOP;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        // 点滅させる：色は赤
        int count = 0;
        while (count < 10)
        {
            // 消える
            spriteRenderer.color = new Color32(255, 120, 120, 100);
            yield return new WaitForSeconds(0.05f); // 0.05秒待つ
            // つく
            spriteRenderer.color = new Color32(255, 120, 120, 255);
            yield return new WaitForSeconds(0.05f); // 0.05秒待つ

            count++;
        }

        // リスタートさせる
        gm.GameOver();
    }
}
