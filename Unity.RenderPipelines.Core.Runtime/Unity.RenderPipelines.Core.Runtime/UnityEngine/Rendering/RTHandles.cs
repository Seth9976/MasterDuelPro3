using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200019C RID: 412
	public static class RTHandles
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x000297C6 File Offset: 0x000279C6
		public static int maxWidth
		{
			get
			{
				return RTHandles.s_DefaultInstance.GetMaxWidth();
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x000297D2 File Offset: 0x000279D2
		public static int maxHeight
		{
			get
			{
				return RTHandles.s_DefaultInstance.GetMaxHeight();
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x000297DE File Offset: 0x000279DE
		public static RTHandleProperties rtHandleProperties
		{
			get
			{
				return RTHandles.s_DefaultInstance.rtHandleProperties;
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000297EA File Offset: 0x000279EA
		public static Vector2Int CalculateDimensions(Vector2 scaleFactor)
		{
			return RTHandles.s_DefaultInstance.CalculateDimensions(scaleFactor);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000297F7 File Offset: 0x000279F7
		public static Vector2Int CalculateDimensions(ScaleFunc scaleFunc)
		{
			return RTHandles.s_DefaultInstance.CalculateDimensions(scaleFunc);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00029804 File Offset: 0x00027A04
		public static RTHandle Alloc(int width, int height, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(width, height, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00029844 File Offset: 0x00027A44
		public static RTHandle Alloc(int width, int height, GraphicsFormat format, int slices = 1, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(width, height, format, slices, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00029880 File Offset: 0x00027A80
		public static RTHandle Alloc(int width, int height, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV, TextureWrapMode wrapModeW = TextureWrapMode.Repeat, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(width, height, wrapModeU, wrapModeV, wrapModeW, slices, depthBufferBits, colorFormat, filterMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000298C1 File Offset: 0x00027AC1
		public static RTHandle Alloc(int width, int height, RTHandleAllocInfo info)
		{
			return RTHandles.s_DefaultInstance.Alloc(width, height, info);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000298D0 File Offset: 0x00027AD0
		public static RTHandle Alloc(in RenderTextureDescriptor descriptor, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			RenderTextureDescriptor renderTextureDescriptor = descriptor;
			GraphicsFormat format = RTHandles.GetFormat(renderTextureDescriptor.graphicsFormat, descriptor.depthStencilFormat);
			RTHandleSystem rthandleSystem = RTHandles.s_DefaultInstance;
			int width = descriptor.width;
			int height = descriptor.height;
			GraphicsFormat graphicsFormat = format;
			int volumeDepth = descriptor.volumeDepth;
			TextureDimension dimension = descriptor.dimension;
			renderTextureDescriptor = descriptor;
			bool enableRandomWrite = renderTextureDescriptor.enableRandomWrite;
			renderTextureDescriptor = descriptor;
			bool useMipMap = renderTextureDescriptor.useMipMap;
			renderTextureDescriptor = descriptor;
			bool autoGenerateMips = renderTextureDescriptor.autoGenerateMips;
			MSAASamples msaaSamples = (MSAASamples)descriptor.msaaSamples;
			renderTextureDescriptor = descriptor;
			bool bindMS = renderTextureDescriptor.bindMS;
			renderTextureDescriptor = descriptor;
			bool useDynamicScale = renderTextureDescriptor.useDynamicScale;
			renderTextureDescriptor = descriptor;
			return rthandleSystem.Alloc(width, height, graphicsFormat, volumeDepth, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindMS, useDynamicScale, renderTextureDescriptor.useDynamicScaleExplicit, descriptor.memoryless, descriptor.vrUsage, name);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00029989 File Offset: 0x00027B89
		internal static GraphicsFormat GetFormat(GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat)
		{
			if (depthStencilFormat != GraphicsFormat.None)
			{
				return depthStencilFormat;
			}
			return colorFormat;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00029994 File Offset: 0x00027B94
		public static RTHandle Alloc(Vector2 scaleFactor, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(scaleFactor, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000299D0 File Offset: 0x00027BD0
		public static RTHandle Alloc(Vector2 scaleFactor, GraphicsFormat format, int slices = 1, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(scaleFactor, format, slices, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00029A0C File Offset: 0x00027C0C
		public static RTHandle Alloc(Vector2 scaleFactor, in RenderTextureDescriptor descriptor, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			RenderTextureDescriptor renderTextureDescriptor = descriptor;
			GraphicsFormat format = RTHandles.GetFormat(renderTextureDescriptor.graphicsFormat, descriptor.depthStencilFormat);
			RTHandleSystem rthandleSystem = RTHandles.s_DefaultInstance;
			GraphicsFormat graphicsFormat = format;
			int volumeDepth = descriptor.volumeDepth;
			TextureDimension dimension = descriptor.dimension;
			renderTextureDescriptor = descriptor;
			bool enableRandomWrite = renderTextureDescriptor.enableRandomWrite;
			renderTextureDescriptor = descriptor;
			bool useMipMap = renderTextureDescriptor.useMipMap;
			renderTextureDescriptor = descriptor;
			bool autoGenerateMips = renderTextureDescriptor.autoGenerateMips;
			MSAASamples msaaSamples = (MSAASamples)descriptor.msaaSamples;
			renderTextureDescriptor = descriptor;
			bool bindMS = renderTextureDescriptor.bindMS;
			renderTextureDescriptor = descriptor;
			bool useDynamicScale = renderTextureDescriptor.useDynamicScale;
			renderTextureDescriptor = descriptor;
			return rthandleSystem.Alloc(scaleFactor, graphicsFormat, volumeDepth, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindMS, useDynamicScale, renderTextureDescriptor.useDynamicScaleExplicit, descriptor.memoryless, descriptor.vrUsage, name);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00029ABB File Offset: 0x00027CBB
		public static RTHandle Alloc(Vector2 scaleFactor, RTHandleAllocInfo info)
		{
			return RTHandles.s_DefaultInstance.Alloc(scaleFactor, info);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00029ACC File Offset: 0x00027CCC
		public static RTHandle Alloc(ScaleFunc scaleFunc, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(scaleFunc, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00029B08 File Offset: 0x00027D08
		public static RTHandle Alloc(ScaleFunc scaleFunc, GraphicsFormat format, int slices = 1, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, bool useDynamicScaleExplicit = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(scaleFunc, format, slices, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, useDynamicScaleExplicit, memoryless, vrUsage, name);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00029B44 File Offset: 0x00027D44
		public static RTHandle Alloc(ScaleFunc scaleFunc, in RenderTextureDescriptor descriptor, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			RenderTextureDescriptor renderTextureDescriptor = descriptor;
			GraphicsFormat format = RTHandles.GetFormat(renderTextureDescriptor.graphicsFormat, descriptor.depthStencilFormat);
			RTHandleSystem rthandleSystem = RTHandles.s_DefaultInstance;
			GraphicsFormat graphicsFormat = format;
			int volumeDepth = descriptor.volumeDepth;
			TextureDimension dimension = descriptor.dimension;
			renderTextureDescriptor = descriptor;
			bool enableRandomWrite = renderTextureDescriptor.enableRandomWrite;
			renderTextureDescriptor = descriptor;
			bool useMipMap = renderTextureDescriptor.useMipMap;
			renderTextureDescriptor = descriptor;
			bool autoGenerateMips = renderTextureDescriptor.autoGenerateMips;
			MSAASamples msaaSamples = (MSAASamples)descriptor.msaaSamples;
			renderTextureDescriptor = descriptor;
			bool bindMS = renderTextureDescriptor.bindMS;
			renderTextureDescriptor = descriptor;
			bool useDynamicScale = renderTextureDescriptor.useDynamicScale;
			renderTextureDescriptor = descriptor;
			return rthandleSystem.Alloc(scaleFunc, graphicsFormat, volumeDepth, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindMS, useDynamicScale, renderTextureDescriptor.useDynamicScaleExplicit, descriptor.memoryless, descriptor.vrUsage, name);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00029BF3 File Offset: 0x00027DF3
		public static RTHandle Alloc(ScaleFunc scaleFunc, RTHandleAllocInfo info)
		{
			return RTHandles.s_DefaultInstance.Alloc(scaleFunc, info);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00029C01 File Offset: 0x00027E01
		public static RTHandle Alloc(Texture tex)
		{
			return RTHandles.s_DefaultInstance.Alloc(tex);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00029C0E File Offset: 0x00027E0E
		public static RTHandle Alloc(RenderTexture tex, bool transferOwnership = false)
		{
			return RTHandles.s_DefaultInstance.Alloc(tex, transferOwnership);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00029C1C File Offset: 0x00027E1C
		public static RTHandle Alloc(RenderTargetIdentifier tex)
		{
			return RTHandles.s_DefaultInstance.Alloc(tex);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00029C29 File Offset: 0x00027E29
		public static RTHandle Alloc(RenderTargetIdentifier tex, string name)
		{
			return RTHandles.s_DefaultInstance.Alloc(tex, name);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000296F7 File Offset: 0x000278F7
		private static RTHandle Alloc(RTHandle tex)
		{
			Debug.LogError("Allocation a RTHandle from another one is forbidden.");
			return null;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00029C37 File Offset: 0x00027E37
		public static void Initialize(int width, int height)
		{
			RTHandles.s_DefaultInstance.Initialize(width, height);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00029C45 File Offset: 0x00027E45
		[Obsolete("useLegacyDynamicResControl is deprecated. Please use SetHardwareDynamicResolutionState() instead.")]
		public static void Initialize(int width, int height, bool useLegacyDynamicResControl = false)
		{
			RTHandles.s_DefaultInstance.Initialize(width, height, useLegacyDynamicResControl);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00029C54 File Offset: 0x00027E54
		public static void Release(RTHandle rth)
		{
			RTHandles.s_DefaultInstance.Release(rth);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00029C61 File Offset: 0x00027E61
		public static void SetHardwareDynamicResolutionState(bool hwDynamicResRequested)
		{
			RTHandles.s_DefaultInstance.SetHardwareDynamicResolutionState(hwDynamicResRequested);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00029C6E File Offset: 0x00027E6E
		public static void SetReferenceSize(int width, int height)
		{
			RTHandles.s_DefaultInstance.SetReferenceSize(width, height);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00029C7C File Offset: 0x00027E7C
		public static void ResetReferenceSize(int width, int height)
		{
			RTHandles.s_DefaultInstance.ResetReferenceSize(width, height);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00029C8C File Offset: 0x00027E8C
		public static Vector2 CalculateRatioAgainstMaxSize(int width, int height)
		{
			RTHandleSystem rthandleSystem = RTHandles.s_DefaultInstance;
			Vector2Int vector2Int = new Vector2Int(width, height);
			return rthandleSystem.CalculateRatioAgainstMaxSize(in vector2Int);
		}

		// Token: 0x040007E7 RID: 2023
		private static RTHandleSystem s_DefaultInstance = new RTHandleSystem();
	}
}
