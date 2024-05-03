using XFrame.Modules.Reflection;
using CsvHelper;

namespace XFrameServer.Core
{
    public partial class TypeChecker : ITypeCheckHelper
    {
        public string[] AssemblyList => s_Types.ToArray();

        public bool CheckType(Type type)
        {
            //Console.WriteLine("check " + type.FullName);
            if (s_ExcludeNameSpaceList.Count > 0)
            {
                if (s_ExcludeNameSpaceList.Contains(type.Namespace))
                {
                    return false;
                }
            }

            if (s_ExcludeClassList.Count > 0)
            {
                if (s_ExcludeClassList.Contains(type.FullName))
                {
                    return false;
                }
            }

            if (s_AllClassList.Count > 0)
            {
                foreach (Type t in s_AllClassList)
                {
                    if (type.IsAssignableFrom(t) || type == t)
                    {
                        return false;
                    }
                }
            }

            if (type.IsAnonymous())
                return false;

            return true;
        }
    }
}
