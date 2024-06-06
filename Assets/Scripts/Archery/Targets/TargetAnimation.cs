using System.Collections;
using UnityEngine;

public class TargetAnimation : MonoBehaviour
{
    public void TriggerDestroyAnimation()
    {
        StartCoroutine(ScaleToZeroCoroutine(1.0f));
    }

    private IEnumerator ScaleToZeroCoroutine(float duration)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float scaleValue = EaseInBounce(1.0f, 0.0f, t);
            transform.localScale = Vector3.Lerp(targetScale, initialScale, scaleValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        Destroy(gameObject);
    }
    
    private static float EaseInBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        return end - EaseOutBounce(0, end, d - value) + start;
    }

    private static float EaseOutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);
            return end * (7.5625f * (value) * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);
            return end * (7.5625f * (value) * value + .9375f) + start;
        }
        else
        {
            value -= (2.625f / 2.75f);
            return end * (7.5625f * (value) * value + .984375f) + start;
        }
    }

    private static float EaseInOutBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        if (value < d * 0.5f) return EaseInBounce(0, end, value * 2) * 0.5f + start;
        else return EaseOutBounce(0, end, value * 2 - d) * 0.5f + end * 0.5f + start;
    }
}
