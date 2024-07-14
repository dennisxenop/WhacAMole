internal interface IHoleView
{
    public bool IsActive { get; }
    public void SetActiveState(bool isActive);
}