using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	public class RenderPipelineGraphicsSettingsContainer : ISerializationCallbackReceiver
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00024F03 File Offset: 0x00023103
		public List<IRenderPipelineGraphicsSettings> settingsList
		{
			get
			{
				return this.m_RuntimeSettings.settingsList;
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00005704 File Offset: 0x00003904
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00005704 File Offset: 0x00003904
		public void OnAfterDeserialize()
		{
		}

		// Token: 0x040006C4 RID: 1732
		[SerializeField]
		[HideInInspector]
		private RenderPipelineGraphicsSettingsCollection m_RuntimeSettings = new RenderPipelineGraphicsSettingsCollection();
	}
}
