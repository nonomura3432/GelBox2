using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(1,0,0);
            transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
         
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            transform.Translate(0, 0, 1,Space.World);
            // x軸を軸にして毎秒2度、回転させるQuaternionを作成（変数をrotとする）
            Quaternion rot = Quaternion.AngleAxis(90, Vector3.right);
            // 現在の自身の回転の情報を取得する。
            Quaternion q = this.transform.rotation;
            // 合成して、自身に設定
            this.transform.rotation = q * rot;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
          
        }
    }

}