using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 20;
    public int width = 256;
    public int height = 256;
    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public Material[] terrainMaterials; // Array to hold different terrain materials
    private TreeGeneration treeGeneration;

    void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
        treeGeneration = GetComponent<TreeGeneration>();
        GenerateTerrainWithMaterial();
    }

    void GenerateTerrainWithMaterial()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            terrain.terrainData = GenerateTerrain(terrain.terrainData);
            AssignRandomMaterial();
        }
        else
        {
            Debug.LogError("Terrain component not found on this GameObject.");
        }
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    void AssignRandomMaterial()
    {
        if (terrainMaterials.Length > 0)
        {
            Terrain terrain = GetComponent<Terrain>();
            if (terrain != null)
            {
                int randomIndex = Random.Range(0, terrainMaterials.Length);
                terrain.materialTemplate = terrainMaterials[randomIndex];
                treeGeneration.SetTerrainMaterial(terrainMaterials[randomIndex]);  // Set the material for tree generation
            }
            else
            {
                Debug.LogError("Terrain component not found when trying to assign material.");
            }
        }
        else
        {
            Debug.LogError("No materials assigned to terrainMaterials array.");
        }
    }
}
