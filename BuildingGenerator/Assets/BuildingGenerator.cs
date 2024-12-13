using UnityEngine;
using System.Collections;

public class BuildingGenerator : MonoBehaviour
{
    public GameObject wallPrefab; 
    public GameObject floorPrefab;
    private int[] squareSizes;
    public int numberOfSquares = 1;
    public int minSize = 2;
    public int maxSize = 10;
    void Start()
    {
        wallGeneorator();
    }

    string GenerateSquare(int size)
    {
        string squareRepresentation = "";

        float wallPrefabWidth = 10; // Replace 1f with your prefab width if different
        float wallPrefabHeight = 10; // Replace 1f with your prefab height if different

        float floorPrefabWidth = 5; // Replace 1f with your prefab width if different
        float floorPabHeight = 10; // Replace 1f with your prefab height if different


        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                bool isEdge = (row == 0 || row == size - 1 || col == 0 || col == size - 1);
                
                if (isEdge)
                {
                    squareRepresentation += "X";  
                    Vector3 wallPosition = new Vector3(col * floorPrefabWidth, 0f, row * floorPabHeight);
                    
                    Quaternion wallRotation = Quaternion.identity;  
                    
                    
                    if (row == 0 || row == size - 1)
                    {
                        wallRotation = Quaternion.Euler(0f, 90f, 0f);  // Rotate to face left/right
                    }
                    
                    Instantiate(wallPrefab, wallPosition, wallRotation);
                }
                else
                {
                    squareRepresentation += "O";  
                    Vector3 floorPosition = new Vector3(col * wallPrefabWidth, 0f, row * wallPrefabHeight);
                    Instantiate(floorPrefab, floorPosition, Quaternion.identity);
                }
            }

            squareRepresentation += "\n";
        }

        return squareRepresentation;
    }

    public void wallGeneorator()
    {
        squareSizes = new int[numberOfSquares];

        for (int i = 0; i < numberOfSquares; i++)
        {
            int size = Random.Range(minSize, maxSize); 
            squareSizes[i] = size;
            Debug.Log($"Square {i + 1}:\n" + GenerateSquare(size));
        }
    }
}
