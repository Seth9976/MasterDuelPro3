using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200042C RID: 1068
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class StyleRule
	{
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x00070D30 File Offset: 0x0006EF30
		public StyleProperty[] properties
		{
			get
			{
				return this.m_Properties;
			}
		}

		// Token: 0x04000D84 RID: 3460
		[SerializeField]
		private StyleProperty[] m_Properties;

		// Token: 0x04000D85 RID: 3461
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int line;

		// Token: 0x04000D86 RID: 3462
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[NonSerialized]
		internal int customPropertiesCount;
	}
}
