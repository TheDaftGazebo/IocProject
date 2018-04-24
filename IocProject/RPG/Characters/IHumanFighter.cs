using IocProject.RPG.Characters.Classes;
using IocProject.RPG.Characters.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocProject.RPG.Characters
{
  public interface IHumanFighter
  {
    IHuman HumanRace { get; set; }
    IFighter FighterClass { get; set; }
    string Name { get; set; }
    int Level { get; }
    int Strength { get; }
    int Dexterity { get; }
    int Intelligence { get; }
  }
}
