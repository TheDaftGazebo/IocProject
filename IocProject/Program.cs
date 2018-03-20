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

  public class TestUser : ILogin
  {
    public string Username { get; }
    public string Password { get; }

    public TestUser(string username, string password)
    {
      Username = username;
      Password = password;
    }

    public bool Login(string username, string password)
    {
      return username.Equals(Username, StringComparison.InvariantCulture)
        && password.Equals(Password, StringComparison.InvariantCulture);
    }
  }

  public class Program
  {
    static void Main(string[] args)
    {
      var assembly = Assembly.GetExecutingAssembly();
      var userType = assembly.GetType("IocProject.TestUser");
      var ctorArgs = userType.GetConstructors().First().GetParameters();
      // Phase 1: Get this list of types from the constructor, and then build a list of default values for primitive types, nulls for non-primitive for the given constructor
      // Then execute to create an object

      // Phase 2: Create a Dictionary of registered concrete types to construct default values for non-primitive types using recursion to identify parameters to get its own values

      var ctor = userType.GetConstructor(new Type[] { typeof(string), typeof(string) });
      var userInstance = ctor.Invoke(new object[] { "aclay", "test123" });
      var loginMethod = userType.GetMethod("Login");
      Console.WriteLine(loginMethod.Invoke(userInstance, new object[] { "aclay", "test123" }));

      Console.ReadKey();
    }
  }
}
