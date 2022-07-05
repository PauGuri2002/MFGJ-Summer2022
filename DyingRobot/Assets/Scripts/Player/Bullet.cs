using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [HideInInspector] public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * Time.deltaTime * speed;
        if(Mathf.Abs(transform.position.x) > 15 || Mathf.Abs(transform.position.y) > 15)
        {
            Destroy(this.gameObject);
        }
    }
}