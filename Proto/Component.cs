﻿using System;

namespace Proto
{
  public abstract class Component
  {
    #region Shortcuts
    protected Core core { get { return Core.Instance; } }
    protected int ticks { get { return core.Ticks; } }
    #endregion

    public virtual void Load()
    {
    }

    public virtual void Unload()
    {
    }

    public virtual void Update()
    { 
    }

    public virtual void Draw()
    {
    }
  }
}

