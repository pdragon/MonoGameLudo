using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop
{
    public class Input
    {
        private static MouseState _prevState;
        private static MouseState _currentState;
        private static char? _lastCharPressed = null;
        private static Keys? _lastKeyPressed = null;

        public Input(GameWindow window)
        {
            window.TextInput += OnTextInput;
        }

        private void OnTextInput(object sender, TextInputEventArgs e)
        {
            _lastCharPressed = e.Character;
            _lastKeyPressed = e.Key;
        }

        public static void Update()
        {
            _prevState = _currentState;
            _currentState = Mouse.GetState();
        }

        public static bool IsLeftButtonReleased()
        {
            return _prevState.LeftButton == ButtonState.Pressed &&
                   _currentState.LeftButton == ButtonState.Released;
        }

        // Проверка отпускания ПРАВОЙ кнопки
        public static bool IsRightButtonReleased()
        {
            return _prevState.RightButton == ButtonState.Pressed &&
                   _currentState.RightButton == ButtonState.Released;
        }

        public static bool IsMiddleButtonReleased()
        {
            return _prevState.MiddleButton == ButtonState.Pressed &&
                   _currentState.MiddleButton == ButtonState.Released;
        }

        public static Point GetMousePosition() => new Point(_currentState.X, _currentState.Y);
        
        // Пример масштабирования под виртуальное разрешение 1920x1080
        static readonly GraphicsDeviceManager graphics;

        public static Vector2 GetScaledMousePosition()
        {
            MouseState mouseState = Mouse.GetState();
            float scaleX = (float)graphics.PreferredBackBufferWidth / 1920;
            float scaleY = (float)graphics.PreferredBackBufferHeight / 1080;
            return new Vector2(mouseState.X / scaleX, mouseState.Y / scaleY);
        }

        public static char? GetCharPressed()
        {
            return _lastCharPressed;
        }

        public static Keys? GetKeyPressed()
        {
            return _lastKeyPressed;
        }

        // Пример для 2D-камеры
        // Для проекций с камерой или матрицей вида
        // Этот метод учитывает трансформации камеры (https://community.monogame.net/t/solved-how-can-i-get-the-world-coords-of-the-mouse-2d/11263)
        //Matrix viewMatrix = Matrix.CreateLookAt(...);
        //Matrix projectionMatrix = Matrix.CreateOrthographicOffCenter(...);

        //Vector2 GetWorldMousePosition()
        //{
        //    MouseState mouseState = Mouse.GetState();
        //    Vector3 nearPoint = GraphicsDevice.Viewport.Unproject(
        //        new Vector3(mouseState.X, mouseState.Y, 0),
        //        projectionMatrix,
        //        viewMatrix,
        //        Matrix.Identity
        //    );
        //    return new Vector2(nearPoint.X, nearPoint.Y);
        //}
    }
}
