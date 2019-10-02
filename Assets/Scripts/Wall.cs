using UnityEngine;

public class Wall : MonoBehaviour
{
    private Vector3 target = new Vector3(0,0,-20);
    const float speed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeGrace();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (System.Math.Abs(transform.position.z - target.z) < 0.2)
        {
            Destroy(this.gameObject);
        }

    }

    void RandomizeGrace()
    {
        foreach (var item in GetComponentsInChildren<Transform>())
        {
            if (item.CompareTag("Grace"))
            {
                item.position = new Vector3(Random.Range(-8.5f,8.5f), Random.Range(1.8f,5.2f), item.position.z);
            }
        } 
    }

    internal void Break()
    {
        /*foreach (var item in GetComponentsInChildren<Transform>())
        {
            GameObject.Destroy(item.gameObject);
        }*/
        GameObject.Destroy(this.gameObject);
    }
}
