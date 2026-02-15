using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200013A RID: 314
	public class SnakeCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x000242CD File Offset: 0x000224CD
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0002EC9E File Offset: 0x0002CE9E
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000242F4 File Offset: 0x000224F4
		public SnakeCaseNamingStrategy()
		{
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0002ECAF File Offset: 0x0002CEAF
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToSnakeCase(name);
		}
	}
}
