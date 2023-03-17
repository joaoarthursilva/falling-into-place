using UnityEngine;

namespace Managers
{
    public class ParameterManager : MonoBehaviour
    {
        public GameObject prefab;

        private void Awake()
        {
            prefab.transform.localScale = new Vector3(1,1,1); // arrumar isso
        }
    }
}
