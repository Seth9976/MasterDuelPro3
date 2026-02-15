using System;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x020003C8 RID: 968
	internal class TripleDESTransform : SymmetricTransform
	{
		// Token: 0x060020FB RID: 8443 RVA: 0x00089A1C File Offset: 0x00087C1C
		public TripleDESTransform(TripleDES algo, bool encryption, byte[] key, byte[] iv)
			: base(algo, encryption, iv)
		{
			if (key == null)
			{
				key = TripleDESTransform.GetStrongKey();
			}
			if (TripleDES.IsWeakKey(key))
			{
				throw new CryptographicException(Locale.GetText("This is a known weak key."));
			}
			byte[] array = new byte[8];
			byte[] array2 = new byte[8];
			byte[] array3 = new byte[8];
			DES des = DES.Create();
			Buffer.BlockCopy(key, 0, array, 0, 8);
			Buffer.BlockCopy(key, 8, array2, 0, 8);
			if (key.Length == 16)
			{
				Buffer.BlockCopy(key, 0, array3, 0, 8);
			}
			else
			{
				Buffer.BlockCopy(key, 16, array3, 0, 8);
			}
			if (encryption || algo.Mode == CipherMode.CFB)
			{
				this.E1 = new DESTransform(des, true, array, iv);
				this.D2 = new DESTransform(des, false, array2, iv);
				this.E3 = new DESTransform(des, true, array3, iv);
				return;
			}
			this.D1 = new DESTransform(des, false, array3, iv);
			this.E2 = new DESTransform(des, true, array2, iv);
			this.D3 = new DESTransform(des, false, array, iv);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00089B10 File Offset: 0x00087D10
		protected override void ECB(byte[] input, byte[] output)
		{
			DESTransform.Permutation(input, output, DESTransform.ipTab, false);
			if (this.encrypt)
			{
				this.E1.ProcessBlock(output, output);
				this.D2.ProcessBlock(output, output);
				this.E3.ProcessBlock(output, output);
			}
			else
			{
				this.D1.ProcessBlock(output, output);
				this.E2.ProcessBlock(output, output);
				this.D3.ProcessBlock(output, output);
			}
			DESTransform.Permutation(output, output, DESTransform.fpTab, true);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x00089B90 File Offset: 0x00087D90
		internal static byte[] GetStrongKey()
		{
			int num = DESTransform.BLOCK_BYTE_SIZE * 3;
			byte[] array = KeyBuilder.Key(num);
			while (TripleDES.IsWeakKey(array))
			{
				array = KeyBuilder.Key(num);
			}
			return array;
		}

		// Token: 0x04000F72 RID: 3954
		private DESTransform E1;

		// Token: 0x04000F73 RID: 3955
		private DESTransform D2;

		// Token: 0x04000F74 RID: 3956
		private DESTransform E3;

		// Token: 0x04000F75 RID: 3957
		private DESTransform D1;

		// Token: 0x04000F76 RID: 3958
		private DESTransform E2;

		// Token: 0x04000F77 RID: 3959
		private DESTransform D3;
	}
}
