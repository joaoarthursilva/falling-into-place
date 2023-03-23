using UnityEngine;

namespace Managers
{
    public class ParameterManager : MonoBehaviour
    {
        private GameObject[] _objects;
        [SerializeField] private float sizeToSet;

        private void Awake()
        {
            _objects = GameObject.FindGameObjectsWithTag("RedSquare");
        }

        public void ChangeScale()
        {
            foreach (var gameobject in _objects)
            {
                gameobject.transform.localScale = new Vector3(sizeToSet, sizeToSet, sizeToSet);
            }
        }
    }
}