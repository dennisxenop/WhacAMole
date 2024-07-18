using Dennis.Variables;
using UnityEngine;

namespace Dennis.Hole
{
    [CreateAssetMenu(fileName = "HoleListVariable", menuName = "Variables/HoleListVariable")]
    public class HolesListVariable : ListSOVariable<IHole>
    {
    }
}