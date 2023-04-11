using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Block _segment;
    [SerializeField] private Block _block;
    [SerializeField] private Block _finishSegment;
    [SerializeField] private int _towerSize;

    private void Start()
    {
        BuildTower();
    }


    private void BuildTower()
    {
        GameObject currentObject = gameObject;

        for (int i = 0; i < _towerSize; i++)
        {
            currentObject = BuildSegments(currentObject, _segment.gameObject);
            currentObject = BuildSegments(currentObject, _block.gameObject);
        }

        BuildSegments(currentObject, _finishSegment.gameObject);

    }


    private GameObject BuildSegments(GameObject currentSegment, GameObject nextSegment)
    {
        return Instantiate(nextSegment, GetBuildPoint(currentSegment.transform, nextSegment.transform), Quaternion.identity);
    }


    private Vector3 GetBuildPoint(Transform currentSegment, Transform nextSegment)
    {
        return new Vector3(transform.position.x, currentSegment.position.y + currentSegment.localScale.y/2 + nextSegment.localScale.y/2, transform.position.z);
    }

}
