using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType { course }

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.course;
    SpriteRenderer sprite;
    BoxCollider2D itemCollider;
    bool hasBeenCollected = false;
    public int value = 1;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<BoxCollider2D>();
    }

    void Show()
    {
        sprite.enabled = true;
        itemCollider.enabled = true;
        hasBeenCollected = false;
    }

    void Hide()
    {
        sprite.enabled = false;
        itemCollider.enabled = false;
    }

    void Collect()
    {
        Hide();
        hasBeenCollected = true;
        switch (this.type)
        {
            case CollectableType.course:
                GameManager.instance.CollectObject(this);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "Player") && !hasBeenCollected)
        {
            Collect();
        }
    }
}
