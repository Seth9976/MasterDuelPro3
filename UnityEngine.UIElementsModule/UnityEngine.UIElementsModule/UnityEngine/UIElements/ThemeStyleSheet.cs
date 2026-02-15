using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000440 RID: 1088
	[HelpURL("UIE-tss")]
	[Serializable]
	public class ThemeStyleSheet : StyleSheet
	{
		// Token: 0x06001F58 RID: 8024 RVA: 0x00072307 File Offset: 0x00070507
		internal override void OnEnable()
		{
			base.isDefaultStyleSheet = true;
			base.OnEnable();
		}
	}
}
