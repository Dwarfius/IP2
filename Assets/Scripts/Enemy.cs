using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public Sprite markedSprite;
    [HideInInspector] public bool marked;

    public void Mark()
    {
        Debug.Log(gameObject.name);
        marked = true;
        (renderer as SpriteRenderer).sprite = markedSprite;
    }
}
