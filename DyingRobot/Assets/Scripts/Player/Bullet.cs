using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [HideInInspector] public Vector2 direction;
    public GameObject hitParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * Time.deltaTime * speed;
        if(Mathf.Abs(transform.position.x) > 10 || Mathf.Abs(transform.position.y) > 10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Instantiate(hitParticle, transform.position, Quaternion.Euler(-90,0,0));
            Destroy(this.gameObject);
            FindObjectOfType<AudioManager>().PlaySound("BulletHit");
        }
        
    }
}
