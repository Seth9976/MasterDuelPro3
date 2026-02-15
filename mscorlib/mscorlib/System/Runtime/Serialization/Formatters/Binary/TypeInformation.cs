using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004FF RID: 1279
	internal sealed class TypeInformation
	{
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x000A257A File Offset: 0x000A077A
		internal string FullTypeName
		{
			get
			{
				return this.fullTypeName;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002831 RID: 10289 RVA: 0x000A2582 File Offset: 0x000A0782
		internal string AssemblyString
		{
			get
			{
				return this.assemblyString;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x000A258A File Offset: 0x000A078A
		internal bool HasTypeForwardedFrom
		{
			get
			{
				return this.hasTypeForwardedFrom;
			}
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000A2592 File Offset: 0x000A0792
		internal TypeInformation(string fullTypeName, string assemblyString, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = fullTypeName;
			this.assemblyString = assemblyString;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x0400141A RID: 5146
		private string fullTypeName;

		// Token: 0x0400141B RID: 5147
		private string assemblyString;

		// Token: 0x0400141C RID: 5148
		private bool hasTypeForwardedFrom;
	}
}
