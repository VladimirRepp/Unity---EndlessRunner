using System;
using UnityEngine;

public abstract class ObserverLevelConfig : MonoBehaviour
{
   public abstract void Startup(LevelConfig setting);
   public abstract void UpadteSettings(LevelConfig setting);
}