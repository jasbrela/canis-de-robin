namespace Interfaces
{
    public interface IInteractable
    {
        void OnEnterArea();
        void OnInteract();
        void OnQuitArea();
        
        // https://github.com/jasbrela/fishing-system/blob/main/Assets/Scripts/FishingTrigger.cs
    }
}
