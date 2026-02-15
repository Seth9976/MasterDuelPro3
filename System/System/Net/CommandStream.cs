using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net
{
	// Token: 0x02000388 RID: 904
	internal class CommandStream : NetworkStreamWrapper
	{
		// Token: 0x0600168F RID: 5775 RVA: 0x0005FA64 File Offset: 0x0005DC64
		internal CommandStream(TcpClient client)
			: base(client)
		{
			this._decoder = this._encoding.GetDecoder();
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0005FA94 File Offset: 0x0005DC94
		internal virtual void Abort(Exception e)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, "closing control Stream", "Abort");
			}
			lock (this)
			{
				if (this._aborted)
				{
					return;
				}
				this._aborted = true;
			}
			try
			{
				base.Close(0);
			}
			finally
			{
				if (e != null)
				{
					this.InvokeRequestCallback(e);
				}
				else
				{
					this.InvokeRequestCallback(null);
				}
			}
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0005FB1C File Offset: 0x0005DD1C
		protected override void Dispose(bool disposing)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "Dispose");
			}
			this.InvokeRequestCallback(null);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0005FB38 File Offset: 0x0005DD38
		protected void InvokeRequestCallback(object obj)
		{
			WebRequest request = this._request;
			if (request != null)
			{
				((FtpWebRequest)request).RequestCallback(obj);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0005FB5B File Offset: 0x0005DD5B
		internal bool RecoverableFailure
		{
			get
			{
				return this._recoverableFailure;
			}
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0005FB63 File Offset: 0x0005DD63
		protected void MarkAsRecoverableFailure()
		{
			if (this._index <= 1)
			{
				this._recoverableFailure = true;
			}
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0005FB78 File Offset: 0x0005DD78
		internal Stream SubmitRequest(WebRequest request, bool isAsync, bool readInitalResponseOnConnect)
		{
			this.ClearState();
			CommandStream.PipelineEntry[] array = this.BuildCommandsList(request);
			this.InitCommandPipeline(request, array, isAsync);
			if (readInitalResponseOnConnect)
			{
				this._doSend = false;
				this._index = -1;
			}
			return this.ContinueCommandPipeline();
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0005FBB3 File Offset: 0x0005DDB3
		protected virtual void ClearState()
		{
			this.InitCommandPipeline(null, null, false);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000027B6 File Offset: 0x000009B6
		protected virtual CommandStream.PipelineEntry[] BuildCommandsList(WebRequest request)
		{
			return null;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0005FBBE File Offset: 0x0005DDBE
		protected Exception GenerateException(string message, WebExceptionStatus status, Exception innerException)
		{
			return new WebException(message, innerException, status, null);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0005FBC9 File Offset: 0x0005DDC9
		protected Exception GenerateException(FtpStatusCode code, string statusDescription, Exception innerException)
		{
			return new WebException(SR.Format("The remote server returned an error: {0}.", NetRes.GetWebStatusCodeString(code, statusDescription)), innerException, WebExceptionStatus.ProtocolError, null);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0005FBE4 File Offset: 0x0005DDE4
		protected void InitCommandPipeline(WebRequest request, CommandStream.PipelineEntry[] commands, bool isAsync)
		{
			this._commands = commands;
			this._index = 0;
			this._request = request;
			this._aborted = false;
			this._doRead = true;
			this._doSend = true;
			this._currentResponseDescription = null;
			this._isAsync = isAsync;
			this._recoverableFailure = false;
			this._abortReason = string.Empty;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0005FC3C File Offset: 0x0005DE3C
		internal void CheckContinuePipeline()
		{
			if (this._isAsync)
			{
				return;
			}
			try
			{
				this.ContinueCommandPipeline();
			}
			catch (Exception ex)
			{
				this.Abort(ex);
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0005FC78 File Offset: 0x0005DE78
		protected Stream ContinueCommandPipeline()
		{
			bool isAsync = this._isAsync;
			while (this._index < this._commands.Length)
			{
				if (this._doSend)
				{
					if (this._index < 0)
					{
						throw new InternalException();
					}
					byte[] bytes = this.Encoding.GetBytes(this._commands[this._index].Command);
					if (NetEventSource.Log.IsEnabled())
					{
						string text = this._commands[this._index].Command.Substring(0, this._commands[this._index].Command.Length - 2);
						if (this._commands[this._index].HasFlag(CommandStream.PipelineEntryFlags.DontLogParameter))
						{
							int num = text.IndexOf(' ');
							if (num != -1)
							{
								text = text.Substring(0, num) + " ********";
							}
						}
						if (NetEventSource.IsEnabled)
						{
							NetEventSource.Info(this, FormattableStringFactory.Create("Sending command {0}", new object[] { text }), "ContinueCommandPipeline");
						}
					}
					try
					{
						if (isAsync)
						{
							this.BeginWrite(bytes, 0, bytes.Length, CommandStream.s_writeCallbackDelegate, this);
						}
						else
						{
							this.Write(bytes, 0, bytes.Length);
						}
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					if (isAsync)
					{
						return null;
					}
				}
				Stream stream = null;
				if (this.PostSendCommandProcessing(ref stream))
				{
					return stream;
				}
			}
			lock (this)
			{
				this.Close();
			}
			return null;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0005FE0C File Offset: 0x0005E00C
		private bool PostSendCommandProcessing(ref Stream stream)
		{
			if (this._doRead)
			{
				bool isAsync = this._isAsync;
				int index = this._index;
				CommandStream.PipelineEntry[] commands = this._commands;
				try
				{
					ResponseDescription responseDescription = this.ReceiveCommandResponse();
					if (isAsync)
					{
						return true;
					}
					this._currentResponseDescription = responseDescription;
				}
				catch
				{
					if (index < 0 || index >= commands.Length || commands[index].Command != "QUIT\r\n")
					{
						throw;
					}
				}
			}
			return this.PostReadCommandProcessing(ref stream);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0005FE8C File Offset: 0x0005E08C
		private bool PostReadCommandProcessing(ref Stream stream)
		{
			if (this._index >= this._commands.Length)
			{
				return false;
			}
			this._doSend = false;
			this._doRead = false;
			CommandStream.PipelineEntry pipelineEntry;
			if (this._index == -1)
			{
				pipelineEntry = null;
			}
			else
			{
				pipelineEntry = this._commands[this._index];
			}
			CommandStream.PipelineInstruction pipelineInstruction;
			if (this._currentResponseDescription == null && pipelineEntry.Command == "QUIT\r\n")
			{
				pipelineInstruction = CommandStream.PipelineInstruction.Advance;
			}
			else
			{
				pipelineInstruction = this.PipelineCallback(pipelineEntry, this._currentResponseDescription, false, ref stream);
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Abort)
			{
				Exception ex;
				if (this._abortReason != string.Empty)
				{
					ex = new WebException(this._abortReason);
				}
				else
				{
					ex = this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
				}
				this.Abort(ex);
				throw ex;
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Advance)
			{
				this._currentResponseDescription = null;
				this._doSend = true;
				this._doRead = true;
				this._index++;
			}
			else
			{
				if (pipelineInstruction == CommandStream.PipelineInstruction.Pause)
				{
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.GiveStream)
				{
					this._currentResponseDescription = null;
					this._doRead = true;
					if (this._isAsync)
					{
						this.ContinueCommandPipeline();
						this.InvokeRequestCallback(stream);
					}
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.Reread)
				{
					this._currentResponseDescription = null;
					this._doRead = true;
				}
			}
			return false;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000028AE File Offset: 0x00000AAE
		protected virtual CommandStream.PipelineInstruction PipelineCallback(CommandStream.PipelineEntry entry, ResponseDescription response, bool timeout, ref Stream stream)
		{
			return CommandStream.PipelineInstruction.Abort;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0005FFAC File Offset: 0x0005E1AC
		private static void ReadCallback(IAsyncResult asyncResult)
		{
			ReceiveState receiveState = (ReceiveState)asyncResult.AsyncState;
			try
			{
				Stream connection = receiveState.Connection;
				int num = 0;
				try
				{
					num = connection.EndRead(asyncResult);
					if (num == 0)
					{
						receiveState.Connection.CloseSocket();
					}
				}
				catch (IOException)
				{
					receiveState.Connection.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				receiveState.Connection.ReceiveCommandResponseCallback(receiveState, num);
			}
			catch (Exception ex)
			{
				receiveState.Connection.Abort(ex);
			}
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00060040 File Offset: 0x0005E240
		private static void WriteCallback(IAsyncResult asyncResult)
		{
			CommandStream commandStream = (CommandStream)asyncResult.AsyncState;
			try
			{
				try
				{
					commandStream.EndWrite(asyncResult);
				}
				catch (IOException)
				{
					commandStream.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				Stream stream = null;
				if (!commandStream.PostSendCommandProcessing(ref stream))
				{
					commandStream.ContinueCommandPipeline();
				}
			}
			catch (Exception ex)
			{
				commandStream.Abort(ex);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x000600B8 File Offset: 0x0005E2B8
		// (set) Token: 0x060016A3 RID: 5795 RVA: 0x000600C0 File Offset: 0x0005E2C0
		protected Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
				this._decoder = this._encoding.GetDecoder();
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000028AE File Offset: 0x00000AAE
		protected virtual bool CheckValid(ResponseDescription response, ref int validThrough, ref int completeLength)
		{
			return false;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000600DC File Offset: 0x0005E2DC
		private ResponseDescription ReceiveCommandResponse()
		{
			ReceiveState receiveState = new ReceiveState(this);
			try
			{
				if (this._buffer.Length > 0)
				{
					this.ReceiveCommandResponseCallback(receiveState, -1);
				}
				else
				{
					try
					{
						if (this._isAsync)
						{
							this.BeginRead(receiveState.Buffer, 0, receiveState.Buffer.Length, CommandStream.s_readCallbackDelegate, receiveState);
							return null;
						}
						int num = this.Read(receiveState.Buffer, 0, receiveState.Buffer.Length);
						if (num == 0)
						{
							base.CloseSocket();
						}
						this.ReceiveCommandResponseCallback(receiveState, num);
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is WebException)
				{
					throw;
				}
				throw this.GenerateException("The underlying connection was closed: An unexpected error occurred on a receive", WebExceptionStatus.ReceiveFailure, ex);
			}
			return receiveState.Resp;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x000601B4 File Offset: 0x0005E3B4
		private void ReceiveCommandResponseCallback(ReceiveState state, int bytesRead)
		{
			int num = -1;
			for (;;)
			{
				int validThrough = state.ValidThrough;
				if (this._buffer.Length > 0)
				{
					state.Resp.StatusBuffer.Append(this._buffer);
					this._buffer = string.Empty;
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						break;
					}
				}
				else
				{
					if (bytesRead <= 0)
					{
						goto Block_3;
					}
					char[] array = new char[this._decoder.GetCharCount(state.Buffer, 0, bytesRead)];
					int chars = this._decoder.GetChars(state.Buffer, 0, bytesRead, array, 0, false);
					string text = new string(array, 0, chars);
					state.Resp.StatusBuffer.Append(text);
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						goto Block_4;
					}
					if (num >= 0)
					{
						int num2 = state.Resp.StatusBuffer.Length - num;
						if (num2 > 0)
						{
							this._buffer = text.Substring(text.Length - num2, num2);
						}
					}
				}
				if (num < 0)
				{
					state.ValidThrough = validThrough;
					try
					{
						if (this._isAsync)
						{
							this.BeginRead(state.Buffer, 0, state.Buffer.Length, CommandStream.s_readCallbackDelegate, state);
							return;
						}
						bytesRead = this.Read(state.Buffer, 0, state.Buffer.Length);
						if (bytesRead == 0)
						{
							base.CloseSocket();
						}
						continue;
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					goto IL_017B;
				}
				goto IL_017B;
			}
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			Block_3:
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			Block_4:
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			IL_017B:
			string text2 = state.Resp.StatusBuffer.ToString();
			state.Resp.StatusDescription = text2.Substring(0, num);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("Received response: {0}", new object[] { text2.Substring(0, num - 2) }), "ReceiveCommandResponseCallback");
			}
			if (this._isAsync)
			{
				if (state.Resp != null)
				{
					this._currentResponseDescription = state.Resp;
				}
				Stream stream = null;
				if (this.PostReadCommandProcessing(ref stream))
				{
					return;
				}
				this.ContinueCommandPipeline();
			}
		}

		// Token: 0x04000DAD RID: 3501
		private static readonly AsyncCallback s_writeCallbackDelegate = new AsyncCallback(CommandStream.WriteCallback);

		// Token: 0x04000DAE RID: 3502
		private static readonly AsyncCallback s_readCallbackDelegate = new AsyncCallback(CommandStream.ReadCallback);

		// Token: 0x04000DAF RID: 3503
		private bool _recoverableFailure;

		// Token: 0x04000DB0 RID: 3504
		protected WebRequest _request;

		// Token: 0x04000DB1 RID: 3505
		protected bool _isAsync;

		// Token: 0x04000DB2 RID: 3506
		private bool _aborted;

		// Token: 0x04000DB3 RID: 3507
		protected CommandStream.PipelineEntry[] _commands;

		// Token: 0x04000DB4 RID: 3508
		protected int _index;

		// Token: 0x04000DB5 RID: 3509
		private bool _doRead;

		// Token: 0x04000DB6 RID: 3510
		private bool _doSend;

		// Token: 0x04000DB7 RID: 3511
		private ResponseDescription _currentResponseDescription;

		// Token: 0x04000DB8 RID: 3512
		protected string _abortReason;

		// Token: 0x04000DB9 RID: 3513
		private string _buffer = string.Empty;

		// Token: 0x04000DBA RID: 3514
		private Encoding _encoding = Encoding.UTF8;

		// Token: 0x04000DBB RID: 3515
		private Decoder _decoder;

		// Token: 0x02000389 RID: 905
		internal enum PipelineInstruction
		{
			// Token: 0x04000DBD RID: 3517
			Abort,
			// Token: 0x04000DBE RID: 3518
			Advance,
			// Token: 0x04000DBF RID: 3519
			Pause,
			// Token: 0x04000DC0 RID: 3520
			Reread,
			// Token: 0x04000DC1 RID: 3521
			GiveStream
		}

		// Token: 0x0200038A RID: 906
		[Flags]
		internal enum PipelineEntryFlags
		{
			// Token: 0x04000DC3 RID: 3523
			UserCommand = 1,
			// Token: 0x04000DC4 RID: 3524
			GiveDataStream = 2,
			// Token: 0x04000DC5 RID: 3525
			CreateDataConnection = 4,
			// Token: 0x04000DC6 RID: 3526
			DontLogParameter = 8
		}

		// Token: 0x0200038B RID: 907
		internal class PipelineEntry
		{
			// Token: 0x060016A8 RID: 5800 RVA: 0x00060400 File Offset: 0x0005E600
			internal PipelineEntry(string command)
			{
				this.Command = command;
			}

			// Token: 0x060016A9 RID: 5801 RVA: 0x0006040F File Offset: 0x0005E60F
			internal PipelineEntry(string command, CommandStream.PipelineEntryFlags flags)
			{
				this.Command = command;
				this.Flags = flags;
			}

			// Token: 0x060016AA RID: 5802 RVA: 0x00060425 File Offset: 0x0005E625
			internal bool HasFlag(CommandStream.PipelineEntryFlags flags)
			{
				return (this.Flags & flags) > (CommandStream.PipelineEntryFlags)0;
			}

			// Token: 0x04000DC7 RID: 3527
			internal string Command;

			// Token: 0x04000DC8 RID: 3528
			internal CommandStream.PipelineEntryFlags Flags;
		}
	}
}
