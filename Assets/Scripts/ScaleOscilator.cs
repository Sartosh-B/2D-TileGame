using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOscilator : MonoBehaviour
{
    public Vector2 minScale = new Vector2(0.5f, 0.5f);   // Minimalne skale x i y
    public Vector2 maxScale = new Vector2(2f, 2f);       // Maksymalne skale x i y
    public float period = 2f;                             // Okres czasu oscylacji w sekundach

    private Vector3 initialScale;                        // Pocz¹tkowa skala obiektu
    private float time;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void Update()
    {
        time += Time.deltaTime;

        // Oblicz wartoœæ skalowania w zakresie [-1, 1] na podstawie funkcji sinusoidalnej
        float t = Mathf.Sin(time / period * Mathf.PI * 2f);

        // Interpoluj skalê pomiêdzy minimaln¹ a maksymaln¹ na podstawie wartoœci t
        float scaleX = Mathf.Lerp(minScale.x, maxScale.x, (t + 1f) / 2f);
        float scaleY = Mathf.Lerp(minScale.y, maxScale.y, (t + 1f) / 2f);

        // Zastosuj now¹ skalê do obiektu
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}


