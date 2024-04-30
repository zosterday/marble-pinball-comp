using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim12DestroyableBlock : MonoBehaviour
{
    private const float shrinkDuration = 0.5f;

    private SpriteRenderer spriteRenderer;

    private float initialScale;

    private bool isBroken;

    public bool IsBroken => isBroken;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale.x;
    }

    public IEnumerator BreakBlock()
    {
        isBroken = true;

        var elapsedTime = 0f;
        while (elapsedTime < shrinkDuration)
        {
            var t = elapsedTime / shrinkDuration;
            var newScale = Mathf.Lerp(initialScale, 0f, t);
            transform.localScale = new Vector3(newScale, newScale, 1f);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        gameObject.SetActive(false);
    }
}
