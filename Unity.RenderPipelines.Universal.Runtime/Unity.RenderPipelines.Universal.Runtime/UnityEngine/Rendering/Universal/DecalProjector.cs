using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200007F RID: 127
	[ExecuteAlways]
	[AddComponentMenu("Rendering/URP Decal Projector")]
	public class DecalProjector : MonoBehaviour
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002C5 RID: 709 RVA: 0x000099F8 File Offset: 0x00007BF8
		// (remove) Token: 0x060002C6 RID: 710 RVA: 0x00009A2C File Offset: 0x00007C2C
		internal static event DecalProjector.DecalProjectorAction onDecalAdd;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002C7 RID: 711 RVA: 0x00009A60 File Offset: 0x00007C60
		// (remove) Token: 0x060002C8 RID: 712 RVA: 0x00009A94 File Offset: 0x00007C94
		internal static event DecalProjector.DecalProjectorAction onDecalRemove;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002C9 RID: 713 RVA: 0x00009AC8 File Offset: 0x00007CC8
		// (remove) Token: 0x060002CA RID: 714 RVA: 0x00009AFC File Offset: 0x00007CFC
		internal static event DecalProjector.DecalProjectorAction onDecalPropertyChange;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060002CB RID: 715 RVA: 0x00009B30 File Offset: 0x00007D30
		// (remove) Token: 0x060002CC RID: 716 RVA: 0x00009B64 File Offset: 0x00007D64
		internal static event Action onAllDecalPropertyChange;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060002CD RID: 717 RVA: 0x00009B98 File Offset: 0x00007D98
		// (remove) Token: 0x060002CE RID: 718 RVA: 0x00009BCC File Offset: 0x00007DCC
		internal static event DecalProjector.DecalProjectorAction onDecalMaterialChange;

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00009BFF File Offset: 0x00007DFF
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00009C06 File Offset: 0x00007E06
		internal static Material defaultMaterial { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00009C0E File Offset: 0x00007E0E
		internal static bool isSupported
		{
			get
			{
				return DecalProjector.onDecalAdd != null;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00009C18 File Offset: 0x00007E18
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00009C20 File Offset: 0x00007E20
		internal DecalEntity decalEntity { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00009C29 File Offset: 0x00007E29
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00009C31 File Offset: 0x00007E31
		public Material material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00009C40 File Offset: 0x00007E40
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00009C48 File Offset: 0x00007E48
		public float drawDistance
		{
			get
			{
				return this.m_DrawDistance;
			}
			set
			{
				this.m_DrawDistance = Mathf.Max(0f, value);
				this.OnValidate();
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00009C61 File Offset: 0x00007E61
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00009C69 File Offset: 0x00007E69
		public float fadeScale
		{
			get
			{
				return this.m_FadeScale;
			}
			set
			{
				this.m_FadeScale = Mathf.Clamp01(value);
				this.OnValidate();
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00009C7D File Offset: 0x00007E7D
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00009C85 File Offset: 0x00007E85
		public float startAngleFade
		{
			get
			{
				return this.m_StartAngleFade;
			}
			set
			{
				this.m_StartAngleFade = Mathf.Clamp(value, 0f, 180f);
				this.OnValidate();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00009CA3 File Offset: 0x00007EA3
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00009CAB File Offset: 0x00007EAB
		public float endAngleFade
		{
			get
			{
				return this.m_EndAngleFade;
			}
			set
			{
				this.m_EndAngleFade = Mathf.Clamp(value, this.m_StartAngleFade, 180f);
				this.OnValidate();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00009CCA File Offset: 0x00007ECA
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00009CD2 File Offset: 0x00007ED2
		public Vector2 uvScale
		{
			get
			{
				return this.m_UVScale;
			}
			set
			{
				this.m_UVScale = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00009CE1 File Offset: 0x00007EE1
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00009CE9 File Offset: 0x00007EE9
		public Vector2 uvBias
		{
			get
			{
				return this.m_UVBias;
			}
			set
			{
				this.m_UVBias = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00009CF8 File Offset: 0x00007EF8
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00009D00 File Offset: 0x00007F00
		public uint renderingLayerMask
		{
			get
			{
				return this.m_DecalLayerMask;
			}
			set
			{
				this.m_DecalLayerMask = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00009D09 File Offset: 0x00007F09
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00009D11 File Offset: 0x00007F11
		public DecalScaleMode scaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				this.m_ScaleMode = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00009D20 File Offset: 0x00007F20
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00009D28 File Offset: 0x00007F28
		public Vector3 pivot
		{
			get
			{
				return this.m_Offset;
			}
			set
			{
				this.m_Offset = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009D37 File Offset: 0x00007F37
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00009D3F File Offset: 0x00007F3F
		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00009D4E File Offset: 0x00007F4E
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00009D56 File Offset: 0x00007F56
		public float fadeFactor
		{
			get
			{
				return this.m_FadeFactor;
			}
			set
			{
				this.m_FadeFactor = Mathf.Clamp01(value);
				this.OnValidate();
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00009D6A File Offset: 0x00007F6A
		internal Vector3 effectiveScale
		{
			get
			{
				if (this.m_ScaleMode != DecalScaleMode.InheritFromHierarchy)
				{
					return Vector3.one;
				}
				return base.transform.lossyScale;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00009D86 File Offset: 0x00007F86
		internal Vector3 decalSize
		{
			get
			{
				return new Vector3(this.m_Size.x, this.m_Size.z, this.m_Size.y);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00009DAE File Offset: 0x00007FAE
		internal Vector3 decalOffset
		{
			get
			{
				return new Vector3(this.m_Offset.x, -this.m_Offset.z, this.m_Offset.y);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00009DD7 File Offset: 0x00007FD7
		internal Vector4 uvScaleBias
		{
			get
			{
				return new Vector4(this.m_UVScale.x, this.m_UVScale.y, this.m_UVBias.x, this.m_UVBias.y);
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00009E0A File Offset: 0x0000800A
		private void InitMaterial()
		{
			this.m_Material == null;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009E19 File Offset: 0x00008019
		private void OnEnable()
		{
			this.InitMaterial();
			this.m_OldMaterial = this.m_Material;
			DecalProjector.DecalProjectorAction decalProjectorAction = DecalProjector.onDecalAdd;
			if (decalProjectorAction == null)
			{
				return;
			}
			decalProjectorAction(this);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00009E3D File Offset: 0x0000803D
		private void OnDisable()
		{
			DecalProjector.DecalProjectorAction decalProjectorAction = DecalProjector.onDecalRemove;
			if (decalProjectorAction == null)
			{
				return;
			}
			decalProjectorAction(this);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00009E50 File Offset: 0x00008050
		internal void OnValidate()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			if (this.m_Material != this.m_OldMaterial)
			{
				DecalProjector.DecalProjectorAction decalProjectorAction = DecalProjector.onDecalMaterialChange;
				if (decalProjectorAction != null)
				{
					decalProjectorAction(this);
				}
				this.m_OldMaterial = this.m_Material;
				return;
			}
			DecalProjector.DecalProjectorAction decalProjectorAction2 = DecalProjector.onDecalPropertyChange;
			if (decalProjectorAction2 == null)
			{
				return;
			}
			decalProjectorAction2(this);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009EA8 File Offset: 0x000080A8
		public bool IsValid()
		{
			return !(this.material == null) && (this.material.FindPass("DBufferProjector") != -1 || this.material.FindPass("DecalProjectorForwardEmissive") != -1 || this.material.FindPass("DecalScreenSpaceProjector") != -1 || this.material.FindPass("DecalGBufferProjector") != -1);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00009F1A File Offset: 0x0000811A
		internal static void UpdateAllDecalProperties()
		{
			Action action = DecalProjector.onAllDecalPropertyChange;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x04000240 RID: 576
		[SerializeField]
		private Material m_Material;

		// Token: 0x04000241 RID: 577
		[SerializeField]
		private float m_DrawDistance = 1000f;

		// Token: 0x04000242 RID: 578
		[SerializeField]
		[Range(0f, 1f)]
		private float m_FadeScale = 0.9f;

		// Token: 0x04000243 RID: 579
		[SerializeField]
		[Range(0f, 180f)]
		private float m_StartAngleFade = 180f;

		// Token: 0x04000244 RID: 580
		[SerializeField]
		[Range(0f, 180f)]
		private float m_EndAngleFade = 180f;

		// Token: 0x04000245 RID: 581
		[SerializeField]
		private Vector2 m_UVScale = new Vector2(1f, 1f);

		// Token: 0x04000246 RID: 582
		[SerializeField]
		private Vector2 m_UVBias = new Vector2(0f, 0f);

		// Token: 0x04000247 RID: 583
		[SerializeField]
		private uint m_DecalLayerMask = 1U;

		// Token: 0x04000248 RID: 584
		[SerializeField]
		private DecalScaleMode m_ScaleMode;

		// Token: 0x04000249 RID: 585
		[SerializeField]
		internal Vector3 m_Offset = new Vector3(0f, 0f, 0.5f);

		// Token: 0x0400024A RID: 586
		[SerializeField]
		internal Vector3 m_Size = new Vector3(1f, 1f, 1f);

		// Token: 0x0400024B RID: 587
		[SerializeField]
		[Range(0f, 1f)]
		private float m_FadeFactor = 1f;

		// Token: 0x0400024C RID: 588
		private Material m_OldMaterial;

		// Token: 0x02000080 RID: 128
		// (Invoke) Token: 0x060002F8 RID: 760
		internal delegate void DecalProjectorAction(DecalProjector decalProjector);
	}
}
