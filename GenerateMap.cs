using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public Transform player;

    Vector2 playerPos = new Vector2(0,0);

    public int chunksize;
    public int renderDiameter;
    public int renderDistance;
    public GameObject chunkPrefab;

    public List<Chunk> chunkPool;
    private Chunk[] grid;

    public static GenerateMap instance;

    void Awake()
    {
        instance = this;
        SetUpChunks();
    }

    public void SetPlayerPosition(Vector2 pos){
        playerPos = pos;
        foreach (Chunk item in chunkPool)
        {
            item.UpdatePos(playerPos);
            Debug.Log("jjj");
        }
    }

    void SetUpChunks(){
        grid = new Chunk[renderDiameter * renderDiameter];
        chunkPool = new List<Chunk>();
        int n = 1;

        for (int y = 0; y < renderDiameter; y++)
        {
            for (int x = 0; x < renderDiameter; x++)
            {
                GameObject c = Instantiate(chunkPrefab, GetWorldPositionFromGrid(new Vector2(x, y)), Quaternion.identity);
                grid[n -1] = c.GetComponent<Chunk>();
                chunkPool.Add(c.GetComponent<Chunk>());
                c.GetComponent<Chunk>().SetGridPos(n);
                n++;
            }
        }
    }

    public Vector2 GetWorldPositionFromGrid(Vector2 gridPos){
        Vector2 worldPos = centerGridPosition(gridPos) * chunksize;
        return worldPos + playerPos;
    }

    public Vector2 GetGridPositionFromWorld(Vector2 worldPos){
        Vector2 relPos = worldPos - (Vector2)playerPos;
        Vector2 gridPos = relPos / chunksize;
        return new Vector2(gridPos.x + (renderDiameter/2), gridPos.y + (renderDiameter/2));

    }

    public Vector2 centerGridPosition(Vector2 gridPos){
        return new Vector2(gridPos.x - (renderDiameter/2), gridPos.y - (renderDiameter/2));
    }

    public Vector2 uncenterGridPosition(Vector2 gridPos){
        return new Vector2(gridPos.x + (renderDiameter/2f), gridPos.y + (renderDiameter/2f));
    }
}
