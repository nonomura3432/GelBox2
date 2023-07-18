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
    private float keyPressInterval = 0.35f; // �A�����͂̊Ԋu�i�b�j
    private float basePitch = 1f;
    private float maxPitch = 100f;
    private float currentPitch = 1f;
    private bool justBlockNormal;
    [SerializeField] private GameObject PlayerShadow;
    private RaycastHit hitInfo; // ���C�L���X�g�̏Փˏ��

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
                        0, 1000); // �ő�l�𒴂��Ȃ��悤�ɐ���
            }

            if (GameObject.FindWithTag("RemainingGelManager").GetComponent<RemainingGelManager>().remainingGel <= 999 &&
                timer >= 0.3f)
            {
                audioSource.pitch = basePitch;
                currentPitch = basePitch;
                // 0.3�b�ȏ�o�߂�����SE���Đ�����
                audioSource.PlayOneShot(charge);
                timer = 0f; // �^�C�}�[�����Z�b�g����
            }

            remainingGelText.GetComponent<Text>().text = "�c��̃Q���F" + GameObject.FindWithTag("RemainingGelManager")
                .GetComponent<RemainingGelManager>().remainingGel.ToString();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Time.time - keyPressTime > keyPressInterval)
            {
                // �Ԋu���������ꍇ�̓s�b�`�����Z�b�g
                audioSource.pitch = basePitch;
                currentPitch = basePitch;

            }

            if (Time.time - keyPressTime <= keyPressInterval)
            {
                // �Ԋu���������ɘA���ŉ����ꂽ�ꍇ�̏���
                currentPitch += 0.1f;
                audioSource.pitch = currentPitch;

            }

            // �L�[�̉��������X�V
            isKeyPressed = true;
            keyPressTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) ||
            Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            // �L�[�������ꂽ�牟���������Z�b�g
            isKeyPressed = false;
        }

        if (transform.position == new Vector3(1f, 0f, -65f))
        {
            Debug.Log("�N���A�ł��I");
            moveEnable = false;
            GAMECLEAR.SetActive(true);
            // YouLostAllGel�I�u�W�F�N�g��\������
            PressSpace.SetActive(true);
            // Panel_GameOver�I�u�W�F�N�g��\������
            Panel_Clear.SetActive(true);
            // �����ŃQ�[���I�[�o�[�Ɋւ��鏈����ǉ�����i��: �Q�[���I�[�o�[�̕\���⃊�X�^�[�g�Ȃǁj


            if (Input.GetKeyDown(KeyCode.Space))
            {
                // �Q�[���I�[�o�[���̏������L�q����
                // GAMEOVER�I�u�W�F�N�g��\������
                GAMECLEAR.SetActive(false);
                // YouLostAllGel�I�u�W�F�N�g��\������
                PressSpace.SetActive(false);
                // Panel_GameOver�I�u�W�F�N�g��\������
                Panel_Clear.SetActive(false);
                Debug.Log("GameOver");
                // �����ŃQ�[���I�[�o�[�Ɋւ��鏈����ǉ�����i��: �Q�[���I�[�o�[�̕\���⃊�X�^�[�g�Ȃǁj
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
        // �Q�[���I�[�o�[���̏������L�q����
            // GAMEOVER�I�u�W�F�N�g��\������
            GAMEOVER.SetActive(true);
            // YouLostAllGel�I�u�W�F�N�g��\������
            YouLostAllGel.SetActive(true);
            // Panel_GameOver�I�u�W�F�N�g��\������
            Panel_GameOver.SetActive(true);
            Debug.Log("GameOver");
            // �����ŃQ�[���I�[�o�[�Ɋւ��鏈����ǉ�����i��: �Q�[���I�[�o�[�̕\���⃊�X�^�[�g�Ȃǁj

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
