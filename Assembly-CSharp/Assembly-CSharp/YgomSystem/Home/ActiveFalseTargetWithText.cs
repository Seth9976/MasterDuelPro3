using System;
using YgomSystem.YGomTMPro;

namespace YgomSystem.Home
{
	// Token: 0x02000761 RID: 1889
	public class ActiveFalseTargetWithText : ActiveFalseTarget<ExtendedTextMeshProUGUI>
	{
		// Token: 0x06003AF5 RID: 15093 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool IsActive()
		{
			return false;
		}
	}
}
