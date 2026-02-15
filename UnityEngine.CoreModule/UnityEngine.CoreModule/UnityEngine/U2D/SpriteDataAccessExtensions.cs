using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.U2D
{
	// Token: 0x02000412 RID: 1042
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	public static class SpriteDataAccessExtensions
	{
		// Token: 0x06001B91 RID: 7057 RVA: 0x0003D068 File Offset: 0x0003B268
		private static void CheckAttributeTypeMatchesAndThrow<T>(VertexAttribute channel)
		{
			bool channelTypeMatches;
			switch (channel)
			{
			case VertexAttribute.Position:
			case VertexAttribute.Normal:
				channelTypeMatches = typeof(T) == typeof(Vector3);
				break;
			case VertexAttribute.Tangent:
				channelTypeMatches = typeof(T) == typeof(Vector4);
				break;
			case VertexAttribute.Color:
				channelTypeMatches = typeof(T) == typeof(Color32);
				break;
			case VertexAttribute.TexCoord0:
			case VertexAttribute.TexCoord1:
			case VertexAttribute.TexCoord2:
			case VertexAttribute.TexCoord3:
			case VertexAttribute.TexCoord4:
			case VertexAttribute.TexCoord5:
			case VertexAttribute.TexCoord6:
			case VertexAttribute.TexCoord7:
				channelTypeMatches = typeof(T) == typeof(Vector2);
				break;
			case VertexAttribute.BlendWeight:
				channelTypeMatches = typeof(T) == typeof(BoneWeight);
				break;
			default:
				throw new InvalidOperationException(string.Format("The requested channel '{0}' is unknown.", channel));
			}
			bool flag = !channelTypeMatches;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("The requested channel '{0}' does not match the return type {1}.", channel, typeof(T).Name));
			}
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0003D190 File Offset: 0x0003B390
		public unsafe static NativeSlice<T> GetVertexAttribute<T>(this Sprite sprite, VertexAttribute channel) where T : struct
		{
			SpriteDataAccessExtensions.CheckAttributeTypeMatchesAndThrow<T>(channel);
			SpriteChannelInfo info = SpriteDataAccessExtensions.GetChannelInfo(sprite, channel);
			byte* buffer = (byte*)info.buffer + info.offset;
			return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>((void*)buffer, info.stride, info.count);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0003D1D8 File Offset: 0x0003B3D8
		public static NativeArray<ushort> GetIndices(this Sprite sprite)
		{
			SpriteChannelInfo info = SpriteDataAccessExtensions.GetIndicesInfo(sprite);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ushort>(info.buffer, info.count, Allocator.Invalid);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0003D208 File Offset: 0x0003B408
		private static SpriteChannelInfo GetIndicesInfo([NotNull] Sprite sprite)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			SpriteChannelInfo spriteChannelInfo;
			SpriteDataAccessExtensions.GetIndicesInfo_Injected(intPtr, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0003D244 File Offset: 0x0003B444
		private static SpriteChannelInfo GetChannelInfo([NotNull] Sprite sprite, VertexAttribute channel)
		{
			if (sprite == null)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(sprite);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sprite, "sprite");
			}
			SpriteChannelInfo spriteChannelInfo;
			SpriteDataAccessExtensions.GetChannelInfo_Injected(intPtr, channel, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06001B96 RID: 7062
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetIndicesInfo_Injected(IntPtr sprite, out SpriteChannelInfo ret);

		// Token: 0x06001B97 RID: 7063
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetChannelInfo_Injected(IntPtr sprite, VertexAttribute channel, out SpriteChannelInfo ret);
	}
}
