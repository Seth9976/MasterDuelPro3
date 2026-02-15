using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000EDF RID: 3807
	public class PlaceStatusManager
	{
		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06006F0C RID: 28428 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006F0D RID: 28429 RVA: 0x0000216D File Offset: 0x0000036D
		public bool visibleAll
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06006F0E RID: 28430 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(GameObject parent, GameObject labelPrefab, GameObject handLabelPrefab, GameObject rootPrefab)
		{
		}

		// Token: 0x06006F0F RID: 28431 RVA: 0x0000216A File Offset: 0x0000036A
		private PlaceStatusLabel Create()
		{
			return null;
		}

		// Token: 0x06006F10 RID: 28432 RVA: 0x0000216A File Offset: 0x0000036A
		private PlaceStatusLabel CreateHand()
		{
			return null;
		}

		// Token: 0x06006F11 RID: 28433 RVA: 0x0000216A File Offset: 0x0000036A
		public PlaceStatusLabel Use(SharedDefinition.Location location, bool lieDown, bool hand)
		{
			return null;
		}

		// Token: 0x06006F12 RID: 28434 RVA: 0x0000216D File Offset: 0x0000036D
		public void Unuse(PlaceStatusLabel instance)
		{
		}

		// Token: 0x06006F13 RID: 28435 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowAll()
		{
		}

		// Token: 0x06006F14 RID: 28436 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideAll()
		{
		}

		// Token: 0x06006F15 RID: 28437 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x0400AA21 RID: 43553
		private GameObject root;

		// Token: 0x0400AA22 RID: 43554
		private global::UnityEngine.Object labelPrefab;

		// Token: 0x0400AA23 RID: 43555
		private global::UnityEngine.Object handLabelPrefab;
	}
}
