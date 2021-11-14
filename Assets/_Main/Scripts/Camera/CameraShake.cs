using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private IEnumerator Shake(float duration,float magnitude)
    {
        Vector3 originalPos = new Vector3(0,0,-10);

        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);

            elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public void Shk(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
}
