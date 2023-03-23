using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace Managers
{
    public class ParameterManager : MonoBehaviour
    {
        private GameObject[] _objects;
        [SerializeField] private new string tag;
        [SerializeField] private float sizeToSet;

        private void Awake()
        {
            _objects = GameObject.FindGameObjectsWithTag(tag);
        }

        public void ChangeScale()
        {
            foreach (var go in _objects)
            {
                go.transform.localScale = new Vector3(sizeToSet, sizeToSet, sizeToSet);
            }
        }
    }
}