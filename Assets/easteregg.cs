using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class easteregg : MonoBehaviour
{
    public GameObject Box;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Finish"))
        {
            Destroy(Box);
        }
    }
}
