using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [field: SerializeField] public float tScalMin {get; private set;}
    public static TimeManager instance;
    public float tScale;
    
    public bool deScale;
    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }
    void Start()
    {
        
        deScale = true;
        tScale = 1;
        StartCoroutine(ScaleTime());
    }
    public void setTimeScale()
    {
        if (tScale < 0) return;
        Time.timeScale = tScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    IEnumerator ScaleTime()
    {
        while (true)
        {
            if (tScale > tScalMin && deScale)
            {
                tScale -= 0.02f;
                setTimeScale();
            }else if(tScale < 1 && !deScale)
            {
                tScale += 0.02f;
                setTimeScale();
            }
            yield return new WaitForSeconds(0.02f);
        }
        
    }
    IEnumerator UpScaleTimer()
    {
        deScale = false;
        yield return new WaitForSeconds(0.2f);
        deScale = true;

    }
    public void startTimmer ()
    {
        StartCoroutine(UpScaleTimer());
    }
}
