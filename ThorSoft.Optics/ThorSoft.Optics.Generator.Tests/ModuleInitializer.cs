using System.Runtime.CompilerServices;

namespace ThorSoft.Optics.Generator.Tests
{
    public static class ModuleInitializer
    {
        [ModuleInitializer]
        public static void Init()
        {
            VerifySourceGenerators.Initialize();
        }
    }
}
