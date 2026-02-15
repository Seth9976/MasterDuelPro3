using System;
using UnityEngine.UI;

namespace YgomSystem.Home
{
	// Token: 0x0200075F RID: 1887
	public class ActiveFalseTargetWithImage : ActiveFalseTarget<Image>
	{
		// Token: 0x06003AF1 RID: 15089 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool IsActive()
		{
			return false;
		}
	}
}
