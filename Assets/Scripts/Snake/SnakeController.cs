using Inputs;
using Modules;
using Zenject;

namespace SnakeGame
{
    public class SnakeController: ITickable
    {
        private readonly ISnakeInput input;
        private readonly ISnake snake;

        public SnakeController(ISnakeInput input, ISnake snake)
        {
            this.input = input;
            this.snake = snake;
        }

        public void Tick()
        {
            var direction = input.GetDirection();
            
            if (direction != SnakeDirection.NONE)
            {
                snake.Turn(direction);
            }
        }
    }
}