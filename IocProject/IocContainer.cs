using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IocProject
{
  public class IocContainer
  {
    private static Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();
    private Dictionary<Type, Tuple<ConstructorInfo, ParameterInfo[]>> _cachedConstructors = new Dictionary<Type, Tuple<ConstructorInfo, ParameterInfo[]>>();

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

        ConstructorInfo ctor;
        ParameterInfo[] paramList;
        if (_cachedConstructors.ContainsKey(type))
        {
          ctor = _cachedConstructors[type].Item1;
          paramList = _cachedConstructors[type].Item2;
        }
        else
        {
          ctor = type.GetConstructors().OrderByDescending(x => x.GetParameters().Count()).First();
          paramList = ctor.GetParameters();
          _cachedConstructors.Add(type, new Tuple<ConstructorInfo, ParameterInfo[]>(ctor, ctor.GetParameters()));
        }

        var defaultParams = new List<object>();

        foreach (var param in paramList)
        {
          defaultParams.Add(CreateInstanceOf(param.ParameterType));
        }

        return ctor.Invoke(defaultParams.ToArray());
      }

      return null;
    }
  }
}
