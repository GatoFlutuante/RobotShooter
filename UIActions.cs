using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActions : MonoBehaviour
{
    GameObject [] hitFeedback;
    public float speed;
    void Update()
    {
        hitFeedback = GameObject.FindGameObjectsWithTag("Feedback");
        if(hitFeedback != null)
        {
            foreach(GameObject go in hitFeedback)
            {
                go.transform.Translate(0f, speed, 0f * Time.deltaTime);
                Destroy(go, 0.2f);
            }
        }
    }
}
