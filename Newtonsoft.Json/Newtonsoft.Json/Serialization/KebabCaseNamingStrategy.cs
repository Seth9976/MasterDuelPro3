using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000132 RID: 306
	public class KebabCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x000242CD File Offset: 0x000224CD
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0002E85C File Offset: 0x0002CA5C
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x000242F4 File Offset: 0x000224F4
		public KebabCaseNamingStrategy()
		{
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0002E86D File Offset: 0x0002CA6D
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToKebabCase(name);
		}
	}
}
