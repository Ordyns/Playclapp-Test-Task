using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameObjectPool<T> where T : MonoBehaviour
{
    [SerializeField] private T prefab;
    [SerializeField] private Transform parent;
    [Space]
    [SerializeField] private int minCapacity = 0;
    [SerializeField] private int maxCapacity = 1;
    [Space]
    [SerializeField] private int objectsCountOnStart;

    private List<T> _pool;

    public GameObjectPool(){
        _pool = new List<T>(minCapacity);

        for (int i = 0; i < objectsCountOnStart; i++)
            CreateNewObject();
    }

    public T Get(){
        T freeObject;
        if(TryGetFreeObject(out freeObject) == false)
            freeObject = CreateNewObject();

        freeObject.gameObject.SetActive(true);
        return freeObject;
    }

    private T CreateNewObject(){
        if(_pool.Count >= maxCapacity)
            throw new System.Exception("Run out of pool capacity. Please increase the maximum capacity.");

        T newObject = MonoBehaviour.Instantiate(prefab, parent);
        newObject.gameObject.SetActive(false);
        _pool.Add(newObject);
        return newObject;
    }

    private bool TryGetFreeObject(out T freeObject){
        foreach (T item in _pool){
            if(item.gameObject.activeInHierarchy == false){
                freeObject = item;
                return true;
            }
        }

        freeObject = null;
        return false;
    }
}
