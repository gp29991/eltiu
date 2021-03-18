using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class icicleSpawner : MonoBehaviour
{ 
    public GameObject icicle;
    private float minX;
    private float maxX;
    

    // Start is called before the first frame update
    void Start()
    {
        float offset = 0.3f;
        minX = (((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) * -1) + offset;
        maxX = ((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) - offset;
        StartCoroutine(zacznijSpawn());
    }
    
    IEnumerator zacznijSpawn()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));

        GameObject k = Instantiate(icicle);

        float x = Random.Range(minX, maxX);

        k.transform.position = new Vector2(x, transform.position.y);

        StartCoroutine(zacznijSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
