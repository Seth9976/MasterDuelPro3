using System;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	public class GUILayout
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00004268 File Offset: 0x00002468
		public static GUILayoutOption Width(float width)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedWidth, width);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004288 File Offset: 0x00002488
		public static GUILayoutOption Height(float height)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedHeight, height);
		}
	}
}
