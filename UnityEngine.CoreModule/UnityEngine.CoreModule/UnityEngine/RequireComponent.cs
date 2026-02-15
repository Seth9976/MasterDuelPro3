using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000177 RID: 375
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	[RequiredByNativeCode]
	public sealed class RequireComponent : Attribute
	{
		// Token: 0x06000F8C RID: 3980 RVA: 0x00020CD2 File Offset: 0x0001EED2
		public RequireComponent(Type requiredComponent)
		{
			this.m_Type0 = requiredComponent;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00020CE3 File Offset: 0x0001EEE3
		public RequireComponent(Type requiredComponent, Type requiredComponent2)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
		}

		// Token: 0x04000616 RID: 1558
		public Type m_Type0;

		// Token: 0x04000617 RID: 1559
		public Type m_Type1;

		// Token: 0x04000618 RID: 1560
		public Type m_Type2;
	}
}
