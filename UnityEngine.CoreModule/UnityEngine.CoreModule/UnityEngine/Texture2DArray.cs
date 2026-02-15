using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200012F RID: 303
	[NativeHeader("Runtime/Graphics/Texture2DArray.h")]
	[ExcludeFromPreset]
	public sealed class Texture2DArray : Texture
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000BF5 RID: 3061
		public static extern int allSlices
		{
			[NativeName("GetAllTextureLayersIdentifier")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00016890 File Offset: 0x00014A90
		public override bool isReadable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2DArray.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000168B4 File Offset: 0x00014AB4
		[FreeFunction("Texture2DArrayScripting::Create")]
		private unsafe static bool Internal_CreateImpl([Writable] Texture2DArray mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, bool ignoreMipmapLimit, string mipmapLimitGroupName)
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
				flag = Texture2DArray.Internal_CreateImpl_Injected(mono, w, h, d, mipCount, format, colorSpace, flags, ignoreMipmapLimit, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0001691C File Offset: 0x00014B1C
		private static void Internal_Create([Writable] Texture2DArray mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, bool ignoreMipmapLimit, string mipmapLimitGroupName)
		{
			bool flag = !Texture2DArray.Internal_CreateImpl(mono, w, h, d, mipCount, format, colorSpace, flags, ignoreMipmapLimit, mipmapLimitGroupName);
			if (flag)
			{
				throw new UnityException("Failed to create 2D array texture because of invalid parameters.");
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00016954 File Offset: 0x00014B54
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

		// Token: 0x06000BFA RID: 3066 RVA: 0x000169C0 File Offset: 0x00014BC0
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

		// Token: 0x06000BFB RID: 3067 RVA: 0x00016A23 File Offset: 0x00014C23
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00016A39 File Offset: 0x00014C39
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags, int mipCount)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags, mipCount)
		{
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00016A51 File Offset: 0x00014C51
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags, int mipCount, MipmapLimitDescriptor mipmapLimitDescriptor)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags, mipCount, mipmapLimitDescriptor)
		{
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00016A6C File Offset: 0x00014C6C
		[RequiredByNativeCode]
		public Texture2DArray(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, height, depth, format, flags, Texture.GenerateAllMips, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00016A98 File Offset: 0x00014C98
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
			: this(width, height, depth, format, flags, mipCount, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00016AC0 File Offset: 0x00014CC0
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags, int mipCount, MipmapLimitDescriptor mipmapLimitDescriptor)
		{
			bool flag = !this.ValidateFormat(format, width, height);
			if (!flag)
			{
				Texture2DArray.ValidateIsNotCrunched(flags);
				Texture2DArray.Internal_Create(this, width, height, depth, mipCount, format, base.GetTextureColorSpace(format), flags, !mipmapLimitDescriptor.useMipmapLimit, mipmapLimitDescriptor.groupName);
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00016B1C File Offset: 0x00014D1C
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, int mipCount, bool linear, bool createUninitialized, MipmapLimitDescriptor mipmapLimitDescriptor)
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
				Texture2DArray.ValidateIsNotCrunched(flags);
				Texture2DArray.Internal_Create(this, width, height, depth, mipCount, format, base.GetTextureColorSpace(linear), flags, !mipmapLimitDescriptor.useMipmapLimit, mipmapLimitDescriptor.groupName);
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00016BAC File Offset: 0x00014DAC
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, int mipCount, bool linear, bool createUninitialized)
			: this(width, height, depth, textureFormat, mipCount, linear, createUninitialized, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00016BD8 File Offset: 0x00014DD8
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, int mipCount, bool linear)
			: this(width, height, depth, textureFormat, mipCount, linear, false, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00016C00 File Offset: 0x00014E00
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized)
			: this(width, height, depth, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear, createUninitialized, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00016C33 File Offset: 0x00014E33
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool linear)
			: this(width, height, depth, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear)
		{
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00016C50 File Offset: 0x00014E50
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain)
			: this(width, height, depth, textureFormat, mipChain ? Texture.GenerateAllMips : 1, false)
		{
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00016C6C File Offset: 0x00014E6C
		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched Texture2DArray is not supported.");
			}
		}

		// Token: 0x06000C08 RID: 3080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x06000C09 RID: 3081
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl_Injected([Writable] Texture2DArray mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, bool ignoreMipmapLimit, ref ManagedSpanWrapper mipmapLimitGroupName);
	}
}
