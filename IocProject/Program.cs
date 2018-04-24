using IocProject.RPG.Characters;
using IocProject.RPG.Characters.Classes;
using IocProject.RPG.Characters.Races;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IocProject
{
  public class Program
  {
    static void Main(string[] args)
    {
      var ioc = new IocContainer();
      ioc.Register<IHuman, Human>();
      ioc.Register<IFighter, Fighter>();
      ioc.Register<IHumanFighter, HumanFighter>();

      var iocSimple = new IocContainerSimple();
      iocSimple.Register<IHuman, Human>();
      iocSimple.Register<IFighter, Fighter>();
      iocSimple.Register<IHumanFighter, HumanFighter>();

      var npcs = new List<IHumanFighter>();
      var numberOfIterations = 1000000;

      GetTimeForAction(() =>
      {
        for (var i = 0; i < numberOfIterations; i++)
        {
          npcs.Add(new HumanFighter(new Human(), new Fighter(), "Joe"));
        }
      });

      GetTimeForAction(() =>
      {
        for (var i = 0; i < numberOfIterations; i++)
        {
          npcs.Add(iocSimple.Resolve<IHumanFighter>());
        }
      });

      GetTimeForAction(() =>
      {
        for (var i = 0; i < numberOfIterations; i++)
        {
          npcs.Add(ioc.Resolve<IHumanFighter>());
        }
      });

      // Phase 2: Create a Dictionary of registered concrete types to construct default values for non-primitive types using recursion to identify parameters to get its own values
      // Phase 3: Create an Ioc object to orchestrate registration and creation
      // Register<T, T>(), Resolve<T>(), Register is abstract/concrete, Resolve is just abstract

      Console.ReadKey();
    }

    private static void GetTimeForAction(Action action)
    {
      var stopwatch = new Stopwatch();
      stopwatch.Start();
      action.Invoke();
      stopwatch.Stop();
      Console.WriteLine(stopwatch.ElapsedMilliseconds);
    }

    public static string ObjectPropertiesToString(object obj)
    {
      var result = string.Empty;
      var properties = obj.GetType().GetProperties();
      foreach (var p in properties)
      {
        var pType = p.PropertyType;
        if (pType.IsPrimitive || pType == typeof(string))
        {
          result += $"{p.Name} = {p.GetValue(obj)},\n";
        }
        else
        {
          //ObjectPropertiesToString(p);
          result += p.ToString() + "\n";
        }
      }
      return result;
    }
  }
}
