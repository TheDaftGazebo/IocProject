using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IocProject
{
  public interface ILogin
  {
    bool Login(string username, string password);
  }

  public class IocTestClass
  {
    public int TestInt { get; }
    public double TestDouble { get; }
    public char TestChar { get; }
    public bool TestBool { get; }
    public string TestString { get; }

    public IocTestClass(int testInt, double testDouble, char testChar, bool testBool, string testString)
    {
      TestInt = testInt;
      TestDouble = testDouble;
      TestChar = testChar;
      TestBool = testBool;
      TestString = testString;
    }
  }

  public class IocTestClass2
  {
    public int TestInt { get; }
  }

  public class Program
  {
    private static Dictionary<Type, Type> _registeredTypes;

    static void Main(string[] args)
    {
      _registeredTypes = new Dictionary<Type, Type>();
      _registeredTypes.Add(typeof(IocTestClass), typeof(IocTestClass));
      var assembly = Assembly.GetExecutingAssembly();
      var userType = assembly.GetType("IocProject.IocTestClass");
      var ctorArgs = userType.GetConstructors().First().GetParameters();
      var defaultCtorArgs = GetDefaultValuesForParameters(ctorArgs);
      var instance = (IocTestClass)Activator.CreateInstance(typeof(IocTestClass), defaultCtorArgs);
      // Phase 2: Create a Dictionary of registered concrete types to construct default values for non-primitive types using recursion to identify parameters to get its own values
      Console.WriteLine(ObjectPropertiesToString(instance));
      Console.ReadKey();
    }

    public static object[] GetDefaultValuesForParameters(ParameterInfo[] parameters)
    {
      var result = new List<object>();
      foreach (var parameter in parameters)
      {
        var parameterType = parameter.ParameterType;
        if (parameterType.IsPrimitive)
        {
          result.Add(Activator.CreateInstance(parameterType));
        }
        else if (_registeredTypes.ContainsKey(parameterType))
        {
          var ctorArgs = parameterType.GetConstructors().First().GetParameters();
          var defaultCtorArgs = GetDefaultValuesForParameters(ctorArgs);
          var instance = (IocTestClass)Activator.CreateInstance(typeof(IocTestClass), defaultCtorArgs);
          result.Add(instance);
        }
        else
        {
          result.Add(null);
        }
      }
      return result.ToArray();
    }

    public static string ObjectPropertiesToString(object obj)
    {
      var result = string.Empty;
      var properties = obj.GetType().GetProperties();
      foreach (var p in properties)
      {
        result += $"{p.Name} = {p.GetValue(obj)},\n";
      }
      return result;
    }
  }
}
