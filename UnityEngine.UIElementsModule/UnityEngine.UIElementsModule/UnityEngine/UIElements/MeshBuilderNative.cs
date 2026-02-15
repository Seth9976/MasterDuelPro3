using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000278 RID: 632
	[NativeHeader("Modules/UIElements/Core/Native/Renderer/UIRMeshBuilder.bindings.h")]
	internal static class MeshBuilderNative
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x00048DDC File Offset: 0x00046FDC
		[ThreadSafe]
		public static MeshWriteDataInterface MakeBorder(MeshBuilderNative.NativeBorderParams borderParams, float posZ)
		{
			MeshWriteDataInterface meshWriteDataInterface;
			MeshBuilderNative.MakeBorder_Injected(ref borderParams, posZ, out meshWriteDataInterface);
			return meshWriteDataInterface;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00048DF4 File Offset: 0x00046FF4
		[ThreadSafe]
		public static MeshWriteDataInterface MakeSolidRect(MeshBuilderNative.NativeRectParams rectParams, float posZ)
		{
			MeshWriteDataInterface meshWriteDataInterface;
			MeshBuilderNative.MakeSolidRect_Injected(ref rectParams, posZ, out meshWriteDataInterface);
			return meshWriteDataInterface;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00048E0C File Offset: 0x0004700C
		[ThreadSafe]
		public static MeshWriteDataInterface MakeTexturedRect(MeshBuilderNative.NativeRectParams rectParams, float posZ)
		{
			MeshWriteDataInterface meshWriteDataInterface;
			MeshBuilderNative.MakeTexturedRect_Injected(ref rectParams, posZ, out meshWriteDataInterface);
			return meshWriteDataInterface;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00048E24 File Offset: 0x00047024
		[ThreadSafe]
		public unsafe static MeshWriteDataInterface MakeVectorGraphicsStretchBackground(Vertex[] svgVertices, ushort[] svgIndices, float svgWidth, float svgHeight, Rect targetRect, Rect sourceUV, ScaleMode scaleMode, Color tint, MeshBuilderNative.NativeColorPage colorPage)
		{
			Span<Vertex> span = new Span<Vertex>(svgVertices);
			fixed (Vertex* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<ushort> span2 = new Span<ushort>(svgIndices);
				MeshWriteDataInterface meshWriteDataInterface;
				fixed (ushort* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					MeshBuilderNative.MakeVectorGraphicsStretchBackground_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, svgWidth, svgHeight, ref targetRect, ref sourceUV, scaleMode, ref tint, ref colorPage, out meshWriteDataInterface);
					ptr = null;
				}
				return meshWriteDataInterface;
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00048E98 File Offset: 0x00047098
		[ThreadSafe]
		public unsafe static MeshWriteDataInterface MakeVectorGraphics9SliceBackground(Vertex[] svgVertices, ushort[] svgIndices, float svgWidth, float svgHeight, Rect targetRect, Vector4 sliceLTRB, Color tint, MeshBuilderNative.NativeColorPage colorPage)
		{
			Span<Vertex> span = new Span<Vertex>(svgVertices);
			fixed (Vertex* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<ushort> span2 = new Span<ushort>(svgIndices);
				MeshWriteDataInterface meshWriteDataInterface;
				fixed (ushort* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					MeshBuilderNative.MakeVectorGraphics9SliceBackground_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, svgWidth, svgHeight, ref targetRect, ref sliceLTRB, ref tint, ref colorPage, out meshWriteDataInterface);
					ptr = null;
				}
				return meshWriteDataInterface;
			}
		}

		// Token: 0x060010FB RID: 4347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MakeBorder_Injected([In] ref MeshBuilderNative.NativeBorderParams borderParams, float posZ, out MeshWriteDataInterface ret);

		// Token: 0x060010FC RID: 4348
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MakeSolidRect_Injected([In] ref MeshBuilderNative.NativeRectParams rectParams, float posZ, out MeshWriteDataInterface ret);

		// Token: 0x060010FD RID: 4349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MakeTexturedRect_Injected([In] ref MeshBuilderNative.NativeRectParams rectParams, float posZ, out MeshWriteDataInterface ret);

		// Token: 0x060010FE RID: 4350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MakeVectorGraphicsStretchBackground_Injected(ref ManagedSpanWrapper svgVertices, ref ManagedSpanWrapper svgIndices, float svgWidth, float svgHeight, [In] ref Rect targetRect, [In] ref Rect sourceUV, ScaleMode scaleMode, [In] ref Color tint, [In] ref MeshBuilderNative.NativeColorPage colorPage, out MeshWriteDataInterface ret);

		// Token: 0x060010FF RID: 4351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MakeVectorGraphics9SliceBackground_Injected(ref ManagedSpanWrapper svgVertices, ref ManagedSpanWrapper svgIndices, float svgWidth, float svgHeight, [In] ref Rect targetRect, [In] ref Vector4 sliceLTRB, [In] ref Color tint, [In] ref MeshBuilderNative.NativeColorPage colorPage, out MeshWriteDataInterface ret);

		// Token: 0x02000279 RID: 633
		public struct NativeColorPage
		{
			// Token: 0x04000996 RID: 2454
			public int isValid;

			// Token: 0x04000997 RID: 2455
			public Color32 pageAndID;
		}

		// Token: 0x0200027A RID: 634
		public struct NativeBorderParams
		{
			// Token: 0x04000998 RID: 2456
			public Rect rect;

			// Token: 0x04000999 RID: 2457
			public Color leftColor;

			// Token: 0x0400099A RID: 2458
			public Color topColor;

			// Token: 0x0400099B RID: 2459
			public Color rightColor;

			// Token: 0x0400099C RID: 2460
			public Color bottomColor;

			// Token: 0x0400099D RID: 2461
			public float leftWidth;

			// Token: 0x0400099E RID: 2462
			public float topWidth;

			// Token: 0x0400099F RID: 2463
			public float rightWidth;

			// Token: 0x040009A0 RID: 2464
			public float bottomWidth;

			// Token: 0x040009A1 RID: 2465
			public Vector2 topLeftRadius;

			// Token: 0x040009A2 RID: 2466
			public Vector2 topRightRadius;

			// Token: 0x040009A3 RID: 2467
			public Vector2 bottomRightRadius;

			// Token: 0x040009A4 RID: 2468
			public Vector2 bottomLeftRadius;

			// Token: 0x040009A5 RID: 2469
			internal MeshBuilderNative.NativeColorPage leftColorPage;

			// Token: 0x040009A6 RID: 2470
			internal MeshBuilderNative.NativeColorPage topColorPage;

			// Token: 0x040009A7 RID: 2471
			internal MeshBuilderNative.NativeColorPage rightColorPage;

			// Token: 0x040009A8 RID: 2472
			internal MeshBuilderNative.NativeColorPage bottomColorPage;
		}

		// Token: 0x0200027B RID: 635
		public struct NativeRectParams
		{
			// Token: 0x040009A9 RID: 2473
			public Rect rect;

			// Token: 0x040009AA RID: 2474
			public Rect subRect;

			// Token: 0x040009AB RID: 2475
			public Rect uv;

			// Token: 0x040009AC RID: 2476
			public Color color;

			// Token: 0x040009AD RID: 2477
			public ScaleMode scaleMode;

			// Token: 0x040009AE RID: 2478
			public IntPtr backgroundRepeatInstanceList;

			// Token: 0x040009AF RID: 2479
			public int backgroundRepeatInstanceListStartIndex;

			// Token: 0x040009B0 RID: 2480
			public int backgroundRepeatInstanceListEndIndex;

			// Token: 0x040009B1 RID: 2481
			public Vector2 topLeftRadius;

			// Token: 0x040009B2 RID: 2482
			public Vector2 topRightRadius;

			// Token: 0x040009B3 RID: 2483
			public Vector2 bottomRightRadius;

			// Token: 0x040009B4 RID: 2484
			public Vector2 bottomLeftRadius;

			// Token: 0x040009B5 RID: 2485
			public Rect backgroundRepeatRect;

			// Token: 0x040009B6 RID: 2486
			public IntPtr texture;

			// Token: 0x040009B7 RID: 2487
			public IntPtr sprite;

			// Token: 0x040009B8 RID: 2488
			public IntPtr vectorImage;

			// Token: 0x040009B9 RID: 2489
			public IntPtr spriteTexture;

			// Token: 0x040009BA RID: 2490
			public IntPtr spriteVertices;

			// Token: 0x040009BB RID: 2491
			public IntPtr spriteUVs;

			// Token: 0x040009BC RID: 2492
			public IntPtr spriteTriangles;

			// Token: 0x040009BD RID: 2493
			public Rect spriteGeomRect;

			// Token: 0x040009BE RID: 2494
			public Vector2 contentSize;

			// Token: 0x040009BF RID: 2495
			public Vector2 textureSize;

			// Token: 0x040009C0 RID: 2496
			public float texturePixelsPerPoint;

			// Token: 0x040009C1 RID: 2497
			public int leftSlice;

			// Token: 0x040009C2 RID: 2498
			public int topSlice;

			// Token: 0x040009C3 RID: 2499
			public int rightSlice;

			// Token: 0x040009C4 RID: 2500
			public int bottomSlice;

			// Token: 0x040009C5 RID: 2501
			public float sliceScale;

			// Token: 0x040009C6 RID: 2502
			public Vector4 rectInset;

			// Token: 0x040009C7 RID: 2503
			public MeshBuilderNative.NativeColorPage colorPage;

			// Token: 0x040009C8 RID: 2504
			public int meshFlags;
		}
	}
}
