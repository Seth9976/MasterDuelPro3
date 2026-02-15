using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000DB RID: 219
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	[NativeHeader("Runtime/Camera/LightProbeProxyVolume.h")]
	[NativeHeader("Runtime/Graphics/CopyTexture.h")]
	[NativeHeader("Runtime/Graphics/ColorGamut.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	public class Graphics
	{
		// Token: 0x0600058F RID: 1423
		[FreeFunction("GraphicsScripting::GetMaxDrawMeshInstanceCount", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetMaxDrawMeshInstanceCount();

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000590 RID: 1424
		// (set) Token: 0x06000591 RID: 1425
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern GraphicsTier activeTier
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000592 RID: 1426
		[NativeMethod(Name = "GetPreserveFramebufferAlpha")]
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetPreserveFramebufferAlpha();

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000C408 File Offset: 0x0000A608
		public static bool preserveFramebufferAlpha
		{
			get
			{
				return Graphics.GetPreserveFramebufferAlpha();
			}
		}

		// Token: 0x06000594 RID: 1428
		[NativeMethod(Name = "GetMinOpenGLESVersion")]
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern OpenGLESVersion GetMinOpenGLESVersion();

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000C420 File Offset: 0x0000A620
		public static OpenGLESVersion minOpenGLESVersion
		{
			get
			{
				return Graphics.GetMinOpenGLESVersion();
			}
		}

		// Token: 0x06000596 RID: 1430
		[FreeFunction("GraphicsScripting::SetNullRT")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetNullRT();

		// Token: 0x06000597 RID: 1431 RVA: 0x0000C438 File Offset: 0x0000A638
		[NativeMethod(Name = "GraphicsScripting::SetRTSimple", IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_SetRTSimple(RenderBuffer color, RenderBuffer depth, int mip, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetRTSimple_Injected(ref color, ref depth, mip, face, depthSlice);
		}

		// Token: 0x06000598 RID: 1432
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearRandomWriteTargets();

		// Token: 0x06000599 RID: 1433 RVA: 0x0000C454 File Offset: 0x0000A654
		[FreeFunction("CopyTexture")]
		private static void CopyTexture_Slice(Texture src, int srcElement, int srcMip, Texture dst, int dstElement, int dstMip)
		{
			Graphics.CopyTexture_Slice_Injected(Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, Object.MarshalledUnityObject.Marshal<Texture>(dst), dstElement, dstMip);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000C478 File Offset: 0x0000A678
		[FreeFunction("CopyTextureRegion")]
		private static void CopyTexture_Region(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY)
		{
			Graphics.CopyTexture_Region_Injected(Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, Object.MarshalledUnityObject.Marshal<Texture>(dst), dstElement, dstMip, dstX, dstY);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		[FreeFunction("GraphicsScripting::DrawMesh")]
		private static void Internal_DrawMesh(Mesh mesh, int submeshIndex, Matrix4x4 matrix, Material material, int layer, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			Graphics.Internal_DrawMesh_Injected(Object.MarshalledUnityObject.Marshal<Mesh>(mesh), submeshIndex, ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), layer, Object.MarshalledUnityObject.Marshal<Camera>(camera), (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, Object.MarshalledUnityObject.Marshal<Transform>(probeAnchor), lightProbeUsage, Object.MarshalledUnityObject.Marshal<LightProbeProxyVolume>(lightProbeProxyVolume));
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
		[FreeFunction("GraphicsScripting::DrawMeshInstanced")]
		private unsafe static void Internal_DrawMeshInstanced([NotNull] Mesh mesh, int submeshIndex, [NotNull] Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			if (material == null)
			{
				ThrowHelper.ThrowArgumentNullException(material, "material");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Material>(material);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(material, "material");
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(matrices);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Graphics.Internal_DrawMeshInstanced_Injected(intPtr, submeshIndex, intPtr2, ref managedSpanWrapper, count, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), castShadows, receiveShadows, layer, Object.MarshalledUnityObject.Marshal<Camera>(camera), lightProbeUsage, Object.MarshalledUnityObject.Marshal<LightProbeProxyVolume>(lightProbeProxyVolume));
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000C5A4 File Offset: 0x0000A7A4
		[FreeFunction("GraphicsScripting::Blit")]
		private static void Blit4(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset)
		{
			Graphics.Blit4_Injected(Object.MarshalledUnityObject.Marshal<Texture>(source), Object.MarshalledUnityObject.Marshal<RenderTexture>(dest), ref scale, ref offset);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
		[NativeMethod(Name = "GraphicsScripting::ExecuteCommandBuffer", IsFreeFunction = true, ThrowsException = true)]
		public static void ExecuteCommandBuffer([NotNull] CommandBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			Graphics.ExecuteCommandBuffer_Injected(intPtr);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000C5FE File Offset: 0x0000A7FE
		internal static void SetRenderTargetImpl(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetRTSimple(colorBuffer, depthBuffer, mipLevel, face, depthSlice);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000C610 File Offset: 0x0000A810
		internal static void SetRenderTargetImpl(RenderTexture rt, int mipLevel, CubemapFace face, int depthSlice)
		{
			bool flag = rt;
			if (flag)
			{
				Graphics.SetRenderTargetImpl(rt.colorBuffer, rt.depthBuffer, mipLevel, face, depthSlice);
			}
			else
			{
				Graphics.Internal_SetNullRT();
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000C645 File Offset: 0x0000A845
		public static void SetRenderTarget(RenderTexture rt, [DefaultValue("0")] int mipLevel, [DefaultValue("CubemapFace.Unknown")] CubemapFace face, [DefaultValue("0")] int depthSlice)
		{
			Graphics.SetRenderTargetImpl(rt, mipLevel, face, depthSlice);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000C652 File Offset: 0x0000A852
		public static void CopyTexture(Texture src, int srcElement, int srcMip, Texture dst, int dstElement, int dstMip)
		{
			Graphics.CopyTexture_Slice(src, srcElement, srcMip, dst, dstElement, dstMip);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000C664 File Offset: 0x0000A864
		public static void CopyTexture(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY)
		{
			Graphics.CopyTexture_Region(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dst, dstElement, dstMip, dstX, dstY);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000C68C File Offset: 0x0000A88C
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			Graphics.Internal_DrawMesh(mesh, submeshIndex, matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, [DefaultValue("matrices.Length")] int count, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = !material.enableInstancing;
			if (flag5)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with DrawMeshInstanced.");
			}
			bool flag6 = matrices == null;
			if (flag6)
			{
				throw new ArgumentNullException("matrices");
			}
			bool flag7 = count < 0 || count > Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length);
			if (flag7)
			{
				throw new ArgumentOutOfRangeException("count", string.Format("Count must be in the range of 0 to {0}.", Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length)));
			}
			bool flag8 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag8)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			bool flag9 = count > 0;
			if (flag9)
			{
				Graphics.Internal_DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000C810 File Offset: 0x0000AA10
		public static void Blit(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset)
		{
			Graphics.Blit4(source, dest, scale, offset);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000C820 File Offset: 0x0000AA20
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000C850 File Offset: 0x0000AA50
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000C874 File Offset: 0x0000AA74
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000C89A File Offset: 0x0000AA9A
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderTexture rt)
		{
			Graphics.SetRenderTarget(rt, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000C8A7 File Offset: 0x0000AAA7
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderTexture rt, int mipLevel)
		{
			Graphics.SetRenderTarget(rt, mipLevel, CubemapFace.Unknown, 0);
		}

		// Token: 0x060005AD RID: 1453
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRTSimple_Injected([In] ref RenderBuffer color, [In] ref RenderBuffer depth, int mip, CubemapFace face, int depthSlice);

		// Token: 0x060005AE RID: 1454
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Slice_Injected(IntPtr src, int srcElement, int srcMip, IntPtr dst, int dstElement, int dstMip);

		// Token: 0x060005AF RID: 1455
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Region_Injected(IntPtr src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, IntPtr dst, int dstElement, int dstMip, int dstX, int dstY);

		// Token: 0x060005B0 RID: 1456
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMesh_Injected(IntPtr mesh, int submeshIndex, [In] ref Matrix4x4 matrix, IntPtr material, int layer, IntPtr camera, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, IntPtr probeAnchor, LightProbeUsage lightProbeUsage, IntPtr lightProbeProxyVolume);

		// Token: 0x060005B1 RID: 1457
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstanced_Injected(IntPtr mesh, int submeshIndex, IntPtr material, ref ManagedSpanWrapper matrices, int count, IntPtr properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, IntPtr camera, LightProbeUsage lightProbeUsage, IntPtr lightProbeProxyVolume);

		// Token: 0x060005B2 RID: 1458
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit4_Injected(IntPtr source, IntPtr dest, [In] ref Vector2 scale, [In] ref Vector2 offset);

		// Token: 0x060005B3 RID: 1459
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteCommandBuffer_Injected(IntPtr buffer);

		// Token: 0x04000293 RID: 659
		internal static readonly int kMaxDrawMeshInstanceCount = Graphics.Internal_GetMaxDrawMeshInstanceCount();

		// Token: 0x04000294 RID: 660
		internal static Dictionary<int, RenderInstancedDataLayout> s_RenderInstancedDataLayouts = new Dictionary<int, RenderInstancedDataLayout>();
	}
}
