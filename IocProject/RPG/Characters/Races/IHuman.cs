using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocProject.RPG.Characters.Races
{
  public interface IHuman
  {
    int StrengthModifier { get; }
    int DexterityModifier { get; }
    int IntelligenceModifier { get; }
  }
}
