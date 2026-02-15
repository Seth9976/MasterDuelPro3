using System;

namespace UnityEngine
{
	// Token: 0x0200015A RID: 346
	internal interface IPlayerEditorConnectionNative
	{
		// Token: 0x06000F0C RID: 3852
		void Initialize();

		// Token: 0x06000F0D RID: 3853
		void DisconnectAll();

		// Token: 0x06000F0E RID: 3854
		void SendMessage(Guid messageId, byte[] data, int playerId);

		// Token: 0x06000F0F RID: 3855
		bool TrySendMessage(Guid messageId, byte[] data, int playerId);

		// Token: 0x06000F10 RID: 3856
		void Poll();

		// Token: 0x06000F11 RID: 3857
		void RegisterInternal(Guid messageId);

		// Token: 0x06000F12 RID: 3858
		void UnregisterInternal(Guid messageId);

		// Token: 0x06000F13 RID: 3859
		bool IsConnected();
	}
}
