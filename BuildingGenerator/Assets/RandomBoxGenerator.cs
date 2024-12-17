using UnityEngine;

public class RandomBoxGenerator : MonoBehaviour
{
    public int minLength = 1;
    public int maxLength = 10;

    public int minWidth = 1;
    public int maxWidth = 10;
    
    public float wallThickness = 0.1f;
    public float length = 0;
    public float width = 0;

    private GameObject parentBox;

    void Start()
    {
        parentBox = null;
        GenerateRandomBoxes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateRandomBoxes();
        }
    }

    void GenerateRandomBoxes()
    {
        if (parentBox != null)
        {
            Destroy(parentBox);
        }

        parentBox = new GameObject("RandomBoxes");

        width = Random.Range(minWidth, maxWidth) * 10;
        length = Random.Range(minLength, maxLength) * 10;
        float height = 10;

        GameObject box = new GameObject("Box");
        box.transform.SetParent(parentBox.transform);

        // Create floor and walls
        CreateWall("Floor", new Vector3(width + wallThickness, wallThickness, length + wallThickness), new Vector3(0, -0.15f, 0), box.transform);

        CreateWall("Wall_Left", new Vector3(wallThickness, height, length), new Vector3(-width / 2, height / 2, wallThickness / 2), box.transform);
        CreateWall("Wall_Right", new Vector3(wallThickness, height, length), new Vector3(width / 2, height / 2, -wallThickness / 2), box.transform);

        // Create a wall with a door
        CreateWallWithDoor("Wall_Front", new Vector3(width, height, wallThickness), new Vector3(0, height / 2, -length / 2), box.transform);

        CreateWall("Wall_Back", new Vector3(width, height, wallThickness), new Vector3(wallThickness / 2, height / 2, length / 2), box.transform);
    }

    GameObject CreateWall(string name, Vector3 size, Vector3 position, Transform parent)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = name;
        wall.transform.localScale = size;
        wall.transform.localPosition = position;
        wall.transform.SetParent(parent);
        wall.GetComponent<Renderer>().material.color = Color.white;
        return wall;
    }

    void CreateWallWithDoor(string name, Vector3 size, Vector3 position, Transform parent)
    {
        float doorWidth = 5f; // Width of the door opening
        float doorHeight = 2f; // Height of the door top cube
        float wallHeight = size.y;

        // Generate a random offset for the door within the wall boundaries
        float maxDoorOffset = (size.x / 2) - (doorWidth / 2);
        float doorCenterOffset = Random.Range(-maxDoorOffset, maxDoorOffset); // Random position for the door

        // Calculate the size of the side walls
        float leftWallWidth = (size.x / 2) + doorCenterOffset - (doorWidth / 2);
        float rightWallWidth = (size.x / 2) - doorCenterOffset - (doorWidth / 2);

        // Create the left side of the wall
        CreateWall(name + "_Left", new Vector3(leftWallWidth + .3f, wallHeight, size.z),
                position + new Vector3(-(size.x / 2 - leftWallWidth / 2), 0, 0), parent);

        // Create the right side of the wall
        CreateWall(name + "_Right", new Vector3(rightWallWidth, wallHeight, size.z),
                position + new Vector3((size.x / 2 - rightWallWidth / 2), 0, 0), parent);

        // Create the top part of the door as a 5x2 cube
        GameObject doorTop = GameObject.CreatePrimitive(PrimitiveType.Cube);
        doorTop.name = name + "_DoorTop";
        doorTop.transform.localScale = new Vector3(doorWidth, doorHeight, size.z);
        doorTop.transform.localPosition = position + new Vector3(doorCenterOffset, (wallHeight / 2) - (doorHeight / 2), 0);
        doorTop.transform.SetParent(parent);
        doorTop.GetComponent<Renderer>().material.color = Color.white;
    }

}
