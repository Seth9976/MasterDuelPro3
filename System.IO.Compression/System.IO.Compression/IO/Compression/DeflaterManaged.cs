using System;

namespace System.IO.Compression
{
	// Token: 0x0200000A RID: 10
	internal sealed class DeflaterManaged
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002B3E File Offset: 0x00000D3E
		internal bool NeedsInput()
		{
			return this._input.Count == 0 && this._deflateEncoder.BytesInHistory == 0;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B60 File Offset: 0x00000D60
		internal void SetInput(byte[] inputBuffer, int startIndex, int count)
		{
			this._input.Buffer = inputBuffer;
			this._input.Count = count;
			this._input.StartIndex = startIndex;
			if (count > 0 && count < 256)
			{
				DeflaterManaged.DeflaterState processingState = this._processingState;
				if (processingState != DeflaterManaged.DeflaterState.NotStarted)
				{
					if (processingState == DeflaterManaged.DeflaterState.CompressThenCheck)
					{
						this._processingState = DeflaterManaged.DeflaterState.HandlingSmallData;
						return;
					}
					if (processingState != DeflaterManaged.DeflaterState.CheckingForIncompressible)
					{
						return;
					}
				}
				this._processingState = DeflaterManaged.DeflaterState.StartingSmallData;
				return;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BC0 File Offset: 0x00000DC0
		internal int GetDeflateOutput(byte[] outputBuffer)
		{
			this._output.UpdateBuffer(outputBuffer);
			switch (this._processingState)
			{
			case DeflaterManaged.DeflaterState.NotStarted:
			{
				DeflateInput.InputState inputState = this._input.DumpState();
				OutputBuffer.BufferState bufferState = this._output.DumpState();
				this._deflateEncoder.GetBlockHeader(this._output);
				this._deflateEncoder.GetCompressedData(this._input, this._output);
				if (!this.UseCompressed(this._deflateEncoder.LastCompressionRatio))
				{
					this._input.RestoreState(inputState);
					this._output.RestoreState(bufferState);
					this._copyEncoder.GetBlock(this._input, this._output, false);
					this.FlushInputWindows();
					this._processingState = DeflaterManaged.DeflaterState.CheckingForIncompressible;
					goto IL_023A;
				}
				this._processingState = DeflaterManaged.DeflaterState.CompressThenCheck;
				goto IL_023A;
			}
			case DeflaterManaged.DeflaterState.SlowDownForIncompressible1:
				this._deflateEncoder.GetBlockFooter(this._output);
				this._processingState = DeflaterManaged.DeflaterState.SlowDownForIncompressible2;
				break;
			case DeflaterManaged.DeflaterState.SlowDownForIncompressible2:
				break;
			case DeflaterManaged.DeflaterState.StartingSmallData:
				this._deflateEncoder.GetBlockHeader(this._output);
				this._processingState = DeflaterManaged.DeflaterState.HandlingSmallData;
				goto IL_0223;
			case DeflaterManaged.DeflaterState.CompressThenCheck:
				this._deflateEncoder.GetCompressedData(this._input, this._output);
				if (!this.UseCompressed(this._deflateEncoder.LastCompressionRatio))
				{
					this._processingState = DeflaterManaged.DeflaterState.SlowDownForIncompressible1;
					this._inputFromHistory = this._deflateEncoder.UnprocessedInput;
					goto IL_023A;
				}
				goto IL_023A;
			case DeflaterManaged.DeflaterState.CheckingForIncompressible:
			{
				DeflateInput.InputState inputState2 = this._input.DumpState();
				OutputBuffer.BufferState bufferState2 = this._output.DumpState();
				this._deflateEncoder.GetBlock(this._input, this._output, 8072);
				if (!this.UseCompressed(this._deflateEncoder.LastCompressionRatio))
				{
					this._input.RestoreState(inputState2);
					this._output.RestoreState(bufferState2);
					this._copyEncoder.GetBlock(this._input, this._output, false);
					this.FlushInputWindows();
					goto IL_023A;
				}
				goto IL_023A;
			}
			case DeflaterManaged.DeflaterState.HandlingSmallData:
				goto IL_0223;
			default:
				goto IL_023A;
			}
			if (this._inputFromHistory.Count > 0)
			{
				this._copyEncoder.GetBlock(this._inputFromHistory, this._output, false);
			}
			if (this._inputFromHistory.Count == 0)
			{
				this._deflateEncoder.FlushInput();
				this._processingState = DeflaterManaged.DeflaterState.CheckingForIncompressible;
				goto IL_023A;
			}
			goto IL_023A;
			IL_0223:
			this._deflateEncoder.GetCompressedData(this._input, this._output);
			IL_023A:
			return this._output.BytesWritten;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002E14 File Offset: 0x00001014
		internal bool Finish(byte[] outputBuffer, out int bytesRead)
		{
			if (this._processingState == DeflaterManaged.DeflaterState.NotStarted)
			{
				bytesRead = 0;
				return true;
			}
			this._output.UpdateBuffer(outputBuffer);
			if (this._processingState == DeflaterManaged.DeflaterState.CompressThenCheck || this._processingState == DeflaterManaged.DeflaterState.HandlingSmallData || this._processingState == DeflaterManaged.DeflaterState.SlowDownForIncompressible1)
			{
				this._deflateEncoder.GetBlockFooter(this._output);
			}
			this.WriteFinal();
			bytesRead = this._output.BytesWritten;
			return true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002E7A File Offset: 0x0000107A
		private bool UseCompressed(double ratio)
		{
			return ratio <= 1.0;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E8B File Offset: 0x0000108B
		private void FlushInputWindows()
		{
			this._deflateEncoder.FlushInput();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002E98 File Offset: 0x00001098
		private void WriteFinal()
		{
			this._copyEncoder.GetBlock(null, this._output, true);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002EAD File Offset: 0x000010AD
		public void Dispose()
		{
		}

		// Token: 0x04000025 RID: 37
		private readonly FastEncoder _deflateEncoder;

		// Token: 0x04000026 RID: 38
		private readonly CopyEncoder _copyEncoder;

		// Token: 0x04000027 RID: 39
		private readonly DeflateInput _input;

		// Token: 0x04000028 RID: 40
		private readonly OutputBuffer _output;

		// Token: 0x04000029 RID: 41
		private DeflaterManaged.DeflaterState _processingState;

		// Token: 0x0400002A RID: 42
		private DeflateInput _inputFromHistory;

		// Token: 0x0200000B RID: 11
		private enum DeflaterState
		{
			// Token: 0x0400002C RID: 44
			NotStarted,
			// Token: 0x0400002D RID: 45
			SlowDownForIncompressible1,
			// Token: 0x0400002E RID: 46
			SlowDownForIncompressible2,
			// Token: 0x0400002F RID: 47
			StartingSmallData,
			// Token: 0x04000030 RID: 48
			CompressThenCheck,
			// Token: 0x04000031 RID: 49
			CheckingForIncompressible,
			// Token: 0x04000032 RID: 50
			HandlingSmallData
		}
	}
}
