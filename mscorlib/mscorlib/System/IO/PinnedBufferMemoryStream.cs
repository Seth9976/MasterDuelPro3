using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020007A0 RID: 1952
	internal sealed class PinnedBufferMemoryStream : UnmanagedMemoryStream
	{
		// Token: 0x06003DB0 RID: 15792 RVA: 0x000EDB88 File Offset: 0x000EBD88
		internal unsafe PinnedBufferMemoryStream(byte[] array)
		{
			this._array = array;
			this._pinningHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			int num = array.Length;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(array))
			{
				byte* ptr = reference;
				base.Initialize(ptr, (long)num, (long)num, FileAccess.Read);
			}
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x000EDBD1 File Offset: 0x000EBDD1
		public override int Read(Span<byte> buffer)
		{
			return base.ReadCore(buffer);
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000EDBDA File Offset: 0x000EBDDA
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			base.WriteCore(buffer);
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000EDBE4 File Offset: 0x000EBDE4
		~PinnedBufferMemoryStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000EDC14 File Offset: 0x000EBE14
		protected override void Dispose(bool disposing)
		{
			if (this._pinningHandle.IsAllocated)
			{
				this._pinningHandle.Free();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001FA0 RID: 8096
		private byte[] _array;

		// Token: 0x04001FA1 RID: 8097
		private GCHandle _pinningHandle;
	}
}
