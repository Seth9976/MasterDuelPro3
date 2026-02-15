using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000F8 RID: 248
	public class CamelCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x000242CD File Offset: 0x000224CD
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000242E3 File Offset: 0x000224E3
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000242F4 File Offset: 0x000224F4
		public CamelCaseNamingStrategy()
		{
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000242FC File Offset: 0x000224FC
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToCamelCase(name);
		}
	}
}
