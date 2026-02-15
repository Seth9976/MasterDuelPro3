using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200012B RID: 299
	[UsedByNativeCode]
	[NativeHeader("Runtime/Streaming/TextureStreamingManager.h")]
	[NativeHeader("Runtime/Graphics/Texture.h")]
	public class Texture : Object
	{
		// Token: 0x06000AC5 RID: 2757 RVA: 0x00004DC7 File Offset: 0x00002FC7
		protected Texture()
		{
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00014308 File Offset: 0x00012508
		public int mipmapCount
		{
			[NativeName("GetMipmapCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_mipmapCount_Injected(intPtr);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001432C File Offset: 0x0001252C
		public virtual GraphicsFormat graphicsFormat
		{
			get
			{
				return GraphicsFormatUtility.GetFormat(this);
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00014344 File Offset: 0x00012544
		[ThreadSafe]
		private int GetDataWidth()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.GetDataWidth_Injected(intPtr);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00014368 File Offset: 0x00012568
		[ThreadSafe]
		private int GetDataHeight()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.GetDataHeight_Injected(intPtr);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001438C File Offset: 0x0001258C
		[ThreadSafe]
		private TextureDimension GetDimension()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.GetDimension_Injected(intPtr);
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000143B0 File Offset: 0x000125B0
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x000143C8 File Offset: 0x000125C8
		public virtual int width
		{
			get
			{
				return this.GetDataWidth();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000143D0 File Offset: 0x000125D0
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x000143C8 File Offset: 0x000125C8
		public virtual int height
		{
			get
			{
				return this.GetDataHeight();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x000143E8 File Offset: 0x000125E8
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x000143C8 File Offset: 0x000125C8
		public virtual TextureDimension dimension
		{
			get
			{
				return this.GetDimension();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00014400 File Offset: 0x00012600
		public virtual bool isReadable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00014424 File Offset: 0x00012624
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x00014448 File Offset: 0x00012648
		public TextureWrapMode wrapMode
		{
			[NativeName("GetWrapModeU")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_wrapMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_wrapMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0001446C File Offset: 0x0001266C
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x00014490 File Offset: 0x00012690
		public TextureWrapMode wrapModeU
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_wrapModeU_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_wrapModeU_Injected(intPtr, value);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x000144B4 File Offset: 0x000126B4
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x000144D8 File Offset: 0x000126D8
		public TextureWrapMode wrapModeV
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_wrapModeV_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_wrapModeV_Injected(intPtr, value);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x000144FC File Offset: 0x000126FC
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x00014520 File Offset: 0x00012720
		public TextureWrapMode wrapModeW
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_wrapModeW_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_wrapModeW_Injected(intPtr, value);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00014544 File Offset: 0x00012744
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x00014568 File Offset: 0x00012768
		public FilterMode filterMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_filterMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_filterMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001458C File Offset: 0x0001278C
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x000145B0 File Offset: 0x000127B0
		public int anisoLevel
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_anisoLevel_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_anisoLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x000145D4 File Offset: 0x000127D4
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x000145F8 File Offset: 0x000127F8
		public float mipMapBias
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_mipMapBias_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_mipMapBias_Injected(intPtr, value);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0001461C File Offset: 0x0001281C
		public Vector2 texelSize
		{
			[NativeName("GetTexelSize")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Texture.get_texelSize_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00014644 File Offset: 0x00012844
		public uint updateCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_updateCount_Injected(intPtr);
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00014668 File Offset: 0x00012868
		public void IncrementUpdateCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture.IncrementUpdateCount_Injected(intPtr);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001468C File Offset: 0x0001288C
		[NativeMethod("GetActiveTextureColorSpace")]
		private int Internal_GetActiveTextureColorSpace()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.Internal_GetActiveTextureColorSpace_Injected(intPtr);
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x000146B0 File Offset: 0x000128B0
		internal ColorSpace activeTextureColorSpace
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "Unity.UIElements" })]
			get
			{
				return (this.Internal_GetActiveTextureColorSpace() == 0) ? ColorSpace.Linear : ColorSpace.Gamma;
			}
		}

		// Token: 0x06000AE5 RID: 2789
		[FreeFunction("GetTextureStreamingManager().SetStreamingTextureMaterialDebugPropertiesWithSlot")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStreamingTextureMaterialDebugPropertiesWithSlot(int materialTextureSlot);

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000146CE File Offset: 0x000128CE
		public static void SetStreamingTextureMaterialDebugProperties(int materialTextureSlot)
		{
			Texture.SetStreamingTextureMaterialDebugPropertiesWithSlot(materialTextureSlot);
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000AE7 RID: 2791
		// (set) Token: 0x06000AE8 RID: 2792
		public static extern bool streamingTextureDiscardUnusedMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDiscardUnusedMips")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetDiscardUnusedMips")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000146D8 File Offset: 0x000128D8
		internal ulong GetPixelDataSize(int mipLevel, int element = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.GetPixelDataSize_Injected(intPtr, mipLevel, element);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000146FC File Offset: 0x000128FC
		internal ulong GetPixelDataOffset(int mipLevel, int element = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.GetPixelDataOffset_Injected(intPtr, mipLevel, element);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00014720 File Offset: 0x00012920
		internal TextureColorSpace GetTextureColorSpace(bool linear)
		{
			return linear ? TextureColorSpace.Linear : TextureColorSpace.sRGB;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001473C File Offset: 0x0001293C
		internal TextureColorSpace GetTextureColorSpace(GraphicsFormat format)
		{
			return this.GetTextureColorSpace(!GraphicsFormatUtility.IsSRGBFormat(format));
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00014760 File Offset: 0x00012960
		internal bool ValidateFormat(TextureFormat format)
		{
			bool flag = SystemInfo.SupportsTextureFormat(format);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = GraphicsFormatUtility.IsCompressedFormat(format) && GraphicsFormatUtility.CanDecompressFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
				if (flag3)
				{
					Debug.LogWarning(string.Format("'{0}' is not supported on this platform. Decompressing texture. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", format.ToString()), this);
					flag2 = true;
				}
				else
				{
					Debug.LogError(string.Format("Texture creation failed. '{0}' is not supported on this platform. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", format.ToString()), this);
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000147E0 File Offset: 0x000129E0
		internal bool ValidateFormat(GraphicsFormat format, GraphicsFormatUsage usage)
		{
			bool flag = SystemInfo.IsFormatSupported(format, usage);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Debug.LogError(string.Format("Texture creation failed. '{0}' is not supported for {1} usage on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", format.ToString(), usage.ToString()), this);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00014830 File Offset: 0x00012A30
		internal UnityException CreateNonReadableException(Texture t)
		{
			return new UnityException(string.Format("Texture '{0}' is not readable, the texture memory can not be accessed from scripts. You can make the texture readable in the Texture Import Settings.", t.name));
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00014858 File Offset: 0x00012A58
		internal UnityException IgnoreMipmapLimitCannotBeToggledException(Texture t)
		{
			return new UnityException(string.Format("Failed to toggle ignoreMipmapLimit, Texture '{0}' is not readable. You can make the texture readable in the Texture Import Settings.", t.name));
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00014880 File Offset: 0x00012A80
		internal UnityException CreateNativeArrayLengthOverflowException()
		{
			return new UnityException("Failed to create NativeArray, length exceeds the allowed maximum of Int32.MaxValue. Use a larger type as template argument to reduce the array length.");
		}

		// Token: 0x06000AF3 RID: 2803
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_mipmapCount_Injected(IntPtr _unity_self);

		// Token: 0x06000AF4 RID: 2804
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDataWidth_Injected(IntPtr _unity_self);

		// Token: 0x06000AF5 RID: 2805
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDataHeight_Injected(IntPtr _unity_self);

		// Token: 0x06000AF6 RID: 2806
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureDimension GetDimension_Injected(IntPtr _unity_self);

		// Token: 0x06000AF7 RID: 2807
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x06000AF8 RID: 2808
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureWrapMode get_wrapMode_Injected(IntPtr _unity_self);

		// Token: 0x06000AF9 RID: 2809
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapMode_Injected(IntPtr _unity_self, TextureWrapMode value);

		// Token: 0x06000AFA RID: 2810
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureWrapMode get_wrapModeU_Injected(IntPtr _unity_self);

		// Token: 0x06000AFB RID: 2811
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapModeU_Injected(IntPtr _unity_self, TextureWrapMode value);

		// Token: 0x06000AFC RID: 2812
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureWrapMode get_wrapModeV_Injected(IntPtr _unity_self);

		// Token: 0x06000AFD RID: 2813
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapModeV_Injected(IntPtr _unity_self, TextureWrapMode value);

		// Token: 0x06000AFE RID: 2814
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureWrapMode get_wrapModeW_Injected(IntPtr _unity_self);

		// Token: 0x06000AFF RID: 2815
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapModeW_Injected(IntPtr _unity_self, TextureWrapMode value);

		// Token: 0x06000B00 RID: 2816
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FilterMode get_filterMode_Injected(IntPtr _unity_self);

		// Token: 0x06000B01 RID: 2817
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_filterMode_Injected(IntPtr _unity_self, FilterMode value);

		// Token: 0x06000B02 RID: 2818
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_anisoLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000B03 RID: 2819
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anisoLevel_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000B04 RID: 2820
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_mipMapBias_Injected(IntPtr _unity_self);

		// Token: 0x06000B05 RID: 2821
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mipMapBias_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000B06 RID: 2822
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_texelSize_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000B07 RID: 2823
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_updateCount_Injected(IntPtr _unity_self);

		// Token: 0x06000B08 RID: 2824
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IncrementUpdateCount_Injected(IntPtr _unity_self);

		// Token: 0x06000B09 RID: 2825
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetActiveTextureColorSpace_Injected(IntPtr _unity_self);

		// Token: 0x06000B0A RID: 2826
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong GetPixelDataSize_Injected(IntPtr _unity_self, int mipLevel, int element);

		// Token: 0x06000B0B RID: 2827
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong GetPixelDataOffset_Injected(IntPtr _unity_self, int mipLevel, int element);

		// Token: 0x040003FE RID: 1022
		public static readonly int GenerateAllMips = -1;
	}
}
