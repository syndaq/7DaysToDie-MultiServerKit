using Autofac;
using SdtdMultiServerKit.Functions;
using System.Reflection;

namespace SdtdMultiServerKit.Managers
{
    /// <summary>
    /// Featuremanager
    /// </summary>
    public static class FunctionManager
    {
        private static readonly Dictionary<string, IFunction> _functionDict = new Dictionary<string, IFunction>();

        /// <summary>
        /// Loadallfeature
        /// </summary>
        public static void Init()
        {
            try
            {
                foreach (var type in GetFunctionTypes())
                {
                    var function = (IFunction)ModApi.ServiceContainer.Resolve(type);
                    function.LoadSettings();
                    _functionDict.Add(function.Name, function);
                }

                CustomLogger.Info($"Loaded {_functionDict.Count} functions.");
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex, "Error in FunctionManager.LoadFunctions");
            }
        }

        internal static List<Type> GetFunctionTypes()
        {
            var types = new List<Type>();

            foreach (var assembly in ModApi.LoadedPlugins)
            {
                foreach (var type in assembly.GetExportedTypes())
                {
                    if (typeof(IFunction).IsAssignableFrom(type) && type.IsAbstract == false)
                    {
                        types.Add(type);
                    }
                }
            }
            
            return types;
        }

        /// <summary>
        /// Getallfeature
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IFunction> GetAll()
        {
            return _functionDict.Values;
        }

        /// <summary>
        /// Getfeature
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static bool TryGetFunction(string functionName, out IFunction? function)
        {
            return _functionDict.TryGetValue(functionName, out function);
        }

        /// <summary>
        /// Getfeature
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public static bool TryGetFunction<TFunction>(out TFunction? function) where TFunction : IFunction
        {
            if(_functionDict.TryGetValue(typeof(TFunction).Name, out var _function))
            {
                function = (TFunction)_function;
               return true;
            }
            else
            {
                function = default;
                return false;
            }
        }
    }
}