using System;
using System.Collections.Generic;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x0200000F RID: 15
	public readonly struct AttributesScope : IDisposable
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000259C File Offset: 0x0000079C
		public AttributesScope(IProperty target, IProperty source)
		{
			this.m_Target = target as IAttributes;
			IAttributes attributes = target as IAttributes;
			this.m_Previous = ((attributes != null) ? attributes.Attributes : null);
			bool flag = this.m_Target != null;
			if (flag)
			{
				IAttributes target2 = this.m_Target;
				IAttributes attributes2 = source as IAttributes;
				target2.Attributes = ((attributes2 != null) ? attributes2.Attributes : null);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025F9 File Offset: 0x000007F9
		internal AttributesScope(IAttributes target, List<Attribute> attributes)
		{
			this.m_Target = target;
			this.m_Previous = target.Attributes;
			target.Attributes = attributes;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002618 File Offset: 0x00000818
		public void Dispose()
		{
			bool flag = this.m_Target != null;
			if (flag)
			{
				this.m_Target.Attributes = this.m_Previous;
			}
		}

		// Token: 0x0400001E RID: 30
		private readonly IAttributes m_Target;

		// Token: 0x0400001F RID: 31
		private readonly List<Attribute> m_Previous;
	}
}
