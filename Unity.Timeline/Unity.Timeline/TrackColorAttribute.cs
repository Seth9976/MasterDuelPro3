using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002B RID: 43
	[AttributeUsage(AttributeTargets.Class)]
	public class TrackColorAttribute : Attribute
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00006CE7 File Offset: 0x00004EE7
		public Color color
		{
			get
			{
				return this.m_Color;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006CEF File Offset: 0x00004EEF
		public TrackColorAttribute(float r, float g, float b)
		{
			this.m_Color = new Color(r, g, b);
		}

		// Token: 0x040000D3 RID: 211
		private Color m_Color;
	}
}
