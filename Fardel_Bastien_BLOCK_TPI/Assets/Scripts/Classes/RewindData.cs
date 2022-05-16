using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RewindData
{
    public Vector2 player_position;
    public bool is_flipped;
    public bool is_grounded;
    public float player_speed;
}
