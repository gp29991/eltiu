using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundFit : MonoBehaviour
{
    public GameObject player;
    private int targetLevel = 2;
    public Sprite[] backgroundArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerController pc = player.GetComponent<playerController>();
        if (pc.level >= targetLevel && pc.level <= backgroundArray.Length)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = backgroundArray[pc.level - 1];
            targetLevel++;
        }
    }

    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        //print("Sprite x: " + spriteSize.x + ", Camera x: " + cameraSize.x);

        float scaleX = (cameraSize.x * 1.4f) / spriteSize.x;
        float scaleY = cameraSize.y / spriteSize.y;

        Vector2 scale = new Vector2(scaleX, scaleY);

        transform.localScale = scale;
    }
}
