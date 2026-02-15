using System;
using UnityEngine.Scripting;

namespace UnityEngine.Serialization
{
	// Token: 0x02000239 RID: 569
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	public class FormerlySerializedAsAttribute : Attribute
	{
		// Token: 0x06001493 RID: 5267 RVA: 0x0002B795 File Offset: 0x00029995
		public FormerlySerializedAsAttribute(string oldName)
		{
			this.m_oldName = oldName;
		}

		// Token: 0x04000796 RID: 1942
		private string m_oldName;
	}
}
