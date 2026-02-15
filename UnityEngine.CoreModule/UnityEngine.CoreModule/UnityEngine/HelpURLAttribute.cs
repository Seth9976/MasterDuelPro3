using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200017E RID: 382
	[UsedByNativeCode]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class HelpURLAttribute : Attribute
	{
		// Token: 0x06000F9A RID: 3994 RVA: 0x00020D82 File Offset: 0x0001EF82
		public HelpURLAttribute(string url)
		{
			this.m_Url = url;
			this.m_DispatchingFieldName = "";
			this.m_Dispatcher = false;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00020DA5 File Offset: 0x0001EFA5
		public virtual string URL
		{
			get
			{
				return this.m_Url;
			}
		}

		// Token: 0x04000621 RID: 1569
		internal readonly string m_Url;

		// Token: 0x04000622 RID: 1570
		internal readonly bool m_Dispatcher;

		// Token: 0x04000623 RID: 1571
		internal readonly string m_DispatchingFieldName;
	}
}
