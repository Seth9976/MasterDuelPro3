using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Scenario
{
	// Token: 0x020009A8 RID: 2472
	public class ScenarioBGContainer : ScenarioContainerBase
	{
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06004815 RID: 18453 RVA: 0x0000216A File Offset: 0x0000036A
		protected override GameObject shakeTarget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x000F4916 File Offset: 0x000F2B16
		public ScenarioBGContainer(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ScenarioWork work)
		{
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToBlurOn()
		{
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToBlurOff()
		{
		}

		// Token: 0x04008652 RID: 34386
		private readonly string k_ELabelBGCanvas;

		// Token: 0x04008653 RID: 34387
		private readonly string k_ELabelBGOutlineCanvas;

		// Token: 0x04008654 RID: 34388
		private readonly string k_ELabelBGRawImage;

		// Token: 0x04008655 RID: 34389
		private readonly string k_ELabelBGSubRawImage;

		// Token: 0x04008656 RID: 34390
		private readonly Canvas bgCanvas;

		// Token: 0x04008657 RID: 34391
		private readonly Canvas bgOutlineCanvas;

		// Token: 0x04008658 RID: 34392
		public readonly ScenarioBGActor bgActor;

		// Token: 0x04008659 RID: 34393
		public readonly ScenarioBGActor bgSubActor;
	}
}
