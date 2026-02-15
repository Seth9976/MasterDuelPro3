using System;

namespace UnityWebSocket
{
	// Token: 0x02000007 RID: 7
	public interface IWebSocket
	{
		// Token: 0x06000015 RID: 21
		void ConnectAsync();

		// Token: 0x06000016 RID: 22
		void CloseAsync();

		// Token: 0x06000017 RID: 23
		void SendAsync(byte[] data);

		// Token: 0x06000018 RID: 24
		void SendAsync(string text);

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25
		string Address { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26
		string[] SubProtocols { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27
		WebSocketState ReadyState { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28
		// (set) Token: 0x0600001D RID: 29
		string BinaryType { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001E RID: 30
		// (remove) Token: 0x0600001F RID: 31
		event EventHandler<OpenEventArgs> OnOpen;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000020 RID: 32
		// (remove) Token: 0x06000021 RID: 33
		event EventHandler<CloseEventArgs> OnClose;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000022 RID: 34
		// (remove) Token: 0x06000023 RID: 35
		event EventHandler<ErrorEventArgs> OnError;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000024 RID: 36
		// (remove) Token: 0x06000025 RID: 37
		event EventHandler<MessageEventArgs> OnMessage;
	}
}
