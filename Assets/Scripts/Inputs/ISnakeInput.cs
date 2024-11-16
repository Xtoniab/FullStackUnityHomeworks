using Modules;
using UnityEngine;

namespace Inputs
{
    public interface ISnakeInput
    {
        SnakeDirection GetDirection();
    }
}