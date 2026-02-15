using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000130 RID: 304
	[NativeHeader("Runtime/Graphics/CubemapArrayTexture.h")]
	[ExcludeFromPreset]
	public sealed class CubemapArray : Texture
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00016C94 File Offset: 0x00014E94
		public override bool isReadable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CubemapArray.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x06000C0B RID: 3083
		[FreeFunction("CubemapArrayScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] CubemapArray mono, int ext, int count, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags);

		// Token: 0x06000C0C RID: 3084 RVA: 0x00016CB8 File Offset: 0x00014EB8
		private static void Internal_Create([Writable] CubemapArray mono, int ext, int count, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags)
		{
			bool flag = !CubemapArray.Internal_CreateImpl(mono, ext, count, mipCount, format, colorSpace, flags);
			if (flag)
			{
				throw new UnityException("Failed to create cubemap array texture because of invalid parameters.");
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00016CE8 File Offset: 0x00014EE8
		[FreeFunction(Name = "CubemapArrayScripting::Apply", HasExplicitThis = true)]
		private void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CubemapArray.ApplyImpl_Injected(intPtr, updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00016D0C File Offset: 0x00014F0C
		[FreeFunction(Name = "CubemapArrayScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetPixels(Color[] colors, CubemapFace face, int arrayElement, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color> span = new Span<Color>(colors);
			fixed (Color* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CubemapArray.SetPixels_Injected(intPtr, ref managedSpanWrapper, face, arrayElement, miplevel);
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00016D58 File Offset: 0x00014F58
		public void SetPixels(Color[] colors, CubemapFace face, int arrayElement)
		{
			this.SetPixels(colors, face, arrayElement, 0);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00016D66 File Offset: 0x00014F66
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, DefaultFormat format, TextureCreationFlags flags)
			: this(width, cubemapCount, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00016D7A File Offset: 0x00014F7A
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, DefaultFormat format, TextureCreationFlags flags, [DefaultValue("Texture.GenerateAllMips")] int mipCount)
			: this(width, cubemapCount, SystemInfo.GetGraphicsFormat(format), flags, mipCount)
		{
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00016D90 File Offset: 0x00014F90
		[RequiredByNativeCode]
		public CubemapArray(int width, int cubemapCount, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, cubemapCount, format, flags, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00016DA4 File Offset: 0x00014FA4
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, GraphicsFormat format, TextureCreationFlags flags, [DefaultValue("Texture.GenerateAllMips")] int mipCount)
		{
			bool flag = !base.ValidateFormat(format, GraphicsFormatUsage.Sample);
			if (!flag)
			{
				CubemapArray.ValidateIsNotCrunched(flags);
				CubemapArray.Internal_Create(this, width, cubemapCount, mipCount, format, base.GetTextureColorSpace(format), flags);
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00016DE8 File Offset: 0x00014FE8
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, int mipCount, bool linear, [DefaultValue("false")] bool createUninitialized)
		{
			bool flag = !base.ValidateFormat(textureFormat);
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
				CubemapArray.ValidateIsNotCrunched(flags);
				CubemapArray.Internal_Create(this, width, cubemapCount, mipCount, format, base.GetTextureColorSpace(linear), flags);
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00016E5E File Offset: 0x0001505E
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, int mipCount, bool linear)
			: this(width, cubemapCount, textureFormat, mipCount, linear, false)
		{
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00016E70 File Offset: 0x00015070
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized)
			: this(width, cubemapCount, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear, createUninitialized)
		{
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00016E8D File Offset: 0x0001508D
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool linear)
			: this(width, cubemapCount, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear)
		{
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00016EA8 File Offset: 0x000150A8
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain)
			: this(width, cubemapCount, textureFormat, mipChain ? Texture.GenerateAllMips : 1, false)
		{
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00016EC4 File Offset: 0x000150C4
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00016EF0 File Offset: 0x000150F0
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00016EFC File Offset: 0x000150FC
		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched TextureCubeArray is not supported.");
			}
		}

		// Token: 0x06000C1C RID: 3100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x06000C1D RID: 3101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyImpl_Injected(IntPtr _unity_self, bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06000C1E RID: 3102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixels_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, CubemapFace face, int arrayElement, int miplevel);
	}
}
