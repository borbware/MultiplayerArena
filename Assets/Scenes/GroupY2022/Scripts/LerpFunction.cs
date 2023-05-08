using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFunction : MonoBehaviour
{

    public static IEnumerator LerpPosition(Transform _transform, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = _transform.position;
        float time = 0;
        while (time < duration)
        {
            _transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        _transform.position = targetPosition;
        


    }
}
