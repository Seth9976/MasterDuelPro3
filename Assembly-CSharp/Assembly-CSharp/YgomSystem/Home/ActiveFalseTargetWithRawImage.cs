using System;
using UnityEngine.UI;

namespace YgomSystem.Home
{
	// Token: 0x02000760 RID: 1888
	public class ActiveFalseTargetWithRawImage : ActiveFalseTarget<RawImage>
	{
		// Token: 0x06003AF3 RID: 15091 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool IsActive()
		{
			return false;
		}
	}
}
