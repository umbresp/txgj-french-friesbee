using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizer : MonoBehaviour
{
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        transform.localScale = new Vector2(Screen.width / width, Screen.height / height);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1, 1, 1);
        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        var tempScale = transform.localScale;
        tempScale.x = (float)(worldScreenWidth / width);
        tempScale.y = (float)(worldScreenHeight / height);
        transform.localScale = tempScale;
    }
}
