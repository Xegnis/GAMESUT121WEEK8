using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridSystem : MonoBehaviour {

    public List<MyGridCell> gridCellList = new List<MyGridCell>();

    public int rows = 5;
    public int columns = 5;
    public float cellSize = 1.0f;

    System.Random random;

    public int spawnCount = 10;

    public float pauseTime = 1.0f;
    public float buddySpeed = 1.0f;

    // Start is called before the first frame update
    void Start() {
        DrawGrid();
        DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        random = new System.Random( (int)(DateTime.Now - epochStart).TotalSeconds);

    }

    void DrawGrid() {
        float startX = ((-columns / 2.0f) * cellSize) + (cellSize / 2.0f);
        float startY = ((-rows/ 2.0f) * cellSize) + (cellSize / 2.0f);

        for(int i = 0; i < rows; i++) {
            Debug.Log($"Hey this is number #{i}");
            for(int j = 0; j < columns; j++) {
                gridCellList.Add(
                    new MyGridCell(
                         startX + (j * cellSize),
                         startY + (i * cellSize)
                         )
                    );
            }
        }
    }


    public Vector2 GetRandomLocation() {
        return gridCellList[random.Next(gridCellList.Count)].location;
    }

    private void OnDrawGizmos() {
        for (int i = 0; i < gridCellList.Count; i++)
            Gizmos.DrawWireCube(gridCellList[i].location, Vector2.one * cellSize);
    }


}

[System.Serializable]
public class MyGridCell
{

    public Vector2 location;

    public MyGridCell() { }
    public MyGridCell(Vector2 v)
    {
        location = new Vector2(v.x, v.y);
    }
    public MyGridCell(float x, float y)
    {
        location = new Vector2(x, y);
    }

}

