using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [field: SerializeField] CanvasGroup black;

    public void onStart()
    {
        StartCoroutine(Close());
    }
    IEnumerator Close()
    {
        while (black.alpha < 1)
        {
            black.alpha += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync("BaseScene");
        yield return null;
    }
}
