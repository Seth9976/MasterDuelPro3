using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000048 RID: 72
	public class ARC4Managed : RC4, ICryptoTransform, IDisposable
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00009705 File Offset: 0x00007905
		public ARC4Managed()
		{
			this.state = new byte[256];
			this.m_disposed = false;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00009724 File Offset: 0x00007924
		~ARC4Managed()
		{
			this.Dispose(true);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009754 File Offset: 0x00007954
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				this.x = 0;
				this.y = 0;
				if (this.key != null)
				{
					Array.Clear(this.key, 0, this.key.Length);
					this.key = null;
				}
				Array.Clear(this.state, 0, this.state.Length);
				this.state = null;
				GC.SuppressFinalize(this);
				this.m_disposed = true;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000097C2 File Offset: 0x000079C2
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000097E4 File Offset: 0x000079E4
		public override byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Key");
				}
				this.KeyValue = (this.key = (byte[])value.Clone());
				this.KeySetup(this.key);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00009825 File Offset: 0x00007A25
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000982F File Offset: 0x00007A2F
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this.CreateEncryptor();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000983E File Offset: 0x00007A3E
		public override void GenerateIV()
		{
			this.IV = new byte[0];
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000984C File Offset: 0x00007A4C
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00009861 File Offset: 0x00007A61
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00009861 File Offset: 0x00007A61
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00009861 File Offset: 0x00007A61
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00009864 File Offset: 0x00007A64
		private void KeySetup(byte[] key)
		{
			byte b = 0;
			byte b2 = 0;
			for (int i = 0; i < 256; i++)
			{
				this.state[i] = (byte)i;
			}
			this.x = 0;
			this.y = 0;
			for (int j = 0; j < 256; j++)
			{
				b2 = key[(int)b] + this.state[j] + b2;
				byte b3 = this.state[j];
				this.state[j] = this.state[(int)b2];
				this.state[(int)b2] = b3;
				b = (byte)((int)(b + 1) % key.Length);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000098EC File Offset: 0x00007AEC
		private void CheckInput(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "< 0");
			}
			if (inputCount < 0)
			{
				throw new ArgumentOutOfRangeException("inputCount", "< 0");
			}
			if (inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException(Locale.GetText("Overflow"), "inputBuffer");
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000994C File Offset: 0x00007B4C
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("outputOffset", "< 0");
			}
			if (outputOffset > outputBuffer.Length - inputCount)
			{
				throw new ArgumentException(Locale.GetText("Overflow"), "outputBuffer");
			}
			return this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000099B4 File Offset: 0x00007BB4
		private int InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = 0; i < inputCount; i++)
			{
				this.x += 1;
				this.y = this.state[(int)this.x] + this.y;
				byte b = this.state[(int)this.x];
				this.state[(int)this.x] = this.state[(int)this.y];
				this.state[(int)this.y] = b;
				byte b2 = this.state[(int)this.x] + this.state[(int)this.y];
				outputBuffer[outputOffset + i] = inputBuffer[inputOffset + i] ^ this.state[(int)b2];
			}
			return inputCount;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009A68 File Offset: 0x00007C68
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			byte[] array = new byte[inputCount];
			this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x040001F6 RID: 502
		private byte[] key;

		// Token: 0x040001F7 RID: 503
		private byte[] state;

		// Token: 0x040001F8 RID: 504
		private byte x;

		// Token: 0x040001F9 RID: 505
		private byte y;

		// Token: 0x040001FA RID: 506
		private bool m_disposed;
	}
}
