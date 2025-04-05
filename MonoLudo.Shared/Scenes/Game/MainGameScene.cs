using System.Data;
using MonoLudo.Core;
using System.Collections.Generic;
using System.Collections;
//using MonoLudo.Shared.Actions;
using MonoLudo.Shared.Scenes.Game;
using System.Linq;

namespace MonoLudo.Shared.Scenes
{
    public class MainGameScene
    {
        public static ActionQueue Queue = new ActionQueue();
        Game.Game GameManager;
        Board Board;
        public static bool GameIsOver = false;
        public MainGameScene()
        {
            MainGameScene.GameIsOver = false;
            GameManager = new Game.Game();
            Board = new Board();
            GameManager.SetNextPlayer();
        }

        public bool Update()
        {
            bool gameOver = Game.Game.Players!.All(s => s == null || s.CurrentState == Player.State.AlreadyVictory);
            //InputHandler.ProcessInput(Game);

            Board.Draw();
            Game.Game.Dice.Draw();

            //Timer.Update();
            foreach (var player in Game.Game.Players!)
            {
                if (player != null)
                {
                    player.DrawTokens();
                }
            }
            if (!gameOver && !Queue.Update()) // ���� � ������� ���-�� ���� �� ��������� ������ �������
            {
                GameManager.Draw();
            }
            if (gameOver)
            {
                //Queue.Enqueue(new GameOverAction());
                return true;
            }
            return false;
        }

        //public void Complete()
        //{
        //    if (gameOver)
        //    {
        //        Console.WriteLine("game is over");
        //    }
        //}
    }
}

