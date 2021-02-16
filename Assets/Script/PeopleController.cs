using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleController : MonoBehaviour
{
    Animator animator;
     
    public GameObject camera;
    public GameObject bulletPrefab;
    public GameObject bulletParent;

    float orgDir;
    float orgZ;
    float speed = 0.06f;

    int isAtteck;
    bool getBullet = false;
    Vector3 NowLookAt;

    float startTime;
    bool countStart=true;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        orgZ = this.transform.position.y;
        isAtteck = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        NowLookAt = (this.transform.position - camera.transform.position).normalized;
        SetRun();
        setDir();
        Attack();
    }

    void SetRun()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isRun", true);
            NowLookAt = (this.transform.position - camera.transform.position).normalized*speed;
            this.transform.position += new Vector3(NowLookAt.x, 0, NowLookAt.z);
            print(NowLookAt);
        }
        else
            animator.SetBool("isRun", false);
    }

    void pause()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("pase");
            animator.speed = 0;
        }

    }
    void Attack()
    {
        if (Input.GetKey(KeyCode.R) && isAtteck < 100 && !getBullet) //拉弓
        {
            animator.SetBool("isAttack", true);
            countTime(countStart);
            countStart = false;
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.speed = 1;
                countStart = true;
                GameObject bullet = GameObject.Instantiate(bulletPrefab);
                bullet.transform.rotation = bulletParent.transform.rotation;

                getBullet = true;
                bullet.transform.position = new Vector3(this.transform.position.x + NowLookAt.x * 2, this.transform.position.y + 1, this.transform.position.z + NowLookAt.z * 2);
                bullet.GetComponent<Rigidbody>().AddForce(NowLookAt.x * 2500, 0, NowLookAt.z * 2500);
                isAtteck++;
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            
            animator.SetBool("isAttack", false);
            getBullet = false;
        }
        else
        {
            animator.SetBool("isAttack", false);
            animator.speed = 1;
            countStart = true;
        }
    }

  

    void countTime(bool countStart)
    {
        float nowTime = Time.time;
        if (countStart)
             startTime = Time.time;
        float TimeSpan = nowTime - startTime;

        if (TimeSpan > 0.433f)
            animator.speed = 0;
    }


    void setDir()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 1, 0), 0.6f);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, -1, 0), 0.6f);
        }

    }

}
