using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Drawing
{
	// Token: 0x0200003C RID: 60
	internal sealed class ComIStreamWrapper : IStream
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00006F5B File Offset: 0x0000515B
		internal ComIStreamWrapper(Stream stream)
		{
			this.baseStream = stream;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006F74 File Offset: 0x00005174
		private void SetSizeToPosition()
		{
			if (this.position != -1L)
			{
				if (this.position > this.baseStream.Length)
				{
					this.baseStream.SetLength(this.position);
				}
				this.baseStream.Position = this.position;
				this.position = -1L;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006FC8 File Offset: 0x000051C8
		public void Read(byte[] pv, int cb, IntPtr pcbRead)
		{
			int num = 0;
			if (cb != 0)
			{
				this.SetSizeToPosition();
				num = this.baseStream.Read(pv, 0, cb);
			}
			if (pcbRead != IntPtr.Zero)
			{
				Marshal.WriteInt32(pcbRead, num);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007003 File Offset: 0x00005203
		public void Write(byte[] pv, int cb, IntPtr pcbWritten)
		{
			if (cb != 0)
			{
				this.SetSizeToPosition();
				this.baseStream.Write(pv, 0, cb);
			}
			if (pcbWritten != IntPtr.Zero)
			{
				Marshal.WriteInt32(pcbWritten, cb);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007030 File Offset: 0x00005230
		public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
		{
			long length = this.baseStream.Length;
			long num;
			switch (dwOrigin)
			{
			case 0:
				num = dlibMove;
				break;
			case 1:
				if (this.position == -1L)
				{
					num = this.baseStream.Position + dlibMove;
				}
				else
				{
					num = this.position + dlibMove;
				}
				break;
			case 2:
				num = length + dlibMove;
				break;
			default:
				throw new ExternalException(null, -2147287039);
			}
			if (num > length)
			{
				this.position = num;
			}
			else
			{
				this.baseStream.Position = num;
				this.position = -1L;
			}
			if (plibNewPosition != IntPtr.Zero)
			{
				Marshal.WriteInt64(plibNewPosition, num);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000070CF File Offset: 0x000052CF
		public void SetSize(long libNewSize)
		{
			this.baseStream.SetLength(libNewSize);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000070E0 File Offset: 0x000052E0
		public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
		{
			long num = 0L;
			if (cb != 0L)
			{
				int num2;
				if (cb < 4096L)
				{
					num2 = (int)cb;
				}
				else
				{
					num2 = 4096;
				}
				byte[] array = new byte[num2];
				this.SetSizeToPosition();
				int num3;
				while ((num3 = this.baseStream.Read(array, 0, num2)) != 0)
				{
					pstm.Write(array, num3, IntPtr.Zero);
					num += (long)num3;
					if (num >= cb)
					{
						break;
					}
					if (cb - num < 4096L)
					{
						num2 = (int)(cb - num);
					}
				}
			}
			if (pcbRead != IntPtr.Zero)
			{
				Marshal.WriteInt64(pcbRead, num);
			}
			if (pcbWritten != IntPtr.Zero)
			{
				Marshal.WriteInt64(pcbWritten, num);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007178 File Offset: 0x00005378
		public void Commit(int grfCommitFlags)
		{
			this.baseStream.Flush();
			this.SetSizeToPosition();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000718B File Offset: 0x0000538B
		public void Revert()
		{
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000718B File Offset: 0x0000538B
		public void LockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000718B File Offset: 0x0000538B
		public void UnlockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007198 File Offset: 0x00005398
		public void Stat(out STATSTG pstatstg, int grfStatFlag)
		{
			pstatstg = default(STATSTG);
			pstatstg.cbSize = this.baseStream.Length;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000071B2 File Offset: 0x000053B2
		public void Clone(out IStream ppstm)
		{
			ppstm = null;
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x04000128 RID: 296
		private readonly Stream baseStream;

		// Token: 0x04000129 RID: 297
		private long position = -1L;
	}
}
