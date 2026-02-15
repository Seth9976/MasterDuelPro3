using System;

namespace UnityEngine
{
	// Token: 0x020000BB RID: 187
	public interface IExposedPropertyTable
	{
		// Token: 0x0600049C RID: 1180
		Object GetReferenceValue(PropertyName id, out bool idValid);
	}
}
