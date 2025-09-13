using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System;
using System.Collections.Generic;

public class Dictionary : MonoBehaviour
{

    public static Dictionary Instance
    {
        get; private set;
    }

    public List<string> words;
    public List<string> trigrams;

    public TextAsset dictionaryFile;
    public TextAsset trigramsFile;



    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadDictionary();

    }

    void LoadDictionary()
    {
        words = new List<string>();
        trigrams = new List<string>();

        foreach (var line in dictionaryFile.text.Split('\n'))
        {
            var word = line.Trim();
            if (!string.IsNullOrEmpty(word))
            {
                words.Add(word);
            }

        }

        foreach (var line in trigramsFile.text.Split('\n'))
        {
            var trigram = line.Trim();
            if (!string.IsNullOrEmpty(trigram))
            {
                trigrams.Add(trigram);
            }

        }

    }

}
