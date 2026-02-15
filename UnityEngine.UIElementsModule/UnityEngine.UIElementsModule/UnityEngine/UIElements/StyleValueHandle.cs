using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000436 RID: 1078
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal struct StyleValueHandle
	{
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x00071958 File Offset: 0x0006FB58
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x00071970 File Offset: 0x0006FB70
		public StyleValueType valueType
		{
			get
			{
				return this.m_ValueType;
			}
			internal set
			{
				this.m_ValueType = value;
			}
		}

		// Token: 0x04000DB6 RID: 3510
		[SerializeField]
		private StyleValueType m_ValueType;

		// Token: 0x04000DB7 RID: 3511
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[SerializeField]
		internal int valueIndex;
	}
}
