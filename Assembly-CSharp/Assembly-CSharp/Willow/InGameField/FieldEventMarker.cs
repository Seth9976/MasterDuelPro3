using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Scripting;
using UnityEngine.Timeline;

namespace Willow.InGameField
{
	// Token: 0x02001564 RID: 5476
	[DisplayName]
	[Preserve]
	[Serializable]
	public class FieldEventMarker : Marker, INotification
	{
		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x06009EE3 RID: 40675 RVA: 0x0019B640 File Offset: 0x00199840
		public PropertyName id
		{
			get
			{
				return default(PropertyName);
			}
		}

		// Token: 0x0400DE3A RID: 56890
		public TriggerFieldEventCommandSet triggerFieldEventCommandSet;
	}
}
