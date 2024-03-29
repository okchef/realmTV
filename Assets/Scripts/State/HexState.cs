﻿using System.Collections.Generic;
using System;

[Serializable]
public class HexState : IReadHexState
{
    public bool visible;

    public String terrain;

    public void UpdateWith(HexState other) {
        this.visible = other.visible;

        if (!String.IsNullOrEmpty(other.terrain)) {
            this.terrain = other.terrain;
        }
    }

    string IReadHexState.GetTerrain() {
        return this.terrain;
    }

    bool IReadHexState.IsVisible() {
        return this.visible;
    }
}
