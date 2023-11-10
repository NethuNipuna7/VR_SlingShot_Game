using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonotDestroy : MonoBehaviour
{
    public string objectID;
    // Start is called before the first frame update
    private void Awake()
    {
        objectID = name + transform.position.ToString() + transform.eulerAngles.ToString();
    }
    void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<DonotDestroy>().Length; i++)
        {
            if (Object.FindObjectsOfType<DonotDestroy>()[i] != this)
            {
                if (Object.FindObjectsOfType<DonotDestroy>()[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
