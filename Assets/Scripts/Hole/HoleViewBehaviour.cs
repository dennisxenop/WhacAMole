using Dennis.Common;
using Dennis.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace Dennis.Hole
{
    public class HoleViewBehaviour : MonoBehaviour, IHoleView, IClick
    {
        [SerializeField]
        private GameEvent clickEvent;

        private bool isActive;

        public bool IsActive => isActive;

        public void OnClick()
        {
            Assert.IsNotNull(clickEvent, "clickEvent is not found");
            clickEvent.Raise();
            gameObject.SetActive(false);
            isActive = false;
        }

        public void SetActiveState(bool isActive)
        {
            this.isActive = isActive;
            gameObject.SetActive(isActive);
        }
    }
}