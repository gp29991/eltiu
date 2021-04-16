using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class icicleSpawner : MonoBehaviour
{ 
    public GameObject icicle;
    private float minX;
    private float maxX;
    public GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        float offset = 0.3f;
        minX = (((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) * -1) + offset;
        maxX = ((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) - offset;
        StartCoroutine(beginSpawn());
    }
    
    IEnumerator beginSpawn()
    {
        playerController pc = player.GetComponent<playerController>();
        yield return new WaitForSeconds(Random.Range(1f/Mathf.Pow(2f, pc.level - 1f), 2f/ Mathf.Pow(2f, pc.level - 1f)));

        GameObject k = Instantiate(icicle);
        switch (pc.level)
        {
            case 1:
                k.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                break;
            case 2:
                k.GetComponent<SpriteRenderer>().color = new Color(0.51f, 0.59f, 1f, 1f);
                break;
            case 3:
                k.GetComponent<SpriteRenderer>().color = new Color(0.70f, 0.47f, 0.78f, 1f);
                break;
            default:
                k.GetComponent<SpriteRenderer>().color = new Color(0.70f, 0.47f, 0.78f, 1f);
                break;
        }
        

        float x = Random.Range(minX, maxX);

        k.transform.position = new Vector2(x, transform.position.y);

        StartCoroutine(beginSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
