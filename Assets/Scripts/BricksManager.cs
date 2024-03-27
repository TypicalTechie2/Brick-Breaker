using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviour
{

    [SerializeField] private GameObject brickPrefab;
    private int rows = 5;
    private int columns = 5;
    private float brickWidth = 3f;
    private float brickHeight = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPosition = new Vector3(-6, 1f, 0f);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = startPosition + new Vector3(col * brickWidth, row * brickHeight, 0);
                Instantiate(brickPrefab, position, Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
