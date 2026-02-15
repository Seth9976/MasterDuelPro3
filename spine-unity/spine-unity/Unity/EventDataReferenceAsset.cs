using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x0200000F RID: 15
	[CreateAssetMenu(menuName = "Spine/EventData Reference Asset", order = 100)]
	public class EventDataReferenceAsset : ScriptableObject
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003106 File Offset: 0x00001306
		public EventData EventData
		{
			get
			{
				if (this.eventData == null)
				{
					this.Initialize();
				}
				return this.eventData;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000311C File Offset: 0x0000131C
		public void Initialize()
		{
			if (this.skeletonDataAsset == null)
			{
				return;
			}
			this.eventData = this.skeletonDataAsset.GetSkeletonData(true).FindEvent(this.eventName);
			if (this.eventData == null)
			{
				Debug.LogWarningFormat("Event Data '{0}' not found in SkeletonData : {1}.", new object[]
				{
					this.eventName,
					this.skeletonDataAsset.name
				});
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003184 File Offset: 0x00001384
		public static implicit operator EventData(EventDataReferenceAsset asset)
		{
			return asset.EventData;
		}

		// Token: 0x04000025 RID: 37
		private const bool QuietSkeletonData = true;

		// Token: 0x04000026 RID: 38
		[SerializeField]
		protected SkeletonDataAsset skeletonDataAsset;

		// Token: 0x04000027 RID: 39
		[SerializeField]
		[SpineEvent("", "skeletonDataAsset", true, false, false)]
		protected string eventName;

		// Token: 0x04000028 RID: 40
		private EventData eventData;
	}
}
