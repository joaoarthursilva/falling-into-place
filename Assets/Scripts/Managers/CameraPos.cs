using UnityEngine;

namespace Managers
{
    public class CameraPos : MonoBehaviour
    {
        [SerializeField] private Transform player;

        // Update is called once per frame
        private void LateUpdate()
        {
            var position = player.position;
            transform.position = new Vector3(position.x, position.y, -10);
        }
    }
}
