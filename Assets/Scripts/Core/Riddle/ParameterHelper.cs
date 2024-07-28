using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Riddle
{
    public static class ParameterHelper
    {
        public static Dictionary<string, List<string>> GetParameterOptions()
        {
            Dictionary<string, List<string>> parameterOptions = new Dictionary<string, List<string>>();

            // Get all enums from the current assembly
            var enums = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsEnum && t.Name.Contains("Parameter"));

            foreach (var enumType in enums)
            {
                string enumName = enumType.Name.Replace("Parameter", "");
                List<string> options = Enum.GetNames(enumType).ToList();
                parameterOptions.Add(enumName, options);
            }

            return parameterOptions;
        }
        
        public static List<string> GetOptionsByName(string name)
        {
            // Get the enum type based on the name with "Parameter" suffix
            var enumType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.IsEnum && t.Name == $"{name}Parameter");

            if (enumType != null)
            {
                return Enum.GetNames(enumType).ToList();
            }

            return new List<string>();
        }
    }
}