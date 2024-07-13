using System.Collections.Generic;
using UnityEngine;

namespace Dennis.Variables
{
    [CreateAssetMenu(fileName = "HoleListVariable", menuName = "Variables/HoleListVariable")]

    public class HolesListVariable : ScriptableObjectVariable<List<HoleBehaviour>>
    {
        //return false when already added
        public bool AddHole(HoleBehaviour holeBehaviour)
        {
            if (Value.Contains(holeBehaviour)) return false;
            Value.Add(holeBehaviour);
            return true;
        }
    }
}