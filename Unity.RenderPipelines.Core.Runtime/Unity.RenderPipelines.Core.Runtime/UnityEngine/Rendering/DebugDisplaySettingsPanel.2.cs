using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000070 RID: 112
	public abstract class DebugDisplaySettingsPanel<T> : DebugDisplaySettingsPanel where T : IDebugDisplaySettingsData
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00009E39 File Offset: 0x00008039
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x00009E41 File Offset: 0x00008041
		public T data
		{
			get
			{
				return this.m_Data;
			}
			internal set
			{
				this.m_Data = value;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00009E4A File Offset: 0x0000804A
		protected DebugDisplaySettingsPanel(T data)
		{
			this.m_Data = data;
		}

		// Token: 0x04000159 RID: 345
		internal T m_Data;
	}
}
