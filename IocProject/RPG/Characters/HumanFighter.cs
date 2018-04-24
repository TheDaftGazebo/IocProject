using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocProject.RPG.Characters.Classes;
using IocProject.RPG.Characters.Races;

namespace IocProject.RPG.Characters
{
  class HumanFighter : IHumanFighter
  {
    public IHuman HumanRace { get; set; }
    public IFighter FighterClass { get; set; }
    public string Name { get; set; }
    public int Level => FighterClass.Level;
    public int Strength => FighterClass.Strength + HumanRace.StrengthModifier;
    public int Dexterity => FighterClass.Dexterity + HumanRace.DexterityModifier;
    public int Intelligence => FighterClass.Intelligence + HumanRace.IntelligenceModifier;

    public HumanFighter(IHuman human, IFighter fighter, string name)
    {
      HumanRace = human;
      FighterClass = fighter;
      Name = name;
    }
  }
}
