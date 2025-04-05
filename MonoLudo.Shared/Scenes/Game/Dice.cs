using System;
//using System.Numerics;
using MonoLudo.Core;
using Microsoft.Xna.Framework;

namespace MonoLudo.Shared.Scenes.Game;

public class Dice
{
    private Rectangle Bounds = Config.GetDiceBounds();
    private static short Value;
    public bool IsRolling { get; private set; }
    private double RollTime;

    public Dice()
    {
        Roll();
    }

    public static void Cheat(short value)
    {
        Value = value;
    }

    public void SetBounds(Rectangle bounds)
    {
        Bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
    }

    public static short GetValue()
    {
        return Value;
    }

    //public Rectangle GetBounds(Rectangle bounds) => Bounds;

    public void Roll()
    {
        //InitRandom();
        IsRolling = true;
        RollTime = 0.4f; // Длительность анимации в секундах

        // Генерация финального значения
        //Value = (rand() % 6) + 1;
        Random rnd = new Random((int)DateTime.Now.Ticks);
        Value = (short)rnd.Next(1, 7);
        //Value = 6;
    }

    public void Draw()
    {
   //     // Анимация вращения
   //     if (IsRolling)
   //     {
   //         //RollTime -= Raylib.GetFrameTime();
   //         RollTime -= Config.DeltaTime;//Time.GetFrameTime();

			//if (RollTime <= 0)
   //         {
   //             IsRolling = false;
   //         }

   //         // Мигание во время броска
   //         if ((int)(RollTime * 10) % 2 == 0)
   //         {
   //             //Image.DrawRectangleRec(Bounds, ColorIndex.Maroon);
   //             return;
   //         }
   //     }

   //     // Отрисовка кубика
   //     Graphics.DrawRectangleRounded(Bounds, 0.2f, 6, Color.Gray);

   //     // Точки на кубике
   //     const float dotSize = 8.0f;
   //     Color dotColor = Color.Black;
   //     Vector2 Center = new Vector2(
   //     Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height / 2);

   //     switch (Value)
   //     {
   //         case 1:
   //             Graphics.DrawCircleV(Center, dotSize, dotColor);
   //             break;

   //         case 2:
   //             Graphics.DrawCircleV(new Vector2(Center.X - 15, Center.Y - 15), dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X + 15, Center.Y + 15), dotSize, dotColor);
   //             break;

   //         case 3:
   //             Graphics.DrawCircleV(new Vector2(Center.X - 15, Center.Y - 15), dotSize, dotColor);
   //             Graphics.DrawCircleV(Center, dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X + 15, Center.Y + 15), dotSize, dotColor);
   //             break;

   //         case 4:
   //             Graphics.DrawCircleV(new Vector2(Center.X - 15, Center.Y - 15), dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X + 15, Center.Y - 15), dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X - 15, Center.Y + 15), dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X + 15, Center.Y + 15), dotSize, dotColor);
   //             break;

   //         case 5:
   //             Graphics.DrawCircleV(new Vector2(Center.X - 15, Center.Y - 15), dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X + 15, Center.Y - 15), dotSize, dotColor);
   //             Graphics.DrawCircleV(Center, dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X - 15, Center.Y + 15), dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X + 15, Center.Y + 15), dotSize, dotColor);
   //             break;

   //         case 6:
   //             Graphics.DrawCircleV(new Vector2(Center.X - 15, Center.Y - 25), dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X - 15, Center.Y), dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X - 15, Center.Y + 25), dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X + 15, Center.Y - 25), dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X + 15, Center.Y), dotSize, dotColor);
   //             Graphics.DrawCircleV(new Vector2(Center.X + 15, Center.Y + 25), dotSize, dotColor);
   //             break;
   //         default:
   //             //Raylib.DrawText("a",1,1, 8, new Color());
   //             Console.WriteLine("Ошибка значения кубика!");
   //             break;
   //     }
    }
}