using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLudo.Shared;
using MonoLudo.Core;
using static MonoLudo.Core.Config;
using System;
using Microsoft.Xna.Framework.Content;

namespace Desktop
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch { get; private set; }
        private float _targetAspectRatio = 1.0f; // 1:1 (квадрат)
        public static GameTime GameTime = new GameTime();
        //public static Texture2D PixelTexture { get; private set; }
        private MonoLudo.Shared.Main SharedMain;
        private Point _lastWindowPosition;
        private bool FirstFrameWithWindow = true;
        private bool _isResizing = false;
        private bool _matrixNeedsUpdate = true;
        private SpriteFont _font;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            SharedMain = new MonoLudo.Shared.Main(Window, GraphicsDevice, _graphics, Content);
            //Window.ClientSizeChanged += OnWindowResized;
            Window.ClientSizeChanged += (s, e) =>
            {
                // Минимальный размер окна
                if (Window.ClientBounds.Width < 800 || Window.ClientBounds.Height < 600)
                {
                    _graphics.PreferredBackBufferWidth = 800;
                    _graphics.PreferredBackBufferHeight = 600;
                    _graphics.ApplyChanges();
                    SharedMain.UpdateScaleMatrix();
                }
            };
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            new Input(Window);
        }

        protected override void LoadContent()
        {
            //SharedMain.UpdateScaleMatrix();
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            //sharedMain.Lo
            // TODO: use this.Content to load your game content here
            SharedMain.LoadContent(GraphicsDevice, Content);
            _font = Content.Load<SpriteFont>("Shafarik-Regular"); // Имя файла .spritefont без расширения
        }

        protected override void Update(GameTime gameTime)
        {
            if (Window.Position != _lastWindowPosition)
            {
                _matrixNeedsUpdate = true;
                _lastWindowPosition = Window.Position;
            }
            Config.DeltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
            Input.Update();
            SharedMain.Update(gameTime);
            if (FirstFrameWithWindow && GraphicsDevice != null)
            {
                FirstFrameWithWindow = false;
                SharedMain.UpdateScaleMatrix();
            }
        }

        private void OnWindowResized(object sender, EventArgs e)
        {
            // Фиксируем соотношение сторон (например, 16:9)
            float targetAspectRatio = 16f / 9f;
            int newWidth = Window.ClientBounds.Width;
            int newHeight = (int)(newWidth / targetAspectRatio);

            // Применяем новые размеры
            _graphics.PreferredBackBufferWidth = newWidth;
            _graphics.PreferredBackBufferHeight = newHeight;
            _graphics.ApplyChanges();
        }

        //private void OnWindowResized(object sender, EventArgs e)
        //{
        //    if (_isResizing) return;
        //    _isResizing = true;

        //    // Текущие размеры окна
        //    int newWidth = Window.ClientBounds.Width;
        //    int newHeight = Window.ClientBounds.Height;

        //    // Вычисляем целевую высоту на основе ширины
        //    int targetHeight = (int)(newWidth / _targetAspectRatio);

        //    // Если текущая высота не совпадает с целевой — корректируем
        //    if (newHeight != targetHeight)
        //    {
        //        _graphics.PreferredBackBufferWidth = newWidth;
        //        _graphics.PreferredBackBufferHeight = targetHeight;
        //        _graphics.ApplyChanges();
        //    }

        //    _isResizing = false;
        //}

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SharedMain.Draw(GraphicsDevice, gameTime, Window);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
