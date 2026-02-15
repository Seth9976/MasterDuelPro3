using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200012C RID: 300
	[ExcludeFromPreset]
	[UsedByNativeCode]
	[HelpURL("texture-type-default")]
	[NativeHeader("Runtime/Graphics/GeneratedTextures.h")]
	[NativeHeader("Runtime/Graphics/Texture2D.h")]
	public sealed class Texture2D : Texture
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x000148A4 File Offset: 0x00012AA4
		public TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_format_Injected(intPtr);
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000148C8 File Offset: 0x00012AC8
		private bool IgnoreMipmapLimit()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.IgnoreMipmapLimit_Injected(intPtr);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x000148EC File Offset: 0x00012AEC
		private void SetIgnoreMipmapLimitAndReload(bool value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.SetIgnoreMipmapLimitAndReload_Injected(intPtr, value);
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00014910 File Offset: 0x00012B10
		public string mipmapLimitGroup
		{
			[NativeName("GetMipmapLimitGroupName")]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					Texture2D.get_mipmapLimitGroup_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00014950 File Offset: 0x00012B50
		public int activeMipmapLimit
		{
			[NativeName("GetMipmapLimit")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_activeMipmapLimit_Injected(intPtr);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00014974 File Offset: 0x00012B74
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D whiteTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_whiteTexture_Injected());
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0001498C File Offset: 0x00012B8C
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D blackTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_blackTexture_Injected());
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x000149A4 File Offset: 0x00012BA4
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D redTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_redTexture_Injected());
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x000149BC File Offset: 0x00012BBC
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D grayTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_grayTexture_Injected());
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000149D4 File Offset: 0x00012BD4
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D linearGrayTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_linearGrayTexture_Injected());
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x000149EC File Offset: 0x00012BEC
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D normalTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_normalTexture_Injected());
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00014A04 File Offset: 0x00012C04
		public void Compress(bool highQuality)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.Compress_Injected(intPtr, highQuality);
		}

		// Token: 0x06000B18 RID: 2840
		[FreeFunction("Texture2DScripting::CreateEmpty")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateEmptyImpl([Writable] Texture2D mono);

		// Token: 0x06000B19 RID: 2841 RVA: 0x00014A28 File Offset: 0x00012C28
		[FreeFunction("Texture2DScripting::Create")]
		private unsafe static bool Internal_CreateImpl([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex, bool ignoreMipmapLimit, string mipmapLimitGroupName)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(mipmapLimitGroupName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = mipmapLimitGroupName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Texture2D.Internal_CreateImpl_Injected(mono, w, h, mipCount, format, colorSpace, flags, nativeTex, ignoreMipmapLimit, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00014A90 File Offset: 0x00012C90
		private static void Internal_Create([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex, bool ignoreMipmapLimit, string mipmapLimitGroupName)
		{
			bool flag = !Texture2D.Internal_CreateImpl(mono, w, h, mipCount, format, colorSpace, flags, nativeTex, ignoreMipmapLimit, mipmapLimitGroupName);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00014AC8 File Offset: 0x00012CC8
		public override bool isReadable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00014AEC File Offset: 0x00012CEC
		[NativeName("VTOnly")]
		[NativeConditional("ENABLE_VIRTUALTEXTURING && UNITY_EDITOR")]
		public bool vtOnly
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_vtOnly_Injected(intPtr);
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00014B10 File Offset: 0x00012D10
		[NativeName("Apply")]
		private void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.ApplyImpl_Injected(intPtr, updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00014B34 File Offset: 0x00012D34
		[NativeName("Reinitialize")]
		private bool ReinitializeImpl(int width, int height)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.ReinitializeImpl_Injected(intPtr, width, height);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00014B58 File Offset: 0x00012D58
		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int mip, int x, int y, Color color)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.SetPixelImpl_Injected(intPtr, image, mip, x, y, ref color);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00014B84 File Offset: 0x00012D84
		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int mip, int x, int y)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			Texture2D.GetPixelImpl_Injected(intPtr, image, mip, x, y, out color);
			return color;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00014BB0 File Offset: 0x00012DB0
		[NativeName("GetPixelBilinear")]
		private Color GetPixelBilinearImpl(int image, int mip, float u, float v)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			Texture2D.GetPixelBilinearImpl_Injected(intPtr, image, mip, u, v, out color);
			return color;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00014BDC File Offset: 0x00012DDC
		[FreeFunction(Name = "Texture2DScripting::ReinitializeWithFormat", HasExplicitThis = true)]
		private bool ReinitializeWithFormatImpl(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.ReinitializeWithFormatImpl_Injected(intPtr, width, height, format, hasMipMap);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00014C04 File Offset: 0x00012E04
		[FreeFunction(Name = "Texture2DScripting::ReinitializeWithTextureFormat", HasExplicitThis = true)]
		private bool ReinitializeWithTextureFormatImpl(int width, int height, TextureFormat textureFormat, bool hasMipMap)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.ReinitializeWithTextureFormatImpl_Injected(intPtr, width, height, textureFormat, hasMipMap);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00014C2C File Offset: 0x00012E2C
		[FreeFunction(Name = "Texture2DScripting::ReadPixels", HasExplicitThis = true)]
		private void ReadPixelsImpl(Rect source, int destX, int destY, bool recalculateMipMaps)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.ReadPixelsImpl_Injected(intPtr, ref source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00014C54 File Offset: 0x00012E54
		[FreeFunction(Name = "Texture2DScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void SetPixelsImpl(int x, int y, int w, int h, Color[] pixel, int miplevel, int frame)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color> span = new Span<Color>(pixel);
			fixed (Color* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Texture2D.SetPixelsImpl_Injected(intPtr, x, y, w, h, ref managedSpanWrapper, miplevel, frame);
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00014CA8 File Offset: 0x00012EA8
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		private bool LoadRawTextureDataImpl(IntPtr data, ulong size)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.LoadRawTextureDataImpl_Injected(intPtr, data, size);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00014CCC File Offset: 0x00012ECC
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		private unsafe bool LoadRawTextureDataImplArray(byte[] data)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<byte> span = new Span<byte>(data);
			bool flag;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				flag = Texture2D.LoadRawTextureDataImplArray_Injected(intPtr, ref managedSpanWrapper);
			}
			return flag;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00014D14 File Offset: 0x00012F14
		[FreeFunction(Name = "Texture2DScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		private bool SetPixelDataImplArray(Array data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.SetPixelDataImplArray_Injected(intPtr, data, mipLevel, elementSize, dataArraySize, sourceDataStartIndex);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00014D40 File Offset: 0x00012F40
		[FreeFunction(Name = "Texture2DScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		private bool SetPixelDataImpl(IntPtr data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.SetPixelDataImpl_Injected(intPtr, data, mipLevel, elementSize, dataArraySize, sourceDataStartIndex);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00014D6C File Offset: 0x00012F6C
		private IntPtr GetWritableImageData(int frame)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.GetWritableImageData_Injected(intPtr, frame);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00014D90 File Offset: 0x00012F90
		private ulong GetImageDataSize()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.GetImageDataSize_Injected(intPtr);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00014DB4 File Offset: 0x00012FB4
		[FreeFunction("Texture2DScripting::GenerateAtlas")]
		private unsafe static void GenerateAtlasImpl(Vector2[] sizes, int padding, int atlasSize, [Out] Rect[] rect)
		{
			try
			{
				Span<Vector2> span = new Span<Vector2>(sizes);
				fixed (Vector2* ptr = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
					BlittableArrayWrapper blittableArrayWrapper;
					if (rect != null)
					{
						fixed (Rect[] array = rect)
						{
							if (array.Length != 0)
							{
								blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
							}
						}
					}
					Texture2D.GenerateAtlasImpl_Injected(ref managedSpanWrapper, padding, atlasSize, out blittableArrayWrapper);
				}
			}
			finally
			{
				Vector2* ptr = null;
				Rect[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Rect>(ref array);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00014E30 File Offset: 0x00013030
		internal bool isPreProcessed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_isPreProcessed_Injected(intPtr);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00014E54 File Offset: 0x00013054
		public bool streamingMipmaps
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_streamingMipmaps_Injected(intPtr);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00014E78 File Offset: 0x00013078
		public int streamingMipmapsPriority
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_streamingMipmapsPriority_Injected(intPtr);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00014E9C File Offset: 0x0001309C
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x00014EC0 File Offset: 0x000130C0
		public int requestedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetRequestedMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_requestedMipmapLevel_Injected(intPtr);
			}
			[FreeFunction(Name = "GetTextureStreamingManager().SetRequestedMipmapLevel", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture2D.set_requestedMipmapLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00014EE4 File Offset: 0x000130E4
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x00014F08 File Offset: 0x00013108
		public int minimumMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetMinimumMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_minimumMipmapLevel_Injected(intPtr);
			}
			[FreeFunction(Name = "GetTextureStreamingManager().SetMinimumMipmapLevel", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture2D.set_minimumMipmapLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00014F2C File Offset: 0x0001312C
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x00014F50 File Offset: 0x00013150
		internal bool loadAllMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadAllMips", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_loadAllMips_Injected(intPtr);
			}
			[FreeFunction(Name = "GetTextureStreamingManager().SetLoadAllMips", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture2D.set_loadAllMips_Injected(intPtr, value);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00014F74 File Offset: 0x00013174
		public int calculatedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetCalculatedMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_calculatedMipmapLevel_Injected(intPtr);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00014F98 File Offset: 0x00013198
		public int desiredMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDesiredMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_desiredMipmapLevel_Injected(intPtr);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00014FBC File Offset: 0x000131BC
		public int loadingMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadingMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_loadingMipmapLevel_Injected(intPtr);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00014FE0 File Offset: 0x000131E0
		public int loadedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadedMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_loadedMipmapLevel_Injected(intPtr);
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00015004 File Offset: 0x00013204
		[FreeFunction(Name = "GetTextureStreamingManager().ClearRequestedMipmapLevel", HasExplicitThis = true)]
		public void ClearRequestedMipmapLevel()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.ClearRequestedMipmapLevel_Injected(intPtr);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00015028 File Offset: 0x00013228
		[FreeFunction(Name = "GetTextureStreamingManager().IsRequestedMipmapLevelLoaded", HasExplicitThis = true)]
		public bool IsRequestedMipmapLevelLoaded()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.IsRequestedMipmapLevelLoaded_Injected(intPtr);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0001504C File Offset: 0x0001324C
		[FreeFunction(Name = "GetTextureStreamingManager().ClearMinimumMipmapLevel", HasExplicitThis = true)]
		public void ClearMinimumMipmapLevel()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.ClearMinimumMipmapLevel_Injected(intPtr);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00015070 File Offset: 0x00013270
		[FreeFunction("Texture2DScripting::UpdateExternalTexture", HasExplicitThis = true)]
		public void UpdateExternalTexture(IntPtr nativeTex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.UpdateExternalTexture_Injected(intPtr, nativeTex);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00015094 File Offset: 0x00013294
		[FreeFunction("Texture2DScripting::SetAllPixels32", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void SetAllPixels32(Color32[] colors, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color32> span = new Span<Color32>(colors);
			fixed (Color32* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Texture2D.SetAllPixels32_Injected(intPtr, ref managedSpanWrapper, miplevel);
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000150E0 File Offset: 0x000132E0
		[FreeFunction("Texture2DScripting::SetBlockOfPixels32", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void SetBlockOfPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color32> span = new Span<Color32>(colors);
			fixed (Color32* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Texture2D.SetBlockOfPixels32_Injected(intPtr, x, y, blockWidth, blockHeight, ref managedSpanWrapper, miplevel);
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00015130 File Offset: 0x00013330
		[FreeFunction("Texture2DScripting::GetRawTextureData", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public byte[] GetRawTextureData()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.GetRawTextureData_Injected(intPtr);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00015154 File Offset: 0x00013354
		[FreeFunction("Texture2DScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight, [DefaultValue("0")] int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.GetPixels_Injected(intPtr, x, y, blockWidth, blockHeight, miplevel);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00015180 File Offset: 0x00013380
		[ExcludeFromDocs]
		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight)
		{
			return this.GetPixels(x, y, blockWidth, blockHeight, 0);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000151A0 File Offset: 0x000133A0
		[FreeFunction("Texture2DScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public Color32[] GetPixels32([DefaultValue("0")] int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.GetPixels32_Injected(intPtr, miplevel);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000151C4 File Offset: 0x000133C4
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			return this.GetPixels32(0);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000151E0 File Offset: 0x000133E0
		[FreeFunction("Texture2DScripting::PackTextures", HasExplicitThis = true)]
		[return: Unmarshalled]
		public Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize, bool makeNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.PackTextures_Injected(intPtr, textures, padding, maximumAtlasSize, makeNoLongerReadable);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00015208 File Offset: 0x00013408
		public Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize)
		{
			return this.PackTextures(textures, padding, maximumAtlasSize, false);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00015224 File Offset: 0x00013424
		public Rect[] PackTextures(Texture2D[] textures, int padding)
		{
			return this.PackTextures(textures, padding, 2048);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00015244 File Offset: 0x00013444
		[FreeFunction(Name = "Texture2DScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Full(Texture src)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.CopyPixels_Full_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src));
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001526C File Offset: 0x0001346C
		[FreeFunction(Name = "Texture2DScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Slice(Texture src, int srcElement, int srcMip, int dstMip)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.CopyPixels_Slice_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, dstMip);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00015298 File Offset: 0x00013498
		[FreeFunction(Name = "Texture2DScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Region(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstMip, int dstX, int dstY)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.CopyPixels_Region_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dstMip, dstX, dstY);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000152D0 File Offset: 0x000134D0
		internal bool ValidateFormat(TextureFormat format, int width, int height)
		{
			bool isValid = base.ValidateFormat(format);
			bool flag = isValid;
			if (flag)
			{
				bool requireSquarePOT = TextureFormat.PVRTC_RGB2 <= format && format <= TextureFormat.PVRTC_RGBA4;
				bool flag2 = requireSquarePOT && (width != height || !Mathf.IsPowerOfTwo(width));
				if (flag2)
				{
					throw new UnityException(string.Format("'{0}' demands texture to be square and have power-of-two dimensions", format.ToString()));
				}
			}
			return isValid;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0001533C File Offset: 0x0001353C
		internal bool ValidateFormat(GraphicsFormat format, int width, int height)
		{
			bool isValid = base.ValidateFormat(format, GraphicsFormatUsage.Sample);
			bool flag = isValid;
			if (flag)
			{
				bool requireSquarePOT = GraphicsFormatUtility.IsPVRTCFormat(format);
				bool flag2 = requireSquarePOT && (width != height || !Mathf.IsPowerOfTwo(width));
				if (flag2)
				{
					throw new UnityException(string.Format("'{0}' demands texture to be square and have power-of-two dimensions", format.ToString()));
				}
			}
			return isValid;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000153A0 File Offset: 0x000135A0
		internal Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags, int mipCount, IntPtr nativeTex, MipmapLimitDescriptor mipmapLimitDescriptor)
		{
			bool useMipmapLimit = mipmapLimitDescriptor.useMipmapLimit;
			string mipmapLimitGroupName = mipmapLimitDescriptor.groupName;
			bool deprecatedIgnoreFlagWasSet = (flags & TextureCreationFlags.IgnoreMipmapLimit) > TextureCreationFlags.None;
			bool flag = deprecatedIgnoreFlagWasSet;
			if (flag)
			{
				useMipmapLimit = false;
			}
			bool flag2 = this.ValidateFormat(format, width, height);
			if (flag2)
			{
				Texture2D.Internal_Create(this, width, height, mipCount, format, base.GetTextureColorSpace(format), flags, nativeTex, !useMipmapLimit, mipmapLimitGroupName);
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00015403 File Offset: 0x00013603
		[ExcludeFromDocs]
		public Texture2D(int width, int height, DefaultFormat format, TextureCreationFlags flags)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00015418 File Offset: 0x00013618
		[ExcludeFromDocs]
		public Texture2D(int width, int height, DefaultFormat format, int mipCount, TextureCreationFlags flags)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), flags, mipCount, IntPtr.Zero, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00015448 File Offset: 0x00013648
		[Obsolete("Please provide mipmap limit information using a MipmapLimitDescriptor argument", false)]
		[ExcludeFromDocs]
		public Texture2D(int width, int height, DefaultFormat format, int mipCount, string mipmapLimitGroupName, TextureCreationFlags flags)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), flags, mipCount, IntPtr.Zero, new MipmapLimitDescriptor(true, mipmapLimitGroupName))
		{
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00015476 File Offset: 0x00013676
		[ExcludeFromDocs]
		public Texture2D(int width, int height, DefaultFormat format, int mipCount, TextureCreationFlags flags, MipmapLimitDescriptor mipmapLimitDescriptor)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), flags, mipCount, IntPtr.Zero, mipmapLimitDescriptor)
		{
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00015494 File Offset: 0x00013694
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, height, format, flags, Texture.GenerateAllMips, IntPtr.Zero, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000154C4 File Offset: 0x000136C4
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, int mipCount, TextureCreationFlags flags)
			: this(width, height, format, flags, mipCount, IntPtr.Zero, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000154F0 File Offset: 0x000136F0
		[Obsolete("Please provide mipmap limit information using a MipmapLimitDescriptor argument", false)]
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, int mipCount, string mipmapLimitGroupName, TextureCreationFlags flags)
			: this(width, height, format, flags, mipCount, IntPtr.Zero, new MipmapLimitDescriptor(true, mipmapLimitGroupName))
		{
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00015519 File Offset: 0x00013719
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, int mipCount, TextureCreationFlags flags, MipmapLimitDescriptor mipmapLimitDescriptor)
			: this(width, height, format, flags, mipCount, IntPtr.Zero, mipmapLimitDescriptor)
		{
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00015534 File Offset: 0x00013734
		internal Texture2D(int width, int height, TextureFormat textureFormat, int mipCount, bool linear, IntPtr nativeTex, bool createUninitialized, MipmapLimitDescriptor mipmapLimitDescriptor)
		{
			bool flag = !this.ValidateFormat(textureFormat, width, height);
			if (!flag)
			{
				GraphicsFormat format = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				TextureCreationFlags flags = ((mipCount != 1) ? TextureCreationFlags.MipChain : TextureCreationFlags.None);
				bool flag2 = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
				if (flag2)
				{
					flags |= TextureCreationFlags.Crunch;
				}
				if (createUninitialized)
				{
					flags |= TextureCreationFlags.DontInitializePixels | TextureCreationFlags.DontUploadUponCreate;
				}
				Texture2D.Internal_Create(this, width, height, mipCount, format, base.GetTextureColorSpace(linear), flags, nativeTex, !mipmapLimitDescriptor.useMipmapLimit, mipmapLimitDescriptor.groupName);
			}
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000155B8 File Offset: 0x000137B8
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear)
			: this(width, height, textureFormat, mipCount, linear, IntPtr.Zero, false, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x000155E4 File Offset: 0x000137E4
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized)
			: this(width, height, textureFormat, mipCount, linear, IntPtr.Zero, createUninitialized, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00015610 File Offset: 0x00013810
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized, MipmapLimitDescriptor mipmapLimitDescriptor)
			: this(width, height, textureFormat, mipCount, linear, IntPtr.Zero, createUninitialized, mipmapLimitDescriptor)
		{
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00015638 File Offset: 0x00013838
		[Obsolete("Please provide mipmap limit information using a MipmapLimitDescriptor argument", false)]
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized, [DefaultValue("true")] bool ignoreMipmapLimit, [DefaultValue("null")] string mipmapLimitGroupName)
			: this(width, height, textureFormat, mipCount, linear, IntPtr.Zero, createUninitialized, new MipmapLimitDescriptor(!ignoreMipmapLimit, mipmapLimitGroupName))
		{
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00015668 File Offset: 0x00013868
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("true")] bool mipChain, [DefaultValue("false")] bool linear)
			: this(width, height, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear, IntPtr.Zero, false, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000156A0 File Offset: 0x000138A0
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("true")] bool mipChain, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized)
			: this(width, height, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear, IntPtr.Zero, createUninitialized, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000156D8 File Offset: 0x000138D8
		public Texture2D(int width, int height, TextureFormat textureFormat, bool mipChain)
			: this(width, height, textureFormat, mipChain ? Texture.GenerateAllMips : 1, false, IntPtr.Zero, false, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0001570C File Offset: 0x0001390C
		public Texture2D(int width, int height)
		{
			TextureFormat format = TextureFormat.RGBA32;
			bool flag = width == 0 && height == 0;
			if (flag)
			{
				Texture2D.Internal_CreateEmptyImpl(this);
			}
			else
			{
				bool flag2 = this.ValidateFormat(format, width, height);
				if (flag2)
				{
					Texture2D.Internal_Create(this, width, height, Texture.GenerateAllMips, GraphicsFormatUtility.GetGraphicsFormat(format, true), base.GetTextureColorSpace(false), TextureCreationFlags.MipChain, IntPtr.Zero, true, null);
				}
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001576C File Offset: 0x0001396C
		public static Texture2D CreateExternalTexture(int width, int height, TextureFormat format, bool mipChain, bool linear, IntPtr nativeTex)
		{
			bool flag = nativeTex == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Texture2D(width, height, format, mipChain ? (-1) : 1, linear, nativeTex, false, default(MipmapLimitDescriptor));
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000157B8 File Offset: 0x000139B8
		[ExcludeFromDocs]
		public void SetPixel(int x, int y, Color color)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, 0, x, y, color);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000157E8 File Offset: 0x000139E8
		public void SetPixel(int x, int y, Color color, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, mipLevel, x, y, color);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00015818 File Offset: 0x00013A18
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue("0")] int miplevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelsImpl(x, y, blockWidth, blockHeight, colors, miplevel, 0);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0001584C File Offset: 0x00013A4C
		[ExcludeFromDocs]
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors)
		{
			this.SetPixels(x, y, blockWidth, blockHeight, colors, 0);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00015860 File Offset: 0x00013A60
		public void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel)
		{
			int w = this.width >> miplevel;
			bool flag = w < 1;
			if (flag)
			{
				w = 1;
			}
			int h = this.height >> miplevel;
			bool flag2 = h < 1;
			if (flag2)
			{
				h = 1;
			}
			this.SetPixels(0, 0, w, h, colors, miplevel);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x000158A7 File Offset: 0x00013AA7
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors)
		{
			this.SetPixels(0, 0, this.width, this.height, colors, 0);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x000158C4 File Offset: 0x00013AC4
		[ExcludeFromDocs]
		public Color GetPixel(int x, int y)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, 0, x, y);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000158F8 File Offset: 0x00013AF8
		public Color GetPixel(int x, int y, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, mipLevel, x, y);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001592C File Offset: 0x00013B2C
		[ExcludeFromDocs]
		public Color GetPixelBilinear(float u, float v)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, 0, u, v);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00015960 File Offset: 0x00013B60
		public Color GetPixelBilinear(float u, float v, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, mipLevel, u, v);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00015994 File Offset: 0x00013B94
		public void LoadRawTextureData(IntPtr data, int size)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == IntPtr.Zero || size == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImpl(data, (ulong)((long)size));
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x000159FC File Offset: 0x00013BFC
		public void LoadRawTextureData(byte[] data)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == null || data.Length == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImplArray(data);
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00015A58 File Offset: 0x00013C58
		public void LoadRawTextureData<T>(NativeArray<T> data) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !data.IsCreated || data.Length == 0;
			if (flag2)
			{
				throw new UnityException("No texture data provided to LoadRawTextureData");
			}
			bool flag3 = !this.LoadRawTextureDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), (ulong)((long)data.Length * (long)UnsafeUtility.SizeOf<T>()));
			if (flag3)
			{
				throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00015AD4 File Offset: 0x00013CD4
		public void SetPixelData<T>(T[] data, int mipLevel, [DefaultValue("0")] int sourceDataStartIndex = 0)
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = data == null || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImplArray(data, mipLevel, Marshal.SizeOf<T>(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00015B40 File Offset: 0x00013D40
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, [DefaultValue("0")] int sourceDataStartIndex = 0) where T : struct
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = !data.IsCreated || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00015BBC File Offset: 0x00013DBC
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = mipLevel < 0 || mipLevel >= base.mipmapCount;
			if (flag2)
			{
				throw new ArgumentException("The passed in miplevel " + mipLevel.ToString() + " is invalid. It needs to be in the range 0 and " + (base.mipmapCount - 1).ToString());
			}
			bool flag3 = this.GetWritableImageData(0).ToInt64() == 0L;
			if (flag3)
			{
				throw new UnityException("Texture '" + base.name + "' has no data.");
			}
			ulong chainOffset = base.GetPixelDataOffset(mipLevel, 0);
			ulong arraySize = base.GetPixelDataSize(mipLevel, 0);
			int stride = UnsafeUtility.SizeOf<T>();
			ulong arrayLength = arraySize / (ulong)((long)stride);
			bool flag4 = arrayLength > 2147483647UL;
			if (flag4)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			IntPtr dataPtr = new IntPtr((long)this.GetWritableImageData(0) + (long)chainOffset);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)dataPtr, (int)arrayLength, Allocator.None);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00015CC0 File Offset: 0x00013EC0
		public unsafe NativeArray<T> GetRawTextureData<T>() where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int stride = UnsafeUtility.SizeOf<T>();
			ulong arrayLength = this.GetImageDataSize() / (ulong)((long)stride);
			bool flag2 = arrayLength > 2147483647UL;
			if (flag2)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetWritableImageData(0), (int)arrayLength, Allocator.None);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00015D28 File Offset: 0x00013F28
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00015D54 File Offset: 0x00013F54
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00015D60 File Offset: 0x00013F60
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00015D6C File Offset: 0x00013F6C
		public bool Reinitialize(int width, int height)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ReinitializeImpl(width, height);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00015D9C File Offset: 0x00013F9C
		public bool Reinitialize(int width, int height, TextureFormat format, bool hasMipMap)
		{
			return this.ReinitializeWithTextureFormatImpl(width, height, format, hasMipMap);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00015DBC File Offset: 0x00013FBC
		public bool Reinitialize(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ReinitializeWithFormatImpl(width, height, format, hasMipMap);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00015DF0 File Offset: 0x00013FF0
		[Obsolete("Texture2D.Resize(int, int) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32)", false)]
		public bool Resize(int width, int height)
		{
			return this.Reinitialize(width, height);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00015E0C File Offset: 0x0001400C
		[Obsolete("Texture2D.Resize(int, int, TextureFormat, bool) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int, TextureFormat, bool) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32, UnityEngine.TextureFormat, [*] System.Boolean)", false)]
		public bool Resize(int width, int height, TextureFormat format, bool hasMipMap)
		{
			return this.Reinitialize(width, height, format, hasMipMap);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00015E2C File Offset: 0x0001402C
		[Obsolete("Texture2D.Resize(int, int, GraphicsFormat, bool) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int, GraphicsFormat, bool) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32, UnityEngine.Experimental.Rendering.GraphicsFormat, [*] System.Boolean)", false)]
		public bool Resize(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			return this.Reinitialize(width, height, format, hasMipMap);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00015E4C File Offset: 0x0001404C
		public void ReadPixels(Rect source, int destX, int destY, [DefaultValue("true")] bool recalculateMipMaps)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ReadPixelsImpl(source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00015E7B File Offset: 0x0001407B
		[ExcludeFromDocs]
		public void ReadPixels(Rect source, int destX, int destY)
		{
			this.ReadPixels(source, destX, destY, true);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00015E8C File Offset: 0x0001408C
		public static bool GenerateAtlas(Vector2[] sizes, int padding, int atlasSize, List<Rect> results)
		{
			bool flag = sizes == null;
			if (flag)
			{
				throw new ArgumentException("sizes array can not be null");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentException("results list cannot be null");
			}
			bool flag3 = padding < 0;
			if (flag3)
			{
				throw new ArgumentException("padding can not be negative");
			}
			bool flag4 = atlasSize <= 0;
			if (flag4)
			{
				throw new ArgumentException("atlas size must be positive");
			}
			results.Clear();
			bool flag5 = sizes.Length == 0;
			bool flag6;
			if (flag5)
			{
				flag6 = true;
			}
			else
			{
				NoAllocHelpers.EnsureListElemCount<Rect>(results, sizes.Length);
				Texture2D.GenerateAtlasImpl(sizes, padding, atlasSize, NoAllocHelpers.ExtractArrayFromList<Rect>(results));
				flag6 = results.Count != 0;
			}
			return flag6;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00015F28 File Offset: 0x00014128
		public void SetPixels32(Color32[] colors, [DefaultValue("0")] int miplevel)
		{
			this.SetAllPixels32(colors, miplevel);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00015F34 File Offset: 0x00014134
		[ExcludeFromDocs]
		public void SetPixels32(Color32[] colors)
		{
			this.SetPixels32(colors, 0);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00015F40 File Offset: 0x00014140
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, [DefaultValue("0")] int miplevel)
		{
			this.SetBlockOfPixels32(x, y, blockWidth, blockHeight, colors, miplevel);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00015F53 File Offset: 0x00014153
		[ExcludeFromDocs]
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors)
		{
			this.SetPixels32(x, y, blockWidth, blockHeight, colors, 0);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00015F68 File Offset: 0x00014168
		public Color[] GetPixels([DefaultValue("0")] int miplevel)
		{
			int w = this.width >> miplevel;
			bool flag = w < 1;
			if (flag)
			{
				w = 1;
			}
			int h = this.height >> miplevel;
			bool flag2 = h < 1;
			if (flag2)
			{
				h = 1;
			}
			return this.GetPixels(0, 0, w, h, miplevel);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00015FB4 File Offset: 0x000141B4
		[ExcludeFromDocs]
		public Color[] GetPixels()
		{
			return this.GetPixels(0);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00015FD0 File Offset: 0x000141D0
		public void CopyPixels(Texture src)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !src.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(src);
			}
			this.CopyPixels_Full(src);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00016010 File Offset: 0x00014210
		public void CopyPixels(Texture src, int srcElement, int srcMip, int dstMip)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !src.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(src);
			}
			this.CopyPixels_Slice(src, srcElement, srcMip, dstMip);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00016054 File Offset: 0x00014254
		public void CopyPixels(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstMip, int dstX, int dstY)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !src.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(src);
			}
			this.CopyPixels_Region(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dstMip, dstX, dstY);
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x000160A4 File Offset: 0x000142A4
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x000160BC File Offset: 0x000142BC
		public bool ignoreMipmapLimit
		{
			get
			{
				return this.IgnoreMipmapLimit();
			}
			set
			{
				bool flag = !this.isReadable;
				if (flag)
				{
					throw base.IgnoreMipmapLimitCannotBeToggledException(this);
				}
				this.SetIgnoreMipmapLimitAndReload(value);
			}
		}

		// Token: 0x06000B88 RID: 2952
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureFormat get_format_Injected(IntPtr _unity_self);

		// Token: 0x06000B89 RID: 2953
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IgnoreMipmapLimit_Injected(IntPtr _unity_self);

		// Token: 0x06000B8A RID: 2954
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIgnoreMipmapLimitAndReload_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000B8B RID: 2955
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mipmapLimitGroup_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000B8C RID: 2956
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_activeMipmapLimit_Injected(IntPtr _unity_self);

		// Token: 0x06000B8D RID: 2957
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_whiteTexture_Injected();

		// Token: 0x06000B8E RID: 2958
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_blackTexture_Injected();

		// Token: 0x06000B8F RID: 2959
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_redTexture_Injected();

		// Token: 0x06000B90 RID: 2960
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_grayTexture_Injected();

		// Token: 0x06000B91 RID: 2961
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_linearGrayTexture_Injected();

		// Token: 0x06000B92 RID: 2962
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_normalTexture_Injected();

		// Token: 0x06000B93 RID: 2963
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Compress_Injected(IntPtr _unity_self, bool highQuality);

		// Token: 0x06000B94 RID: 2964
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl_Injected([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex, bool ignoreMipmapLimit, ref ManagedSpanWrapper mipmapLimitGroupName);

		// Token: 0x06000B95 RID: 2965
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x06000B96 RID: 2966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_vtOnly_Injected(IntPtr _unity_self);

		// Token: 0x06000B97 RID: 2967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyImpl_Injected(IntPtr _unity_self, bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06000B98 RID: 2968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ReinitializeImpl_Injected(IntPtr _unity_self, int width, int height);

		// Token: 0x06000B99 RID: 2969
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixelImpl_Injected(IntPtr _unity_self, int image, int mip, int x, int y, [In] ref Color color);

		// Token: 0x06000B9A RID: 2970
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPixelImpl_Injected(IntPtr _unity_self, int image, int mip, int x, int y, out Color ret);

		// Token: 0x06000B9B RID: 2971
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPixelBilinearImpl_Injected(IntPtr _unity_self, int image, int mip, float u, float v, out Color ret);

		// Token: 0x06000B9C RID: 2972
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ReinitializeWithFormatImpl_Injected(IntPtr _unity_self, int width, int height, GraphicsFormat format, bool hasMipMap);

		// Token: 0x06000B9D RID: 2973
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ReinitializeWithTextureFormatImpl_Injected(IntPtr _unity_self, int width, int height, TextureFormat textureFormat, bool hasMipMap);

		// Token: 0x06000B9E RID: 2974
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReadPixelsImpl_Injected(IntPtr _unity_self, [In] ref Rect source, int destX, int destY, bool recalculateMipMaps);

		// Token: 0x06000B9F RID: 2975
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixelsImpl_Injected(IntPtr _unity_self, int x, int y, int w, int h, ref ManagedSpanWrapper pixel, int miplevel, int frame);

		// Token: 0x06000BA0 RID: 2976
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadRawTextureDataImpl_Injected(IntPtr _unity_self, IntPtr data, ulong size);

		// Token: 0x06000BA1 RID: 2977
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadRawTextureDataImplArray_Injected(IntPtr _unity_self, ref ManagedSpanWrapper data);

		// Token: 0x06000BA2 RID: 2978
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPixelDataImplArray_Injected(IntPtr _unity_self, Array data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex);

		// Token: 0x06000BA3 RID: 2979
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPixelDataImpl_Injected(IntPtr _unity_self, IntPtr data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex);

		// Token: 0x06000BA4 RID: 2980
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetWritableImageData_Injected(IntPtr _unity_self, int frame);

		// Token: 0x06000BA5 RID: 2981
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong GetImageDataSize_Injected(IntPtr _unity_self);

		// Token: 0x06000BA6 RID: 2982
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateAtlasImpl_Injected(ref ManagedSpanWrapper sizes, int padding, int atlasSize, out BlittableArrayWrapper rect);

		// Token: 0x06000BA7 RID: 2983
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPreProcessed_Injected(IntPtr _unity_self);

		// Token: 0x06000BA8 RID: 2984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_streamingMipmaps_Injected(IntPtr _unity_self);

		// Token: 0x06000BA9 RID: 2985
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_streamingMipmapsPriority_Injected(IntPtr _unity_self);

		// Token: 0x06000BAA RID: 2986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_requestedMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000BAB RID: 2987
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_requestedMipmapLevel_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000BAC RID: 2988
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_minimumMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000BAD RID: 2989
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_minimumMipmapLevel_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000BAE RID: 2990
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_loadAllMips_Injected(IntPtr _unity_self);

		// Token: 0x06000BAF RID: 2991
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loadAllMips_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000BB0 RID: 2992
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_calculatedMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000BB1 RID: 2993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_desiredMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000BB2 RID: 2994
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_loadingMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000BB3 RID: 2995
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_loadedMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000BB4 RID: 2996
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearRequestedMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000BB5 RID: 2997
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRequestedMipmapLevelLoaded_Injected(IntPtr _unity_self);

		// Token: 0x06000BB6 RID: 2998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearMinimumMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000BB7 RID: 2999
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateExternalTexture_Injected(IntPtr _unity_self, IntPtr nativeTex);

		// Token: 0x06000BB8 RID: 3000
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAllPixels32_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, int miplevel);

		// Token: 0x06000BB9 RID: 3001
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBlockOfPixels32_Injected(IntPtr _unity_self, int x, int y, int blockWidth, int blockHeight, ref ManagedSpanWrapper colors, int miplevel);

		// Token: 0x06000BBA RID: 3002
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] GetRawTextureData_Injected(IntPtr _unity_self);

		// Token: 0x06000BBB RID: 3003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Color[] GetPixels_Injected(IntPtr _unity_self, int x, int y, int blockWidth, int blockHeight, [DefaultValue("0")] int miplevel);

		// Token: 0x06000BBC RID: 3004
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Color32[] GetPixels32_Injected(IntPtr _unity_self, [DefaultValue("0")] int miplevel);

		// Token: 0x06000BBD RID: 3005
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Rect[] PackTextures_Injected(IntPtr _unity_self, Texture2D[] textures, int padding, int maximumAtlasSize, bool makeNoLongerReadable);

		// Token: 0x06000BBE RID: 3006
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Full_Injected(IntPtr _unity_self, IntPtr src);

		// Token: 0x06000BBF RID: 3007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Slice_Injected(IntPtr _unity_self, IntPtr src, int srcElement, int srcMip, int dstMip);

		// Token: 0x06000BC0 RID: 3008
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Region_Injected(IntPtr _unity_self, IntPtr src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstMip, int dstX, int dstY);

		// Token: 0x040003FF RID: 1023
		internal const int streamingMipmapsPriorityMin = -128;

		// Token: 0x04000400 RID: 1024
		internal const int streamingMipmapsPriorityMax = 127;
	}
}
