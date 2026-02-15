using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	public abstract class Asn1Object : ISerializable
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00018E21 File Offset: 0x00017021
		public Asn1Object(Asn1Identifier id)
		{
			this.id = id;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00002A00 File Offset: 0x00000C00
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x06000610 RID: 1552
		public abstract void encode(Asn1Encoder enc, Stream out_Renamed);

		// Token: 0x06000611 RID: 1553 RVA: 0x00018E30 File Offset: 0x00017030
		public virtual Asn1Identifier getIdentifier()
		{
			return this.id;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00018E38 File Offset: 0x00017038
		public virtual void setIdentifier(Asn1Identifier id)
		{
			this.id = id;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00018E44 File Offset: 0x00017044
		[CLSCompliant(false)]
		public sbyte[] getEncoding(Asn1Encoder enc)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				this.encode(enc, memoryStream);
			}
			catch (IOException ex)
			{
				throw new SystemException("IOException while encoding to byte array: " + ex.ToString());
			}
			return SupportClass.ToSByteArray(memoryStream.ToArray());
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00018E94 File Offset: 0x00017094
		[CLSCompliant(false)]
		public override string ToString()
		{
			string[] array = new string[] { "[UNIVERSAL ", "[APPLICATION ", "[CONTEXT ", "[PRIVATE " };
			StringBuilder stringBuilder = new StringBuilder();
			Asn1Identifier identifier = this.getIdentifier();
			stringBuilder.Append(array[identifier.Asn1Class]).Append(identifier.Tag).Append("] ");
			return stringBuilder.ToString();
		}

		// Token: 0x040004ED RID: 1261
		private Asn1Identifier id;
	}
}
