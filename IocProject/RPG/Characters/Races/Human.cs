using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocProject.RPG.Characters.Races
{
  public class Human : IHuman
  {
    public int StrengthModifier => 2;
    public int DexterityModifier => 2;
    public int IntelligenceModifier => 2;
  }
}
