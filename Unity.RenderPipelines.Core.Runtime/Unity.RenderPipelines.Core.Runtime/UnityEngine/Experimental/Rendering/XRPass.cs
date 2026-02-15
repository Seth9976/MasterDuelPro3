using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000012 RID: 18
	public class XRPass
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000038CD File Offset: 0x00001ACD
		public XRPass()
		{
			this.m_Views = new List<XRView>(2);
			this.m_OcclusionMesh = new XROcclusionMesh(this);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000038ED File Offset: 0x00001AED
		public static XRPass CreateDefault(XRPassCreateInfo createInfo)
		{
			XRPass xrpass = GenericPool<XRPass>.Get();
			xrpass.InitBase(createInfo);
			return xrpass;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000038FB File Offset: 0x00001AFB
		public virtual void Release()
		{
			GenericPool<XRPass>.Release(this);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003903 File Offset: 0x00001B03
		public bool enabled
		{
			get
			{
				return this.viewCount > 0;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool supportsFoveatedRendering
		{
			get
			{
				return this.enabled && this.foveatedRenderingInfo != IntPtr.Zero && XRSystem.foveatedRenderingCaps > FoveatedRenderingCaps.None;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003934 File Offset: 0x00001B34
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000393C File Offset: 0x00001B3C
		public bool copyDepth { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003945 File Offset: 0x00001B45
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000394D File Offset: 0x00001B4D
		public bool hasMotionVectorPass { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003956 File Offset: 0x00001B56
		public bool isFirstCameraPass
		{
			get
			{
				return this.multipassId == 0;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003961 File Offset: 0x00001B61
		public bool isLastCameraPass
		{
			get
			{
				return (this.multipassId == 1 && this.viewCount <= 1) || (this.multipassId == 0 && this.viewCount > 1) || (this.multipassId == 0 && this.viewCount == 0);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000399B File Offset: 0x00001B9B
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000039A3 File Offset: 0x00001BA3
		public int multipassId { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000039AC File Offset: 0x00001BAC
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000039B4 File Offset: 0x00001BB4
		public int cullingPassId { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000039BD File Offset: 0x00001BBD
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000039C5 File Offset: 0x00001BC5
		public RenderTargetIdentifier renderTarget { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000039CE File Offset: 0x00001BCE
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000039D6 File Offset: 0x00001BD6
		public RenderTextureDescriptor renderTargetDesc { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000039DF File Offset: 0x00001BDF
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000039E7 File Offset: 0x00001BE7
		public RenderTargetIdentifier motionVectorRenderTarget { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000039F0 File Offset: 0x00001BF0
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000039F8 File Offset: 0x00001BF8
		public RenderTextureDescriptor motionVectorRenderTargetDesc { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003A01 File Offset: 0x00001C01
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003A09 File Offset: 0x00001C09
		public ScriptableCullingParameters cullingParams { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003A12 File Offset: 0x00001C12
		public int viewCount
		{
			get
			{
				return this.m_Views.Count;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003A1F File Offset: 0x00001C1F
		public bool singlePassEnabled
		{
			get
			{
				return this.viewCount > 1;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003A2A File Offset: 0x00001C2A
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003A32 File Offset: 0x00001C32
		public IntPtr foveatedRenderingInfo { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003A3B File Offset: 0x00001C3B
		public bool isHDRDisplayOutputActive
		{
			get
			{
				HDROutputSettings hdrOutputSettings = XRSystem.GetActiveDisplay().hdrOutputSettings;
				return hdrOutputSettings != null && hdrOutputSettings.active;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003A52 File Offset: 0x00001C52
		public ColorGamut hdrDisplayOutputColorGamut
		{
			get
			{
				HDROutputSettings hdrOutputSettings = XRSystem.GetActiveDisplay().hdrOutputSettings;
				if (hdrOutputSettings == null)
				{
					return ColorGamut.sRGB;
				}
				return hdrOutputSettings.displayColorGamut;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003A6C File Offset: 0x00001C6C
		public HDROutputUtils.HDRDisplayInformation hdrDisplayOutputInformation
		{
			get
			{
				HDROutputSettings hdrOutputSettings = XRSystem.GetActiveDisplay().hdrOutputSettings;
				int num = ((hdrOutputSettings != null) ? hdrOutputSettings.maxFullFrameToneMapLuminance : (-1));
				HDROutputSettings hdrOutputSettings2 = XRSystem.GetActiveDisplay().hdrOutputSettings;
				int num2 = ((hdrOutputSettings2 != null) ? hdrOutputSettings2.maxToneMapLuminance : (-1));
				HDROutputSettings hdrOutputSettings3 = XRSystem.GetActiveDisplay().hdrOutputSettings;
				int num3 = ((hdrOutputSettings3 != null) ? hdrOutputSettings3.minToneMapLuminance : (-1));
				HDROutputSettings hdrOutputSettings4 = XRSystem.GetActiveDisplay().hdrOutputSettings;
				return new HDROutputUtils.HDRDisplayInformation(num, num2, num3, (hdrOutputSettings4 != null) ? hdrOutputSettings4.paperWhiteNits : 160f);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003ADA File Offset: 0x00001CDA
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003AE2 File Offset: 0x00001CE2
		public float occlusionMeshScale { get; private set; }

		// Token: 0x06000064 RID: 100 RVA: 0x00003AEB File Offset: 0x00001CEB
		public Matrix4x4 GetProjMatrix(int viewIndex = 0)
		{
			return this.m_Views[viewIndex].projMatrix;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003AFE File Offset: 0x00001CFE
		public Matrix4x4 GetViewMatrix(int viewIndex = 0)
		{
			return this.m_Views[viewIndex].viewMatrix;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003B11 File Offset: 0x00001D11
		public bool GetPrevViewValid(int viewIndex = 0)
		{
			return this.m_Views[viewIndex].isPrevViewMatrixValid;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003B24 File Offset: 0x00001D24
		public Matrix4x4 GetPrevViewMatrix(int viewIndex = 0)
		{
			return this.m_Views[viewIndex].prevViewMatrix;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003B37 File Offset: 0x00001D37
		public Rect GetViewport(int viewIndex = 0)
		{
			return this.m_Views[viewIndex].viewport;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003B4A File Offset: 0x00001D4A
		public Mesh GetOcclusionMesh(int viewIndex = 0)
		{
			return this.m_Views[viewIndex].occlusionMesh;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003B5D File Offset: 0x00001D5D
		public int GetTextureArraySlice(int viewIndex = 0)
		{
			return this.m_Views[viewIndex].textureArraySlice;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003B70 File Offset: 0x00001D70
		public void StartSinglePass(CommandBuffer cmd)
		{
			if (!this.enabled || !this.singlePassEnabled)
			{
				return;
			}
			if (this.viewCount > TextureXR.slices)
			{
				throw new NotImplementedException(string.Format("Invalid XR setup for single-pass, trying to render too many views! Max supported: {0}", TextureXR.slices));
			}
			if (SystemInfo.supportsMultiview)
			{
				cmd.EnableKeyword(in SinglepassKeywords.STEREO_MULTIVIEW_ON);
				return;
			}
			cmd.EnableKeyword(in SinglepassKeywords.STEREO_INSTANCING_ON);
			cmd.SetInstanceMultiplier((uint)this.viewCount);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003BDF File Offset: 0x00001DDF
		public void StartSinglePass(IRasterCommandBuffer cmd)
		{
			this.StartSinglePass((cmd as BaseCommandBuffer).m_WrappedCommandBuffer);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003BF2 File Offset: 0x00001DF2
		public void StopSinglePass(CommandBuffer cmd)
		{
			if (this.enabled && this.singlePassEnabled)
			{
				if (SystemInfo.supportsMultiview)
				{
					cmd.DisableKeyword(in SinglepassKeywords.STEREO_MULTIVIEW_ON);
					return;
				}
				cmd.DisableKeyword(in SinglepassKeywords.STEREO_INSTANCING_ON);
				cmd.SetInstanceMultiplier(1U);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003C29 File Offset: 0x00001E29
		public void StopSinglePass(BaseCommandBuffer cmd)
		{
			this.StopSinglePass(cmd.m_WrappedCommandBuffer);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003C37 File Offset: 0x00001E37
		public bool hasValidOcclusionMesh
		{
			get
			{
				return this.m_OcclusionMesh.hasValidOcclusionMesh;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003C44 File Offset: 0x00001E44
		public void RenderOcclusionMesh(CommandBuffer cmd, bool renderIntoTexture = false)
		{
			if (this.occlusionMeshScale > 0f)
			{
				this.m_OcclusionMesh.RenderOcclusionMesh(cmd, this.occlusionMeshScale, renderIntoTexture);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003C66 File Offset: 0x00001E66
		public void RenderOcclusionMesh(RasterCommandBuffer cmd, bool renderIntoTexture = false)
		{
			if (this.occlusionMeshScale > 0f)
			{
				this.m_OcclusionMesh.RenderOcclusionMesh(cmd.m_WrappedCommandBuffer, this.occlusionMeshScale, renderIntoTexture);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003C90 File Offset: 0x00001E90
		public void RenderDebugXRViewsFrustum()
		{
			for (int i = 0; i < this.m_Views.Count; i++)
			{
				XRView view = this.m_Views[i];
				Vector3[] corners = CoreUtils.CalculateViewSpaceCorners(view.projMatrix, 10f);
				Vector3 worldSpaceCameraPos = -view.viewMatrix.GetColumn(3);
				for (int j = 0; j < 4; j++)
				{
					Debug.DrawLine(worldSpaceCameraPos, view.viewMatrix.MultiplyPoint(corners[j]), (i == 0) ? Color.green : Color.red);
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003D2C File Offset: 0x00001F2C
		public Vector4 ApplyXRViewCenterOffset(Vector2 center)
		{
			Vector4 result = Vector4.zero;
			float centerDeltaX = 0.5f - center.x;
			float centerDeltaY = 0.5f - center.y;
			result.x = this.m_Views[0].eyeCenterUV.x - centerDeltaX;
			result.y = this.m_Views[0].eyeCenterUV.y - centerDeltaY;
			if (this.singlePassEnabled)
			{
				result.z = this.m_Views[1].eyeCenterUV.x - centerDeltaX;
				result.w = this.m_Views[1].eyeCenterUV.y - centerDeltaY;
			}
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003DDE File Offset: 0x00001FDE
		internal void AssignView(int viewId, XRView xrView)
		{
			if (viewId < 0 || viewId >= this.m_Views.Count)
			{
				throw new ArgumentOutOfRangeException("viewId");
			}
			this.m_Views[viewId] = xrView;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003E0A File Offset: 0x0000200A
		internal void AssignCullingParams(int cullingPassId, ScriptableCullingParameters cullingParams)
		{
			cullingParams.cullingOptions &= ~CullingOptions.Stereo;
			this.cullingPassId = cullingPassId;
			this.cullingParams = cullingParams;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E2A File Offset: 0x0000202A
		internal void UpdateCombinedOcclusionMesh()
		{
			this.m_OcclusionMesh.UpdateCombinedMesh();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003E38 File Offset: 0x00002038
		public void InitBase(XRPassCreateInfo createInfo)
		{
			this.m_Views.Clear();
			this.copyDepth = createInfo.copyDepth;
			this.multipassId = createInfo.multipassId;
			this.AssignCullingParams(createInfo.cullingPassId, createInfo.cullingParameters);
			this.renderTarget = new RenderTargetIdentifier(createInfo.renderTarget, 0, CubemapFace.Unknown, -1);
			this.renderTargetDesc = createInfo.renderTargetDesc;
			this.motionVectorRenderTarget = new RenderTargetIdentifier(createInfo.motionVectorRenderTarget, 0, CubemapFace.Unknown, -1);
			this.motionVectorRenderTargetDesc = createInfo.motionVectorRenderTargetDesc;
			this.hasMotionVectorPass = createInfo.hasMotionVectorPass;
			this.m_OcclusionMesh.SetMaterial(createInfo.occlusionMeshMaterial);
			this.occlusionMeshScale = createInfo.occlusionMeshScale;
			this.foveatedRenderingInfo = createInfo.foveatedRenderingInfo;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003EEF File Offset: 0x000020EF
		internal void AddView(XRView xrView)
		{
			if (this.m_Views.Count < TextureXR.slices)
			{
				this.m_Views.Add(xrView);
				return;
			}
			throw new NotImplementedException(string.Format("Invalid XR setup for single-pass, trying to add too many views! Max supported: {0}", TextureXR.slices));
		}

		// Token: 0x04000055 RID: 85
		private readonly List<XRView> m_Views;

		// Token: 0x04000056 RID: 86
		private readonly XROcclusionMesh m_OcclusionMesh;
	}
}
