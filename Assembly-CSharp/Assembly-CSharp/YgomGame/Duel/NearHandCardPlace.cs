using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000ED8 RID: 3800
	public class NearHandCardPlace : HandCardPlace
	{
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06006EB4 RID: 28340 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectedIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06006EB5 RID: 28341 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int decidedIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06006EB6 RID: 28342 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int handCardNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06006EB7 RID: 28343 RVA: 0x000F6140 File Offset: 0x000F4340
		private Vector3 targetCenterPosition
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06006EB8 RID: 28344 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override HandCardManager.ViewSortMode sortMode
		{
			get
			{
				return HandCardManager.ViewSortMode.EngineIndex;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06006EB9 RID: 28345 RVA: 0x000F6158 File Offset: 0x000F4358
		protected override Vector3 locatorOffsetTargetDeciding
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06006EBA RID: 28346 RVA: 0x000F6170 File Offset: 0x000F4370
		protected override Vector3 locatorOffsetTargetSelecting
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06006EBB RID: 28347 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isAllOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006EBC RID: 28348 RVA: 0x000F5FBE File Offset: 0x000F41BE
		public NearHandCardPlace(DuelFieldBase duelField, int team, int position)
			: base(null, 0, 0)
		{
		}

		// Token: 0x06006EBD RID: 28349 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SelectNextItem(int index)
		{
		}

		// Token: 0x06006EBE RID: 28350 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SelectPrevItem(int index)
		{
		}

		// Token: 0x06006EBF RID: 28351 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HighlightImpl(int index, SharedDefinition.ActivateAura auraType)
		{
		}

		// Token: 0x06006EC0 RID: 28352 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnUpdate()
		{
		}

		// Token: 0x06006EC1 RID: 28353 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCenter()
		{
		}

		// Token: 0x06006EC2 RID: 28354 RVA: 0x0000216D File Offset: 0x0000036D
		public override void UpdateCenterPosition()
		{
		}

		// Token: 0x06006EC3 RID: 28355 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetupCardLocator(CardLocator cardLocator)
		{
		}

		// Token: 0x06006EC4 RID: 28356 RVA: 0x000F5FC9 File Offset: 0x000F41C9
		public override void GetPosture(int index, out Vector3 position, out Quaternion rotation, out float depth, int card_num = -1, bool originPosition = false)
		{
			position = default(Vector3);
			rotation = default(Quaternion);
			depth = 0f;
		}

		// Token: 0x06006EC5 RID: 28357 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetViewIndex(int index)
		{
			return 0;
		}

		// Token: 0x06006EC6 RID: 28358 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetIndexByViewIndex(int viewIndex)
		{
			return 0;
		}

		// Token: 0x06006EC7 RID: 28359 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int UniqueIdToIndex(int uniqueId)
		{
			return 0;
		}

		// Token: 0x06006EC8 RID: 28360 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetSortImpl(HandCardManager.ViewSortMode sort)
		{
		}

		// Token: 0x06006EC9 RID: 28361 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InsertViewIndexImpl(int targetIndex, int InsertViewIndex)
		{
		}

		// Token: 0x06006ECA RID: 28362 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void RefreshCardLocatorsImpl()
		{
		}

		// Token: 0x06006ECB RID: 28363 RVA: 0x0000216D File Offset: 0x0000036D
		public override void UpdateAllCardPosition()
		{
		}

		// Token: 0x0400A99C RID: 43420
		private Vector3 centerCameraDelta;

		// Token: 0x0400A99D RID: 43421
		private float cameraDistance;

		// Token: 0x0400A99E RID: 43422
		private Vector3 centerPosition;

		// Token: 0x0400A99F RID: 43423
		private Vector3 scale;

		// Token: 0x0400A9A0 RID: 43424
		private Vector3 targetScale;

		// Token: 0x0400A9A1 RID: 43425
		private Vector3 modeChangeStartCenterPosition;

		// Token: 0x0400A9A2 RID: 43426
		private Vector3 modeChangeStartScale;

		// Token: 0x0400A9A3 RID: 43427
		private float modeChangeTime;

		// Token: 0x0400A9A4 RID: 43428
		private float modeChangeTimeCounter;

		// Token: 0x0400A9A5 RID: 43429
		private Vector3 centerViewportSmall;

		// Token: 0x0400A9A6 RID: 43430
		private Vector3 _locatorOffsetTargetDeciding;

		// Token: 0x0400A9A7 RID: 43431
		private Vector3 _locatorOffsetTargetSelecting;
	}
}
