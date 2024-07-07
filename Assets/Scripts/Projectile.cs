using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public System.Action destroyed;

    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;

    void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.destroyed != null) 
        {
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
