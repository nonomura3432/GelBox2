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
            // x�������ɂ��Ė��b2�x�A��]������Quaternion���쐬�i�ϐ���rot�Ƃ���j
            Quaternion rot = Quaternion.AngleAxis(90, Vector3.right);
            // ���݂̎��g�̉�]�̏����擾����B
            Quaternion q = this.transform.rotation;
            // �������āA���g�ɐݒ�
            this.transform.rotation = q * rot;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
          
        }
    }

}