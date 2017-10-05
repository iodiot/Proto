using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Proto.Actors;

namespace Proto
{
  public class Core : Component
  {
    public readonly Game1 Game;
    public readonly GraphicsDevice GraphicsDevice;
    public readonly SpriteBatch SpriteBatch;
    public readonly ContentManager Content;

    public Texture2D OneWhitePixel;

    public int Ticks { get; private set; }

    private static Core instance;
    private static bool initalized;

    private readonly List<Actor> actors, actorsToAdd, actorsToRemove;

    private Core(Game1 game, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, ContentManager content)
    {
      Game = game;
      GraphicsDevice = graphicsDevice;
      SpriteBatch = spriteBatch;
      Content = content;

      actors = new List<Actor>();
      actorsToAdd = new List<Actor>();
      actorsToRemove = new List<Actor>();
    }

    public static Core Instance
    {
      get
      {
        return instance;
      }
    }

    public static void Initialize(Game1 game, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, ContentManager content)
    {
      if (!initalized)
      {
        instance = new Core(game, graphicsDevice, spriteBatch, content);
        initalized = true;
      }
    }

    public override void Load()
    {
      OneWhitePixel = Content.Load<Texture2D>("one");

      AddActor(new PlayerActor(10, 10));

      base.Load();
    }

    public override void Update()
    {
      ++Ticks;

      foreach (var actor in actorsToRemove)
        if (actors.Contains(actor))
          actors.Remove(actor);
      actorsToRemove.Clear();

      foreach (var actor in actorsToAdd)
        actors.Add(actor);
      actorsToAdd.Clear();

      foreach (var actor in actors)
        actor.Update();

      base.Update();
    }

    public override void Draw()
    {
      foreach (var actor in actors)
        actor.Draw();

      base.Draw();
    }

    public void AddActor(Actor actor)
    {
      actorsToAdd.Add(actor);
    }

    public void RemoveActor(Actor actor)
    {
      actorsToRemove.Add(actor);
    }

    public void DrawRectangle(Rectangle rect, Color tint)
    {
      SpriteBatch.Draw(
        OneWhitePixel,
        new Vector2(rect.X, rect.Y),
        rect,
        tint
      );
    }
  }
}
