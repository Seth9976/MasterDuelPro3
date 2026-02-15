using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200042B RID: 1067
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class StyleProperty
	{
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x00070D00 File Offset: 0x0006EF00
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x00070D18 File Offset: 0x0006EF18
		public StyleValueHandle[] values
		{
			get
			{
				return this.m_Values;
			}
		}

		// Token: 0x04000D7F RID: 3455
		[SerializeField]
		private string m_Name;

		// Token: 0x04000D80 RID: 3456
		[SerializeField]
		private int m_Line;

		// Token: 0x04000D81 RID: 3457
		[SerializeField]
		private StyleValueHandle[] m_Values;

		// Token: 0x04000D82 RID: 3458
		[NonSerialized]
		internal bool isCustomProperty;

		// Token: 0x04000D83 RID: 3459
		[NonSerialized]
		internal bool requireVariableResolve;
	}
}
