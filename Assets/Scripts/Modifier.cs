using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
   public float modifierNumber;
   public ModifierType modifierType;
}

public enum ModifierType { Add, Multiply};
