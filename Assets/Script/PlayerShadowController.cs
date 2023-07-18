using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShadowController : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 100f; // Ray の飛ばす距離

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
            if (PlayerController.GetComponent<PlayerController>().noRay == false) // PlayerController スクリプトの noRay フラグを参照する
            {
                // 下方向に Ray を飛ばす
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -Vector3.up, out hit, raycastDistance))
                {
                    // Ray が BlockNormal オブジェクトに衝突した場合の処理
                    if (hit.collider.gameObject.CompareTag("BlockNormal"))
                    {
                        Debug.Log("BlockNormal オブジェクトが検出されました");
                        onBlockNormal = true;
                    }

                    // Ray が BlockGelオブジェクトに衝突した場合の処理
                    if (hit.collider.gameObject.CompareTag("BlockGel"))
                    {
                        Debug.Log("BlockGel オブジェクトが検出されました");
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
            if (PlayerController.GetComponent<PlayerController>().noRay == false) // PlayerController スクリプトの noRay フラグを参照する
            {
                RaycastHit hit;
                if (!Physics.Raycast(transform.position, -Vector3.up, out hit, raycastDistance))
                {
                    Debug.Log("オブジェクトが検出されませんでした");
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
