using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000049 RID: 73
	[CustomStyle("SignalEmitter")]
	[ExcludeFromPreset]
	[Serializable]
	public class SignalEmitter : Marker, INotification, INotificationOptionProvider
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00009017 File Offset: 0x00007217
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000901F File Offset: 0x0000721F
		public bool retroactive
		{
			get
			{
				return this.m_Retroactive;
			}
			set
			{
				this.m_Retroactive = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00009028 File Offset: 0x00007228
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00009030 File Offset: 0x00007230
		public bool emitOnce
		{
			get
			{
				return this.m_EmitOnce;
			}
			set
			{
				this.m_EmitOnce = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00009039 File Offset: 0x00007239
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00009041 File Offset: 0x00007241
		public SignalAsset asset
		{
			get
			{
				return this.m_Asset;
			}
			set
			{
				this.m_Asset = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000904A File Offset: 0x0000724A
		PropertyName INotification.id
		{
			get
			{
				if (this.m_Asset != null)
				{
					return new PropertyName(this.m_Asset.name);
				}
				return new PropertyName(string.Empty);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00009075 File Offset: 0x00007275
		NotificationFlags INotificationOptionProvider.flags
		{
			get
			{
				return (this.retroactive ? NotificationFlags.Retroactive : ((NotificationFlags)0)) | (this.emitOnce ? NotificationFlags.TriggerOnce : ((NotificationFlags)0)) | NotificationFlags.TriggerInEditMode;
			}
		}

		// Token: 0x0400012E RID: 302
		[SerializeField]
		private bool m_Retroactive;

		// Token: 0x0400012F RID: 303
		[SerializeField]
		private bool m_EmitOnce;

		// Token: 0x04000130 RID: 304
		[SerializeField]
		private SignalAsset m_Asset;
	}
}
