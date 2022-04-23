using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolv1 : MonoBehaviour
{
    private Dictionary<string,Queue<GameObject>> objectPool = new Dictionary<string,Queue<GameObject>>();

    public GameObject GetObject(GameObject gameObject)
    {
        if(objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList)){
            if(objectList.Count == 0) {
                return CreateNewObject(gameObject);
            }
            else
            {
                GameObject _Object = objectList.Dequeue();
                _Object.SetActive(true);
                return _Object;
            }
        }
        else
        {
            return CreateNewObject(gameObject);
        }
        
    }

    private GameObject CreateNewObject(GameObject gameObject)
    {
        GameObject newObject = Instantiate(gameObject);
        newObject.name = gameObject.name;
        return newObject;
    }

    public void ReturnGameObject(GameObject gameObject)
    {
        if(objectPool.TryGetValue((gameObject.name), out Queue<GameObject> objectList))
        {
            objectList.Enqueue(gameObject);
        }
        else
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObject);
            objectPool.Add(gameObject.name, newObjectQueue);
        }
        gameObject.SetActive(false);    
    }

}
