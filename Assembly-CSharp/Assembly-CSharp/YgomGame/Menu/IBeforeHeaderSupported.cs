using System;
using UnityEngine;

namespace YgomGame.Menu
{
	// Token: 0x02000A88 RID: 2696
	public interface IBeforeHeaderSupported
	{
		// Token: 0x06004F01 RID: 20225
		GameObject GetBeforeParts();

		// Token: 0x06004F02 RID: 20226
		void SetBeforePartsVisible(bool visible);
	}
}
