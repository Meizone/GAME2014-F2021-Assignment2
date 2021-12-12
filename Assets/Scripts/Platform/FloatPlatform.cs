using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPlatform : MonoBehaviour
{

    [SerializeField] private float VerticalDisplacement;
    [SerializeField] private float DisplacementSpeed;
    [SerializeField] private float ResetSpeed;
    [SerializeField] private Vector3 OriginalPos;

    [SerializeField] private float timeSinceOn = 0;
    [SerializeField] private bool PlayerOntop = false;

    void Start()
    {
        OriginalPos = transform.position;
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        PlayerOntop = true;
        timeSinceOn += 1.0f;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(OriginalPos.x, OriginalPos.y-VerticalDisplacement, OriginalPos.z), Time.deltaTime * DisplacementSpeed);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        PlayerOntop = false;
        timeSinceOn = 0.0f;
    }

    void Update()
    {
        if(!PlayerOntop && timeSinceOn > 0.0f)
            timeSinceOn -= 1.0f;

        if(timeSinceOn == 0.0f) 
            transform.position = Vector3.Lerp(transform.position, OriginalPos, Time.deltaTime * ResetSpeed);
        
    }


}
