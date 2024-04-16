using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLife : MonoBehaviour
{
    // Start is called before the first frame update
    public float waitTime;
    void Start()
    {
        StartCoroutine("Del");
    }

    // Update is called once per frame
   IEnumerator Del(){
    yield return new WaitForSeconds(waitTime);
    Destroy(gameObject);
    }
}
