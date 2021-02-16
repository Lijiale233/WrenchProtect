using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    
    public GameObject bear;
    public GameObject parentBullet;
    public GameObject bearPosition;

    Animator bearAnimator;

    float relativePositionX;
    bool isOn=false;
    // Start is called before the first frame update
    void Start()
    {
        bear = GameObject.Find("Bear");
        parentBullet = GameObject.Find("parentBullet");
        bearPosition = GameObject.Find("joint1");

        bearAnimator = bear.GetComponent<Animator>();

        if (parentBullet != null) 
            parentBullet.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

      
    }

    //自动调用 当子弹触屏到熊时自动静止
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Bear")
        {
            arrowOnTarget();
        }
    }

    public void arrowOnTarget()
    {
        bearAnimator.SetBool("isDead", true);
        isOn = true;
        bear.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        Destroy(this.GetComponent<Rigidbody>());

        relativePositionX = this.transform.rotation.x - bear.transform.rotation.x;
    }
}
