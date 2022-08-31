using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // Start is called before the first frame update
    class Object
    {
        public Object(string name, GameObject obj)
        {
            Name = name;
            Instance = obj;
            Instance.name = Name;
        }

        public void ChangePosition(Vector3 position)
        {
            Instance.transform.localPosition = position;
        }

        public void ChangeAngle(float angle)
        {
            Instance.transform.localEulerAngles = new Vector3(0, 0, angle);
        }

        public void ChangeSize(Vector3 size)
        {
            Instance.transform.localScale = size;
        }

        public bool IsName(string n)
        {
            if (Name == n)
                return true;
            return false;
        }

        string Name = "obj";
        Vector3 Size;
        Vector3 Position;
        public GameObject Instance;
    }

    List<Object> ObjectList;

    public static ObjectManager instance = null;
    public GameObject Product;
    public GameObject Ground;

    Vector3 DeltaPosition;

    public Vector3 calibOriginPoint = new Vector3(0, 0, 0);
    public float objScale = 0.01f;


    void Awake()
    {
        instance = this;
    }

    static public ObjectManager GetInstance()
    {
        if (instance == null)
            instance = new ObjectManager();

        return instance;
    }

    void Start()
    {
        ObjectList = new List<Object>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RenderObjects()
    {

    }

    public void UpdateObject(string name, Vector3 size, Vector3 position, float angle)
    {
        // if not exit, creat one
        Debug.Log(name);
        Debug.Log(size);
        Debug.Log(position);

        position += calibOriginPoint;
        Vector3 unitySize = new Vector3(objScale * size.x, objScale * size.y, objScale * size.z);
        Vector3 unityPosition = new Vector3(position.x * objScale, position.y * -objScale, position.z * objScale);

        unityPosition.z = Ground.transform.localPosition.z + unitySize.z/2 + unityPosition.z + System.Math.Abs(Ground.transform.localScale.z/2);


        if (!IsObjectExit(name))
        {
            Debug.Log(name);
            GameObject newObject = Instantiate(Product, unityPosition, Quaternion.identity);
            newObject.transform.SetParent(this.transform);
            newObject.SetActive(true);
            newObject.transform.localScale = unitySize;
            newObject.transform.localEulerAngles = new Vector3(0, 0, angle);
            Object obj = new Object(name, newObject);

            ObjectList.Add(obj);
        }

        for(int i = 0; i < ObjectList.Count; i++)
        {
            if (ObjectList[i].IsName(name))
            {
                ObjectList[i].ChangePosition(unityPosition);
                ObjectList[i].ChangeSize(unitySize);
                ObjectList[i].ChangePosition(unityPosition);
                ObjectList[i].ChangeAngle(angle);
            }
        }
    }

    public void DeleteObject(string name)
    {
        if (name == "all")
        {
            DeleteAllObjects();
            return;
        }

        for (int i = 0; i < ObjectList.Count; i++)
        {
            if (ObjectList[i].IsName(name))
            {
                Destroy(ObjectList[i].Instance);
                ObjectList.RemoveAt(i);
                return;
            }
        }
    }

    public void DeleteAllObjects()
    {
        for (int i = 0; i < ObjectList.Count; i++)
        {
            Destroy(ObjectList[i].Instance);
        }
        ObjectList.Clear();
    }

    bool IsObjectExit(string name)
    {
        foreach (var obj in ObjectList)
        {
            if (obj.IsName(name))
                return true;
        }

        return false;
    }
}
