using System;
using System.Runtime.InteropServices;
using System.Threading;
using Mono.Util;

namespace System.IO.Compression
{
	// Token: 0x02000376 RID: 886
	internal class DeflateStreamNative
	{
		// Token: 0x0600160B RID: 5643 RVA: 0x000026E5 File Offset: 0x000008E5
		private DeflateStreamNative()
		{
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0005DB70 File Offset: 0x0005BD70
		public static DeflateStreamNative Create(Stream compressedStream, CompressionMode mode, bool gzip)
		{
			DeflateStreamNative deflateStreamNative = new DeflateStreamNative();
			deflateStreamNative.data = GCHandle.Alloc(deflateStreamNative);
			deflateStreamNative.feeder = ((mode == CompressionMode.Compress) ? new DeflateStreamNative.UnmanagedReadOrWrite(DeflateStreamNative.UnmanagedWrite) : new DeflateStreamNative.UnmanagedReadOrWrite(DeflateStreamNative.UnmanagedRead));
			deflateStreamNative.z_stream = DeflateStreamNative.CreateZStream(mode, gzip, deflateStreamNative.feeder, GCHandle.ToIntPtr(deflateStreamNative.data));
			if (deflateStreamNative.z_stream.IsInvalid)
			{
				deflateStreamNative.Dispose(true);
				return null;
			}
			deflateStreamNative.base_stream = compressedStream;
			return deflateStreamNative;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x0005DBF0 File Offset: 0x0005BDF0
		~DeflateStreamNative()
		{
			this.Dispose(false);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0005DC20 File Offset: 0x0005BE20
		public void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				GC.SuppressFinalize(this);
			}
			else
			{
				this.base_stream = Stream.Null;
			}
			this.io_buffer = null;
			if (this.z_stream != null && !this.z_stream.IsInvalid)
			{
				this.z_stream.Dispose();
			}
			GCHandle gchandle = this.data;
			if (this.data.IsAllocated)
			{
				this.data.Free();
			}
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0005DC98 File Offset: 0x0005BE98
		public void Flush()
		{
			int num = DeflateStreamNative.Flush(this.z_stream);
			this.CheckResult(num, "Flush");
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0005DCC0 File Offset: 0x0005BEC0
		public int ReadZStream(IntPtr buffer, int length)
		{
			int num = DeflateStreamNative.ReadZStream(this.z_stream, buffer, length);
			this.CheckResult(num, "ReadInternal");
			return num;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0005DCE8 File Offset: 0x0005BEE8
		public void WriteZStream(IntPtr buffer, int length)
		{
			int num = DeflateStreamNative.WriteZStream(this.z_stream, buffer, length);
			this.CheckResult(num, "WriteInternal");
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0005DD10 File Offset: 0x0005BF10
		[MonoPInvokeCallback(typeof(DeflateStreamNative.UnmanagedReadOrWrite))]
		private static int UnmanagedRead(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStreamNative deflateStreamNative = GCHandle.FromIntPtr(data).Target as DeflateStreamNative;
			if (deflateStreamNative == null)
			{
				return -1;
			}
			return deflateStreamNative.UnmanagedRead(buffer, length);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0005DD40 File Offset: 0x0005BF40
		private int UnmanagedRead(IntPtr buffer, int length)
		{
			if (this.io_buffer == null)
			{
				this.io_buffer = new byte[4096];
			}
			int num = Math.Min(length, this.io_buffer.Length);
			int num2;
			try
			{
				num2 = this.base_stream.Read(this.io_buffer, 0, num);
			}
			catch (Exception ex)
			{
				this.last_error = ex;
				return -12;
			}
			if (num2 > 0)
			{
				Marshal.Copy(this.io_buffer, 0, buffer, num2);
			}
			return num2;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0005DDBC File Offset: 0x0005BFBC
		[MonoPInvokeCallback(typeof(DeflateStreamNative.UnmanagedReadOrWrite))]
		private static int UnmanagedWrite(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStreamNative deflateStreamNative = GCHandle.FromIntPtr(data).Target as DeflateStreamNative;
			if (deflateStreamNative == null)
			{
				return -1;
			}
			return deflateStreamNative.UnmanagedWrite(buffer, length);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0005DDEC File Offset: 0x0005BFEC
		private unsafe int UnmanagedWrite(IntPtr buffer, int length)
		{
			int num = 0;
			while (length > 0)
			{
				if (this.io_buffer == null)
				{
					this.io_buffer = new byte[4096];
				}
				int num2 = Math.Min(length, this.io_buffer.Length);
				Marshal.Copy(buffer, this.io_buffer, 0, num2);
				try
				{
					this.base_stream.Write(this.io_buffer, 0, num2);
				}
				catch (Exception ex)
				{
					this.last_error = ex;
					return -12;
				}
				buffer = new IntPtr((void*)((byte*)buffer.ToPointer() + num2));
				length -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0005DE84 File Offset: 0x0005C084
		private void CheckResult(int result, string where)
		{
			if (result >= 0)
			{
				return;
			}
			Exception ex = Interlocked.Exchange<Exception>(ref this.last_error, null);
			if (ex != null)
			{
				throw ex;
			}
			string text;
			switch (result)
			{
			case -11:
				text = "IO error";
				goto IL_0094;
			case -10:
				text = "Invalid argument(s)";
				goto IL_0094;
			case -6:
				text = "Invalid version";
				goto IL_0094;
			case -5:
				text = "Internal error (no progress possible)";
				goto IL_0094;
			case -4:
				text = "Not enough memory";
				goto IL_0094;
			case -3:
				text = "Corrupted data";
				goto IL_0094;
			case -2:
				text = "Internal error";
				goto IL_0094;
			case -1:
				text = "Unknown error";
				goto IL_0094;
			}
			text = "Unknown error";
			IL_0094:
			throw new IOException(text + " " + where);
		}

		// Token: 0x06001617 RID: 5655
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern DeflateStreamNative.SafeDeflateStreamHandle CreateZStream(CompressionMode compress, bool gzip, DeflateStreamNative.UnmanagedReadOrWrite feeder, IntPtr data);

		// Token: 0x06001618 RID: 5656
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int CloseZStream(IntPtr stream);

		// Token: 0x06001619 RID: 5657
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int Flush(DeflateStreamNative.SafeDeflateStreamHandle stream);

		// Token: 0x0600161A RID: 5658
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int ReadZStream(DeflateStreamNative.SafeDeflateStreamHandle stream, IntPtr buffer, int length);

		// Token: 0x0600161B RID: 5659
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int WriteZStream(DeflateStreamNative.SafeDeflateStreamHandle stream, IntPtr buffer, int length);

		// Token: 0x04000D39 RID: 3385
		private DeflateStreamNative.UnmanagedReadOrWrite feeder;

		// Token: 0x04000D3A RID: 3386
		private Stream base_stream;

		// Token: 0x04000D3B RID: 3387
		private DeflateStreamNative.SafeDeflateStreamHandle z_stream;

		// Token: 0x04000D3C RID: 3388
		private GCHandle data;

		// Token: 0x04000D3D RID: 3389
		private bool disposed;

		// Token: 0x04000D3E RID: 3390
		private byte[] io_buffer;

		// Token: 0x04000D3F RID: 3391
		private Exception last_error;

		// Token: 0x02000377 RID: 887
		// (Invoke) Token: 0x0600161D RID: 5661
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int UnmanagedReadOrWrite(IntPtr buffer, int length, IntPtr data);

		// Token: 0x02000378 RID: 888
		private sealed class SafeDeflateStreamHandle : SafeHandle
		{
			// Token: 0x170004AF RID: 1199
			// (get) Token: 0x0600161E RID: 5662 RVA: 0x0000AAEA File Offset: 0x00008CEA
			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}

			// Token: 0x0600161F RID: 5663 RVA: 0x0005DF36 File Offset: 0x0005C136
			private SafeDeflateStreamHandle()
				: base(IntPtr.Zero, true)
			{
			}

			// Token: 0x06001620 RID: 5664 RVA: 0x0005DF44 File Offset: 0x0005C144
			protected override bool ReleaseHandle()
			{
				try
				{
					DeflateStreamNative.CloseZStream(this.handle);
				}
				catch
				{
				}
				return true;
			}
		}
	}
}
