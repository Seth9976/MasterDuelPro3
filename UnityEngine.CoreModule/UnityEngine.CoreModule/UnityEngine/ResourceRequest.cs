using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200016C RID: 364
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class ResourceRequest : AsyncOperation
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x000204BC File Offset: 0x0001E6BC
		protected virtual Object GetResult()
		{
			return Resources.Load(this.m_Path, this.m_Type);
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x000204E0 File Offset: 0x0001E6E0
		public Object asset
		{
			get
			{
				return this.GetResult();
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x000204F8 File Offset: 0x0001E6F8
		protected ResourceRequest(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x0400060E RID: 1550
		internal string m_Path;

		// Token: 0x0400060F RID: 1551
		internal Type m_Type;
	}
}
