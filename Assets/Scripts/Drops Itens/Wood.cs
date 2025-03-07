using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{

    [SerializeField] private float dropSpeed;
    [SerializeField] private float timeMove;

    private float timeCount;
    
    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount < timeMove)
        {
            transform.Translate(Vector2.right * (Time.deltaTime * dropSpeed));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerItens>().AddWood(1);
            Destroy(gameObject);
        }
    }
}
