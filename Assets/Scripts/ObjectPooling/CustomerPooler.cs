using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPooler : GenericSingleton<CustomerPooler>
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        //public int itemRequire;
        public GameObject objectPrefab;
        
    }
    // List Logic of Object Pooling
    /*  public List<ObjectPoolItem> objectPoolItems;
      private List<GameObject> pooledRequireObjects;
      [SerializeField] LevelData levelDataSO;

      public List<GameObject> PooledRequireObjects { get => pooledRequireObjects; set => pooledRequireObjects = value; }

      private void Start ()
      {
          PooledRequireObjects = new List<GameObject>();
          foreach (ObjectPoolItem poolItem in objectPoolItems)
          {
              for (int i = 0; i < levelDataSO.MaxCustomers; i++)
              {
                  GameObject gameObject = (GameObject)Instantiate(poolItem.objectPrefab);
                  gameObject.SetActive(false);
                  gameObject.GetComponent<CustomerManager>().customerId = i;
                  PooledRequireObjects.Add(gameObject);

              }
          }
      }

      public GameObject GetPooledObject()
      {
          for (int i = 0; i < PooledRequireObjects.Count; i++)
          {
              if (!PooledRequireObjects[i].activeInHierarchy)
              {
                  return PooledRequireObjects[i];
              }

          }
          return null;
      }*/


    // Queue Logic of Object pooling.
    public List<ObjectPoolItem> objectPoolItems;
    private Queue <GameObject> pooledRequireObjects;
    [SerializeField] LevelData levelDataSO;

    public Queue<GameObject> PooledRequireObjects { get => pooledRequireObjects; set => pooledRequireObjects = value; }

    private void Start()
    {
        PooledRequireObjects = new Queue<GameObject>();
        foreach (ObjectPoolItem poolItem in objectPoolItems)
        {
            for (int i = 0; i < levelDataSO.MaxCustomers; i++)
            {
                GameObject gameObject = (GameObject)Instantiate(poolItem.objectPrefab);
                gameObject.SetActive(false);
                gameObject.GetComponent<CustomerManager>().customerId = i;
                PooledRequireObjects.Enqueue(gameObject);

            }
        }
    }

    public GameObject GetPooledObject()
    {
        return !PooledRequireObjects.Peek().activeInHierarchy ? PooledRequireObjects.Dequeue() : null;
    }

    public void SetPooledObjectInPool(GameObject gameObject)
    {
        PooledRequireObjects.Enqueue(gameObject);
    }

}
