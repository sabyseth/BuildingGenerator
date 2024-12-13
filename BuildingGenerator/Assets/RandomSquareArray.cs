using UnityEngine;

public class RandomSquareArray : MonoBehaviour
{
    // Array to hold square sizes
    private int[] squareSizes;

    // Number of squares to create
    public int numberOfSquares = 1;

    void Start()
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

    string GenerateSquare(int size)
    {
        string squareRepresentation = "";
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                squareRepresentation += (row == 0 || row == size - 1 || col == 0 || col == size - 1) ? "X" : "O";
            }
            squareRepresentation += "\n";
        }
        return squareRepresentation;
    }
}
