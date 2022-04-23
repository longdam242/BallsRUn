using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private ObjectPoolv1 pool;
    [SerializeField] public GameObject _object;
    public int nums;

    private void Start()
    {
        pool = FindObjectOfType<ObjectPoolv1>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Add")
        {
            GameObject newO = pool.GetObject(_object);
            newO.transform.position = this.gameObject.transform.position + Vector3.one;
            newO.transform.parent = this.gameObject.transform;
            other.gameObject.SetActive(false);

        }if(other.gameObject.tag == "Wall")
        {
            for(int i = 0; i < nums; i++)
            {
                GameObject newO = pool.GetObject(_object);
                newO.transform.position = this.gameObject.transform.position + Vector3.one;
                newO.transform.parent = this.gameObject.transform;
            }
        }
        if(other.gameObject.tag == "Tru")
        {

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newO = pool.GetObject(_object);
            newO.transform.position = this.gameObject.transform.position;
        }
    }

}
