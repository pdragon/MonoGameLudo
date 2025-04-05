using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLudo.Core;
using MonoLudo.Shared.Scenes.Game;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace MonoLudo.Shared
{
    public class Main 
    {
        private GraphicsDeviceManager _graphics;
        private static ContentManager Content;
        public static SpriteBatch SpriteBatch { get; private set; }
        public static string AssetDirectory { get; private set; }
        public static List<Config.ColorIndex> PlayersAvailable = new();
        public static Texture2D PixelTexture { get; private set; }
        private Board GameBoard = new Board();
        private GraphicsDevice GraphicsDevice;
        public GameWindow Window { get; private set; }
        public GraphicsDeviceManager Graphics { get; private set; }
        private bool _matrixNeedsUpdate = true;


        private Vector2 _virtualResolution = new Vector2(1920, 1080); // Базовое разрешение
        public static Matrix ScaleMatrix { get; private set; } // Матрица масштабирования
        private Viewport _viewport; // Активная область отрисовки
        private bool _needsMatrixUpdate = true;


        public enum GameStage : short
        {
            MainMenu,
            MainLoop,
            GameOver
        }
        private static GameStage Stage = GameStage.MainLoop;
        public Main(GameWindow window, GraphicsDevice graphics, GraphicsDeviceManager gdm, ContentManager content)
        {
            Content = content;
            //UpdateScaleMatrix();
            Window = window;
            Window.ClientSizeChanged += (s, e) => _needsMatrixUpdate = true;
            GraphicsDevice = graphics;
            //gdm.DeviceCreated += OnDeviceCreated;
            Window.ClientSizeChanged += OnWindowSizeChanged;
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.GraphicsDevice = graphicsDevice;
            SpriteBatch = new SpriteBatch(graphicsDevice);

            PixelTexture = new Texture2D(graphicsDevice, 1, 1);
            PixelTexture.SetData(new[] { Color.White }); // Заливаем белым цветом

            // TODO: use this.Content to load your game content here
            var init = new Init();
            for (var colorIndex = 0; colorIndex < Config.PlayerColors.Length; colorIndex++)
            {
                Config.CircleTexture[colorIndex] = init.LoadTokenTexture(graphicsDevice, Config.PlayerColors[colorIndex]);
            }
            Config.BackgroundTexture = content.Load<Texture2D>("background"); // Имя файла без расширения
            init.LoadSettings();
        }

        public void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            
        }

        public void Draw(GraphicsDevice graphicsDevice, GameTime gameTime, GameWindow window)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here

            if (SpriteBatch != null)
            {
                SpriteBatch.Begin();
                SpriteBatch.Draw(
                    Config.BackgroundTexture,
                    new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height), // Растянуть на весь экран
                    Color.White
                );
                
                SpriteBatch.End();
                SpriteBatch.Begin(transformMatrix: ScaleMatrix);
                GameBoard.Draw();

                //Vector2 debugPos = GetVirtualMousePosition();
                //SpriteBatch.DrawString(_font, $"X: {debugPos.X}, Y: {debugPos.Y}", Vector2.Zero, Color.White);
                SpriteBatch.End();
            }
        }

        public static Vector2 ToVirtual(Vector2 screenPos) => Vector2.Transform(screenPos, Matrix.Invert(ScaleMatrix));
        public static Vector2 ToScreen(Vector2 virtualPos) => Vector2.Transform(virtualPos, ScaleMatrix);

        //private void OnDeviceCreated(object sender, EventArgs e) => UpdateScaleMatrix();
        private void OnWindowSizeChanged(object sender, EventArgs e) => UpdateScaleMatrix();

        //public void UpdateScaleMatrix()
        //{
        //    var screenWidth = Window.ClientBounds.Width;
        //    var screenHeight = Window.ClientBounds.Height;

        //    // Рассчитайте соотношение сторон
        //    var scaleX = screenWidth / _virtualResolution.X;
        //    var scaleY = screenHeight / _virtualResolution.Y;
        //    var scale = Math.Min(scaleX, scaleY); // Сохраняем пропорции

        //    // Центрируем доску
        //    var offsetX = (screenWidth - _virtualResolution.X * scale) / 2;
        //    var offsetY = (screenHeight - _virtualResolution.Y * scale) / 2;

        //    // Создаём матрицу преобразования
        //    ScaleMatrix = Matrix.CreateScale(scale, scale, 1) *
        //                   Matrix.CreateTranslation(offsetX, offsetY, 0);

        //    // Обновляем вьюпорт для камеры
        //    _viewport = new Viewport(
        //        (int)offsetX,
        //        (int)offsetY,
        //        (int)(_virtualResolution.X * scale),
        //        (int)(_virtualResolution.Y * scale)
        //    );
        //    GraphicsDevice.Viewport = _viewport;
        //}
        //public void UpdateScaleMatrix()
        //{
        //    var screenWidth = Window.ClientBounds.Width;
        //    var screenHeight = Window.ClientBounds.Height;

        //    //float scale = Math.Min(
        //    //    screenWidth / _virtualResolution.X,
        //    //    screenHeight / _virtualResolution.Y
        //    //);

        //    //ScaleMatrix = Matrix.CreateScale(scale, scale, 1f) *
        //    //               Matrix.CreateTranslation(
        //    //                   (screenWidth - _virtualResolution.X * scale) / 2,
        //    //                   (screenHeight - _virtualResolution.Y * scale) / 2,
        //    //                   0);

        //    // Рассчитываем масштаб без сохранения пропорций
        //    float scaleX = screenWidth / _virtualResolution.X;
        //    float scaleY = screenHeight / _virtualResolution.Y;

        //    ScaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1f);
        //    _matrixNeedsUpdate = false;
        //}

        public void UpdateScaleMatrix()
        {
            int screenWidth = Window.ClientBounds.Width;
            int screenHeight = Window.ClientBounds.Height;

            // Отдельные масштабы для X и Y (вызовет искажение!)
            float scaleX = screenWidth / _virtualResolution.X;
            float scaleY = screenHeight / _virtualResolution.Y;

            ScaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1);

            //_needsMatrixUpdate = false;
        }

        public static void Text(string text, Vector2 position,  Color color)
        {
            SpriteBatch.DrawString(
            Content.Load<SpriteFont>("Fonts/Shafarik-Regular"), // Путь без .spritefont
            text,
            position,
            Board.Fade(color, 5)
        );
        }
    }
}
