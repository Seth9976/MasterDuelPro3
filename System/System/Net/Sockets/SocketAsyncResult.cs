using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x020004C3 RID: 1219
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class SocketAsyncResult : IOAsyncResult
	{
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x00082959 File Offset: 0x00080B59
		public IntPtr Handle
		{
			get
			{
				if (this.socket == null)
				{
					return IntPtr.Zero;
				}
				return this.socket.Handle;
			}
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x00082974 File Offset: 0x00080B74
		public SocketAsyncResult()
		{
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x0008297C File Offset: 0x00080B7C
		public void Init(Socket socket, AsyncCallback callback, object state, SocketOperation operation)
		{
			base.Init(callback, state);
			this.socket = socket;
			this.operation = operation;
			this.DelayedException = null;
			this.EndPoint = null;
			this.Buffer = null;
			this.Offset = 0;
			this.Size = 0;
			this.SockFlags = SocketFlags.None;
			this.AcceptSocket = null;
			this.Addresses = null;
			this.Port = 0;
			this.Buffers = null;
			this.ReuseSocket = false;
			this.CurrentAddress = 0;
			this.AcceptedSocket = null;
			this.Total = 0;
			this.error = 0;
			this.EndCalled = 0;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00082A15 File Offset: 0x00080C15
		public SocketAsyncResult(Socket socket, AsyncCallback callback, object state, SocketOperation operation)
			: base(callback, state)
		{
			this.socket = socket;
			this.operation = operation;
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x00082A30 File Offset: 0x00080C30
		public SocketError ErrorCode
		{
			get
			{
				SocketException ex = this.DelayedException as SocketException;
				if (ex != null)
				{
					return ex.SocketErrorCode;
				}
				if (this.error != 0)
				{
					return (SocketError)this.error;
				}
				return SocketError.Success;
			}
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x00082A63 File Offset: 0x00080C63
		public void CheckIfThrowDelayedException()
		{
			if (this.DelayedException != null)
			{
				this.socket.is_connected = false;
				throw this.DelayedException;
			}
			if (this.error != 0)
			{
				this.socket.is_connected = false;
				throw new SocketException(this.error);
			}
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00082AA0 File Offset: 0x00080CA0
		internal override void CompleteDisposed()
		{
			this.Complete();
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00082AA8 File Offset: 0x00080CA8
		public void Complete()
		{
			if (this.operation != SocketOperation.Receive && this.socket.CleanedUp)
			{
				this.DelayedException = new ObjectDisposedException(this.socket.GetType().ToString());
			}
			base.IsCompleted = true;
			Socket socket = this.socket;
			SocketOperation socketOperation = this.operation;
			if (!base.CompletedSynchronously && base.AsyncCallback != null)
			{
				ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
				{
					((SocketAsyncResult)state).AsyncCallback((SocketAsyncResult)state);
				}, this);
			}
			switch (socketOperation)
			{
			case SocketOperation.Accept:
			case SocketOperation.Receive:
			case SocketOperation.ReceiveFrom:
			case SocketOperation.ReceiveGeneric:
				socket.ReadSem.Release();
				return;
			case SocketOperation.Connect:
			case SocketOperation.RecvJustCallback:
			case SocketOperation.SendJustCallback:
			case SocketOperation.Disconnect:
			case SocketOperation.AcceptReceive:
				break;
			case SocketOperation.Send:
			case SocketOperation.SendTo:
			case SocketOperation.SendGeneric:
				socket.WriteSem.Release();
				break;
			default:
				return;
			}
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00082B81 File Offset: 0x00080D81
		public void Complete(bool synch)
		{
			base.CompletedSynchronously = synch;
			this.Complete();
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00082B90 File Offset: 0x00080D90
		public void Complete(int total)
		{
			this.Total = total;
			this.Complete();
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00082B9F File Offset: 0x00080D9F
		public void Complete(Exception e, bool synch)
		{
			this.DelayedException = e;
			base.CompletedSynchronously = synch;
			this.Complete();
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00082BB5 File Offset: 0x00080DB5
		public void Complete(Exception e)
		{
			this.DelayedException = e;
			this.Complete();
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00082BC4 File Offset: 0x00080DC4
		public void Complete(Socket s)
		{
			this.AcceptedSocket = s;
			this.Complete();
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00082BD3 File Offset: 0x00080DD3
		public void Complete(Socket s, int total)
		{
			this.AcceptedSocket = s;
			this.Total = total;
			this.Complete();
		}

		// Token: 0x0400154A RID: 5450
		public Socket socket;

		// Token: 0x0400154B RID: 5451
		public SocketOperation operation;

		// Token: 0x0400154C RID: 5452
		private Exception DelayedException;

		// Token: 0x0400154D RID: 5453
		public EndPoint EndPoint;

		// Token: 0x0400154E RID: 5454
		public Memory<byte> Buffer;

		// Token: 0x0400154F RID: 5455
		public int Offset;

		// Token: 0x04001550 RID: 5456
		public int Size;

		// Token: 0x04001551 RID: 5457
		public SocketFlags SockFlags;

		// Token: 0x04001552 RID: 5458
		public Socket AcceptSocket;

		// Token: 0x04001553 RID: 5459
		public IPAddress[] Addresses;

		// Token: 0x04001554 RID: 5460
		public int Port;

		// Token: 0x04001555 RID: 5461
		public IList<ArraySegment<byte>> Buffers;

		// Token: 0x04001556 RID: 5462
		public bool ReuseSocket;

		// Token: 0x04001557 RID: 5463
		public int CurrentAddress;

		// Token: 0x04001558 RID: 5464
		public Socket AcceptedSocket;

		// Token: 0x04001559 RID: 5465
		public int Total;

		// Token: 0x0400155A RID: 5466
		internal int error;

		// Token: 0x0400155B RID: 5467
		public int EndCalled;
	}
}
