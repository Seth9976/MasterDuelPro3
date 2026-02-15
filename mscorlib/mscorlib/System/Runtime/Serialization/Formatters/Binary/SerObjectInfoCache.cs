using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004FE RID: 1278
	internal sealed class SerObjectInfoCache
	{
		// Token: 0x0600282E RID: 10286 RVA: 0x000A251C File Offset: 0x000A071C
		internal SerObjectInfoCache(string typeName, string assemblyName, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = typeName;
			this.assemblyString = assemblyName;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000A253C File Offset: 0x000A073C
		internal SerObjectInfoCache(Type type)
		{
			TypeInformation typeInformation = BinaryFormatter.GetTypeInformation(type);
			this.fullTypeName = typeInformation.FullTypeName;
			this.assemblyString = typeInformation.AssemblyString;
			this.hasTypeForwardedFrom = typeInformation.HasTypeForwardedFrom;
		}

		// Token: 0x04001414 RID: 5140
		internal string fullTypeName;

		// Token: 0x04001415 RID: 5141
		internal string assemblyString;

		// Token: 0x04001416 RID: 5142
		internal bool hasTypeForwardedFrom;

		// Token: 0x04001417 RID: 5143
		internal MemberInfo[] memberInfos;

		// Token: 0x04001418 RID: 5144
		internal string[] memberNames;

		// Token: 0x04001419 RID: 5145
		internal Type[] memberTypes;
	}
}
