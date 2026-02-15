using System;
using System.Reflection;
using System.Threading;

namespace System
{
	// Token: 0x020001BD RID: 445
	internal sealed class TypeNameParser
	{
		// Token: 0x06001165 RID: 4453 RVA: 0x00047E40 File Offset: 0x00046040
		internal static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			return TypeSpec.Parse(typeName).Resolve(assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
		}
	}
}
