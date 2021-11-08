using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : MonoBehaviour {

    public enum SeedType { RANDOM, CUSTOM }
    [Header("Random Related Stuff")]
    public SeedType seedType = SeedType.RANDOM;
    System.Random random;
    public int seed = 0;

    [Space]
    public bool animatedPath;
    public List<MyGridCell> gridCellList = new List<MyGridCell>();
    public List<MyGridCell> mainGridCellList = new List<MyGridCell>();
    public int pathLength = 10;
    [Range(1.0f, 10.0f)]
    public float cellSize = 1.0f;

    public Transform startLocation;

    int branchLength = 10;
    int branchNum = 3;

    // Start is called before the first frame update
    void Start() {

    }

    void SetSeed() {
        if (seedType == SeedType.RANDOM) {
            random = new System.Random();
        }
        else if (seedType == SeedType.CUSTOM) {
            random = new System.Random(seed);
        }
    }

    void CreateMainPath() {

        mainGridCellList.Clear();
        Vector2 currentPosition = startLocation.transform.position;
        gridCellList.Add(new MyGridCell(currentPosition));


        //create the main path
        for (int i = 0; i < pathLength; i++) {

            int n = random.Next(100);

            if (n.IsBetween(0, 49))
            {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition));
            mainGridCellList.Add(new MyGridCell(currentPosition));
        }
    }

    void CreateBranchPath(int length)
    {
        int index = random.Next(mainGridCellList.Count);
        Vector2 currentPosition = mainGridCellList[index].location;
        bool exists = false;

        //create branching path
        for (int i = 0; i < length; i++)
        {

            int n = random.Next(100);

            if (n.IsBetween(0, 15))
            {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else if (n.IsBetween(15, 50))
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }
            else if (n.IsBetween(50, 65))
            {
                currentPosition = new Vector2(currentPosition.x - cellSize, currentPosition.y);
            }
            else
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y - cellSize);
            }

            foreach (MyGridCell cell in gridCellList)
            {
                if (currentPosition == cell.location)
                    exists = true;
            }
            if (!exists)
                gridCellList.Add(new MyGridCell(currentPosition));
            
        }
    }



    private void OnDrawGizmos() {
        for (int i = 0; i < gridCellList.Count; i++) {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(gridCellList[i].location, Vector3.one * cellSize);
            Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            Gizmos.DrawCube(gridCellList[i].location, Vector3.one * cellSize);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetSeed();
            gridCellList.Clear();
            mainGridCellList.Clear();
            CreateMainPath();
            for (int i = 0; i < branchNum + random.Next(3); i++)
            {
                CreateBranchPath(branchLength + random.Next(3));
            }
            pathLength += 2;
            branchLength += 4;
            branchNum += 2;
        }
    }
}
