using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004C3 RID: 1219
	internal class TypeLoadExceptionHolder
	{
		// Token: 0x060026E9 RID: 9961 RVA: 0x0009D1EB File Offset: 0x0009B3EB
		internal TypeLoadExceptionHolder(string typeName)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x0009D1FA File Offset: 0x0009B3FA
		internal string TypeName
		{
			get
			{
				return this.m_typeName;
			}
		}

		// Token: 0x0400128D RID: 4749
		private string m_typeName;
	}
}
