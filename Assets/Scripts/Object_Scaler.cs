using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Scaler : MonoBehaviour
{
    [Header("Scale")]
    Vector2 minScale;
    [SerializeField] Vector2 maxScale;
    [SerializeField] bool repetable;
    [SerializeField] float speed;
    [SerializeField] float duration;

    private void OnEnable()
    {
        StartCoroutine(Scale());
    }
    private IEnumerator Scale()
    {
        minScale = transform.localScale;
        while (repetable)
        {
            yield return RepeatLerp(minScale, maxScale, duration);
            yield return RepeatLerp(maxScale, minScale, duration);

        }
    }
    public IEnumerator RepeatLerp(Vector2 a, Vector2 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector2.Lerp(a, b, i);
            yield return null;
        }
    }
}
