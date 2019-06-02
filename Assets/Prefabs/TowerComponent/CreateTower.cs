using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{
    [SerializeField]
    Gradient floorGradient;
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject floor;
    [SerializeField]
    GameObject building;
    [SerializeField, Range(0,1)]
    float spaceDelta;
    [SerializeField, Range(0, 90)]
    int anglesDelta;
    [SerializeField,Range(1,30)]
    int height;
    int weight = 11;
    //[SerializeField]
    //int[,] towerForm;
    //int[,] towerForm =    {
    //    {1,1,1},
    //    {1,1,1},
    //    {1,1,1}
    //};
    int[ , ] towerForm =    {
    { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 },
    { 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0 },
    { 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0 },
    { 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
    { 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
    { 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
    { 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0 },
    { 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0 },
    { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 }
    };
    //int[,] towerForm =    {
    //{ 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 },
    //{ 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
    //{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
    //{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
    //{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    //{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    //{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    //{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
    //{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
    //{ 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
    //{ 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 }
    //};
    //int[,] towerForm =    {
    //{ 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
    //{ 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
    //{ 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
    //{ 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
    //{ 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
    //{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    //{ 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
    //{ 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
    //{ 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
    //{ 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
    //{ 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1 }
    //};
    float positionDelta;
    Vector3 euler;
    Quaternion floorQuaternion;
    void Start()
    {
        positionDelta = weight / 2 * prefab.transform.localScale.x;
        BuildTower();
        euler = floor.transform.eulerAngles;
        floorQuaternion = floor.transform.rotation;
    }


    void Update()
    {
    }
    void BuildTower()
    {
        GameObject newBuilding = Instantiate(building, transform.position, Quaternion.identity);
        for (int k = 0; k < height; k++)
        {
            GameObject newFloor = CreateFloor(k, newBuilding);
            for (int i = 0; i < weight; i++)
            {
                for (int j = 0; j < weight; j++)
                {
                    if (towerForm[i, j] == 1)
                    {
                        Vector3 newPos = new Vector3();
                        newPos.y = k * prefab.transform.localScale.y * 1;
                        if (j - positionDelta < 0)
                            newPos.x = j - positionDelta - spaceDelta * Mathf.Abs(j - positionDelta);
                        else
                            newPos.x = j - positionDelta + spaceDelta * Mathf.Abs(j - positionDelta);
                        if (i - positionDelta < 0)
                            newPos.z = i - positionDelta - spaceDelta * Mathf.Abs(i - positionDelta);
                        else
                            newPos.z = i - positionDelta + spaceDelta * Mathf.Abs(i - positionDelta);
                        CreateBlock(newPos, newFloor);
                    }
                }
            }
            RotateFloor(newFloor);

        }
        Destroy(gameObject);
    }
    GameObject CreateFloor(int y, GameObject newBuilding)
    {
        Vector3 floorPosition = transform.position;
        floorPosition.y = y;
        GameObject ret = Instantiate(floor, floorPosition, Quaternion.identity, newBuilding.transform);
        return (ret);

    }
    void CreateBlock(Vector3 newPosition, GameObject newFloor)
    {
        GameObject newBlock = Instantiate(prefab, newPosition + transform.position, Quaternion.identity, newFloor.transform);
        newBlock.GetComponent<MeshRenderer>().material.color = floorGradient.Evaluate((newPosition.y / 20) % 1);
    }
    void RotateFloor(GameObject newFloor)
    {
        newFloor.transform.eulerAngles = euler;
        euler.y += anglesDelta;
    }
}
