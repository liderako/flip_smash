using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{
    private Gradient _floorGradient;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject _building;
    [SerializeField, Range(0,1)] private float _spaceDelta;
    [SerializeField, Range(0, 90)]private int _anglesDelta;
    [SerializeField,Range(1,30)] private int _height;
    
    private int _weight = 11;
    
    private float _positionDelta;
    private Vector3 _euler;
    //private Quaternion _floorQuaternion;
    

    int[,] towerForm =    {
    { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 },
    { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
    { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
    { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
    { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
    { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
    { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 }
    };
    void Start()
    {
        _floorGradient = GameManager.Gm.GetGradientForCurrentLevel();
        _positionDelta = _weight / 2 * _prefab.transform.localScale.x;
        _euler = _floor.transform.eulerAngles;
        BuildTower();
    }

    void BuildTower()
    {
        GameObject newBuilding = Instantiate(_building, transform.position, Quaternion.identity);
        for (int k = 0; k < _height; k++)
        {
            GameObject newFloor = CreateFloor(k, newBuilding);
            for (int i = 0; i < _weight; i++)
            {
                for (int j = 0; j < _weight; j++)
                {
                    if (towerForm[i, j] == 1)
                    {
                        Vector3 newPos = new Vector3();
                        newPos.y = k * _prefab.transform.localScale.y * 1;
                        if (j - _positionDelta < 0)
                            newPos.x = j - _positionDelta - _spaceDelta * Mathf.Abs(j - _positionDelta);
                        else
                            newPos.x = j - _positionDelta + _spaceDelta * Mathf.Abs(j - _positionDelta);
                        if (i - _positionDelta < 0)
                            newPos.z = i - _positionDelta - _spaceDelta * Mathf.Abs(i - _positionDelta);
                        else
                            newPos.z = i - _positionDelta + _spaceDelta * Mathf.Abs(i - _positionDelta);
                        CreateBlock(newPos, newFloor);
                    }
                }
            }
            RotateFloor(newFloor);

        }
    }
    GameObject CreateFloor(int y, GameObject newBuilding)
    {
        Vector3 floorPosition = transform.position;
        floorPosition.y = y;
        GameObject ret = Instantiate(_floor, floorPosition, Quaternion.identity, newBuilding.transform);
        return (ret);

    }
    void CreateBlock(Vector3 newPosition, GameObject newFloor)
    {
        GameObject newBlock = Instantiate(_prefab, newPosition + transform.position, Quaternion.identity, newFloor.transform);
        newBlock.GetComponent<MeshRenderer>().material.color = _floorGradient.Evaluate((newPosition.y / 20) % 1);
    }
    void RotateFloor(GameObject newFloor)
    {
        newFloor.transform.eulerAngles = _euler;
        _euler.y += _anglesDelta;
    }
}
