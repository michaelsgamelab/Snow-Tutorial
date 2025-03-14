using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject snowSquarePrefab;
    public GameObject randomWalkerPrefab;
    public Vector2Int size;
    public float spacing;
    public int walkerAmount;

    private void Awake()
    {
        GameObject snow_square;

        for (int i = -size.x / 2; i < size.x / 2; i++)
        {
            for (int j = -size.y / 2; j < size.y / 2; j++)
            {
                snow_square = Instantiate(snowSquarePrefab);
                snow_square.transform.SetParent(null);
                snow_square.transform.position = new Vector3(i * spacing, 0f, j * spacing);
                snow_square.transform.SetParent(transform);
            }
        }

        GameObject walker;

        for (int i = 0; i < walkerAmount; i++)
        {
            walker = Instantiate(randomWalkerPrefab);
            walker.transform.SetParent(null);
            walker.transform.position = new Vector3(
                Random.Range((-size.x / 2) * spacing, (size.x / 2) * spacing),
                0f,
                Random.Range((-size.y / 2) * spacing, (size.y / 2) * spacing));
        }
    }
}
