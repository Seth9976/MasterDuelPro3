using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Message Authentication Code (MAC) using <see cref="T:System.Security.Cryptography.TripleDES" /> for the input data <see cref="T:System.Security.Cryptography.CryptoStream" />.</summary>
	// Token: 0x0200039C RID: 924
	[ComVisible(true)]
	public class MACTripleDES : KeyedHashAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.MACTripleDES" /> class.</summary>
		// Token: 0x06001FAF RID: 8111 RVA: 0x0007D470 File Offset: 0x0007B670
		public MACTripleDES()
		{
			this.KeyValue = new byte[24];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			this.des = TripleDES.Create();
			this.HashSizeValue = this.des.BlockSize;
			this.m_bytesPerBlock = this.des.BlockSize / 8;
			this.des.IV = new byte[this.m_bytesPerBlock];
			this.des.Padding = PaddingMode.Zeros;
			this.m_encryptor = null;
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.MACTripleDES" />.</summary>
		// Token: 0x06001FB0 RID: 8112 RVA: 0x0007D4F8 File Offset: 0x0007B6F8
		public override void Initialize()
		{
			this.m_encryptor = null;
		}

		/// <summary>Routes data written to the object into the <see cref="T:System.Security.Cryptography.TripleDES" /> encryptor for computing the Message Authentication Code (MAC).</summary>
		/// <param name="rgbData">The input data. </param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data. </param>
		/// <param name="cbSize">The number of bytes in the array to use as data. </param>
		// Token: 0x06001FB1 RID: 8113 RVA: 0x0007D504 File Offset: 0x0007B704
		protected override void HashCore(byte[] rgbData, int ibStart, int cbSize)
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.Write(rgbData, ibStart, cbSize);
		}

		/// <summary>Returns the computed Message Authentication Code (MAC) after all data is written to the object.</summary>
		/// <returns>The computed MAC.</returns>
		// Token: 0x06001FB2 RID: 8114 RVA: 0x0007D57C File Offset: 0x0007B77C
		protected override byte[] HashFinal()
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.FlushFinalBlock();
			return this._ts.Buffer;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.MACTripleDES" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001FB3 RID: 8115 RVA: 0x0007D5FC File Offset: 0x0007B7FC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.des != null)
				{
					this.des.Clear();
				}
				if (this.m_encryptor != null)
				{
					this.m_encryptor.Dispose();
				}
				if (this._cs != null)
				{
					this._cs.Clear();
				}
				if (this._ts != null)
				{
					this._ts.Clear();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000EE1 RID: 3809
		private ICryptoTransform m_encryptor;

		// Token: 0x04000EE2 RID: 3810
		private CryptoStream _cs;

		// Token: 0x04000EE3 RID: 3811
		private TailStream _ts;

		// Token: 0x04000EE4 RID: 3812
		private int m_bytesPerBlock;

		// Token: 0x04000EE5 RID: 3813
		private TripleDES des;
	}
}
