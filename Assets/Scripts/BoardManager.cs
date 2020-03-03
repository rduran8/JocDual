using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            this.minimum = min;
            this.maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;


    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);

    public GameObject exit;
    public GameObject boneFire;
    public GameObject playerCombat;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    private Transform boardHolder;
    private Transform boardHolderExtra;
    private Transform boardHolderCombat;
    private List<Vector3> gridPositions = new List<Vector3>();
    public static BoardManager script;
    private int seed = 0;


    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        script = this;
        boardHolder = new GameObject("Board").transform;
        boardHolderExtra = new GameObject("BoardExtra").transform;
        boardHolderCombat = new GameObject("BoardCombat").transform;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            instantiateTile(tileChoice,randomPosition);
        }
    }
    
    private void instantiateTile(GameObject tileChoice, Vector3 randomPosition)
    {
        GameObject myObject = Instantiate(tileChoice, randomPosition, Quaternion.identity);
        myObject.transform.parent = boardHolderExtra.transform;
    }

    private GameObject instantiateTileCombat(GameObject tileChoice, Vector3 randomPosition)
    {
        GameObject myObject = Instantiate(tileChoice, randomPosition, Quaternion.identity);
        myObject.transform.parent = boardHolderCombat.transform;
        return myObject;
    }

    public void SetupScene(int level)
    {
        setSeed();
        Debug.Log(Random.state.GetHashCode());
        BoardSetup();
        InitialiseList();
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
        if(level%5==0 || level==1)
        {
            Vector3 pbonfire = new Vector3(columns/2, rows/2, 0f);
            instantiateTile(boneFire,pbonfire);
        }else{
            LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
            int enemyCount = (int)Mathf.Log(level, 2f);
            LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        }
        instantiateTile(exit,new Vector3(columns - 1, rows - 1, 0f));
    }

    public PlayerCombat addPlayerOnCombat()
    {
        GameObject player = instantiateTileCombat(playerCombat,new Vector3(columns/4, (int)(rows/1.25), 0f));
        return player.GetComponent<PlayerCombat>();
    }

    public Enemy addEnemyOnCombat()
    {
        GameObject enemy = instantiateTileCombat(enemyTiles[0],new Vector3((int)(columns/1.25), (int)(rows/1.25), 0f));
        return enemy.GetComponent<Enemy>();
    }

    public void  setSeed(){
        if(seed == 0){
            Random.InitState(Random.Range(-1000000,1000000));
        }else{
            Random.InitState(seed);
        }
    }

    public void  setSeed(int seed){
        this.seed = seed;
        if(seed == 0){
            Random.InitState(Random.Range(-1000000,1000000));
        }else{
            Random.InitState(seed);
        }
    }

    public void saveSeed(){
        this.seed = Random.state.GetHashCode();
    }
}
