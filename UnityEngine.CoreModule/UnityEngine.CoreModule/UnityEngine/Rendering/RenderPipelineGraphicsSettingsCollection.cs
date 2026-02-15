using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000368 RID: 872
	[Serializable]
	public class RenderPipelineGraphicsSettingsCollection
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x000345F5 File Offset: 0x000327F5
		public List<IRenderPipelineGraphicsSettings> settingsList
		{
			get
			{
				return this.m_List;
			}
		}

		// Token: 0x04000A33 RID: 2611
		[SerializeReference]
		private List<IRenderPipelineGraphicsSettings> m_List = new List<IRenderPipelineGraphicsSettings>();
	}
}
