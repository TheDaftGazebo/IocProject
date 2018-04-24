using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocProject.RPG.Characters.Classes
{
  public interface IFighter
  {
    int Level { get; set; }
    int Strength { get; }
    int Dexterity { get; }
    int Intelligence { get; }
  }
}
