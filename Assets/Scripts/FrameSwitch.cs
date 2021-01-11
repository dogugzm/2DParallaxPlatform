using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameSwitch : MonoBehaviour
{
    public GameObject frameBefore;
    public GameObject frameAfter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (frameBefore.activeSelf)
            {
                Debug.Log("aktif1");

                frameAfter.SetActive(true);
                frameBefore.SetActive(false);
            }
            else if (!frameBefore.activeSelf)
            {
                Debug.Log("aktif2");

                frameBefore.SetActive(true);
                frameAfter.SetActive(false);

            }
        }
        
    }

    


