using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour
{
    bool isBouncing = false;
    public void Hit()
    {
        if (!this.isBouncing)
        {
            StartCoroutine(this.Bouncing());

        }
    }

    private IEnumerator Bouncing()
    {
        this.isBouncing = true;
        float time = 0f;
        float duration = 0.1f;

        Vector2 startPosition = this.transform.position;
        Vector2 endPosition = (Vector2)this.transform.position + (Vector2.up * 0.25f);

        while (time < duration)
        {

            this.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);
            time = time + Time.deltaTime;
            yield return null;
        }
        this.transform.position = endPosition;
        time = 0f;
        while (time < duration)
        {

            this.transform.position = Vector2.Lerp(endPosition, startPosition, time / duration);
            time = time + Time.deltaTime;
            yield return null;
        }
        this.transform.position = startPosition;
        this.isBouncing = false;
    }
}
