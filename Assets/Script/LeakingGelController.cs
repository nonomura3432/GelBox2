using DG.Tweening;
using UnityEngine;

public class LeakingGelController : MonoBehaviour
{
    [SerializeField] private GameObject LeakingGel; // �\��������I�u�W�F�N�g
    [SerializeField] private GameObject gelSurface; // GelSurface �I�u�W�F�N�g�� Transform �R���|�[�l���g
    private int previousRemainingGel = 0;
    private float updateInterval = 3f; // �A�b�v�f�[�g�̊Ԋu�i�b�j
    private float timer = 0f;

    private void Start()
    {
       
    }

    private void Update()
    {
        int remainingGel = GameObject.FindWithTag("RemainingGelManager").GetComponent<RemainingGelManager>()
            .remainingGel;
       

        // remainingGel�������Ă��邩�`�F�b�N����
        if (remainingGel < previousRemainingGel)
        {
            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                // 3�b���Ƃ̏��������s����
                // �����ɏ����̃R�[�h���L�q����
                ShowObjectOnGelSurface();

                timer = 0f; // �^�C�}�[�����Z�b�g����
            }
            // �I�u�W�F�N�g��\������
            
        }
        else
        {
            // �I�u�W�F�N�g���\���ɂ���
           LeakingGel.SetActive(false);
        }

// �O�t���[����remainingGel���X�V����
        previousRemainingGel = remainingGel;
    }


    private void ShowObjectOnGelSurface()
    {
        // GelSurface �̏�ɃI�u�W�F�N�g��\��������W��ݒ肷��
        Vector3 targetPosition = gelSurface.transform.position;

        // �I�u�W�F�N�g�̍��W��ݒ肷��
        LeakingGel.transform.position = targetPosition;

        // �I�u�W�F�N�g��\������
        LeakingGel.SetActive(true);
        transform.DOMoveY(transform.position.y+1f, 3f);
    }
}


