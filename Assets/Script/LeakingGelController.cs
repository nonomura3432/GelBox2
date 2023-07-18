using DG.Tweening;
using UnityEngine;

public class LeakingGelController : MonoBehaviour
{
    [SerializeField] private GameObject LeakingGel; // 表示させるオブジェクト
    [SerializeField] private GameObject gelSurface; // GelSurface オブジェクトの Transform コンポーネント
    private int previousRemainingGel = 0;
    private float updateInterval = 3f; // アップデートの間隔（秒）
    private float timer = 0f;

    private void Start()
    {
       
    }

    private void Update()
    {
        int remainingGel = GameObject.FindWithTag("RemainingGelManager").GetComponent<RemainingGelManager>()
            .remainingGel;
       

        // remainingGelが減っているかチェックする
        if (remainingGel < previousRemainingGel)
        {
            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                // 3秒ごとの処理を実行する
                // ここに処理のコードを記述する
                ShowObjectOnGelSurface();

                timer = 0f; // タイマーをリセットする
            }
            // オブジェクトを表示する
            
        }
        else
        {
            // オブジェクトを非表示にする
           LeakingGel.SetActive(false);
        }

// 前フレームのremainingGelを更新する
        previousRemainingGel = remainingGel;
    }


    private void ShowObjectOnGelSurface()
    {
        // GelSurface の上にオブジェクトを表示する座標を設定する
        Vector3 targetPosition = gelSurface.transform.position;

        // オブジェクトの座標を設定する
        LeakingGel.transform.position = targetPosition;

        // オブジェクトを表示する
        LeakingGel.SetActive(true);
        transform.DOMoveY(transform.position.y+1f, 3f);
    }
}


