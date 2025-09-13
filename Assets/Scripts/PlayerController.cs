using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public float thrustForce;
    public float turnSpeed;
    Rigidbody2D rb;

    public GameObject boosterFlame;
    public UIDocument uiDocument;

    public float scoreMultiplier = 10f;

    public GameObject explosionEffect;
    public TMP_Text input;

    private float elapsedTime = 0f;
    private float score = 0f;
    private Label scoreText;

    private Button restartButton;

    private string currentInput = "";

    public static event Action<string> OnWordTyped;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
    }

    // Update is called once per frame
    void Update()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }

        if (GetComponent<CloneMarker>() != null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        Vector2 targetDirection = ((Vector2)(mousePos - transform.position)).normalized;

        transform.up = Vector3.Lerp(transform.up, targetDirection, turnSpeed * Time.deltaTime);

        if (Mouse.current.leftButton.isPressed)
        {
            rb.AddForce(transform.up.normalized * thrustForce * Time.deltaTime);
        }

        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = "Score: " + score;

        if (Keyboard.current == null) return;

        foreach (var key in Keyboard.current.allKeys)
        {
            if (key != null && key.wasPressedThisFrame)
            {
                string c = key.displayName;

                if (key == Keyboard.current.enterKey)
                {
                    OnWordTyped?.Invoke(currentInput);
                    currentInput = "";
                }
                else if (key == Keyboard.current.backspaceKey)
                {
                    if (currentInput.Length > 0)
                        currentInput = currentInput.Substring(0, currentInput.Length - 1);
                }
                else if (c.Length == 1)
                {
                    currentInput += c;

                }
            }
        }
        input.text = currentInput;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
        Destroy(Spawner.Instance);
        Destroy(gameObject);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
