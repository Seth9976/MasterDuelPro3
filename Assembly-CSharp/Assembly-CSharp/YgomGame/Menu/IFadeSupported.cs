using System;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A8F RID: 2703
	public interface IFadeSupported
	{
		// Token: 0x06004F05 RID: 20229
		Color FadeColor(ViewController.TransitionType type);

		// Token: 0x06004F06 RID: 20230
		SystemProgress.ProgressType FadeType(ViewController.TransitionType type);
	}
}
