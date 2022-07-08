using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [HideInInspector] public Vector2 direction;
    public GameObject hitParticle;
    public Sprite[] sprites;
    public float rotationSpeed = 20f;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f,360f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * Time.deltaTime * speed;
        if(Mathf.Abs(transform.position.x) > 10 || Mathf.Abs(transform.position.y) > 10)
        {
            Destroy(this.gameObject);
        }

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Door") && !other.gameObject.CompareTag("Target"))
        {
            Instantiate(hitParticle, transform.position, Quaternion.Euler(-90,0,0));
            Destroy(this.gameObject);
            FindObjectOfType<AudioManager>().PlaySound("BulletHit");
        }
        
    }
}
