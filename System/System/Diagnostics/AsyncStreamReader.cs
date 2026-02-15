using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000170 RID: 368
	internal class AsyncStreamReader : IDisposable
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x0002DFE8 File Offset: 0x0002C1E8
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding)
			: this(process, stream, callback, encoding, 1024)
		{
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002DFFA File Offset: 0x0002C1FA
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding, int bufferSize)
		{
			this.Init(process, stream, callback, encoding, bufferSize);
			this.messageQueue = new Queue();
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002E028 File Offset: 0x0002C228
		private void Init(Process process, Stream stream, UserCallBack callback, Encoding encoding, int bufferSize)
		{
			this.process = process;
			this.stream = stream;
			this.encoding = encoding;
			this.userCallBack = callback;
			this.decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this.charBuffer = new char[this._maxCharsPerBuffer];
			this.cancelOperation = false;
			this.eofEvent = new ManualResetEvent(false);
			this.sb = null;
			this.bLastCarriageReturn = false;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002E0BD File Offset: 0x0002C2BD
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002E0C6 File Offset: 0x0002C2C6
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002E0D8 File Offset: 0x0002C2D8
		protected virtual void Dispose(bool disposing)
		{
			object obj = this.syncObject;
			lock (obj)
			{
				if (disposing && this.stream != null)
				{
					if (this.asyncReadResult != null && !this.asyncReadResult.IsCompleted && this.stream is FileStream)
					{
						SafeHandle safeFileHandle = ((FileStream)this.stream).SafeFileHandle;
						MonoIOError monoIOError;
						while (!this.asyncReadResult.IsCompleted && (MonoIO.Cancel(safeFileHandle, out monoIOError) || monoIOError != MonoIOError.ERROR_NOT_SUPPORTED))
						{
							this.asyncReadResult.AsyncWaitHandle.WaitOne(200);
						}
					}
					this.stream.Close();
				}
				if (this.stream != null)
				{
					this.stream = null;
					this.encoding = null;
					this.decoder = null;
					this.byteBuffer = null;
					this.charBuffer = null;
				}
				if (this.eofEvent != null)
				{
					this.eofEvent.Close();
					this.eofEvent = null;
				}
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002E1D4 File Offset: 0x0002C3D4
		internal void BeginReadLine()
		{
			if (this.cancelOperation)
			{
				this.cancelOperation = false;
			}
			if (this.sb == null)
			{
				this.sb = new StringBuilder(1024);
				this.asyncReadResult = this.stream.BeginRead(this.byteBuffer, 0, this.byteBuffer.Length, new AsyncCallback(this.ReadBuffer), null);
				return;
			}
			this.FlushMessageQueue();
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002E23C File Offset: 0x0002C43C
		internal void CancelOperation()
		{
			this.cancelOperation = true;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002E248 File Offset: 0x0002C448
		private void ReadBuffer(IAsyncResult ar)
		{
			int num;
			try
			{
				object obj = this.syncObject;
				lock (obj)
				{
					this.asyncReadResult = null;
					if (this.stream == null)
					{
						num = 0;
					}
					else
					{
						num = this.stream.EndRead(ar);
					}
				}
			}
			catch (IOException)
			{
				num = 0;
			}
			catch (OperationCanceledException)
			{
				num = 0;
			}
			for (;;)
			{
				object obj;
				if (num == 0)
				{
					Queue queue = this.messageQueue;
					lock (queue)
					{
						if (this.sb.Length != 0)
						{
							this.messageQueue.Enqueue(this.sb.ToString());
							this.sb.Length = 0;
						}
						this.messageQueue.Enqueue(null);
					}
					try
					{
						this.FlushMessageQueue();
						break;
					}
					finally
					{
						obj = this.syncObject;
						lock (obj)
						{
							if (this.eofEvent != null)
							{
								try
								{
									this.eofEvent.Set();
								}
								catch (ObjectDisposedException)
								{
								}
							}
						}
					}
				}
				obj = this.syncObject;
				lock (obj)
				{
					if (this.decoder == null)
					{
						num = 0;
						continue;
					}
					int chars = this.decoder.GetChars(this.byteBuffer, 0, num, this.charBuffer, 0);
					this.sb.Append(this.charBuffer, 0, chars);
				}
				this.GetLinesFromStringBuilder();
				obj = this.syncObject;
				lock (obj)
				{
					if (this.stream == null)
					{
						num = 0;
						continue;
					}
					this.asyncReadResult = this.stream.BeginRead(this.byteBuffer, 0, this.byteBuffer.Length, new AsyncCallback(this.ReadBuffer), null);
				}
				break;
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002E46C File Offset: 0x0002C66C
		private void GetLinesFromStringBuilder()
		{
			int i = this.currentLinePos;
			int num = 0;
			int length = this.sb.Length;
			if (this.bLastCarriageReturn && length > 0 && this.sb[0] == '\n')
			{
				i = 1;
				num = 1;
				this.bLastCarriageReturn = false;
			}
			while (i < length)
			{
				char c = this.sb[i];
				if (c == '\r' || c == '\n')
				{
					string text = this.sb.ToString(num, i - num);
					num = i + 1;
					if (c == '\r' && num < length && this.sb[num] == '\n')
					{
						num++;
						i++;
					}
					Queue queue = this.messageQueue;
					lock (queue)
					{
						this.messageQueue.Enqueue(text);
					}
				}
				i++;
			}
			if (this.sb[length - 1] == '\r')
			{
				this.bLastCarriageReturn = true;
			}
			if (num < length)
			{
				if (num == 0)
				{
					this.currentLinePos = i;
				}
				else
				{
					this.sb.Remove(0, num);
					this.currentLinePos = 0;
				}
			}
			else
			{
				this.sb.Length = 0;
				this.currentLinePos = 0;
			}
			this.FlushMessageQueue();
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002E5B4 File Offset: 0x0002C7B4
		private void FlushMessageQueue()
		{
			while (this.messageQueue.Count > 0)
			{
				Queue queue = this.messageQueue;
				lock (queue)
				{
					if (this.messageQueue.Count > 0)
					{
						string text = (string)this.messageQueue.Dequeue();
						if (!this.cancelOperation)
						{
							this.userCallBack(text);
						}
					}
					continue;
				}
				break;
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0002E630 File Offset: 0x0002C830
		internal void WaitUtilEOF()
		{
			if (this.eofEvent != null)
			{
				this.eofEvent.WaitOne();
				this.eofEvent.Close();
				this.eofEvent = null;
			}
		}

		// Token: 0x04000688 RID: 1672
		private Stream stream;

		// Token: 0x04000689 RID: 1673
		private Encoding encoding;

		// Token: 0x0400068A RID: 1674
		private Decoder decoder;

		// Token: 0x0400068B RID: 1675
		private byte[] byteBuffer;

		// Token: 0x0400068C RID: 1676
		private char[] charBuffer;

		// Token: 0x0400068D RID: 1677
		private int _maxCharsPerBuffer;

		// Token: 0x0400068E RID: 1678
		private Process process;

		// Token: 0x0400068F RID: 1679
		private UserCallBack userCallBack;

		// Token: 0x04000690 RID: 1680
		private bool cancelOperation;

		// Token: 0x04000691 RID: 1681
		private ManualResetEvent eofEvent;

		// Token: 0x04000692 RID: 1682
		private Queue messageQueue;

		// Token: 0x04000693 RID: 1683
		private StringBuilder sb;

		// Token: 0x04000694 RID: 1684
		private bool bLastCarriageReturn;

		// Token: 0x04000695 RID: 1685
		private int currentLinePos;

		// Token: 0x04000696 RID: 1686
		private object syncObject = new object();

		// Token: 0x04000697 RID: 1687
		private IAsyncResult asyncReadResult;
	}
}
