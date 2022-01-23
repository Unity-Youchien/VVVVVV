using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Playerが左から右に移動：カメラを右に移動
    // Playerが右から左に移動：カメラを左に移動

    [SerializeField] Camera cam;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.x > transform.position.x)
            {
                // カメラを右に移動
                cam.transform.position = new Vector3(18, 0, -10);
            }
            else
            {
                // カメラを左に移動
                cam.transform.position = new Vector3(0, 0, -10);
            }
        }
    }
}
