using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200061D RID: 1565
	public class TweenColorToInfo : TweenGenerateInfo
	{
		// Token: 0x04002E33 RID: 11827
		[ColorLabelString]
		public string toLabel;

		// Token: 0x04002E34 RID: 11828
		public Color to;

		// Token: 0x04002E35 RID: 11829
		public bool isRecusive;
	}
}
