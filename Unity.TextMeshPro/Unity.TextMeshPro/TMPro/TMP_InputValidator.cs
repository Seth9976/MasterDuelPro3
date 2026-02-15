using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	public abstract class TMP_InputValidator : ScriptableObject
	{
		// Token: 0x0600030A RID: 778
		public abstract char Validate(ref string text, ref int pos, char ch);
	}
}
