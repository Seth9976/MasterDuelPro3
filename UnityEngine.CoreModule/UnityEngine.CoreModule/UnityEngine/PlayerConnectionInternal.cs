using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200015B RID: 347
	[NativeHeader("Runtime/Export/PlayerConnection/PlayerConnectionInternal.bindings.h")]
	internal class PlayerConnectionInternal : IPlayerEditorConnectionNative
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x0001FEBC File Offset: 0x0001E0BC
		void IPlayerEditorConnectionNative.SendMessage(Guid messageId, byte[] data, int playerId)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("messageId must not be empty");
			}
			PlayerConnectionInternal.SendMessage(messageId.ToString("N"), data, playerId);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		bool IPlayerEditorConnectionNative.TrySendMessage(Guid messageId, byte[] data, int playerId)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("messageId must not be empty");
			}
			return PlayerConnectionInternal.TrySendMessage(messageId.ToString("N"), data, playerId);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0001FF3C File Offset: 0x0001E13C
		void IPlayerEditorConnectionNative.Poll()
		{
			PlayerConnectionInternal.PollInternal();
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0001FF45 File Offset: 0x0001E145
		void IPlayerEditorConnectionNative.RegisterInternal(Guid messageId)
		{
			PlayerConnectionInternal.RegisterInternal(messageId.ToString("N"));
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0001FF5A File Offset: 0x0001E15A
		void IPlayerEditorConnectionNative.UnregisterInternal(Guid messageId)
		{
			PlayerConnectionInternal.UnregisterInternal(messageId.ToString("N"));
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0001FF6F File Offset: 0x0001E16F
		void IPlayerEditorConnectionNative.Initialize()
		{
			PlayerConnectionInternal.Initialize();
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0001FF78 File Offset: 0x0001E178
		bool IPlayerEditorConnectionNative.IsConnected()
		{
			return PlayerConnectionInternal.IsConnected();
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0001FF8F File Offset: 0x0001E18F
		void IPlayerEditorConnectionNative.DisconnectAll()
		{
			PlayerConnectionInternal.DisconnectAll();
		}

		// Token: 0x06000F1C RID: 3868
		[FreeFunction("PlayerConnection_Bindings::IsConnected")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsConnected();

		// Token: 0x06000F1D RID: 3869
		[FreeFunction("PlayerConnection_Bindings::Initialize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Initialize();

		// Token: 0x06000F1E RID: 3870 RVA: 0x0001FF98 File Offset: 0x0001E198
		[FreeFunction("PlayerConnection_Bindings::RegisterInternal")]
		private unsafe static void RegisterInternal(string messageId)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(messageId, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = messageId.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				PlayerConnectionInternal.RegisterInternal_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0001FFEC File Offset: 0x0001E1EC
		[FreeFunction("PlayerConnection_Bindings::UnregisterInternal")]
		private unsafe static void UnregisterInternal(string messageId)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(messageId, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = messageId.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				PlayerConnectionInternal.UnregisterInternal_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00020040 File Offset: 0x0001E240
		[FreeFunction("PlayerConnection_Bindings::SendMessage")]
		private unsafe static void SendMessage(string messageId, byte[] data, int playerId)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(messageId, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = messageId.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Span<byte> span = new Span<byte>(data);
				fixed (byte* ptr2 = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span.Length);
					PlayerConnectionInternal.SendMessage_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, playerId);
				}
			}
			finally
			{
				char* ptr = null;
				byte* ptr2 = null;
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x000200C0 File Offset: 0x0001E2C0
		[FreeFunction("PlayerConnection_Bindings::TrySendMessage")]
		private unsafe static bool TrySendMessage(string messageId, byte[] data, int playerId)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(messageId, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = messageId.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Span<byte> span = new Span<byte>(data);
				fixed (byte* ptr2 = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span.Length);
					flag = PlayerConnectionInternal.TrySendMessage_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, playerId);
				}
			}
			finally
			{
				char* ptr = null;
				byte* ptr2 = null;
			}
			return flag;
		}

		// Token: 0x06000F22 RID: 3874
		[FreeFunction("PlayerConnection_Bindings::PollInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PollInternal();

		// Token: 0x06000F23 RID: 3875
		[FreeFunction("PlayerConnection_Bindings::DisconnectAll")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisconnectAll();

		// Token: 0x06000F25 RID: 3877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterInternal_Injected(ref ManagedSpanWrapper messageId);

		// Token: 0x06000F26 RID: 3878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterInternal_Injected(ref ManagedSpanWrapper messageId);

		// Token: 0x06000F27 RID: 3879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMessage_Injected(ref ManagedSpanWrapper messageId, ref ManagedSpanWrapper data, int playerId);

		// Token: 0x06000F28 RID: 3880
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySendMessage_Injected(ref ManagedSpanWrapper messageId, ref ManagedSpanWrapper data, int playerId);
	}
}
