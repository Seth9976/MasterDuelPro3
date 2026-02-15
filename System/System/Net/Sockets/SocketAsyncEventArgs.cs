using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Net.Sockets
{
	/// <summary>Represents an asynchronous socket operation.</summary>
	// Token: 0x020004C2 RID: 1218
	public class SocketAsyncEventArgs : EventArgs, IDisposable
	{
		/// <summary>Gets or sets the socket to use or the socket created for accepting a connection with an asynchronous socket method.</summary>
		/// <returns>The <see cref="T:System.Net.Sockets.Socket" /> to use or the socket created for accepting a connection with an asynchronous socket method.</returns>
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x000826FF File Offset: 0x000808FF
		// (set) Token: 0x06001DF4 RID: 7668 RVA: 0x00082707 File Offset: 0x00080907
		public Socket AcceptSocket { get; set; }

		/// <summary>Gets the number of bytes transferred in the socket operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the number of bytes transferred in the socket operation.</returns>
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x00082710 File Offset: 0x00080910
		// (set) Token: 0x06001DF6 RID: 7670 RVA: 0x00082718 File Offset: 0x00080918
		public int BytesTransferred { get; private set; }

		/// <summary>Gets the type of socket operation most recently performed with this context object.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketAsyncOperation" /> instance that indicates the type of socket operation most recently performed with this context object.</returns>
		// Token: 0x17000694 RID: 1684
		// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x00082721 File Offset: 0x00080921
		private SocketAsyncOperation LastOperation
		{
			[CompilerGenerated]
			set
			{
				this.<LastOperation>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the remote IP endpoint for an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> that represents the remote IP endpoint for an asynchronous operation.</returns>
		// Token: 0x17000695 RID: 1685
		// (set) Token: 0x06001DF8 RID: 7672 RVA: 0x0008272A File Offset: 0x0008092A
		public EndPoint RemoteEndPoint
		{
			set
			{
				this.remote_ep = value;
			}
		}

		/// <summary>Gets or sets the size, in bytes, of the data block used in the send operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the data block used in the send operation.</returns>
		// Token: 0x17000696 RID: 1686
		// (set) Token: 0x06001DF9 RID: 7673 RVA: 0x00082733 File Offset: 0x00080933
		[MonoTODO("unused property")]
		public int SendPacketsSendSize
		{
			[CompilerGenerated]
			set
			{
				this.<SendPacketsSendSize>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the result of the asynchronous socket operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketError" /> that represents the result of the asynchronous socket operation.</returns>
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0008273C File Offset: 0x0008093C
		// (set) Token: 0x06001DFB RID: 7675 RVA: 0x00082744 File Offset: 0x00080944
		public SocketError SocketError { get; set; }

		/// <summary>Gets the results of an asynchronous socket operation or sets the behavior of an asynchronous operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketFlags" /> that represents the results of an asynchronous socket operation.</returns>
		// Token: 0x17000698 RID: 1688
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x0008274D File Offset: 0x0008094D
		public SocketFlags SocketFlags
		{
			[CompilerGenerated]
			set
			{
				this.<SocketFlags>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets a user or application object associated with this asynchronous socket operation.</summary>
		/// <returns>An object that represents the user or application object associated with this asynchronous socket operation.</returns>
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x00082756 File Offset: 0x00080956
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x0008275E File Offset: 0x0008095E
		public object UserToken { get; set; }

		/// <summary>The event used to complete an asynchronous operation.</summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001DFF RID: 7679 RVA: 0x00082768 File Offset: 0x00080968
		// (remove) Token: 0x06001E00 RID: 7680 RVA: 0x000827A0 File Offset: 0x000809A0
		public event EventHandler<SocketAsyncEventArgs> Completed;

		/// <summary>Creates an empty <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The platform is not supported. </exception>
		// Token: 0x06001E01 RID: 7681 RVA: 0x000827D5 File Offset: 0x000809D5
		public SocketAsyncEventArgs()
		{
			this.SendPacketsSendSize = -1;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000827EF File Offset: 0x000809EF
		internal SocketAsyncEventArgs(bool flowExecutionContext)
		{
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x00082804 File Offset: 0x00080A04
		~SocketAsyncEventArgs()
		{
			this.Dispose(false);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00082834 File Offset: 0x00080A34
		private void Dispose(bool disposing)
		{
			this.disposed = true;
			if (disposing)
			{
				int num = this.in_progress;
				return;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance and optionally disposes of the managed resources.</summary>
		// Token: 0x06001E05 RID: 7685 RVA: 0x0008284A File Offset: 0x00080A4A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00082859 File Offset: 0x00080A59
		internal void SetBytesTransferred(int value)
		{
			this.BytesTransferred = value;
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x00082862 File Offset: 0x00080A62
		internal Socket CurrentSocket
		{
			get
			{
				return this.current_socket;
			}
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x0008286A File Offset: 0x00080A6A
		internal void SetCurrentSocket(Socket socket)
		{
			this.current_socket = socket;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00082873 File Offset: 0x00080A73
		internal void SetLastOperation(SocketAsyncOperation op)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("System.Net.Sockets.SocketAsyncEventArgs");
			}
			if (Interlocked.Exchange(ref this.in_progress, 1) != 0)
			{
				throw new InvalidOperationException("Operation already in progress");
			}
			this.LastOperation = op;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000828A8 File Offset: 0x00080AA8
		internal void Complete_internal()
		{
			this.in_progress = 0;
			this.OnCompleted(this);
		}

		/// <summary>Represents a method that is called when an asynchronous operation completes.</summary>
		/// <param name="e">The event that is signaled.</param>
		// Token: 0x06001E0B RID: 7691 RVA: 0x000828BC File Offset: 0x00080ABC
		protected virtual void OnCompleted(SocketAsyncEventArgs e)
		{
			if (e == null)
			{
				return;
			}
			EventHandler<SocketAsyncEventArgs> completed = e.Completed;
			if (completed != null)
			{
				completed(e.current_socket, e);
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x000828E4 File Offset: 0x00080AE4
		public Memory<byte> MemoryBuffer
		{
			get
			{
				return this._buffer;
			}
		}

		/// <summary>Gets the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</returns>
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x000828EC File Offset: 0x00080AEC
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		/// <summary>Gets the maximum amount of data, in bytes, to send or receive in an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the maximum amount of data, in bytes, to send or receive.</returns>
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x000828F4 File Offset: 0x00080AF4
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Gets or sets an array of data buffers to use with an asynchronous socket method.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that represents an array of data buffers to use with an asynchronous socket method.</returns>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified on a set operation. This exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property has been set to a non-null value and an attempt was made to set the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property to a non-null value.</exception>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x000828FC File Offset: 0x00080AFC
		public IList<ArraySegment<byte>> BufferList
		{
			get
			{
				return this._bufferList;
			}
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00082904 File Offset: 0x00080B04
		public void SetBuffer(Memory<byte> buffer)
		{
			if (buffer.Length != 0 && this._bufferList != null)
			{
				throw new ArgumentException(SR.Format("Buffer and BufferList properties cannot both be non-null.", "BufferList"));
			}
			this._buffer = buffer;
			this._offset = 0;
			this._count = buffer.Length;
			this._bufferIsExplicitArray = false;
		}

		// Token: 0x04001538 RID: 5432
		private bool disposed;

		// Token: 0x04001539 RID: 5433
		internal volatile int in_progress;

		// Token: 0x0400153A RID: 5434
		private EndPoint remote_ep;

		// Token: 0x0400153B RID: 5435
		private Socket current_socket;

		// Token: 0x0400153C RID: 5436
		internal SocketAsyncResult socket_async_result = new SocketAsyncResult();

		// Token: 0x04001545 RID: 5445
		private Memory<byte> _buffer;

		// Token: 0x04001546 RID: 5446
		private int _offset;

		// Token: 0x04001547 RID: 5447
		private int _count;

		// Token: 0x04001548 RID: 5448
		private bool _bufferIsExplicitArray;

		// Token: 0x04001549 RID: 5449
		private IList<ArraySegment<byte>> _bufferList;
	}
}
