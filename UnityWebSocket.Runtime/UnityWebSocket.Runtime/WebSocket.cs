using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityWebSocket
{
	// Token: 0x0200000D RID: 13
	public class WebSocket : IWebSocket
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002253 File Offset: 0x00000453
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000225B File Offset: 0x0000045B
		public string Address { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002264 File Offset: 0x00000464
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000226C File Offset: 0x0000046C
		public string[] SubProtocols { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002278 File Offset: 0x00000478
		public WebSocketState ReadyState
		{
			get
			{
				if (this.socket == null)
				{
					return WebSocketState.Closed;
				}
				switch (this.socket.State)
				{
				case WebSocketState.None:
				case WebSocketState.Closed:
					return WebSocketState.Closed;
				case WebSocketState.Connecting:
					return WebSocketState.Connecting;
				case WebSocketState.Open:
					return WebSocketState.Open;
				case WebSocketState.CloseSent:
				case WebSocketState.CloseReceived:
					return WebSocketState.Closing;
				default:
					return WebSocketState.Closed;
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000022C4 File Offset: 0x000004C4
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000022CC File Offset: 0x000004CC
		public string BinaryType { get; set; } = "arraybuffer";

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000038 RID: 56 RVA: 0x000022D8 File Offset: 0x000004D8
		// (remove) Token: 0x06000039 RID: 57 RVA: 0x00002310 File Offset: 0x00000510
		public event EventHandler<OpenEventArgs> OnOpen;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600003A RID: 58 RVA: 0x00002348 File Offset: 0x00000548
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x00002380 File Offset: 0x00000580
		public event EventHandler<CloseEventArgs> OnClose;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600003C RID: 60 RVA: 0x000023B8 File Offset: 0x000005B8
		// (remove) Token: 0x0600003D RID: 61 RVA: 0x000023F0 File Offset: 0x000005F0
		public event EventHandler<ErrorEventArgs> OnError;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600003E RID: 62 RVA: 0x00002428 File Offset: 0x00000628
		// (remove) Token: 0x0600003F RID: 63 RVA: 0x00002460 File Offset: 0x00000660
		public event EventHandler<MessageEventArgs> OnMessage;

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002495 File Offset: 0x00000695
		private bool isOpening
		{
			get
			{
				return this.socket != null && this.socket.State == WebSocketState.Open;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000024B0 File Offset: 0x000006B0
		public WebSocket(string address)
		{
			this.Address = address;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002504 File Offset: 0x00000704
		public WebSocket(string address, string subProtocol)
		{
			this.Address = address;
			this.SubProtocols = new string[] { subProtocol };
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002568 File Offset: 0x00000768
		public WebSocket(string address, string[] subProtocols)
		{
			this.Address = address;
			this.SubProtocols = subProtocols;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000025C0 File Offset: 0x000007C0
		public void ConnectAsync()
		{
			WebSocketManager.Instance.Add(this);
			if (this.socket != null)
			{
				this.HandleError(new Exception("Socket is busy."));
				return;
			}
			this.socket = new ClientWebSocket();
			if (this.SubProtocols != null)
			{
				foreach (string protocol in this.SubProtocols)
				{
					if (!string.IsNullOrEmpty(protocol))
					{
						this.socket.Options.AddSubProtocol(protocol);
					}
				}
			}
			Task.Run(new Func<Task>(this.ConnectTask));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002648 File Offset: 0x00000848
		public void CloseAsync()
		{
			if (!this.isOpening)
			{
				return;
			}
			this.SendBufferAsync(new WebSocket.SendBuffer(null, WebSocketMessageType.Close));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002660 File Offset: 0x00000860
		public void SendAsync(byte[] data)
		{
			if (!this.isOpening)
			{
				return;
			}
			WebSocket.SendBuffer buffer = new WebSocket.SendBuffer(data, WebSocketMessageType.Binary);
			this.SendBufferAsync(buffer);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002688 File Offset: 0x00000888
		public void SendAsync(string text)
		{
			if (!this.isOpening)
			{
				return;
			}
			WebSocket.SendBuffer buffer = new WebSocket.SendBuffer(Encoding.UTF8.GetBytes(text), WebSocketMessageType.Text);
			this.SendBufferAsync(buffer);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000026B8 File Offset: 0x000008B8
		private async Task ConnectTask()
		{
			try
			{
				Uri uri = new Uri(this.Address);
				await this.socket.ConnectAsync(uri, CancellationToken.None);
			}
			catch (Exception e)
			{
				this.HandleError(e);
				this.HandleClose(1006, e.Message);
				this.SocketDispose();
				return;
			}
			this.HandleOpen();
			await this.ReceiveTask();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000026FC File Offset: 0x000008FC
		private void SendBufferAsync(WebSocket.SendBuffer buffer)
		{
			if (this.isSendTaskRunning)
			{
				object obj = this.sendQueueLock;
				lock (obj)
				{
					if (buffer.type == WebSocketMessageType.Close)
					{
						this.sendQueue.Clear();
					}
					this.sendQueue.Enqueue(buffer);
					return;
				}
			}
			this.isSendTaskRunning = true;
			this.sendQueue.Enqueue(buffer);
			Task.Run(new Func<Task>(this.SendTask));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002784 File Offset: 0x00000984
		private async Task SendTask()
		{
			try
			{
				WebSocket.SendBuffer buffer = null;
				while (this.sendQueue.Count > 0 && this.isOpening)
				{
					object obj = this.sendQueueLock;
					lock (obj)
					{
						buffer = this.sendQueue.Dequeue();
					}
					if (buffer.type == WebSocketMessageType.Close)
					{
						await this.socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Normal Closure", CancellationToken.None);
					}
					else
					{
						await this.socket.SendAsync(new ArraySegment<byte>(buffer.data), buffer.type, true, CancellationToken.None);
					}
				}
			}
			catch (Exception e)
			{
				this.HandleError(e);
			}
			finally
			{
				this.isSendTaskRunning = false;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000027C8 File Offset: 0x000009C8
		private Task ReceiveTask()
		{
			WebSocket.<ReceiveTask>d__43 <ReceiveTask>d__;
			<ReceiveTask>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ReceiveTask>d__.<>4__this = this;
			<ReceiveTask>d__.<>1__state = -1;
			<ReceiveTask>d__.<>t__builder.Start<WebSocket.<ReceiveTask>d__43>(ref <ReceiveTask>d__);
			return <ReceiveTask>d__.<>t__builder.Task;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000280B File Offset: 0x00000A0B
		private void SocketDispose()
		{
			this.sendQueue.Clear();
			this.socket.Dispose();
			this.socket = null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000282A File Offset: 0x00000A2A
		private void HandleOpen()
		{
			this.HandleEventSync(new OpenEventArgs());
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002837 File Offset: 0x00000A37
		private void HandleMessage(Opcode opcode, byte[] rawData)
		{
			this.HandleEventSync(new MessageEventArgs(opcode, rawData));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002846 File Offset: 0x00000A46
		private void HandleClose(ushort code, string reason)
		{
			this.HandleEventSync(new CloseEventArgs(code, reason));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002855 File Offset: 0x00000A55
		private void HandleError(Exception exception)
		{
			this.HandleEventSync(new ErrorEventArgs(exception.Message));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002868 File Offset: 0x00000A68
		private void HandleEventSync(EventArgs eventArgs)
		{
			object obj = this.eventQueueLock;
			lock (obj)
			{
				this.eventQueue.Enqueue(eventArgs);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000028B0 File Offset: 0x00000AB0
		internal void Update()
		{
			while (this.eventQueue.Count > 0)
			{
				object obj = this.eventQueueLock;
				EventArgs e;
				lock (obj)
				{
					e = this.eventQueue.Dequeue();
				}
				if (e is CloseEventArgs)
				{
					EventHandler<CloseEventArgs> onClose = this.OnClose;
					if (onClose != null)
					{
						onClose(this, e as CloseEventArgs);
					}
					WebSocketManager.Instance.Remove(this);
				}
				else if (e is OpenEventArgs)
				{
					EventHandler<OpenEventArgs> onOpen = this.OnOpen;
					if (onOpen != null)
					{
						onOpen(this, e as OpenEventArgs);
					}
				}
				else if (e is MessageEventArgs)
				{
					EventHandler<MessageEventArgs> onMessage = this.OnMessage;
					if (onMessage != null)
					{
						onMessage(this, e as MessageEventArgs);
					}
				}
				else if (e is ErrorEventArgs)
				{
					EventHandler<ErrorEventArgs> onError = this.OnError;
					if (onError != null)
					{
						onError(this, e as ErrorEventArgs);
					}
				}
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000029A0 File Offset: 0x00000BA0
		[Conditional("UNITY_WEB_SOCKET_LOG")]
		private static void Log(string msg)
		{
			global::UnityEngine.Debug.Log(string.Concat(new string[]
			{
				"<color=yellow>[UnityWebSocket]</color>",
				string.Format("<color=green>[T-{0:D3}]</color>", Thread.CurrentThread.ManagedThreadId),
				string.Format("<color=red>[{0}]</color>", DateTime.Now.TimeOfDay),
				" ",
				msg
			}));
		}

		// Token: 0x04000033 RID: 51
		private ClientWebSocket socket;

		// Token: 0x04000034 RID: 52
		private object sendQueueLock = new object();

		// Token: 0x04000035 RID: 53
		private Queue<WebSocket.SendBuffer> sendQueue = new Queue<WebSocket.SendBuffer>();

		// Token: 0x04000036 RID: 54
		private bool isSendTaskRunning;

		// Token: 0x04000037 RID: 55
		private readonly Queue<EventArgs> eventQueue = new Queue<EventArgs>();

		// Token: 0x04000038 RID: 56
		private readonly object eventQueueLock = new object();

		// Token: 0x0200000E RID: 14
		private class SendBuffer
		{
			// Token: 0x06000054 RID: 84 RVA: 0x00002A0C File Offset: 0x00000C0C
			public SendBuffer(byte[] data, WebSocketMessageType type)
			{
				this.data = data;
				this.type = type;
			}

			// Token: 0x04000039 RID: 57
			public byte[] data;

			// Token: 0x0400003A RID: 58
			public WebSocketMessageType type;
		}
	}
}
