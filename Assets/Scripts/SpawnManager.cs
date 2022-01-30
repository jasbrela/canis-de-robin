using System;

using InteractableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Cellphone cellphone;
    [SerializeField] private Disk disk;
    [SerializeField] private SearchableObject[] possibleDiskContainers;
    [SerializeField] private SearchableObject[] possibleCellphoneContainers;

    private void Start()
    {
        RandomizeDiskContainer();
        RandomizeCellphoneContainer();
    }

    private void RandomizeDiskContainer()
    {
        int index = Random.Range(0, possibleDiskContainers.Length - 1);
        possibleDiskContainers[index].SetCollectible(disk);
    }
    
    private void RandomizeCellphoneContainer()
    {
        int index = Random.Range(0, possibleDiskContainers.Length - 1);
        possibleCellphoneContainers[index].SetCollectible(cellphone);
    }
}
