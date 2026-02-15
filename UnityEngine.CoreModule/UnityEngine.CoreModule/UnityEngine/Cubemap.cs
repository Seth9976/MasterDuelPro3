using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200012D RID: 301
	[ExcludeFromPreset]
	[NativeHeader("Runtime/Graphics/CubemapTexture.h")]
	public sealed class Cubemap : Texture
	{
		// Token: 0x06000BC1 RID: 3009
		[FreeFunction("CubemapScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] Cubemap mono, int ext, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex);

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000160E8 File Offset: 0x000142E8
		private static void Internal_Create([Writable] Cubemap mono, int ext, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex)
		{
			bool flag = !Cubemap.Internal_CreateImpl(mono, ext, mipCount, format, colorSpace, flags, nativeTex);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00016118 File Offset: 0x00014318
		[FreeFunction(Name = "CubemapScripting::Apply", HasExplicitThis = true)]
		private void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cubemap.ApplyImpl_Injected(intPtr, updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0001613C File Offset: 0x0001433C
		public override bool isReadable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cubemap.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00016160 File Offset: 0x00014360
		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int mip, int x, int y, Color color)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cubemap.SetPixelImpl_Injected(intPtr, image, mip, x, y, ref color);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0001618C File Offset: 0x0001438C
		internal bool ValidateFormat(TextureFormat format, int width)
		{
			bool isValid = base.ValidateFormat(format);
			bool flag = isValid;
			if (flag)
			{
				bool requireSquarePOT = TextureFormat.PVRTC_RGB2 <= format && format <= TextureFormat.PVRTC_RGBA4;
				bool flag2 = requireSquarePOT && !Mathf.IsPowerOfTwo(width);
				if (flag2)
				{
					throw new UnityException(string.Format("'{0}' demands texture to have power-of-two dimensions", format.ToString()));
				}
			}
			return isValid;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000161F4 File Offset: 0x000143F4
		internal bool ValidateFormat(GraphicsFormat format, int width)
		{
			bool isValid = base.ValidateFormat(format, GraphicsFormatUsage.Sample);
			bool flag = isValid;
			if (flag)
			{
				bool requireSquarePOT = GraphicsFormatUtility.IsPVRTCFormat(format);
				bool flag2 = requireSquarePOT && !Mathf.IsPowerOfTwo(width);
				if (flag2)
				{
					throw new UnityException(string.Format("'{0}' demands texture to have power-of-two dimensions", format.ToString()));
				}
			}
			return isValid;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00016250 File Offset: 0x00014450
		[ExcludeFromDocs]
		public Cubemap(int width, DefaultFormat format, TextureCreationFlags flags)
			: this(width, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00016262 File Offset: 0x00014462
		[ExcludeFromDocs]
		public Cubemap(int width, DefaultFormat format, TextureCreationFlags flags, int mipCount)
			: this(width, SystemInfo.GetGraphicsFormat(format), flags, mipCount)
		{
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00016276 File Offset: 0x00014476
		[ExcludeFromDocs]
		[RequiredByNativeCode]
		public Cubemap(int width, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, format, flags, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00016288 File Offset: 0x00014488
		[ExcludeFromDocs]
		public Cubemap(int width, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
		{
			bool flag = !this.ValidateFormat(format, width);
			if (!flag)
			{
				Cubemap.ValidateIsNotCrunched(flags);
				Cubemap.Internal_Create(this, width, mipCount, format, base.GetTextureColorSpace(format), flags, IntPtr.Zero);
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000162D0 File Offset: 0x000144D0
		internal Cubemap(int width, TextureFormat textureFormat, int mipCount, IntPtr nativeTex, bool createUninitialized)
		{
			bool flag = !this.ValidateFormat(textureFormat, width);
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
				Cubemap.ValidateIsNotCrunched(flags);
				Cubemap.Internal_Create(this, width, mipCount, format, base.GetTextureColorSpace(true), flags, nativeTex);
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00016341 File Offset: 0x00014541
		public Cubemap(int width, TextureFormat textureFormat, bool mipChain)
			: this(width, textureFormat, mipChain ? Texture.GenerateAllMips : 1, IntPtr.Zero, false)
		{
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0001635E File Offset: 0x0001455E
		public Cubemap(int width, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool createUninitialized)
			: this(width, textureFormat, mipChain ? Texture.GenerateAllMips : 1, IntPtr.Zero, createUninitialized)
		{
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0001637C File Offset: 0x0001457C
		public Cubemap(int width, TextureFormat format, int mipCount)
			: this(width, format, mipCount, IntPtr.Zero, false)
		{
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001638F File Offset: 0x0001458F
		public Cubemap(int width, TextureFormat format, int mipCount, [DefaultValue("false")] bool createUninitialized)
			: this(width, format, mipCount, IntPtr.Zero, createUninitialized)
		{
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000163A3 File Offset: 0x000145A3
		[ExcludeFromDocs]
		public void SetPixel(CubemapFace face, int x, int y, Color color)
		{
			this.SetPixel(face, x, y, color, 0);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000163B4 File Offset: 0x000145B4
		public void SetPixel(CubemapFace face, int x, int y, Color color, [DefaultValue("0")] int mip)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl((int)face, mip, x, y, color);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000163E8 File Offset: 0x000145E8
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00016414 File Offset: 0x00014614
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00016420 File Offset: 0x00014620
		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched Cubemap is not supported for textures created from script.");
			}
		}

		// Token: 0x06000BD6 RID: 3030
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyImpl_Injected(IntPtr _unity_self, bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06000BD7 RID: 3031
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x06000BD8 RID: 3032
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixelImpl_Injected(IntPtr _unity_self, int image, int mip, int x, int y, [In] ref Color color);
	}
}
