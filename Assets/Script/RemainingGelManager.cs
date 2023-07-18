using System;
using UnityEngine;
using UnityEngine.UI;

public class RemainingGelManager : MonoBehaviour
{
    [SerializeField] GameObject gelSurface;
    [SerializeField] GameObject sideSurface1;
    [SerializeField] GameObject sideSurface2;
    [SerializeField] GameObject sideSurface3;
    [SerializeField] GameObject faceSurface;
    [SerializeField] GameObject underSurface;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject BlockGel;
    private float span = 0.005f;
    private float delta = 0;
    public int remainingGel = 1000;
    [SerializeField] GameObject remainingGelText;

    private PlayerController playerController;

    [SerializeField] private GameObject GAMEOVER;
    [SerializeField] private GameObject YouLostAllGel;
    [SerializeField] private GameObject Panel_GameOver;

    [SerializeField] private GameObject PlayerShadow;
    

    // Start is called before the first frame update
    void Start()
    {
        playerController = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (underSurface.transform.position.y >= 0.4f )
        {
            if (playerController.onBlockGel==false)
            {
                Debug.Log("GameOver");
                remainingGel = 0;
                GameOver();
            }
        }
        else if (remainingGel == 0)
        {
            Debug.Log("GameOver");
            GameOver();
        }
        else if ((sideSurface1.transform.position.y <= -0.3f ||
                  sideSurface2.transform.position.y <= -0.3f ||
                  sideSurface3.transform.position.y <= -0.3f ||
                  faceSurface.transform.position.y <= -0.3f) &&
                 !playerController.onBlockGel)
        {
            delta += Time.deltaTime;

            if (delta > span)
            {
                delta -= span;
                remainingGel -= 1;
                remainingGel = Mathf.Clamp(remainingGel, 0, 1000); // 最大値を超えないように制御
            }
        }
        remainingGelText.GetComponent<Text>().text = "残りのゲル：" + remainingGel.ToString();
    }

    public void GameOver()
    {
        playerController.moveEnable = false;
        // ゲームオーバー時の処理を記述する
        // GAMEOVERオブジェクトを表示する
        GAMEOVER.SetActive(true);
        // YouLostAllGelオブジェクトを表示する
        YouLostAllGel.SetActive(true);
        // Panel_GameOverオブジェクトを表示する
        Panel_GameOver.SetActive(true);
        Debug.Log("GameOver");
        // ここでゲームオーバーに関する処理を追加する（例: ゲームオーバーの表示やリスタートなど）
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ゲームオーバー時の処理を記述する
            // GAMEOVERオブジェクトを表示する
            GAMEOVER.SetActive(false);
            // YouLostAllGelオブジェクトを表示する
            YouLostAllGel.SetActive(false);
            // Panel_GameOverオブジェクトを表示する
            Panel_GameOver.SetActive(false);
            Debug.Log("GameOver");
            // ここでゲームオーバーに関する処理を追加する（例: ゲームオーバーの表示やリスタートなど）
            Player.transform.position = Vector3.zero;
            Player.transform.rotation = Quaternion.identity;
            PlayerShadow.transform.position = Vector3.zero;
         
            remainingGel = 1000;
            playerController.moveEnable = true;
        }
    }
}