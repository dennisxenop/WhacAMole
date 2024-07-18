namespace Dennis.Hole
{
    public interface IHoleView
    {
        public bool IsActive { get; }

        public void SetActiveState(bool isActive);
    }
}