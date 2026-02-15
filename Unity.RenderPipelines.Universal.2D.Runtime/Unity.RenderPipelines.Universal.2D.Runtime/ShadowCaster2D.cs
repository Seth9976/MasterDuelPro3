using System;
using Unity.Collections;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200006B RID: 107
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("Rendering/2D/Shadow Caster 2D")]
	[MovedFrom(false, "UnityEngine.Experimental.Rendering.Universal", "com.unity.render-pipelines.universal", null)]
	public class ShadowCaster2D : ShadowCasterGroup2D, ISerializationCallbackReceiver
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00014E5E File Offset: 0x0001305E
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x00014E6B File Offset: 0x0001306B
		internal ShadowCaster2D.EdgeProcessing edgeProcessing
		{
			get
			{
				return (ShadowCaster2D.EdgeProcessing)this.m_ShadowMesh.edgeProcessing;
			}
			set
			{
				this.m_ShadowMesh.edgeProcessing = (ShadowMesh2D.EdgeProcessing)value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00014E79 File Offset: 0x00013079
		public Mesh mesh
		{
			get
			{
				return this.m_ShadowMesh.mesh;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00014E86 File Offset: 0x00013086
		public BoundingSphere boundingSphere
		{
			get
			{
				return this.m_ShadowMesh.boundingSphere;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00014E93 File Offset: 0x00013093
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x00014EA0 File Offset: 0x000130A0
		public float trimEdge
		{
			get
			{
				return this.m_ShadowMesh.trimEdge;
			}
			set
			{
				this.m_ShadowMesh.trimEdge = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00014EAE File Offset: 0x000130AE
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x00014EB6 File Offset: 0x000130B6
		public float alphaCutoff
		{
			get
			{
				return this.m_AlphaCutoff;
			}
			set
			{
				this.m_AlphaCutoff = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00014EBF File Offset: 0x000130BF
		public Vector3[] shapePath
		{
			get
			{
				return this.m_ShapePath;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00014EC7 File Offset: 0x000130C7
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00014ECF File Offset: 0x000130CF
		internal int shapePathHash
		{
			get
			{
				return this.m_ShapePathHash;
			}
			set
			{
				this.m_ShapePathHash = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00014ED8 File Offset: 0x000130D8
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00014EE0 File Offset: 0x000130E0
		internal ShadowCaster2D.ShadowCastingSources shadowCastingSource
		{
			get
			{
				return this.m_ShadowCastingSource;
			}
			set
			{
				this.m_ShadowCastingSource = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00014EE9 File Offset: 0x000130E9
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00014EF1 File Offset: 0x000130F1
		internal Component shadowShape2DComponent
		{
			get
			{
				return this.m_ShadowShape2DComponent;
			}
			set
			{
				this.m_ShadowShape2DComponent = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00014EFA File Offset: 0x000130FA
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00014F02 File Offset: 0x00013102
		internal ShadowShape2DProvider shadowShape2DProvider
		{
			get
			{
				return this.m_ShadowShape2DProvider;
			}
			set
			{
				this.m_ShadowShape2DProvider = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00014F0B File Offset: 0x0001310B
		internal int spriteMaterialCount
		{
			get
			{
				return this.m_SpriteMaterialCount;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00014F14 File Offset: 0x00013114
		internal override void CacheValues()
		{
			this.m_CachedPosition = base.transform.position;
			this.m_CachedLossyScale = base.transform.lossyScale;
			this.m_CachedRotation = base.transform.rotation;
			bool flipX;
			bool flipY;
			this.m_ShadowMesh.GetFlip(out flipX, out flipY);
			Vector3 scale = new Vector3((float)(flipX ? (-1) : 1), (float)(flipY ? (-1) : 1), 1f);
			this.m_CachedShadowMatrix = Matrix4x4.TRS(this.m_CachedPosition, this.m_CachedRotation, scale);
			this.m_CachedInverseShadowMatrix = this.m_CachedShadowMatrix.inverse;
			this.m_CachedLocalToWorldMatrix = base.transform.localToWorldMatrix;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00014FC2 File Offset: 0x000131C2
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00014FB9 File Offset: 0x000131B9
		public ShadowCaster2D.ShadowCastingOptions castingOption
		{
			get
			{
				return this.m_CastingOption;
			}
			set
			{
				this.m_CastingOption = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00014FD3 File Offset: 0x000131D3
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00014FCA File Offset: 0x000131CA
		[Obsolete("useRendererSilhoutte is deprecated. Use rendererSilhoutte instead")]
		public bool useRendererSilhouette
		{
			get
			{
				return this.m_UseRendererSilhouette && this.m_HasRenderer;
			}
			set
			{
				this.m_UseRendererSilhouette = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0001503A File Offset: 0x0001323A
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00014FE8 File Offset: 0x000131E8
		public bool selfShadows
		{
			get
			{
				return this.castingOption == ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow || this.castingOption == ShadowCaster2D.ShadowCastingOptions.SelfShadow;
			}
			set
			{
				if (value)
				{
					if (this.castingOption == ShadowCaster2D.ShadowCastingOptions.CastShadow)
					{
						this.castingOption = ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow;
						return;
					}
					if (this.castingOption == ShadowCaster2D.ShadowCastingOptions.NoShadow)
					{
						this.castingOption = ShadowCaster2D.ShadowCastingOptions.SelfShadow;
						return;
					}
				}
				else
				{
					if (this.castingOption == ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow)
					{
						this.castingOption = ShadowCaster2D.ShadowCastingOptions.CastShadow;
						return;
					}
					if (this.castingOption == ShadowCaster2D.ShadowCastingOptions.SelfShadow)
					{
						this.castingOption = ShadowCaster2D.ShadowCastingOptions.NoShadow;
					}
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000150A2 File Offset: 0x000132A2
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00015050 File Offset: 0x00013250
		public bool castsShadows
		{
			get
			{
				return this.castingOption == ShadowCaster2D.ShadowCastingOptions.CastShadow || this.castingOption == ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow;
			}
			set
			{
				if (value)
				{
					if (this.castingOption == ShadowCaster2D.ShadowCastingOptions.SelfShadow)
					{
						this.castingOption = ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow;
						return;
					}
					if (this.castingOption == ShadowCaster2D.ShadowCastingOptions.NoShadow)
					{
						this.castingOption = ShadowCaster2D.ShadowCastingOptions.CastShadow;
						return;
					}
				}
				else
				{
					if (this.castingOption == ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow)
					{
						this.castingOption = ShadowCaster2D.ShadowCastingOptions.SelfShadow;
						return;
					}
					if (this.castingOption == ShadowCaster2D.ShadowCastingOptions.CastShadow)
					{
						this.castingOption = ShadowCaster2D.ShadowCastingOptions.NoShadow;
					}
				}
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000150B8 File Offset: 0x000132B8
		private static int[] SetDefaultSortingLayers()
		{
			int layerCount = SortingLayer.layers.Length;
			int[] allLayers = new int[layerCount];
			for (int layerIndex = 0; layerIndex < layerCount; layerIndex++)
			{
				allLayers[layerIndex] = SortingLayer.layers[layerIndex].id;
			}
			return allLayers;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000150F4 File Offset: 0x000132F4
		internal bool IsLit(Light2D light)
		{
			Vector3 deltaPos;
			deltaPos.x = light.m_CachedPosition.x - this.boundingSphere.position.x;
			deltaPos.y = light.m_CachedPosition.y - this.boundingSphere.position.y;
			deltaPos.z = light.m_CachedPosition.z - this.boundingSphere.position.z;
			float num = Vector3.SqrMagnitude(deltaPos);
			float radiiLength = light.boundingSphere.radius + this.boundingSphere.radius;
			return num <= radiiLength * radiiLength;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00015190 File Offset: 0x00013390
		internal bool IsShadowedLayer(int layer)
		{
			return this.m_ApplyToSortingLayers != null && Array.IndexOf<int>(this.m_ApplyToSortingLayers, layer) >= 0;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000151B0 File Offset: 0x000133B0
		private void SetShadowShape(ShadowMesh2D shadowMesh)
		{
			this.m_ForceShadowMeshRebuild = false;
			if (this.m_ShadowCastingSource == ShadowCaster2D.ShadowCastingSources.ShapeEditor)
			{
				NativeArray<Vector3> nativePath = new NativeArray<Vector3>(this.m_ShapePath, Allocator.Temp);
				NativeArray<int> nativeIndices = new NativeArray<int>(2 * this.m_ShapePath.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
				int lastIndex = this.m_ShapePath.Length - 1;
				for (int i = 0; i < this.m_ShapePath.Length; i++)
				{
					int startingIndex = i << 1;
					nativeIndices[startingIndex] = lastIndex;
					nativeIndices[startingIndex + 1] = i;
					lastIndex = i;
				}
				shadowMesh.SetShapeWithLines(nativePath, nativeIndices, false);
				nativePath.Dispose();
				nativeIndices.Dispose();
			}
			if (this.m_ShadowCastingSource == ShadowCaster2D.ShadowCastingSources.ShapeProvider)
			{
				ShapeProviderUtility.PersistantDataCreated(this.m_ShadowShape2DProvider, this.m_ShadowShape2DComponent, shadowMesh);
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0001525C File Offset: 0x0001345C
		private void Awake()
		{
			if (this.m_ShadowCastingSource < ShadowCaster2D.ShadowCastingSources.None)
			{
				this.m_ShadowCastingSource = ShadowCaster2D.ShadowCastingSources.ShapeEditor;
			}
			Vector3 inverseScale = Vector3.zero;
			Vector3 relOffset = base.transform.position;
			if (base.transform.lossyScale.x != 0f && base.transform.lossyScale.y != 0f)
			{
				inverseScale = new Vector3(1f / base.transform.lossyScale.x, 1f / base.transform.lossyScale.y);
				relOffset = new Vector3(inverseScale.x * -base.transform.position.x, inverseScale.y * -base.transform.position.y);
			}
			if (this.m_ApplyToSortingLayers == null)
			{
				this.m_ApplyToSortingLayers = ShadowCaster2D.SetDefaultSortingLayers();
			}
			Bounds bounds = new Bounds(base.transform.position, Vector3.one);
			Renderer renderer = base.GetComponent<Renderer>();
			if (renderer != null)
			{
				bounds = renderer.bounds;
				this.m_SpriteMaterialCount = renderer.sharedMaterials.Length;
			}
			if (this.m_ShapePath == null || this.m_ShapePath.Length == 0)
			{
				this.m_ShapePath = new Vector3[]
				{
					relOffset + new Vector3(inverseScale.x * bounds.min.x, inverseScale.y * bounds.min.y),
					relOffset + new Vector3(inverseScale.x * bounds.min.x, inverseScale.y * bounds.max.y),
					relOffset + new Vector3(inverseScale.x * bounds.max.x, inverseScale.y * bounds.max.y),
					relOffset + new Vector3(inverseScale.x * bounds.max.x, inverseScale.y * bounds.min.y)
				};
			}
			if (this.m_ShadowMesh == null)
			{
				ShadowMesh2D newShadowMesh = new ShadowMesh2D();
				this.SetShadowShape(newShadowMesh);
				this.m_ShadowMesh = newShadowMesh;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00015494 File Offset: 0x00013694
		protected void OnEnable()
		{
			if (this.m_ShadowShape2DProvider != null)
			{
				this.m_ShadowShape2DProvider.Enabled(this.m_ShadowShape2DComponent);
			}
			this.m_ShadowCasterGroup = null;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000154B6 File Offset: 0x000136B6
		protected void OnDisable()
		{
			ShadowCasterGroup2DManager.RemoveFromShadowCasterGroup(this, this.m_ShadowCasterGroup);
			if (this.m_ShadowShape2DProvider != null)
			{
				this.m_ShadowShape2DProvider.Disabled(this.m_ShadowShape2DComponent);
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000154E0 File Offset: 0x000136E0
		public void Update()
		{
			Renderer renderer;
			this.m_HasRenderer = base.TryGetComponent<Renderer>(out renderer);
			bool rebuildMesh = LightUtility.CheckForChange((int)this.m_ShadowCastingSource, ref this.m_PreviousShadowCastingSource);
			rebuildMesh |= LightUtility.CheckForChange((int)this.edgeProcessing, ref this.m_PreviousEdgeProcessing);
			rebuildMesh |= this.edgeProcessing != ShadowCaster2D.EdgeProcessing.None && LightUtility.CheckForChange(this.trimEdge, ref this.m_PreviousTrimEdge);
			rebuildMesh |= this.m_ForceShadowMeshRebuild;
			if (this.m_ShadowCastingSource == ShadowCaster2D.ShadowCastingSources.ShapeEditor)
			{
				rebuildMesh |= LightUtility.CheckForChange(this.m_ShapePathHash, ref this.m_PreviousPathHash);
				if (rebuildMesh)
				{
					this.SetShadowShape(this.m_ShadowMesh);
				}
			}
			else if ((rebuildMesh || LightUtility.CheckForChange(this.m_ShadowShape2DComponent, ref this.m_PreviousShadowShape2DSource)) && this.m_ShadowShape2DComponent != null)
			{
				this.SetShadowShape(this.m_ShadowMesh);
			}
			this.m_PreviousShadowCasterGroup = this.m_ShadowCasterGroup;
			if (ShadowCasterGroup2DManager.AddToShadowCasterGroup(this, ref this.m_ShadowCasterGroup, ref this.m_Priority) && this.m_ShadowCasterGroup != null)
			{
				if (this.m_PreviousShadowCasterGroup == this)
				{
					ShadowCasterGroup2DManager.RemoveGroup(this);
				}
				ShadowCasterGroup2DManager.RemoveFromShadowCasterGroup(this, this.m_PreviousShadowCasterGroup);
				if (this.m_ShadowCasterGroup == this)
				{
					ShadowCasterGroup2DManager.AddGroup(this);
				}
			}
			if (LightUtility.CheckForChange(this.m_ShadowGroup, ref this.m_PreviousShadowGroup))
			{
				ShadowCasterGroup2DManager.RemoveGroup(this);
				ShadowCasterGroup2DManager.AddGroup(this);
			}
			if (LightUtility.CheckForChange(this.m_CastsShadows, ref this.m_PreviousCastsShadows))
			{
				ShadowCasterGroup2DManager.AddGroup(this);
			}
			if (this.m_ShadowMesh != null)
			{
				this.m_ShadowMesh.UpdateBoundingSphere(base.transform);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001565A File Offset: 0x0001385A
		public void OnBeforeSerialize()
		{
			this.m_ComponentVersion = ShadowCaster2D.ComponentVersions.Version_5;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00015664 File Offset: 0x00013864
		public void OnAfterDeserialize()
		{
			if (this.m_ComponentVersion < ShadowCaster2D.ComponentVersions.Version_2)
			{
				if (this.m_SelfShadows && this.m_CastsShadows)
				{
					this.m_CastingOption = ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow;
				}
				else if (this.m_SelfShadows)
				{
					this.m_CastingOption = ShadowCaster2D.ShadowCastingOptions.SelfShadow;
				}
				else if (this.m_CastsShadows)
				{
					this.m_CastingOption = ShadowCaster2D.ShadowCastingOptions.CastShadow;
				}
				else
				{
					this.m_CastingOption = ShadowCaster2D.ShadowCastingOptions.NoShadow;
				}
			}
			if (this.m_ComponentVersion < ShadowCaster2D.ComponentVersions.Version_3)
			{
				this.m_ShadowMesh = null;
				this.m_ForceShadowMeshRebuild = true;
			}
		}

		// Token: 0x04000259 RID: 601
		private const ShadowCaster2D.ComponentVersions k_CurrentComponentVersion = ShadowCaster2D.ComponentVersions.Version_5;

		// Token: 0x0400025A RID: 602
		[SerializeField]
		private ShadowCaster2D.ComponentVersions m_ComponentVersion;

		// Token: 0x0400025B RID: 603
		[SerializeField]
		private bool m_HasRenderer;

		// Token: 0x0400025C RID: 604
		[SerializeField]
		private bool m_UseRendererSilhouette = true;

		// Token: 0x0400025D RID: 605
		[SerializeField]
		private bool m_CastsShadows = true;

		// Token: 0x0400025E RID: 606
		[SerializeField]
		private bool m_SelfShadows;

		// Token: 0x0400025F RID: 607
		[Range(0f, 1f)]
		[SerializeField]
		private float m_AlphaCutoff = 0.1f;

		// Token: 0x04000260 RID: 608
		[SerializeField]
		private int[] m_ApplyToSortingLayers;

		// Token: 0x04000261 RID: 609
		[SerializeField]
		private Vector3[] m_ShapePath;

		// Token: 0x04000262 RID: 610
		[SerializeField]
		private int m_ShapePathHash;

		// Token: 0x04000263 RID: 611
		[SerializeField]
		private int m_InstanceId;

		// Token: 0x04000264 RID: 612
		[SerializeField]
		private Component m_ShadowShape2DComponent;

		// Token: 0x04000265 RID: 613
		[SerializeReference]
		private ShadowShape2DProvider m_ShadowShape2DProvider;

		// Token: 0x04000266 RID: 614
		[SerializeField]
		private ShadowCaster2D.ShadowCastingSources m_ShadowCastingSource = (ShadowCaster2D.ShadowCastingSources)(-1);

		// Token: 0x04000267 RID: 615
		[SerializeField]
		internal ShadowMesh2D m_ShadowMesh;

		// Token: 0x04000268 RID: 616
		[SerializeField]
		private ShadowCaster2D.ShadowCastingOptions m_CastingOption = ShadowCaster2D.ShadowCastingOptions.CastShadow;

		// Token: 0x04000269 RID: 617
		[SerializeField]
		internal float m_PreviousTrimEdge;

		// Token: 0x0400026A RID: 618
		[SerializeField]
		internal int m_PreviousEdgeProcessing;

		// Token: 0x0400026B RID: 619
		[SerializeField]
		internal int m_PreviousShadowCastingSource;

		// Token: 0x0400026C RID: 620
		[SerializeField]
		internal Component m_PreviousShadowShape2DSource;

		// Token: 0x0400026D RID: 621
		internal ShadowCasterGroup2D m_ShadowCasterGroup;

		// Token: 0x0400026E RID: 622
		internal ShadowCasterGroup2D m_PreviousShadowCasterGroup;

		// Token: 0x0400026F RID: 623
		internal bool m_ForceShadowMeshRebuild;

		// Token: 0x04000270 RID: 624
		private int m_PreviousShadowGroup;

		// Token: 0x04000271 RID: 625
		private bool m_PreviousCastsShadows = true;

		// Token: 0x04000272 RID: 626
		private int m_PreviousPathHash;

		// Token: 0x04000273 RID: 627
		private int m_SpriteMaterialCount;

		// Token: 0x04000274 RID: 628
		internal Vector3 m_CachedPosition;

		// Token: 0x04000275 RID: 629
		internal Vector3 m_CachedLossyScale;

		// Token: 0x04000276 RID: 630
		internal Quaternion m_CachedRotation;

		// Token: 0x04000277 RID: 631
		internal Matrix4x4 m_CachedShadowMatrix;

		// Token: 0x04000278 RID: 632
		internal Matrix4x4 m_CachedInverseShadowMatrix;

		// Token: 0x04000279 RID: 633
		internal Matrix4x4 m_CachedLocalToWorldMatrix;

		// Token: 0x0200006C RID: 108
		internal enum ComponentVersions
		{
			// Token: 0x0400027B RID: 635
			Version_Unserialized,
			// Token: 0x0400027C RID: 636
			Version_1,
			// Token: 0x0400027D RID: 637
			Version_2,
			// Token: 0x0400027E RID: 638
			Version_3,
			// Token: 0x0400027F RID: 639
			Version_4,
			// Token: 0x04000280 RID: 640
			Version_5
		}

		// Token: 0x0200006D RID: 109
		internal enum ShadowCastingSources
		{
			// Token: 0x04000282 RID: 642
			None,
			// Token: 0x04000283 RID: 643
			ShapeEditor,
			// Token: 0x04000284 RID: 644
			ShapeProvider
		}

		// Token: 0x0200006E RID: 110
		public enum ShadowCastingOptions
		{
			// Token: 0x04000286 RID: 646
			SelfShadow,
			// Token: 0x04000287 RID: 647
			CastShadow,
			// Token: 0x04000288 RID: 648
			CastAndSelfShadow,
			// Token: 0x04000289 RID: 649
			NoShadow
		}

		// Token: 0x0200006F RID: 111
		internal enum EdgeProcessing
		{
			// Token: 0x0400028B RID: 651
			None,
			// Token: 0x0400028C RID: 652
			Clipping
		}
	}
}
