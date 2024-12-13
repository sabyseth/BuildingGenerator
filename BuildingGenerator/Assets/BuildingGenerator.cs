using UnityEngine;
using System.Collections;

public class BuildingGenerator : MonoBehaviour
{
    public GameObject wallPrefab; 
    public GameObject floorPrefab;
    private int[] squareSizes;
    public int numberOfSquares = 1;



    void Start()
    {
        wallGeneorator();
    }

    public void SpawnWalls()
    {
        int length = Random.Range(0, 11);
        int width = Random.Range(1, 11);

        Vector3 firstWallPosition = transform.position;
        GameObject firstWall = Instantiate(wallPrefab, firstWallPosition, Quaternion.identity);

        float wallWidth = firstWall.GetComponent<Renderer>().bounds.size.x;

        Debug.Log(length);
        for (int i = 1; i < length; i++)
        {

            Vector3 secondWallPosition = firstWallPosition + new Vector3(0f, 0f, i * 10f);
            Instantiate(wallPrefab, secondWallPosition, Quaternion.identity);
        }
    }

    string GenerateSquare(int size)
    {
        Vector3 firstWallPosition = transform.position;
        GameObject firstWall = Instantiate(wallPrefab, firstWallPosition, Quaternion.identity);

        float wallWidth = firstWall.GetComponent<Renderer>().bounds.size.x;


        string squareRepresentation = "";
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                squareRepresentation += (row == 0 || row == size - 1 || col == 0 || col == size - 1) ? "X" : "O";

              
                Vector3 secondWallPosition = firstWallPosition + new Vector3(row * 10f, 0f, col * 10f);
                Instantiate(wallPrefab, secondWallPosition, Quaternion.identity);
            
                
            
            }
            squareRepresentation += "\n";
            Vector3 thirdWallPosition = firstWallPosition + new Vector3(row * 10f, 0f, 0f);
            Instantiate(wallPrefab, thirdWallPosition, Quaternion.identity);
                
        }
        return squareRepresentation;
    }

    public void wallGeneorator()
    {
        // Initialize the array
        squareSizes = new int[numberOfSquares];

        // Generate random squares and print their details
        for (int i = 0; i < numberOfSquares; i++)
        {
            int size = Random.Range(1, 10); // Random size between 1 and 10
            squareSizes[i] = size;
            Debug.Log($"Square {i + 1}:\n" + GenerateSquare(size));
        }
    }
}
