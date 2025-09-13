using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefab;

    public float startInterval;
    public float decrement;
    public float timer;

    float currentInterval;

    public static Spawner Instance
    {
        get; private set;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentInterval = startInterval;
        timer = currentInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Instantiate(prefab, GetRandomEdgePosition(), Quaternion.identity);

            currentInterval = currentInterval * decrement;

            timer = currentInterval;
        }

    }

    private Vector3 GetRandomEdgePosition()
    {
        float x = 0f;
        float y = 0f;

        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // Left
                x = 0f;
                y = Random.value;
                break;
            case 1: // Top
                x = Random.value;
                y = 1f;
                break;
            case 2: // Right
                x = 1f;
                y = Random.value;
                break;
            case 3: // Bottom
                x = Random.value;
                y = 0f;
                break;
        }

        Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(x, y, Camera.main.nearClipPlane + 5f));
        worldPos.z = 0;
        return worldPos;
    }
}
