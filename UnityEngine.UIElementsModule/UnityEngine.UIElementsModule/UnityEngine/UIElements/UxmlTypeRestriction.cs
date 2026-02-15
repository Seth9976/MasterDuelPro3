using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004AE RID: 1198
	public abstract class UxmlTypeRestriction : IEquatable<UxmlTypeRestriction>
	{
		// Token: 0x06002232 RID: 8754 RVA: 0x0007CF40 File Offset: 0x0007B140
		public virtual bool Equals(UxmlTypeRestriction other)
		{
			return this == other;
		}
	}
}
