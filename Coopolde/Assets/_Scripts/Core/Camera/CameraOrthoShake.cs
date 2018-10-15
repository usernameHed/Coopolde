using UnityEngine;
using System.Collections;

public class CameraOrthoShake : SingletonMono<CameraOrthoShake>
{
    private Vector3 _originalPos;
    private float _timeAtCurrentFrame;
    private float _timeAtLastFrame;
    private float _fakeDelta;

    void Update()
    {
        // Calculate a fake delta time, so we can Shake while game is paused.
        _timeAtCurrentFrame = Time.realtimeSinceStartup;
        _fakeDelta = _timeAtCurrentFrame - _timeAtLastFrame;
        _timeAtLastFrame = _timeAtCurrentFrame;
    }

    public void Shake(float duration, float amount)
    {
        _originalPos = gameObject.transform.localPosition;
        StopAllCoroutines();
        StartCoroutine(CShake(duration, amount));
    }

    public IEnumerator CShake(float duration, float amount)
    {
        float endTime = Time.time + duration;

        while (duration > 0)
        {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

            duration -= _fakeDelta;

            yield return null;
        }

        transform.localPosition = new Vector3(0,10,0);
    }

}