using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SimpleBlock _simpleBlock;
    [SerializeField] private BonusBlock _bonusBlock;
    [SerializeField] private DamageBlock _damageBlock;
    [SerializeField] private FinishBlock _finishBlock;
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
            currentObject = BuildSegments(currentObject, _simpleBlock.gameObject);
            currentObject = BuildSegments(currentObject, _damageBlock.gameObject);
            currentObject = BuildSegments(currentObject, _simpleBlock.gameObject);
            currentObject = BuildSegments(currentObject, _bonusBlock.gameObject);
            currentObject = BuildSegments(currentObject, _simpleBlock.gameObject);
            currentObject = BuildSegments(currentObject, _damageBlock.gameObject);
        }

        BuildSegments(currentObject, _finishBlock.gameObject);

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
