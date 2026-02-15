using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200019A RID: 410
	public class RTHandleSystem : IDisposable
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002869B File Offset: 0x0002689B
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				return this.m_RTHandleProperties;
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000286A3 File Offset: 0x000268A3
		public RTHandleSystem()
		{
			this.m_AutoSizedRTs = new HashSet<RTHandle>();
			this.m_ResizeOnDemandRTs = new HashSet<RTHandle>();
			this.m_MaxWidths = 1;
			this.m_MaxHeights = 1;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000286CF File Offset: 0x000268CF
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000286D8 File Offset: 0x000268D8
		public void Initialize(int width, int height)
		{
			if (this.m_AutoSizedRTs.Count != 0)
			{
				string leakingResources = "Unreleased RTHandles:";
				foreach (RTHandle rt in this.m_AutoSizedRTs)
				{
					leakingResources = string.Format("{0}\n    {1}", leakingResources, rt.name);
				}
				Debug.LogError(string.Format("RTHandleSystem.Initialize should only be called once before allocating any Render Texture. This may be caused by an unreleased RTHandle resource.\n{0}\n", leakingResources));
			}
			this.m_MaxWidths = width;
			this.m_MaxHeights = height;
			this.m_HardwareDynamicResRequested = DynamicResolutionHandler.instance.RequestsHardwareDynamicResolution();
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00028778 File Offset: 0x00026978
		[Obsolete("useLegacyDynamicResControl is deprecated. Please use SetHardwareDynamicResolutionState() instead.")]
		public void Initialize(int width, int height, bool useLegacyDynamicResControl = false)
		{
			this.Initialize(width, height);
			if (useLegacyDynamicResControl)
			{
				this.m_HardwareDynamicResRequested = true;
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002878C File Offset: 0x0002698C
		public void Release(RTHandle rth)
		{
			if (rth != null)
			{
				rth.Release();
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00028797 File Offset: 0x00026997
		internal void Remove(RTHandle rth)
		{
			this.m_AutoSizedRTs.Remove(rth);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000287A6 File Offset: 0x000269A6
		public void ResetReferenceSize(int width, int height)
		{
			this.m_MaxWidths = width;
			this.m_MaxHeights = height;
			this.SetReferenceSize(width, height, true);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x000287BF File Offset: 0x000269BF
		public void SetReferenceSize(int width, int height)
		{
			this.SetReferenceSize(width, height, false);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x000287CC File Offset: 0x000269CC
		public void SetReferenceSize(int width, int height, bool reset)
		{
			this.m_RTHandleProperties.previousViewportSize = this.m_RTHandleProperties.currentViewportSize;
			this.m_RTHandleProperties.previousRenderTargetSize = this.m_RTHandleProperties.currentRenderTargetSize;
			Vector2 lastFrameMaxSize = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			width = Mathf.Max(width, 1);
			height = Mathf.Max(height, 1);
			bool sizeChanged = width > this.GetMaxWidth() || height > this.GetMaxHeight() || reset;
			if (sizeChanged)
			{
				this.Resize(width, height, sizeChanged);
			}
			this.m_RTHandleProperties.currentViewportSize = new Vector2Int(width, height);
			this.m_RTHandleProperties.currentRenderTargetSize = new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight());
			if (this.m_RTHandleProperties.previousViewportSize.x == 0)
			{
				this.m_RTHandleProperties.previousViewportSize = this.m_RTHandleProperties.currentViewportSize;
				this.m_RTHandleProperties.previousRenderTargetSize = this.m_RTHandleProperties.currentRenderTargetSize;
				lastFrameMaxSize = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			}
			Vector2 scales = this.CalculateRatioAgainstMaxSize(in this.m_RTHandleProperties.currentViewportSize);
			if (DynamicResolutionHandler.instance.HardwareDynamicResIsEnabled() && this.m_HardwareDynamicResRequested)
			{
				this.m_RTHandleProperties.rtHandleScale = new Vector4(scales.x, scales.y, this.m_RTHandleProperties.rtHandleScale.x, this.m_RTHandleProperties.rtHandleScale.y);
				return;
			}
			Vector2 scalePrevious = this.m_RTHandleProperties.previousViewportSize / lastFrameMaxSize;
			this.m_RTHandleProperties.rtHandleScale = new Vector4(scales.x, scales.y, scalePrevious.x, scalePrevious.y);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00028974 File Offset: 0x00026B74
		internal Vector2 CalculateRatioAgainstMaxSize(in Vector2Int viewportSize)
		{
			Vector2 maxSize = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			if (DynamicResolutionHandler.instance.HardwareDynamicResIsEnabled() && this.m_HardwareDynamicResRequested && viewportSize != DynamicResolutionHandler.instance.finalViewport)
			{
				Vector2 currentScale = viewportSize / DynamicResolutionHandler.instance.finalViewport;
				maxSize = DynamicResolutionHandler.instance.ApplyScalesOnSize(new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight()), currentScale);
			}
			Vector2Int vector2Int = viewportSize;
			float num = (float)vector2Int.x / maxSize.x;
			vector2Int = viewportSize;
			return new Vector2(num, (float)vector2Int.y / maxSize.y);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00028A34 File Offset: 0x00026C34
		public void SetHardwareDynamicResolutionState(bool enableHWDynamicRes)
		{
			if (enableHWDynamicRes != this.m_HardwareDynamicResRequested)
			{
				this.m_HardwareDynamicResRequested = enableHWDynamicRes;
				Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
				this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
				int i = 0;
				int c = this.m_AutoSizedRTsArray.Length;
				while (i < c)
				{
					RTHandle rth = this.m_AutoSizedRTsArray[i];
					RenderTexture renderTexture = rth.m_RT;
					if (renderTexture)
					{
						renderTexture.Release();
						renderTexture.useDynamicScale = this.m_HardwareDynamicResRequested && rth.m_EnableHWDynamicScale;
						renderTexture.Create();
					}
					i++;
				}
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00028ACC File Offset: 0x00026CCC
		internal void SwitchResizeMode(RTHandle rth, RTHandleSystem.ResizeMode mode)
		{
			if (!rth.useScaling)
			{
				return;
			}
			if (mode != RTHandleSystem.ResizeMode.Auto)
			{
				if (mode == RTHandleSystem.ResizeMode.OnDemand)
				{
					this.m_AutoSizedRTs.Remove(rth);
					this.m_ResizeOnDemandRTs.Add(rth);
					return;
				}
			}
			else
			{
				if (this.m_ResizeOnDemandRTs.Contains(rth))
				{
					this.DemandResize(rth);
				}
				this.m_ResizeOnDemandRTs.Remove(rth);
				this.m_AutoSizedRTs.Add(rth);
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00028B34 File Offset: 0x00026D34
		private void DemandResize(RTHandle rth)
		{
			RenderTexture rt = rth.m_RT;
			rth.referenceSize = new Vector2Int(this.m_MaxWidths, this.m_MaxHeights);
			Vector2Int scaledSize = rth.GetScaledSize(rth.referenceSize);
			scaledSize = Vector2Int.Max(Vector2Int.one, scaledSize);
			if (rt.width != scaledSize.x || rt.height != scaledSize.y)
			{
				rt.Release();
				rt.width = scaledSize.x;
				rt.height = scaledSize.y;
				rt.name = CoreUtils.GetRenderTargetAutoName(rt.width, rt.height, rt.volumeDepth, (rt.depthStencilFormat != GraphicsFormat.None) ? rt.depthStencilFormat : rt.graphicsFormat, rt.dimension, rth.m_Name, rt.useMipMap, rth.m_EnableMSAA, (MSAASamples)rt.antiAliasing, rt.useDynamicScale, rt.useDynamicScaleExplicit);
				rt.Create();
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00028C25 File Offset: 0x00026E25
		public int GetMaxWidth()
		{
			return this.m_MaxWidths;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00028C2D File Offset: 0x00026E2D
		public int GetMaxHeight()
		{
			return this.m_MaxHeights;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00028C38 File Offset: 0x00026E38
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
				this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
				int i = 0;
				int c = this.m_AutoSizedRTsArray.Length;
				while (i < c)
				{
					RTHandle rt = this.m_AutoSizedRTsArray[i];
					this.Release(rt);
					i++;
				}
				this.m_AutoSizedRTs.Clear();
				Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_ResizeOnDemandRTs.Count);
				this.m_ResizeOnDemandRTs.CopyTo(this.m_AutoSizedRTsArray);
				int j = 0;
				int c2 = this.m_AutoSizedRTsArray.Length;
				while (j < c2)
				{
					RTHandle rt2 = this.m_AutoSizedRTsArray[j];
					this.Release(rt2);
					j++;
				}
				this.m_ResizeOnDemandRTs.Clear();
				this.m_AutoSizedRTsArray = null;
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00028D04 File Offset: 0x00026F04
		private void Resize(int width, int height, bool sizeChanged)
		{
			this.m_MaxWidths = Math.Max(width, this.m_MaxWidths);
			this.m_MaxHeights = Math.Max(height, this.m_MaxHeights);
			Vector2Int maxSize = new Vector2Int(this.m_MaxWidths, this.m_MaxHeights);
			Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
			this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
			int i = 0;
			int c = this.m_AutoSizedRTsArray.Length;
			while (i < c)
			{
				RTHandle rth = this.m_AutoSizedRTsArray[i];
				rth.referenceSize = maxSize;
				RenderTexture renderTexture = rth.m_RT;
				renderTexture.Release();
				Vector2Int scaledSize = rth.GetScaledSize(maxSize);
				renderTexture.width = Mathf.Max(scaledSize.x, 1);
				renderTexture.height = Mathf.Max(scaledSize.y, 1);
				renderTexture.name = CoreUtils.GetRenderTargetAutoName(renderTexture.width, renderTexture.height, renderTexture.volumeDepth, (renderTexture.depthStencilFormat != GraphicsFormat.None) ? renderTexture.depthStencilFormat : renderTexture.graphicsFormat, renderTexture.dimension, rth.m_Name, renderTexture.useMipMap, rth.m_EnableMSAA, (MSAASamples)renderTexture.antiAliasing, renderTexture.useDynamicScale, renderTexture.useDynamicScaleExplicit);
				renderTexture.Create();
				i++;
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00028E4C File Offset: 0x0002704C
		public RTHandle Alloc(int width, int height, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			GraphicsFormat format = ((depthBufferBits != DepthBits.None) ? GraphicsFormatUtility.GetDepthStencilFormat((int)depthBufferBits) : colorFormat);
			return this.Alloc(width, height, format, wrapMode, wrapMode, wrapMode, slices, filterMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00028E98 File Offset: 0x00027098
		public RTHandle Alloc(int width, int height, GraphicsFormat format, int slices = 1, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			return this.Alloc(width, height, format, wrapMode, wrapMode, wrapMode, slices, filterMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00028ED4 File Offset: 0x000270D4
		public RTHandle Alloc(int width, int height, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV, TextureWrapMode wrapModeW = TextureWrapMode.Repeat, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			GraphicsFormat format = ((depthBufferBits != DepthBits.None) ? GraphicsFormatUtility.GetDepthStencilFormat((int)depthBufferBits) : colorFormat);
			return this.Alloc(width, height, format, wrapModeU, wrapModeV, wrapModeW, slices, filterMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00028F20 File Offset: 0x00027120
		public RTHandle Alloc(int width, int height, GraphicsFormat format, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV, TextureWrapMode wrapModeW = TextureWrapMode.Repeat, int slices = 1, FilterMode filterMode = FilterMode.Point, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			RenderTexture rt = this.CreateRenderTexture(width, height, format, slices, filterMode, wrapModeU, wrapModeV, wrapModeW, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetRenderTexture(rt, true);
			rthandle.useScaling = false;
			rthandle.m_EnableRandomWrite = enableRandomWrite;
			rthandle.m_EnableMSAA = msaaSamples != MSAASamples.None;
			rthandle.m_EnableHWDynamicScale = useDynamicScale;
			rthandle.m_Name = name;
			rthandle.referenceSize = new Vector2Int(width, height);
			return rthandle;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00028FA8 File Offset: 0x000271A8
		private RenderTexture CreateRenderTexture(int width, int height, GraphicsFormat format, int slices, FilterMode filterMode, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV, TextureWrapMode wrapModeW, TextureDimension dimension, bool enableRandomWrite, bool useMipMap, bool autoGenerateMips, bool isShadowMap, int anisoLevel, float mipMapBias, MSAASamples msaaSamples, bool bindTextureMS, bool useDynamicScale, bool useDynamicScaleExplicit, RenderTextureMemoryless memoryless, VRTextureUsage vrUsage, string name)
		{
			bool enableMSAA = msaaSamples != MSAASamples.None;
			if (!enableMSAA && bindTextureMS)
			{
				Debug.LogWarning("RTHandle allocated without MSAA but with bindMS set to true, forcing bindMS to false.");
				bindTextureMS = false;
			}
			if (enableMSAA && enableRandomWrite)
			{
				Debug.LogWarning("RTHandle that is MSAA-enabled cannot allocate MSAA RT with 'enableRandomWrite = true'.");
				enableRandomWrite = false;
			}
			bool isDepthStencilFormat = GraphicsFormatUtility.IsDepthStencilFormat(format);
			ShadowSamplingMode shadowSamplingMode = ShadowSamplingMode.None;
			GraphicsFormat depthStencilFormat;
			GraphicsFormat colorFormat;
			GraphicsFormat stencilFormat;
			string fullName;
			if (isShadowMap)
			{
				int depthBits = GraphicsFormatUtility.GetDepthBits(format);
				if (depthBits < 16)
				{
					depthBits = 16;
				}
				depthStencilFormat = GraphicsFormatUtility.GetDepthStencilFormat(depthBits, 0);
				colorFormat = GraphicsFormat.None;
				stencilFormat = GraphicsFormat.None;
				shadowSamplingMode = ShadowSamplingMode.CompareDepths;
				fullName = CoreUtils.GetRenderTargetAutoName(width, height, slices, RenderTextureFormat.Shadowmap, name, useMipMap, enableMSAA, msaaSamples);
			}
			else if (isDepthStencilFormat)
			{
				colorFormat = GraphicsFormat.None;
				depthStencilFormat = format;
				stencilFormat = this.GetStencilFormat(format);
				fullName = CoreUtils.GetRenderTargetAutoName(width, height, slices, format, dimension, name, useMipMap, enableMSAA, msaaSamples, useDynamicScale, useDynamicScaleExplicit);
			}
			else
			{
				colorFormat = format;
				depthStencilFormat = GraphicsFormat.None;
				stencilFormat = GraphicsFormat.None;
				fullName = CoreUtils.GetRenderTargetAutoName(width, height, slices, format, dimension, name, useMipMap, enableMSAA, msaaSamples, useDynamicScale, useDynamicScaleExplicit);
			}
			RenderTexture renderTexture = new RenderTexture(new RenderTextureDescriptor(width, height, colorFormat, depthStencilFormat)
			{
				msaaSamples = (int)msaaSamples,
				volumeDepth = slices,
				stencilFormat = stencilFormat,
				dimension = dimension,
				shadowSamplingMode = shadowSamplingMode,
				vrUsage = vrUsage,
				memoryless = memoryless,
				useMipMap = useMipMap,
				autoGenerateMips = autoGenerateMips,
				enableRandomWrite = enableRandomWrite,
				bindMS = bindTextureMS,
				useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
				useDynamicScaleExplicit = (this.m_HardwareDynamicResRequested && useDynamicScaleExplicit)
			});
			renderTexture.name = fullName;
			renderTexture.anisoLevel = anisoLevel;
			renderTexture.mipMapBias = mipMapBias;
			renderTexture.hideFlags = HideFlags.HideAndDontSave;
			renderTexture.filterMode = filterMode;
			renderTexture.wrapModeU = wrapModeU;
			renderTexture.wrapModeV = wrapModeV;
			renderTexture.wrapModeW = wrapModeW;
			renderTexture.Create();
			return renderTexture;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00029154 File Offset: 0x00027354
		public RTHandle Alloc(int width, int height, RTHandleAllocInfo info)
		{
			bool isShadowMap = false;
			bool useDynamicScaleExplicit = false;
			RenderTexture rt = this.CreateRenderTexture(width, height, info.format, info.slices, info.filterMode, info.wrapModeU, info.wrapModeV, info.wrapModeW, info.dimension, info.enableRandomWrite, info.useMipMap, info.autoGenerateMips, isShadowMap, info.anisoLevel, info.mipMapBias, info.msaaSamples, info.bindTextureMS, info.useDynamicScale, useDynamicScaleExplicit, info.memoryless, info.vrUsage, info.name);
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetRenderTexture(rt, true);
			rthandle.useScaling = false;
			rthandle.m_EnableRandomWrite = info.enableRandomWrite;
			rthandle.m_EnableMSAA = info.msaaSamples != MSAASamples.None;
			rthandle.m_EnableHWDynamicScale = info.useDynamicScale;
			rthandle.m_Name = info.name;
			rthandle.referenceSize = new Vector2Int(width, height);
			return rthandle;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002924A File Offset: 0x0002744A
		public Vector2Int CalculateDimensions(Vector2 scaleFactor)
		{
			return new Vector2Int(Mathf.Max(Mathf.RoundToInt(scaleFactor.x * (float)this.GetMaxWidth()), 1), Mathf.Max(Mathf.RoundToInt(scaleFactor.y * (float)this.GetMaxHeight()), 1));
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00029284 File Offset: 0x00027484
		public RTHandle Alloc(Vector2 scaleFactor, GraphicsFormat format, int slices = 1, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			Vector2Int actualDimensions = this.CalculateDimensions(scaleFactor);
			RTHandle rthandle = this.AllocAutoSizedRenderTexture(actualDimensions.x, actualDimensions.y, slices, format, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
			rthandle.referenceSize = actualDimensions;
			rthandle.scaleFactor = scaleFactor;
			return rthandle;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000292E0 File Offset: 0x000274E0
		public RTHandle Alloc(Vector2 scaleFactor, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			GraphicsFormat format = ((depthBufferBits != DepthBits.None) ? GraphicsFormatUtility.GetDepthStencilFormat((int)depthBufferBits) : colorFormat);
			return this.Alloc(scaleFactor, format, slices, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00029324 File Offset: 0x00027524
		public RTHandle Alloc(Vector2 scaleFactor, RTHandleAllocInfo info)
		{
			int width = Mathf.Max(Mathf.RoundToInt(scaleFactor.x * (float)this.GetMaxWidth()), 1);
			int height = Mathf.Max(Mathf.RoundToInt(scaleFactor.y * (float)this.GetMaxHeight()), 1);
			RTHandle rthandle = this.AllocAutoSizedRenderTexture(width, height, info);
			rthandle.referenceSize = new Vector2Int(width, height);
			rthandle.scaleFactor = scaleFactor;
			return rthandle;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00029384 File Offset: 0x00027584
		public Vector2Int CalculateDimensions(ScaleFunc scaleFunc)
		{
			Vector2Int scaleFactor = scaleFunc(new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight()));
			return new Vector2Int(Mathf.Max(scaleFactor.x, 1), Mathf.Max(scaleFactor.y, 1));
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x000293C8 File Offset: 0x000275C8
		public RTHandle Alloc(ScaleFunc scaleFunc, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			GraphicsFormat format = ((depthBufferBits != DepthBits.None) ? GraphicsFormatUtility.GetDepthStencilFormat((int)depthBufferBits) : colorFormat);
			return this.Alloc(scaleFunc, format, slices, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002940C File Offset: 0x0002760C
		public RTHandle Alloc(ScaleFunc scaleFunc, GraphicsFormat format, int slices = 1, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			Vector2Int actualDimensions = this.CalculateDimensions(scaleFunc);
			RTHandle rthandle = this.AllocAutoSizedRenderTexture(actualDimensions.x, actualDimensions.y, slices, format, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
			rthandle.referenceSize = actualDimensions;
			rthandle.scaleFunc = scaleFunc;
			return rthandle;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00029468 File Offset: 0x00027668
		public RTHandle Alloc(ScaleFunc scaleFunc, RTHandleAllocInfo info)
		{
			Vector2Int scaleFactor = scaleFunc(new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight()));
			int width = Mathf.Max(scaleFactor.x, 1);
			int height = Mathf.Max(scaleFactor.y, 1);
			RTHandle rthandle = this.AllocAutoSizedRenderTexture(width, height, info);
			rthandle.referenceSize = new Vector2Int(width, height);
			rthandle.scaleFunc = scaleFunc;
			return rthandle;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000294C8 File Offset: 0x000276C8
		private RTHandle AllocAutoSizedRenderTexture(int width, int height, int slices, GraphicsFormat format, FilterMode filterMode, TextureWrapMode wrapMode, TextureDimension dimension, bool enableRandomWrite, bool useMipMap, bool autoGenerateMips, bool isShadowMap, int anisoLevel, float mipMapBias, MSAASamples msaaSamples, bool bindTextureMS, bool useDynamicScale, bool useDynamicScaleExplicit, RenderTextureMemoryless memoryless, VRTextureUsage vrUsage, string name)
		{
			RenderTexture rt = this.CreateRenderTexture(width, height, format, slices, filterMode, wrapMode, wrapMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
			RTHandle rth = new RTHandle(this);
			rth.SetRenderTexture(rt, true);
			rth.m_EnableMSAA = msaaSamples != MSAASamples.None;
			rth.m_EnableRandomWrite = enableRandomWrite;
			rth.useScaling = true;
			rth.m_EnableHWDynamicScale = useDynamicScale;
			rth.m_Name = name;
			this.m_AutoSizedRTs.Add(rth);
			return rth;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00029550 File Offset: 0x00027750
		private RTHandle AllocAutoSizedRenderTexture(int width, int height, RTHandleAllocInfo info)
		{
			bool isShadowMap = false;
			bool useDynamicScaleExplicit = false;
			RenderTexture rt = this.CreateRenderTexture(width, height, info.format, info.slices, info.filterMode, info.wrapModeU, info.wrapModeV, info.wrapModeW, info.dimension, info.enableRandomWrite, info.useMipMap, info.autoGenerateMips, isShadowMap, info.anisoLevel, info.mipMapBias, info.msaaSamples, info.bindTextureMS, info.useDynamicScale, useDynamicScaleExplicit, info.memoryless, info.vrUsage, info.name);
			RTHandle rth = new RTHandle(this);
			rth.SetRenderTexture(rt, true);
			rth.m_EnableMSAA = info.msaaSamples != MSAASamples.None;
			rth.m_EnableRandomWrite = info.enableRandomWrite;
			rth.useScaling = true;
			rth.m_EnableHWDynamicScale = info.useDynamicScale;
			rth.m_Name = info.name;
			this.m_AutoSizedRTs.Add(rth);
			return rth;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00029648 File Offset: 0x00027848
		public RTHandle Alloc(RenderTexture texture, bool transferOwnership = false)
		{
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetRenderTexture(texture, transferOwnership);
			rthandle.m_EnableMSAA = false;
			rthandle.m_EnableRandomWrite = false;
			rthandle.useScaling = false;
			rthandle.m_EnableHWDynamicScale = false;
			rthandle.m_Name = texture.name;
			return rthandle;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00029680 File Offset: 0x00027880
		public RTHandle Alloc(Texture texture)
		{
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetTexture(texture);
			rthandle.m_EnableMSAA = false;
			rthandle.m_EnableRandomWrite = false;
			rthandle.useScaling = false;
			rthandle.m_EnableHWDynamicScale = false;
			rthandle.m_Name = texture.name;
			return rthandle;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000296B7 File Offset: 0x000278B7
		public RTHandle Alloc(RenderTargetIdentifier texture)
		{
			return this.Alloc(texture, "");
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000296C5 File Offset: 0x000278C5
		public RTHandle Alloc(RenderTargetIdentifier texture, string name)
		{
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetTexture(texture);
			rthandle.m_EnableMSAA = false;
			rthandle.m_EnableRandomWrite = false;
			rthandle.useScaling = false;
			rthandle.m_EnableHWDynamicScale = false;
			rthandle.m_Name = name;
			return rthandle;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000296F7 File Offset: 0x000278F7
		private static RTHandle Alloc(RTHandle tex)
		{
			Debug.LogError("Allocation a RTHandle from another one is forbidden.");
			return null;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00029704 File Offset: 0x00027904
		internal string DumpRTInfo()
		{
			string result = "";
			Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
			this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
			int i = 0;
			int c = this.m_AutoSizedRTsArray.Length;
			while (i < c)
			{
				RenderTexture rt = this.m_AutoSizedRTsArray[i].rt;
				result = string.Format("{0}\nRT ({1})\t Format: {2} W: {3} H {4}\n", new object[] { result, i, rt.format, rt.width, rt.height });
				i++;
			}
			return result;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000297AA File Offset: 0x000279AA
		private GraphicsFormat GetStencilFormat(GraphicsFormat depthStencilFormat)
		{
			if (!GraphicsFormatUtility.IsStencilFormat(depthStencilFormat) || !SystemInfo.IsFormatSupported(GraphicsFormat.R8_UInt, GraphicsFormatUsage.StencilSampling))
			{
				return GraphicsFormat.None;
			}
			return GraphicsFormat.R8_UInt;
		}

		// Token: 0x040007DD RID: 2013
		private bool m_HardwareDynamicResRequested;

		// Token: 0x040007DE RID: 2014
		private HashSet<RTHandle> m_AutoSizedRTs;

		// Token: 0x040007DF RID: 2015
		private RTHandle[] m_AutoSizedRTsArray;

		// Token: 0x040007E0 RID: 2016
		private HashSet<RTHandle> m_ResizeOnDemandRTs;

		// Token: 0x040007E1 RID: 2017
		private RTHandleProperties m_RTHandleProperties;

		// Token: 0x040007E2 RID: 2018
		private int m_MaxWidths;

		// Token: 0x040007E3 RID: 2019
		private int m_MaxHeights;

		// Token: 0x0200019B RID: 411
		internal enum ResizeMode
		{
			// Token: 0x040007E5 RID: 2021
			Auto,
			// Token: 0x040007E6 RID: 2022
			OnDemand
		}
	}
}
