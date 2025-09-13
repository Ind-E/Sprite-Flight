using UnityEngine;
using TMPro;

public class Obstacle : MonoBehaviour
{
    public float minSize;
    public float maxSize;

    public float minSpeed;
    public float maxSpeed;

    public float maxSpinSpeed;

    public TMP_Text text;

    Rigidbody2D rb;

    void Start()
    {

        if (GetComponent<CloneMarker>() != null) return;

        var trigrams = Dictionary.Instance.trigrams;

        string randomTrigram = trigrams[Random.Range(0, trigrams.Count)];

        text.text = randomTrigram;

        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);


        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        Vector2 randomDirection = Random.insideUnitCircle;

        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomSpeed * Time.deltaTime);
        rb.AddTorque(randomTorque);
    }

    void OnEnable()
    {
        PlayerController.OnWordTyped += HandleWordTyped;
    }

    void OnDisable()
    {
        PlayerController.OnWordTyped -= HandleWordTyped;
    }

    void HandleWordTyped(string typedWord)
    {
        if (typedWord.Contains(text.text) && Dictionary.Instance.words.Contains(typedWord))
        {
            OnWordMatched(typedWord);
        }
    }

    private void OnWordMatched(string word)
    {
        Spawner.Instance.timer *= 0.75f;
        Dictionary.Instance.words.Remove(word);
        Destroy(gameObject);
    }


}
