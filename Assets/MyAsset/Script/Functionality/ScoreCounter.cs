using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [field: SerializeField] private TextMeshProUGUI tmp;
    public static ScoreCounter instance;
    public int score;
    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }
    void Start()
    {
        score = 10;
    }
    private void Update()
    {
        tmp.SetText("Score: " + score);
    }
}
