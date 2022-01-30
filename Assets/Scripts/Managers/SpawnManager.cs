using InteractableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
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
            int index = Random.Range(0, possibleDiskContainers.Length);
            SearchableObject container = possibleDiskContainers[index];
        
            container.SetCollectible(disk);
            container.name += " - Disk";
        }
    
        private void RandomizeCellphoneContainer()
        {
            int index = Random.Range(0, possibleCellphoneContainers.Length);
            SearchableObject container = possibleCellphoneContainers[index];
        
            container.SetCollectible(cellphone);
            container.name += " - Cellphone";
        }
    }
}
