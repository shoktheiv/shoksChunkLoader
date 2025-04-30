using TMPro;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private Vector2Int gridPos;
    public TextMeshPro number;

    public Vector2Int GetGridPos(){
        return gridPos;
    }

    public void SetGridPos(int n){
        LoadChunk();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")){
            GenerateMap.instance.SetPlayerPosition(transform.position);
        }
    }

    public void UpdatePos(Vector2 playerPos){
        float chunkSize = GenerateMap.instance.chunksize;
        int renderDiameter = GenerateMap.instance.renderDiameter;
        float totalSize = renderDiameter * chunkSize;

        Vector2 pos = transform.position;
        Vector2 dif = pos - playerPos;

        bool load = false;

        if (Mathf.Abs(dif.x) > totalSize / 2f)
        {
            float offsetX = Mathf.Sign(dif.x) * -totalSize;
            pos.x += offsetX;
            load = true;
        }

        if (Mathf.Abs(dif.y) > totalSize / 2f)
        {
            float offsetY = Mathf.Sign(dif.y) * -totalSize;
            pos.y += offsetY;
            load = true;
        }

        transform.position = pos;

        if(load) LoadChunk();
    }

    void LoadChunk(){
        int seed = GetSeedFromPosition(transform.position);
        Random.InitState(seed);

        number.text = Random.Range(0, 99).ToString();
        
    }

    int GetSeedFromPosition(Vector2 pos){
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);

        unchecked
        {
            int hash = 13;
            hash = hash * 31 + x;
            hash = hash * 31 + y;
            return hash;
        }
    }
}
