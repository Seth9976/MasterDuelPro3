using System;
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
	// Token: 0x0200012E RID: 302
	[ExcludeFromPreset]
	[NativeHeader("Runtime/Graphics/Texture3D.h")]
	public sealed class Texture3D : Texture
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00016448 File Offset: 0x00014648
		public override bool isReadable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture3D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture3D.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0001646C File Offset: 0x0001466C
		[NativeName("SetPixel")]
		private void SetPixelImpl(int mip, int x, int y, int z, Color color)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture3D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture3D.SetPixelImpl_Injected(intPtr, mip, x, y, z, ref color);
		}

		// Token: 0x06000BDB RID: 3035
		[FreeFunction("Texture3DScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] Texture3D mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex);

		// Token: 0x06000BDC RID: 3036 RVA: 0x00016498 File Offset: 0x00014698
		private static void Internal_Create([Writable] Texture3D mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex)
		{
			bool flag = !Texture3D.Internal_CreateImpl(mono, w, h, d, mipCount, format, colorSpace, flags, nativeTex);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000164CC File Offset: 0x000146CC
		[FreeFunction(Name = "Texture3DScripting::Apply", HasExplicitThis = true)]
		private void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture3D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture3D.ApplyImpl_Injected(intPtr, updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000164F0 File Offset: 0x000146F0
		[FreeFunction(Name = "Texture3DScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetPixels(Color[] colors, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture3D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color> span = new Span<Color>(colors);
			fixed (Color* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Texture3D.SetPixels_Injected(intPtr, ref managedSpanWrapper, miplevel);
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0001653C File Offset: 0x0001473C
		private IntPtr GetImageData()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture3D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture3D.GetImageData_Injected(intPtr);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001655E File Offset: 0x0001475E
		[ExcludeFromDocs]
		public Texture3D(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00016574 File Offset: 0x00014774
		[ExcludeFromDocs]
		public Texture3D(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags, int mipCount)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags, mipCount)
		{
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0001658C File Offset: 0x0001478C
		[ExcludeFromDocs]
		[RequiredByNativeCode]
		public Texture3D(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, height, depth, format, flags, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000165A4 File Offset: 0x000147A4
		[ExcludeFromDocs]
		public Texture3D(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags, [DefaultValue("Texture.GenerateAllMips")] int mipCount)
		{
			bool flag = !base.ValidateFormat(format, GraphicsFormatUsage.Sample);
			if (!flag)
			{
				Texture3D.ValidateIsNotCrunched(flags);
				Texture3D.Internal_Create(this, width, height, depth, mipCount, format, base.GetTextureColorSpace(format), flags, IntPtr.Zero);
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000165F0 File Offset: 0x000147F0
		[ExcludeFromDocs]
		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, int mipCount)
			: this(width, height, depth, textureFormat, mipCount, IntPtr.Zero)
		{
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00016606 File Offset: 0x00014806
		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, int mipCount, [DefaultValue("IntPtr.Zero")] IntPtr nativeTex)
			: this(width, height, depth, textureFormat, mipCount, nativeTex, false)
		{
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0001661C File Offset: 0x0001481C
		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, int mipCount, [DefaultValue("IntPtr.Zero")] IntPtr nativeTex, [DefaultValue("false")] bool createUninitialized)
		{
			bool flag = !base.ValidateFormat(textureFormat);
			if (!flag)
			{
				GraphicsFormat format = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, false);
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
				Texture3D.ValidateIsNotCrunched(flags);
				Texture3D.Internal_Create(this, width, height, depth, mipCount, format, base.GetTextureColorSpace(true), flags, nativeTex);
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00016693 File Offset: 0x00014893
		[ExcludeFromDocs]
		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, bool mipChain)
			: this(width, height, depth, textureFormat, mipChain ? Texture.GenerateAllMips : 1)
		{
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000166AE File Offset: 0x000148AE
		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool createUninitialized)
			: this(width, height, depth, textureFormat, mipChain ? Texture.GenerateAllMips : 1, IntPtr.Zero, createUninitialized)
		{
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000166D0 File Offset: 0x000148D0
		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, bool mipChain, [DefaultValue("IntPtr.Zero")] IntPtr nativeTex)
			: this(width, height, depth, textureFormat, mipChain ? Texture.GenerateAllMips : 1, nativeTex)
		{
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000166F0 File Offset: 0x000148F0
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0001671C File Offset: 0x0001491C
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00016728 File Offset: 0x00014928
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00016734 File Offset: 0x00014934
		public void SetPixel(int x, int y, int z, Color color, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(mipLevel, x, y, z, color);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00016768 File Offset: 0x00014968
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
				throw new ArgumentException("The passed in miplevel " + mipLevel.ToString() + " is invalid. The valid range is 0 through  " + (base.mipmapCount - 1).ToString());
			}
			bool flag3 = this.GetImageData().ToInt64() == 0L;
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
			IntPtr dataPtr = new IntPtr((long)this.GetImageData() + (long)chainOffset);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)dataPtr, (int)arrayLength, Allocator.None);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00016868 File Offset: 0x00014A68
		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched Texture3D is not supported.");
			}
		}

		// Token: 0x06000BF0 RID: 3056
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x06000BF1 RID: 3057
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixelImpl_Injected(IntPtr _unity_self, int mip, int x, int y, int z, [In] ref Color color);

		// Token: 0x06000BF2 RID: 3058
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyImpl_Injected(IntPtr _unity_self, bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06000BF3 RID: 3059
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixels_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, int miplevel);

		// Token: 0x06000BF4 RID: 3060
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetImageData_Injected(IntPtr _unity_self);
	}
}
