using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames;

[CreateAssetMenu(fileName = "TargetsData", menuName = "TargetsData", order = 0)]
public class TargetsData : ScriptableObject
{
  [SerializeField] private string id;
  [SerializeField] private int maxHealth;
  [SerializeField] private TargetsType type;
  [SerializeField] private string audioBulletHit;
  [SerializeField] private ParticleSystem particleBulletHit;


  public string ID
  {
    get => id;
  }

  public int MaxHealth
  {
    get => maxHealth;
    set => maxHealth = value;
  }

  public TargetsType Type
  {
    get => type;
  }
}




