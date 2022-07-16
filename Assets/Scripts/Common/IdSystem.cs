using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StringGameObjectDictionary
{
    


    

}




public class IdSystem : MonoBehaviour
{
    public static IdSystem instance { get; private set; }
    private void Awake()
    {
        if (instance == null) instance = this;

        // Заполнение словаря
        _dictionary = new Dictionary<string, GameObject>(_id.Length);
        for (int i = 0; i < _id.Length; i++) _dictionary.Add(_id[i].id, _id[i].gameObject);

    }



    [System.Serializable]
    private struct StringGameObjectStruct
    {
        public string id;
        public GameObject gameObject;
    }

    private Dictionary<string, GameObject> _dictionary;
    [SerializeField] private StringGameObjectStruct[] _id;
    public Dictionary<string, GameObject> id { get => _dictionary; }


    

}
