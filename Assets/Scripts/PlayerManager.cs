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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 不定期に何度も呼ばれる
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ジャンプする
            Jump();
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
        // キャラを上下反転させる
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 1);

        // 重力を逆にする
        rb.gravityScale = -rb.gravityScale;
    }
}
