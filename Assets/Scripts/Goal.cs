using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    bool isGoal;
    [SerializeField] GameManager gm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isGoal)
        {
            isGoal = true;
            gm.GameClear();
            Debug.Log("ゴール！");
        }
    }
}
