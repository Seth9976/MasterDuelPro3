using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000DC RID: 220
	[NativeHeader("Runtime/Camera/Camera.h")]
	[NativeHeader("Runtime/Camera/CameraUtil.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/GfxDevice/GfxDevice.h")]
	public sealed class GL
	{
		// Token: 0x060005B4 RID: 1460
		[NativeName("ImmediateVertex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Vertex3(float x, float y, float z);

		// Token: 0x060005B5 RID: 1461
		[NativeName("ImmediateTexCoordAll")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TexCoord3(float x, float y, float z);

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000C8CA File Offset: 0x0000AACA
		public static void TexCoord2(float x, float y)
		{
			GL.TexCoord3(x, y, 0f);
		}

		// Token: 0x060005B7 RID: 1463
		[NativeName("ImmediateColor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ImmediateColor(float r, float g, float b, float a);

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000C8DA File Offset: 0x0000AADA
		public static void Color(Color c)
		{
			GL.ImmediateColor(c.r, c.g, c.b, c.a);
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005B9 RID: 1465
		public static extern bool wireframe
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000C8FC File Offset: 0x0000AAFC
		private static void SetViewMatrix(Matrix4x4 m)
		{
			GL.SetViewMatrix_Injected(ref m);
		}

		// Token: 0x17000104 RID: 260
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0000C910 File Offset: 0x0000AB10
		public static Matrix4x4 modelview
		{
			set
			{
				GL.SetViewMatrix(value);
			}
		}

		// Token: 0x060005BC RID: 1468
		[FreeFunction("GLPushMatrixScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PushMatrix();

		// Token: 0x060005BD RID: 1469
		[FreeFunction("GLPopMatrixScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PopMatrix();

		// Token: 0x060005BE RID: 1470
		[FreeFunction("GLLoadOrthoScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadOrtho();

		// Token: 0x060005BF RID: 1471 RVA: 0x0000C91C File Offset: 0x0000AB1C
		[FreeFunction("GLLoadProjectionMatrixScript")]
		public static void LoadProjectionMatrix(Matrix4x4 mat)
		{
			GL.LoadProjectionMatrix_Injected(ref mat);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000C930 File Offset: 0x0000AB30
		[FreeFunction("GLGetGPUProjectionMatrix")]
		public static Matrix4x4 GetGPUProjectionMatrix(Matrix4x4 proj, bool renderIntoTexture)
		{
			Matrix4x4 matrix4x;
			GL.GetGPUProjectionMatrix_Injected(ref proj, renderIntoTexture, out matrix4x);
			return matrix4x;
		}

		// Token: 0x060005C1 RID: 1473
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GLLoadPixelMatrixScript(float left, float right, float bottom, float top);

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000C948 File Offset: 0x0000AB48
		public static void LoadPixelMatrix(float left, float right, float bottom, float top)
		{
			GL.GLLoadPixelMatrixScript(left, right, bottom, top);
		}

		// Token: 0x060005C3 RID: 1475
		[FreeFunction("GLBegin", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Begin(int mode);

		// Token: 0x060005C4 RID: 1476
		[FreeFunction("GLEnd")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void End();

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000C958 File Offset: 0x0000AB58
		[FreeFunction]
		private static void GLClear(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			GL.GLClear_Injected(clearDepth, clearColor, ref backgroundColor, depth);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000C96F File Offset: 0x0000AB6F
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor, [DefaultValue("1.0f")] float depth)
		{
			GL.GLClear(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000C97C File Offset: 0x0000AB7C
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			GL.GLClear(clearDepth, clearColor, backgroundColor, 1f);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000C990 File Offset: 0x0000AB90
		[FreeFunction("SetGLViewport")]
		public static void Viewport(Rect pixelRect)
		{
			GL.Viewport_Injected(ref pixelRect);
		}

		// Token: 0x060005C9 RID: 1481
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetViewMatrix_Injected([In] ref Matrix4x4 m);

		// Token: 0x060005CA RID: 1482
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LoadProjectionMatrix_Injected([In] ref Matrix4x4 mat);

		// Token: 0x060005CB RID: 1483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGPUProjectionMatrix_Injected([In] ref Matrix4x4 proj, bool renderIntoTexture, out Matrix4x4 ret);

		// Token: 0x060005CC RID: 1484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GLClear_Injected(bool clearDepth, bool clearColor, [In] ref Color backgroundColor, float depth);

		// Token: 0x060005CD RID: 1485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Viewport_Injected([In] ref Rect pixelRect);
	}
}
