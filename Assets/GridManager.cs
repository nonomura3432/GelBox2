using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public int gridSizeX;  // �}�X�ڂ̉������̐�
    public int gridSizeY;  // �}�X�ڂ̏c�����̐�
    public float cellSize; // �}�X�ڂ̃Z���̃T�C�Y

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // �}�X�ڂ̈ʒu���v�Z
                Vector3 cellPosition = new Vector3(x * cellSize, 0f, y * cellSize);

                // �}�X�ڃI�u�W�F�N�g�̐���
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cell.transform.position = cellPosition;
                cell.transform.localScale = new Vector3(cellSize, 0.1f, cellSize);
                cell.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}

