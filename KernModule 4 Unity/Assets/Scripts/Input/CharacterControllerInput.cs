using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControllerInput : MonoBehaviour {

    public abstract float Horizontal { get; }
    public abstract float Vertical { get; }
    public abstract float Rotation { get; }
    public abstract bool FireKeyPressed { get; }
    public abstract bool FireKeyHeldDown { get; }
    public abstract bool SecondaryKeyPressed { get; }
    public abstract bool SecondaryKeyHeldDown { get; }
}
