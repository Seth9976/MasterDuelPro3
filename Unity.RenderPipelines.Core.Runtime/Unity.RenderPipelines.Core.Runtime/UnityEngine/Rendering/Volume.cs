using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x020001E6 RID: 486
	[ExecuteAlways]
	[AddComponentMenu("Miscellaneous/Volume")]
	public class Volume : MonoBehaviour, IVolume
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00033342 File Offset: 0x00031542
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x0003334A File Offset: 0x0003154A
		public bool isGlobal
		{
			get
			{
				return this.m_IsGlobal;
			}
			set
			{
				this.m_IsGlobal = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00033354 File Offset: 0x00031554
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x00033400 File Offset: 0x00031600
		public VolumeProfile profile
		{
			get
			{
				if (this.m_InternalProfile == null)
				{
					this.m_InternalProfile = ScriptableObject.CreateInstance<VolumeProfile>();
					if (this.sharedProfile != null)
					{
						this.m_InternalProfile.name = this.sharedProfile.name;
						foreach (VolumeComponent volumeComponent in this.sharedProfile.components)
						{
							VolumeComponent itemCopy = Object.Instantiate<VolumeComponent>(volumeComponent);
							this.m_InternalProfile.components.Add(itemCopy);
						}
					}
				}
				return this.m_InternalProfile;
			}
			set
			{
				this.m_InternalProfile = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00033409 File Offset: 0x00031609
		public List<Collider> colliders
		{
			get
			{
				return this.m_Colliders;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00033411 File Offset: 0x00031611
		internal VolumeProfile profileRef
		{
			get
			{
				if (!(this.m_InternalProfile == null))
				{
					return this.m_InternalProfile;
				}
				return this.sharedProfile;
			}
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0003342E File Offset: 0x0003162E
		public bool HasInstantiatedProfile()
		{
			return this.m_InternalProfile != null;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0003343C File Offset: 0x0003163C
		private void OnEnable()
		{
			this.m_PreviousLayer = base.gameObject.layer;
			VolumeManager.instance.Register(this);
			base.GetComponents<Collider>(this.m_Colliders);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00033466 File Offset: 0x00031666
		private void OnDisable()
		{
			VolumeManager.instance.Unregister(this);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00033473 File Offset: 0x00031673
		private void Update()
		{
			this.UpdateLayer();
			this.UpdatePriority();
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00033484 File Offset: 0x00031684
		internal void UpdateLayer()
		{
			int layer = base.gameObject.layer;
			if (layer == this.m_PreviousLayer)
			{
				return;
			}
			VolumeManager.instance.UpdateVolumeLayer(this, this.m_PreviousLayer, layer);
			this.m_PreviousLayer = layer;
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x000334C0 File Offset: 0x000316C0
		internal void UpdatePriority()
		{
			if (Math.Abs(this.priority - this.m_PreviousPriority) <= Mathf.Epsilon)
			{
				return;
			}
			VolumeManager.instance.SetLayerDirty(base.gameObject.layer);
			this.m_PreviousPriority = this.priority;
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x000334FD File Offset: 0x000316FD
		private void OnValidate()
		{
			this.blendDistance = Mathf.Max(this.blendDistance, 0f);
		}

		// Token: 0x04000932 RID: 2354
		[SerializeField]
		[FormerlySerializedAs("isGlobal")]
		private bool m_IsGlobal = true;

		// Token: 0x04000933 RID: 2355
		[Delayed]
		public float priority;

		// Token: 0x04000934 RID: 2356
		public float blendDistance;

		// Token: 0x04000935 RID: 2357
		[Range(0f, 1f)]
		public float weight = 1f;

		// Token: 0x04000936 RID: 2358
		public VolumeProfile sharedProfile;

		// Token: 0x04000937 RID: 2359
		internal List<Collider> m_Colliders = new List<Collider>();

		// Token: 0x04000938 RID: 2360
		private int m_PreviousLayer;

		// Token: 0x04000939 RID: 2361
		private float m_PreviousPriority;

		// Token: 0x0400093A RID: 2362
		private VolumeProfile m_InternalProfile;
	}
}
