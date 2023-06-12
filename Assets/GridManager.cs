using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public int gridSizeX;  // マス目の横方向の数
    public int gridSizeY;  // マス目の縦方向の数
    public float cellSize; // マス目のセルのサイズ

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
                // マス目の位置を計算
                Vector3 cellPosition = new Vector3(x * cellSize, 0f, y * cellSize);

                // マス目オブジェクトの生成
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cell.transform.position = cellPosition;
                cell.transform.localScale = new Vector3(cellSize, 0.1f, cellSize);
                cell.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}

