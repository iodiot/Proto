using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using C3.XNA;

namespace Proto.Actors
{
  public class PlayerActor : Actor
  {
    public int X, Y, Width, Height;

    public PlayerActor(int x, int y)
    {
      X = x;
      Y = y;

      Width = Height = 50;
    }

    public override void Update()
    {
      var v = GetDesiredVelocityFromInput();

      X += (int)v.X;
      Y += (int)v.Y;

    }

    public override void Draw()
    {
      core.DrawRectangle(new Rectangle(X, Y, Width, Height), Color.Yellow);

      core.SpriteBatch.DrawLine(0f, 0f, 500f, 500f, Color.Red, 5f);

      base.Draw();
    }

    private Vector2 GetDesiredVelocityFromInput()
    {
      Vector2 desiredVelocity = new Vector2();

      TouchCollection touchCollection = TouchPanel.GetState();

      if (touchCollection.Count > 0)
      {
        desiredVelocity.X = touchCollection[0].Position.X - this.X;
        desiredVelocity.Y = touchCollection[0].Position.Y - this.Y;

        if (desiredVelocity.X != 0 || desiredVelocity.Y != 0)
        {
          desiredVelocity.Normalize();
          const float desiredSpeed = 200;
          desiredVelocity *= desiredSpeed;
        }
      }

      return desiredVelocity;
    }
  }
}
