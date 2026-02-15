using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000043 RID: 67
	public abstract class Marker : ScriptableObject, IMarker
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00008C6A File Offset: 0x00006E6A
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00008C72 File Offset: 0x00006E72
		public TrackAsset parent { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00008C7B File Offset: 0x00006E7B
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00008C83 File Offset: 0x00006E83
		public double time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = Math.Max(value, 0.0);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00008C9C File Offset: 0x00006E9C
		void IMarker.Initialize(TrackAsset parentTrack)
		{
			if (this.parent == null)
			{
				this.parent = parentTrack;
				try
				{
					this.OnInitialize(parentTrack);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.Message, this);
				}
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00002811 File Offset: 0x00000A11
		public virtual void OnInitialize(TrackAsset aPent)
		{
		}

		// Token: 0x04000127 RID: 295
		[SerializeField]
		[TimeField(TimeFieldAttribute.UseEditMode.ApplyEditMode)]
		[Tooltip("Time for the marker")]
		private double m_Time;
	}
}
