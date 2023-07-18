using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float span = 0.005f;
    private float delta = 0;
    public bool onBlockGel = false;
    public bool onBlockNormal = false;
    [SerializeField] GameObject remainingGelText;
    [SerializeField] private AudioClip move;
    [SerializeField] private AudioClip charge;
    AudioSource audioSource;
    private float timer = 0f;
    private bool isKeyPressed = false;
    private float keyPressTime = 0f;
    private float keyPressInterval = 0.35f; // 連続入力の間隔（秒）
    private float basePitch = 1f;
    private float maxPitch = 100f;
    private float currentPitch = 1f;
    private bool justBlockNormal;
    [SerializeField] private GameObject PlayerShadow;
    private RaycastHit hitInfo; // レイキャストの衝突情報

    [SerializeField] private GameObject GAMEOVER;
    [SerializeField] private GameObject YouLostAllGel;
    [SerializeField] private GameObject Panel_GameOver;

    [SerializeField] private GameObject GAMECLEAR;
    [SerializeField] private GameObject PressSpace;
    [SerializeField] private GameObject Panel_Clear;

    public bool noRay;

    public bool moveEnable=true;


    private void Start()
    {
        onBlockGel = false;
        onBlockNormal = true;
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = basePitch;
        currentPitch = basePitch;
        moveEnable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && moveEnable == true)
        {
            noRay = true;

            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                PlayerShadow.transform.Translate(-1f, 0f, 0f, Space.World);
                transform.DOMoveX(transform.position.x - 1f, 0.05f);
                transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, 90f), 0.05f).SetRelative(true);
                PlayMoveSoundAtPitch();
            }

            noRay = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && moveEnable == true)
        {
            noRay = true;

            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                PlayerShadow.transform.Translate(1f, 0f, 0f, Space.World);
                transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, -90f), 0.05f).SetRelative(true);
                transform.DOMoveX(transform.position.x + 1f, 0.05f);
                PlayMoveSoundAtPitch();
            }

            noRay = false;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && moveEnable == true)
        {
            noRay = true;

            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && moveEnable == true)
            {
                PlayerShadow.transform.Translate(0f, 0f, 1f, Space.World);
                transform.DORotateQuaternion(Quaternion.Euler(90f, 0f, 0f), 0.05f).SetRelative(true);
                transform.DOMoveZ(transform.position.z + 1f, 0.05f);
                PlayMoveSoundAtPitch();
            }

            noRay = false;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && moveEnable == true)
        {
            noRay = true;

            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                PlayerShadow.transform.Translate(0f, 0f, -1f, Space.World);
                transform.DORotateQuaternion(Quaternion.Euler(-90f, 0f, 0f), 0.05f).SetRelative(true);
                transform.DOMoveZ(transform.position.z - 1f, 0.05f);
                PlayMoveSoundAtPitch();
            }

            noRay = false;
        }

        if (onBlockGel)
        {
            delta += Time.deltaTime;
            timer += Time.deltaTime;

            while (delta > span)
            {
                delta -= span;
                GameObject.FindWithTag("RemainingGelManager").GetComponent<RemainingGelManager>().remainingGel += 3;
                GameObject.FindWithTag("RemainingGelManager").GetComponent<RemainingGelManager>().remainingGel =
                    Mathf.Clamp(
                        GameObject.FindWithTag("RemainingGelManager").GetComponent<RemainingGelManager>().remainingGel,
                        0, 1000); // 最大値を超えないように制御
            }

            if (GameObject.FindWithTag("RemainingGelManager").GetComponent<RemainingGelManager>().remainingGel <= 999 &&
                timer >= 0.3f)
            {
                audioSource.pitch = basePitch;
                currentPitch = basePitch;
                // 0.3秒以上経過したらSEを再生する
                audioSource.PlayOneShot(charge);
                timer = 0f; // タイマーをリセットする
            }

            remainingGelText.GetComponent<Text>().text = "残りのゲル：" + GameObject.FindWithTag("RemainingGelManager")
                .GetComponent<RemainingGelManager>().remainingGel.ToString();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Time.time - keyPressTime > keyPressInterval)
            {
                // 間隔をあけた場合はピッチをリセット
                audioSource.pitch = basePitch;
                currentPitch = basePitch;

            }

            if (Time.time - keyPressTime <= keyPressInterval)
            {
                // 間隔をあけずに連続で押された場合の処理
                currentPitch += 0.1f;
                audioSource.pitch = currentPitch;

            }

            // キーの押下情報を更新
            isKeyPressed = true;
            keyPressTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) ||
            Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            // キーが離されたら押下情報をリセット
            isKeyPressed = false;
        }

        if (transform.position == new Vector3(1f, 0f, -65f))
        {
            Debug.Log("クリアです！");
            moveEnable = false;
            GAMECLEAR.SetActive(true);
            // YouLostAllGelオブジェクトを表示する
            PressSpace.SetActive(true);
            // Panel_GameOverオブジェクトを表示する
            Panel_Clear.SetActive(true);
            // ここでゲームオーバーに関する処理を追加する（例: ゲームオーバーの表示やリスタートなど）


            if (Input.GetKeyDown(KeyCode.Space))
            {
                // ゲームオーバー時の処理を記述する
                // GAMEOVERオブジェクトを表示する
                GAMECLEAR.SetActive(false);
                // YouLostAllGelオブジェクトを表示する
                PressSpace.SetActive(false);
                // Panel_GameOverオブジェクトを表示する
                Panel_Clear.SetActive(false);
                Debug.Log("GameOver");
                // ここでゲームオーバーに関する処理を追加する（例: ゲームオーバーの表示やリスタートなど）
                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;
                PlayerShadow.transform.position = Vector3.zero;

                GameObject.FindWithTag("RemainingGelManager").GetComponent<RemainingGelManager>().remainingGel = 1000;
                moveEnable = true;
            }

        }
    }




    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BlockGel"))
        {
            onBlockGel = true;
        }
        if (other.gameObject.CompareTag("BlockNormal"))
        {
            onBlockNormal = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BlockGel"))
        {
            onBlockGel = false;
        }
        if (other.gameObject.CompareTag("BlockNormal"))
        {
            onBlockNormal = false;
        }
    }




    private void GameOver()
    {
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
                transform.Translate(0, 0, 0);
                GameObject.FindWithTag("RemainingGelManager").GetComponent<RemainingGelManager>().remainingGel = 1000;
            }
    }


    private void PlayMoveSoundAtPitch()
    {
        audioSource.PlayOneShot(move);
    }
}
