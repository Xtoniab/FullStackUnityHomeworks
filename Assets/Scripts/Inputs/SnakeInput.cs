using Modules;
using UnityEngine;
using Zenject;

namespace Inputs
{
    public class SnakeInput : ISnakeInput
    {
        public SnakeDirection GetDirection()
        {
            if (Input.GetKey(KeyCode.W)) return SnakeDirection.UP;
            if (Input.GetKey(KeyCode.A)) return SnakeDirection.LEFT;
            if (Input.GetKey(KeyCode.S)) return SnakeDirection.DOWN;
            if (Input.GetKey(KeyCode.D)) return SnakeDirection.RIGHT;
            return SnakeDirection.NONE;
        }
    }
}