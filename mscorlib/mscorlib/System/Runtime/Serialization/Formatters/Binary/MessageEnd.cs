using System;
using System.IO;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004F2 RID: 1266
	internal sealed class MessageEnd
	{
		// Token: 0x06002794 RID: 10132 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal MessageEnd()
		{
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0009F884 File Offset: 0x0009DA84
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(11);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Read(__BinaryParser input)
		{
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump(Stream sout)
		{
		}
	}
}
