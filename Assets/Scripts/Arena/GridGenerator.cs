using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject prefab;
    public int gridSize = 5;
    public float spacing = 1f;

    void Start()
    {
        float offset = (gridSize - 1) * 0.5f * spacing;

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Vector3 position = new Vector3(x * spacing - offset, 0, z * spacing - offset);
                Instantiate(prefab, transform.position + position, Quaternion.identity, transform);
            }
        }
    }
}