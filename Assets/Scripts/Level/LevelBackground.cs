using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField] public float startPositionY;
        [SerializeField] public float endPositionY;
        [SerializeField] public float movingSpeedY;
        
        private float positionX;
        private float positionZ;
        
        private void Awake()
        {
            var position = this.transform.position;
            this.positionX = position.x;
            this.positionZ = position.z;
        }

        private void FixedUpdate()
        {
            if (this.transform.position.y <= this.endPositionY)
            {
                this.transform.position = new Vector3(
                    this.positionX,
                    this.startPositionY,
                    this.positionZ
                );
            }

            this.transform.position -= new Vector3(
                this.positionX,
                this.movingSpeedY * Time.fixedDeltaTime,
                this.positionZ
            );
        }
    }
}