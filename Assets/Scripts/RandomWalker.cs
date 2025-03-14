using UnityEngine;

public class RandomWalker : MonoBehaviour
{
    float speed;
    Vector3 dir;

    private void Awake()
    {
        Invoke(nameof(ChooseRandomDirectionAndSpeed), Random.Range(1f, 10f));
    }

    private void FixedUpdate()
    {
        if (speed == 0) return;
        if (dir == null) return;

        transform.position += (dir * speed) / 10f;
    }

    void ChooseRandomDirectionAndSpeed()
    {
        speed = Random.Range(0.1f, 1f);
        dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        Invoke(nameof(ChooseRandomDirectionAndSpeed), Random.Range(1f, 10f));
    }
}
