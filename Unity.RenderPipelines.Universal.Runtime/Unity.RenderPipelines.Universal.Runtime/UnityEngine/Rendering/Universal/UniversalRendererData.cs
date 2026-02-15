using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B8 RID: 184
	[ReloadGroup]
	[ExcludeFromPreset]
	[Serializable]
	public class UniversalRendererData : ScriptableRendererData, ISerializationCallbackReceiver
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x0001217C File Offset: 0x0001037C
		protected override ScriptableRenderer Create()
		{
			if (!Application.isPlaying)
			{
				this.ReloadAllNullProperties();
			}
			return new UniversalRenderer(this);
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00012191 File Offset: 0x00010391
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00012199 File Offset: 0x00010399
		public LayerMask opaqueLayerMask
		{
			get
			{
				return this.m_OpaqueLayerMask;
			}
			set
			{
				base.SetDirty();
				this.m_OpaqueLayerMask = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000121A8 File Offset: 0x000103A8
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x000121B0 File Offset: 0x000103B0
		public LayerMask transparentLayerMask
		{
			get
			{
				return this.m_TransparentLayerMask;
			}
			set
			{
				base.SetDirty();
				this.m_TransparentLayerMask = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000121BF File Offset: 0x000103BF
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000121C7 File Offset: 0x000103C7
		public StencilStateData defaultStencilState
		{
			get
			{
				return this.m_DefaultStencilState;
			}
			set
			{
				base.SetDirty();
				this.m_DefaultStencilState = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000121D6 File Offset: 0x000103D6
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x000121DE File Offset: 0x000103DE
		public bool shadowTransparentReceive
		{
			get
			{
				return this.m_ShadowTransparentReceive;
			}
			set
			{
				base.SetDirty();
				this.m_ShadowTransparentReceive = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000121ED File Offset: 0x000103ED
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x000121F5 File Offset: 0x000103F5
		public RenderingMode renderingMode
		{
			get
			{
				return this.m_RenderingMode;
			}
			set
			{
				base.SetDirty();
				this.m_RenderingMode = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00012204 File Offset: 0x00010404
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0001220C File Offset: 0x0001040C
		public DepthPrimingMode depthPrimingMode
		{
			get
			{
				return this.m_DepthPrimingMode;
			}
			set
			{
				base.SetDirty();
				this.m_DepthPrimingMode = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001221B File Offset: 0x0001041B
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00012223 File Offset: 0x00010423
		public CopyDepthMode copyDepthMode
		{
			get
			{
				return this.m_CopyDepthMode;
			}
			set
			{
				base.SetDirty();
				this.m_CopyDepthMode = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00012232 File Offset: 0x00010432
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0001225D File Offset: 0x0001045D
		public DepthFormat depthAttachmentFormat
		{
			get
			{
				if (this.m_DepthAttachmentFormat != DepthFormat.Default && !SystemInfo.IsFormatSupported((GraphicsFormat)this.m_DepthAttachmentFormat, GraphicsFormatUsage.Render))
				{
					Debug.LogWarning("Selected Depth Attachment Format is not supported on this platform, falling back to Default");
					return DepthFormat.Default;
				}
				return this.m_DepthAttachmentFormat;
			}
			set
			{
				base.SetDirty();
				if (this.renderingMode == RenderingMode.Deferred && !GraphicsFormatUtility.IsStencilFormat((GraphicsFormat)value))
				{
					Debug.LogWarning("Depth format without stencil is not supported on Deferred renderer, falling back to Default");
					this.m_DepthAttachmentFormat = DepthFormat.Default;
					return;
				}
				this.m_DepthAttachmentFormat = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00012290 File Offset: 0x00010490
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x000122E1 File Offset: 0x000104E1
		public DepthFormat depthTextureFormat
		{
			get
			{
				if (this.m_DepthTextureFormat != DepthFormat.Default && !SystemInfo.IsFormatSupported((GraphicsFormat)this.m_DepthTextureFormat, GraphicsFormatUsage.Render))
				{
					Debug.LogWarning("Selected Depth Texture Format " + this.m_DepthTextureFormat.ToString() + " is not supported on this platform, falling back to Default");
					return DepthFormat.Default;
				}
				return this.m_DepthTextureFormat;
			}
			set
			{
				base.SetDirty();
				this.m_DepthTextureFormat = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x000122F0 File Offset: 0x000104F0
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x000122F8 File Offset: 0x000104F8
		public bool accurateGbufferNormals
		{
			get
			{
				return this.m_AccurateGbufferNormals;
			}
			set
			{
				base.SetDirty();
				this.m_AccurateGbufferNormals = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00012307 File Offset: 0x00010507
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0001230F File Offset: 0x0001050F
		public IntermediateTextureMode intermediateTextureMode
		{
			get
			{
				return this.m_IntermediateTextureMode;
			}
			set
			{
				base.SetDirty();
				this.m_IntermediateTextureMode = value;
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001231E File Offset: 0x0001051E
		protected override void OnEnable()
		{
			base.OnEnable();
			this.ReloadAllNullProperties();
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000217F File Offset: 0x0000037F
		private void ReloadAllNullProperties()
		{
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001232C File Offset: 0x0001052C
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.m_AssetVersion = 2;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00012335 File Offset: 0x00010535
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (this.m_AssetVersion <= 1)
			{
				this.m_CopyDepthMode = CopyDepthMode.AfterOpaques;
			}
			this.m_AssetVersion = 2;
		}

		// Token: 0x040003A5 RID: 933
		[Obsolete("Moved to UniversalRenderPipelineRuntimeXRResources on GraphicsSettings. #from(2023.3)", false)]
		public XRSystemData xrSystemData;

		// Token: 0x040003A6 RID: 934
		public PostProcessData postProcessData;

		// Token: 0x040003A7 RID: 935
		private const int k_LatestAssetVersion = 2;

		// Token: 0x040003A8 RID: 936
		[SerializeField]
		private int m_AssetVersion;

		// Token: 0x040003A9 RID: 937
		[SerializeField]
		private LayerMask m_OpaqueLayerMask = -1;

		// Token: 0x040003AA RID: 938
		[SerializeField]
		private LayerMask m_TransparentLayerMask = -1;

		// Token: 0x040003AB RID: 939
		[SerializeField]
		private StencilStateData m_DefaultStencilState = new StencilStateData
		{
			passOperation = StencilOp.Replace
		};

		// Token: 0x040003AC RID: 940
		[SerializeField]
		private bool m_ShadowTransparentReceive = true;

		// Token: 0x040003AD RID: 941
		[SerializeField]
		private RenderingMode m_RenderingMode;

		// Token: 0x040003AE RID: 942
		[SerializeField]
		private DepthPrimingMode m_DepthPrimingMode;

		// Token: 0x040003AF RID: 943
		[SerializeField]
		private CopyDepthMode m_CopyDepthMode = CopyDepthMode.AfterTransparents;

		// Token: 0x040003B0 RID: 944
		[SerializeField]
		private DepthFormat m_DepthAttachmentFormat;

		// Token: 0x040003B1 RID: 945
		[SerializeField]
		private DepthFormat m_DepthTextureFormat;

		// Token: 0x040003B2 RID: 946
		[SerializeField]
		private bool m_AccurateGbufferNormals;

		// Token: 0x040003B3 RID: 947
		[SerializeField]
		private IntermediateTextureMode m_IntermediateTextureMode = IntermediateTextureMode.Always;
	}
}
