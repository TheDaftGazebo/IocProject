using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IocProject
{
  public class IocContainerSimple
  {
    private static Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();

    public void Register<T, U>() where U : T
    {
      _registrations.Add(typeof(T), typeof(U));
    }

    public T Resolve<T>()
    {
      return (T)CreateInstanceOf(typeof(T));
    }

    private object CreateInstanceOf(Type type)
    {
      if (type.IsPrimitive)
      {
        return Activator.CreateInstance(type);
      }

      if (_registrations.ContainsKey(type))
      {
        type = _registrations[type];

        var ctor = type.GetConstructors().OrderByDescending(x => x.GetParameters().Count()).First();

        var defaultParams = new List<object>();

        foreach (var param in ctor.GetParameters())
        {
          defaultParams.Add(CreateInstanceOf(param.ParameterType));
        }

        return ctor.Invoke(defaultParams.ToArray());
      }

      return null;
    }
  }
}
