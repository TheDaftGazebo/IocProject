using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocProject.RPG.Characters.Classes
{
  public class Fighter : IFighter
  {
    public int Level { get; set; } = 1;
    public int Strength => 10;
    public int Dexterity => 8;
    public int Intelligence => 6;
  }
}
