using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jail : MonoBehaviour
{
    public Sprite closedSprite;
    public Sprite openSprite;
    private BoxCollider2D c;
    private SpriteRenderer r;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<BoxCollider2D>();
        r = GetComponent<SpriteRenderer>();
    }

    public void Unlock()
    {
        c.enabled = false;
        r.sprite = openSprite;
    }

    public void Lock()
    {
        c.enabled = true;
        r.sprite = closedSprite;
    }
}
