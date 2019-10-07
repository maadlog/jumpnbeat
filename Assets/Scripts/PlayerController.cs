using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Color playerColor;
    // Start is called before the first frame update
    void Start()
    {
        playerColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        foreach (var item in this.GetComponentsInChildren<Renderer>())
        {
            item.material.color = playerColor;
        }
    }

    public KeyCode UP = KeyCode.W;
    public KeyCode LEFT = KeyCode.A;
    public KeyCode DOWN = KeyCode.S;
    public KeyCode RIGHT = KeyCode.D;

    

    float speed = 15f;
    private float gracePeriod = 0;
    bool airborne = false;
    bool doubleJump = false;
    bool isIdle = true;
    public float YTolerance = 0.0f;
    public float PlatformPushForce = 4f;
    // Update is called once per frame
    void Update()
    {
        if (platform != null)
        {
            float difToPlatform = (this.platform.transform.localScale.y + this.platform.transform.position.y) - this.gameObject.transform.position.y;
            
            if (Mathf.Abs(difToPlatform) > 0)
            {
                Debug.Log($"Trapped!! {difToPlatform} {platform.name}");

                var dist = Mathf.Min( Mathf.Abs(difToPlatform),2);
                Debug.Log($"TRansofmr!! {dist}");
                transform.position += new Vector3(0, dist, 0);
                this.GetComponent<Rigidbody>().AddForce(new Vector3(0, dist * PlatformPushForce, 0), ForceMode.Impulse);
            }
            
        }

        if (gracePeriod > 0)
        {
            gracePeriod -= Time.deltaTime;
        }
        if (Input.GetKey(LEFT))
        {
            if (isIdle)
            {
                this.GetComponent<Animator>().Play("StartRunning");
                isIdle = false;
            }
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 90, 0), 720 * Time.deltaTime);
        }
        if (Input.GetKey(RIGHT))
        {
            if (isIdle)
            {
                this.GetComponent<Animator>().Play("StartRunning");
                isIdle = false;
            }
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -90, 0), 720 * Time.deltaTime);
        }

        if (Input.GetKeyDown(UP) && !doubleJump)
        {
            if (airborne)
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                doubleJump = true;
            }
            airborne = true;
            this.GetComponent<Rigidbody>().AddForce(0, 15, 0, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(DOWN) && airborne)
        {
            
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, -20, 0), ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!Input.anyKey && !airborne)
        {
            this.isIdle = true;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 720 * Time.deltaTime);
            this.GetComponent<Animator>().Play("Idle");
        }
    }

    private GameObject platform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            airborne = false;
            doubleJump = false;
            platform = collision.gameObject.GetComponentInParent<MovingPlatform>()?.gameObject;
        }
    }

    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.platform = null;
        }
    }


    private bool Graced()
    {
        return gracePeriod > 0;
    }

    public int comebackAt = 4;
    int comebackScore;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grace"))
        {
            gracePeriod = 1f;
            
            GameManager.Instance.AddScore();
            GameManager.Instance.SetMultiplier();
            comebackScore++;
            if (comebackScore >= comebackAt)
            {
                comebackScore = 0;
                this.ReverseHit();
            }
        }

        if (other.gameObject.CompareTag("Harmful") && !Graced())
        {
            this.TakeHit();
            comebackScore = 0;
            GameManager.Instance.ResetMultiplier();
            gracePeriod = 1f;
        }

        if (other.gameObject.CompareTag("Lose"))
        {
            LoseTheGame();
            GameManager.Instance.PlayerDeath();
        }

        if (other.gameObject.CompareTag("PlayerPlatform"))
        {
            airborne = false;
            doubleJump = false;
        }
    }

    private void LoseTheGame()
    {
        this.transform.position = new Vector3(0, 0.8f, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Harmful") || other.gameObject.CompareTag("Grace"))
        {
            other.gameObject.GetComponentInParent<Wall>().Break();
        }
    }

    void TakeHit()
    {
        GameManager.Instance.ShowHit();
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);
    }
    void ReverseHit()
    {
        GameManager.Instance.ShowHitReversion();
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Mathf.Min(this.transform.position.z + 1, 0));
    }
}
