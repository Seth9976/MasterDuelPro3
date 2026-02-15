using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Scenario
{
	// Token: 0x020009DA RID: 2522
	public class ScenarioObjectContainer3D : ScenarioContainerBase
	{
		// Token: 0x06004964 RID: 18788 RVA: 0x000F4916 File Offset: 0x000F2B16
		public ScenarioObjectContainer3D(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ScenarioWork work)
		{
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x0000216D File Offset: 0x0000036D
		public void ApplyCameraScale(Camera camera3d, Camera overuicamera3d)
		{
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreateCardActor(ScenarioCardActor cardActor)
		{
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatePref(string label, GameObject gom)
		{
		}

		// Token: 0x04008734 RID: 34612
		private readonly string k_ELabelBGRoot;

		// Token: 0x04008735 RID: 34613
		private readonly string k_ELabelActorRoot;

		// Token: 0x04008736 RID: 34614
		private readonly string k_ELabelPrefabBackUIRoot;

		// Token: 0x04008737 RID: 34615
		private readonly string k_ELabelPrefabOverUIRoot;

		// Token: 0x04008738 RID: 34616
		private readonly string k_ELabelBlurLayer;

		// Token: 0x04008739 RID: 34617
		public readonly ScenarioBGContainer bgContainer;

		// Token: 0x0400873A RID: 34618
		public readonly ScenarioActorContainer actorContainer;

		// Token: 0x0400873B RID: 34619
		public readonly ScenarioPrefabContainer prefabContainer;

		// Token: 0x0400873C RID: 34620
		public readonly ScenarioBlurLayerActor blurScreenActor;
	}
}
