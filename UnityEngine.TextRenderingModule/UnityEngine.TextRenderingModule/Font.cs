using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	[StaticAccessor("TextRenderingPrivate", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/TextRendering/Public/FontImpl.h")]
	[NativeHeader("Modules/TextRendering/Public/Font.h")]
	[NativeClass("TextRendering::Font")]
	public sealed class Font : Object
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600002C RID: 44 RVA: 0x000029F4 File Offset: 0x00000BF4
		// (remove) Token: 0x0600002D RID: 45 RVA: 0x00002A28 File Offset: 0x00000C28
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<Font> textureRebuilt;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600002E RID: 46 RVA: 0x00002A5C File Offset: 0x00000C5C
		// (remove) Token: 0x0600002F RID: 47 RVA: 0x00002A94 File Offset: 0x00000C94
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event Font.FontTextureRebuildCallback m_FontTextureRebuildCallback;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002ACC File Offset: 0x00000CCC
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public Material material
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Material>(Font.get_material_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Font.set_material_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(value));
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002B1C File Offset: 0x00000D1C
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002B40 File Offset: 0x00000D40
		public string[] fontNames
		{
			[return: Unmarshalled]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Font.get_fontNames_Injected(intPtr);
			}
			[param: Unmarshalled]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Font.set_fontNames_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002B64 File Offset: 0x00000D64
		public bool dynamic
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Font.get_dynamic_Injected(intPtr);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002B88 File Offset: 0x00000D88
		public int ascent
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Font.get_ascent_Injected(intPtr);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002BAC File Offset: 0x00000DAC
		public int fontSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Font.get_fontSize_Injected(intPtr);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002BD0 File Offset: 0x00000DD0
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002C14 File Offset: 0x00000E14
		public unsafe CharacterInfo[] characterInfo
		{
			[FreeFunction("TextRenderingPrivate::GetFontCharacterInfo", HasExplicitThis = true)]
			get
			{
				CharacterInfo[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Font.get_characterInfo_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					CharacterInfo[] array;
					blittableArrayWrapper.Unmarshal<CharacterInfo>(ref array);
					array2 = array;
				}
				return array2;
			}
			[FreeFunction("TextRenderingPrivate::SetFontCharacterInfo", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<CharacterInfo> span = new Span<CharacterInfo>(value);
				fixed (CharacterInfo* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Font.set_characterInfo_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002C5C File Offset: 0x00000E5C
		[NativeProperty("LineSpacing", false, TargetType.Function)]
		public int lineHeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Font.get_lineHeight_Injected(intPtr);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002C80 File Offset: 0x00000E80
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002C98 File Offset: 0x00000E98
		[Obsolete("Font.textureRebuildCallback has been deprecated. Use Font.textureRebuilt instead.")]
		public Font.FontTextureRebuildCallback textureRebuildCallback
		{
			get
			{
				return this.m_FontTextureRebuildCallback;
			}
			set
			{
				this.m_FontTextureRebuildCallback = value;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002CA2 File Offset: 0x00000EA2
		public Font()
		{
			Font.Internal_CreateFont(this, null);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public Font(string name)
		{
			bool isFileName = Path.GetDirectoryName(name) == string.Empty;
			bool flag = isFileName;
			if (flag)
			{
				Font.Internal_CreateFont(this, name);
			}
			else
			{
				Font.Internal_CreateFontFromPath(this, name);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CF1 File Offset: 0x00000EF1
		private Font(string[] names, int size)
		{
			Font.Internal_CreateDynamicFont(this, names, size);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D04 File Offset: 0x00000F04
		public static Font CreateDynamicFontFromOSFont(string fontname, int size)
		{
			return new Font(new string[] { fontname }, size);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D28 File Offset: 0x00000F28
		public static Font CreateDynamicFontFromOSFont(string[] fontnames, int size)
		{
			return new Font(fontnames, size);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D41 File Offset: 0x00000F41
		[RequiredByNativeCode]
		internal static void InvokeTextureRebuilt_Internal(Font font)
		{
			Action<Font> action = Font.textureRebuilt;
			if (action != null)
			{
				action(font);
			}
			Font.FontTextureRebuildCallback fontTextureRebuildCallback = font.m_FontTextureRebuildCallback;
			if (fontTextureRebuildCallback != null)
			{
				fontTextureRebuildCallback();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D68 File Offset: 0x00000F68
		public static int GetMaxVertsForString(string str)
		{
			return str.Length * 4 + 4;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D84 File Offset: 0x00000F84
		internal static Font GetDefault()
		{
			return Unmarshal.UnmarshalUnityObject<Font>(Font.GetDefault_Injected());
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D9C File Offset: 0x00000F9C
		public bool HasCharacter(char c)
		{
			return this.HasCharacter((int)c);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002DB8 File Offset: 0x00000FB8
		private bool HasCharacter(int c)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Font.HasCharacter_Injected(intPtr, c);
		}

		// Token: 0x06000046 RID: 70
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetOSInstalledFontNames();

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetPathsToOSFonts();

		// Token: 0x06000048 RID: 72
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string[] GetOSFallbacks();

		// Token: 0x06000049 RID: 73 RVA: 0x00002DDC File Offset: 0x00000FDC
		private unsafe static void Internal_CreateFont([Writable] Font self, string name)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Font.Internal_CreateFont_Injected(self, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E34 File Offset: 0x00001034
		private unsafe static void Internal_CreateFontFromPath([Writable] Font self, string fontPath)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(fontPath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = fontPath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Font.Internal_CreateFontFromPath_Injected(self, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateDynamicFont([Writable] Font self, [Unmarshalled] string[] _names, int size);

		// Token: 0x0600004C RID: 76 RVA: 0x00002E8C File Offset: 0x0000108C
		[FreeFunction("TextRenderingPrivate::GetCharacterInfo", HasExplicitThis = true)]
		public bool GetCharacterInfo(char ch, out CharacterInfo info, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Font.GetCharacterInfo_Injected(intPtr, ch, out info, size, style);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002EB4 File Offset: 0x000010B4
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info, int size)
		{
			return this.GetCharacterInfo(ch, out info, size, FontStyle.Normal);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002ED0 File Offset: 0x000010D0
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info)
		{
			return this.GetCharacterInfo(ch, out info, 0, FontStyle.Normal);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002EEC File Offset: 0x000010EC
		public unsafe void RequestCharactersInTexture(string characters, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Font>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(characters, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = characters.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Font.RequestCharactersInTexture_Injected(intPtr, ref managedSpanWrapper, size, style);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F54 File Offset: 0x00001154
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters, int size)
		{
			this.RequestCharactersInTexture(characters, size, FontStyle.Normal);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F61 File Offset: 0x00001161
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters)
		{
			this.RequestCharactersInTexture(characters, 0, FontStyle.Normal);
		}

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_material_Injected(IntPtr _unity_self);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_material_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] get_fontNames_Injected(IntPtr _unity_self);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fontNames_Injected(IntPtr _unity_self, string[] value);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_dynamic_Injected(IntPtr _unity_self);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_ascent_Injected(IntPtr _unity_self);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_fontSize_Injected(IntPtr _unity_self);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_characterInfo_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_characterInfo_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_lineHeight_Injected(IntPtr _unity_self);

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefault_Injected();

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasCharacter_Injected(IntPtr _unity_self, int c);

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateFont_Injected([Writable] Font self, ref ManagedSpanWrapper name);

		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateFontFromPath_Injected([Writable] Font self, ref ManagedSpanWrapper fontPath);

		// Token: 0x06000060 RID: 96
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetCharacterInfo_Injected(IntPtr _unity_self, char ch, out CharacterInfo info, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x06000061 RID: 97
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestCharactersInTexture_Injected(IntPtr _unity_self, ref ManagedSpanWrapper characters, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x06000063 RID: 99
		public delegate void FontTextureRebuildCallback();
	}
}
