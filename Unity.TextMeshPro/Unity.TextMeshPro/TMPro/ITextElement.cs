using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200007E RID: 126
	public interface ITextElement
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000432 RID: 1074
		Material sharedMaterial { get; }

		// Token: 0x06000433 RID: 1075
		void Rebuild(CanvasUpdate update);

		// Token: 0x06000434 RID: 1076
		int GetInstanceID();
	}
}
