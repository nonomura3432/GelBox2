using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShadowController : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 100f; // Ray �̔�΂�����

    [SerializeField] GameObject PlayerController;
    [SerializeField] GameObject RemainingGelManager;
    [SerializeField] private GameObject Player;
    private bool onBlockNormal;
    private bool onBlockGel;

    // Start is called before the first frame update
    void Start()
    {
        onBlockNormal = true;
        onBlockGel = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player.transform.position != Vector3.zero)
        {
            if (PlayerController.GetComponent<PlayerController>().noRay == false) // PlayerController �X�N���v�g�� noRay �t���O���Q�Ƃ���
            {
                // �������� Ray ���΂�
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -Vector3.up, out hit, raycastDistance))
                {
                    // Ray �� BlockNormal �I�u�W�F�N�g�ɏՓ˂����ꍇ�̏���
                    if (hit.collider.gameObject.CompareTag("BlockNormal"))
                    {
                        Debug.Log("BlockNormal �I�u�W�F�N�g�����o����܂���");
                        onBlockNormal = true;
                    }

                    // Ray �� BlockGel�I�u�W�F�N�g�ɏՓ˂����ꍇ�̏���
                    if (hit.collider.gameObject.CompareTag("BlockGel"))
                    {
                        Debug.Log("BlockGel �I�u�W�F�N�g�����o����܂���");
                        onBlockGel = false;
                    }

                }
            }

            Debug.DrawRay(transform.position, -Vector3.up * raycastDistance, Color.red);
        }
    }

    void LateUpdate()
    {
        if (Player.transform.position != Vector3.zero)
        {
            if (PlayerController.GetComponent<PlayerController>().noRay == false) // PlayerController �X�N���v�g�� noRay �t���O���Q�Ƃ���
            {
                RaycastHit hit;
                if (!Physics.Raycast(transform.position, -Vector3.up, out hit, raycastDistance))
                {
                    Debug.Log("�I�u�W�F�N�g�����o����܂���ł���");
                    onBlockNormal = false;
                    onBlockGel = false;
                    // Move the player using DOTween
                    Player.transform.DOMoveY(-20, 0.5f).OnComplete(() =>
                    {
                        // Set remainingGel to 0
                        RemainingGelManager.GetComponent<RemainingGelManager>().remainingGel = 0;
                        // Call GameOver
                        RemainingGelManager.GetComponent<RemainingGelManager>().GameOver();
                    });
                }
            }
            
        }
    }

}
