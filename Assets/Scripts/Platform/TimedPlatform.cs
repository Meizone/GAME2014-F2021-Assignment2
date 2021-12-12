using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPlatform : MonoBehaviour
{

    [SerializeField] private float TimeOnPlatform = 0f;
    [SerializeField] private float TimeAllowedOnPlatform = 0f;

    private void OnCollisionStay2D(Collision2D col)
    {
        TimeOnPlatform += Time.deltaTime;

        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer sr in spriteRenderers)
        {
            sr.color = new Color(1,1,1, 1-(TimeOnPlatform/TimeAllowedOnPlatform));
        }


        if(TimeOnPlatform > TimeAllowedOnPlatform)
        {
            Destroy(gameObject);
        }


    }

    private void OnCollisionExit2D(Collision2D col)
    {
        TimeOnPlatform = 0f;
    }
}
