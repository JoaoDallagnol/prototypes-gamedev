using UnityEngine;
using UnityEngine.Tilemaps;

public class CaveGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    public int width = 100;
    public int height = 100;
    [Range(0, 100)]
    public int fillPercent = 45;
    public int smoothingIterations = 5;

    [Header("Tilemap Settings")]
    public Tilemap tilemap;
    public TileBase wallTile;
    public TileBase floorTile;

    private int[,] map;

    void Start()
    {
        GenerateCave();
    }

    public void GenerateCave()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < smoothingIterations; i++)
        {
            SmoothMap();
        }

        RenderMap();
    }

    void RandomFillMap()
    {
        System.Random rand = new System.Random();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    map[x, y] = 1; // borda é parede
                else
                    map[x, y] = rand.Next(0, 100) < fillPercent ? 1 : 0;
            }
        }
    }

    void SmoothMap()
    {
        int[,] newMap = new int[width, height];

        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                int wallCount = GetSurroundingWallCount(x, y);

                if (wallCount > 4)
                    newMap[x, y] = 1;
                else if (wallCount < 4)
                    newMap[x, y] = 0;
                else
                    newMap[x, y] = map[x, y];
            }
        }

        map = newMap;
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int count = 0;

        for (int nx = gridX - 1; nx <= gridX + 1; nx++)
        {
            for (int ny = gridY - 1; ny <= gridY + 1; ny++)
            {
                if (nx == gridX && ny == gridY)
                    continue;

                if (map[nx, ny] == 1)
                    count++;
            }
        }

        return count;
    }

    void RenderMap()
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                tilemap.SetTile(pos, map[x, y] == 1 ? wallTile : floorTile);
            }
        }
    }
}
