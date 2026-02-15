using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x02000414 RID: 1044
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	[NativeType(Header = "Runtime/2D/SpriteAtlas/SpriteAtlas.h")]
	public class SpriteAtlas : Object
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x0003D350 File Offset: 0x0003B550
		public bool isVariant
		{
			[NativeMethod("IsVariant")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteAtlas.get_isVariant_Injected(intPtr);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0003D374 File Offset: 0x0003B574
		public string tag
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					SpriteAtlas.get_tag_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0003D3B4 File Offset: 0x0003B5B4
		public int spriteCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteAtlas.get_spriteCount_Injected(intPtr);
			}
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0003D3D8 File Offset: 0x0003B5D8
		public bool CanBindTo([NotNull] Sprite sprite)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			return SpriteAtlas.CanBindTo_Injected(intPtr, intPtr2);
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0003D420 File Offset: 0x0003B620
		public unsafe Sprite GetSprite(string name)
		{
			Sprite sprite;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr sprite_Injected = SpriteAtlas.GetSprite_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr sprite_Injected;
				sprite = Unmarshal.UnmarshalUnityObject<Sprite>(sprite_Injected);
				char* ptr = null;
			}
			return sprite;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0003D490 File Offset: 0x0003B690
		public int GetSprites(Sprite[] sprites)
		{
			return this.GetSpritesScripting(sprites);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0003D4AC File Offset: 0x0003B6AC
		public int GetSprites(Sprite[] sprites, string name)
		{
			return this.GetSpritesWithNameScripting(sprites, name);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0003D4C8 File Offset: 0x0003B6C8
		private int GetSpritesScripting([Unmarshalled] Sprite[] sprites)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return SpriteAtlas.GetSpritesScripting_Injected(intPtr, sprites);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0003D4EC File Offset: 0x0003B6EC
		private unsafe int GetSpritesWithNameScripting([Unmarshalled] Sprite[] sprites, string name)
		{
			int spritesWithNameScripting_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteAtlas>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				spritesWithNameScripting_Injected = SpriteAtlas.GetSpritesWithNameScripting_Injected(intPtr, sprites, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return spritesWithNameScripting_Injected;
		}

		// Token: 0x06001BA8 RID: 7080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isVariant_Injected(IntPtr _unity_self);

		// Token: 0x06001BA9 RID: 7081
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_tag_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06001BAA RID: 7082
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_spriteCount_Injected(IntPtr _unity_self);

		// Token: 0x06001BAB RID: 7083
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanBindTo_Injected(IntPtr _unity_self, IntPtr sprite);

		// Token: 0x06001BAC RID: 7084
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSprite_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06001BAD RID: 7085
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSpritesScripting_Injected(IntPtr _unity_self, Sprite[] sprites);

		// Token: 0x06001BAE RID: 7086
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSpritesWithNameScripting_Injected(IntPtr _unity_self, Sprite[] sprites, ref ManagedSpanWrapper name);
	}
}
