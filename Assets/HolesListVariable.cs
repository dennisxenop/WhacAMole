using System.Collections.Generic;

internal class HolesListVariable
{
    private List<HoleBehaviour> holeListBehaviours = new List<HoleBehaviour>();

    //return false when already added
    public bool AddHole(HoleBehaviour holeBehaviour)
    {
        if (holeListBehaviours.Contains(holeBehaviour)) return false;
        holeListBehaviours.Add(holeBehaviour);
        return true;
    }
}