using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000588 RID: 1416
	public class ColorLabelSetting : ScriptableObject
	{
		// Token: 0x06002CCA RID: 11466 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup()
		{
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x0000216A File Offset: 0x0000036A
		public ColorLabelSetting.ValueContainer Get(string label)
		{
			return null;
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000F1F3E File Offset: 0x000F013E
		public bool Get(string label, out Color res)
		{
			res = default(Color);
			return false;
		}

		// Token: 0x04002B10 RID: 11024
		public List<ColorLabelSetting.ValueContainer> list;

		// Token: 0x04002B11 RID: 11025
		private Dictionary<string, ColorLabelSetting.ValueContainer> m_labelMap;

		// Token: 0x02000589 RID: 1417
		[Serializable]
		public class ValueContainer
		{
			// Token: 0x06002CCE RID: 11470 RVA: 0x0000216A File Offset: 0x0000036A
			public ColorLabelSetting.ValueContainer Copy()
			{
				return null;
			}

			// Token: 0x04002B12 RID: 11026
			public string label;

			// Token: 0x04002B13 RID: 11027
			public Color color;
		}
	}
}
