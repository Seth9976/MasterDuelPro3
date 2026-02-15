using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A1 RID: 161
	public class GetReplicaInfoResponse : LdapExtendedResponse
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x000143F8 File Offset: 0x000125F8
		public GetReplicaInfoResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode != 0)
			{
				this.partitionID = 0;
				this.replicaState = 0;
				this.modificationTime = 0;
				this.purgeTime = 0;
				this.localPartitionID = 0;
				this.partitionDN = "";
				this.replicaType = 0;
				this.flags = 0;
				return;
			}
			sbyte[] value = this.Value;
			if (value == null)
			{
				throw new IOException("No returned value");
			}
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error");
			}
			MemoryStream memoryStream = new MemoryStream(SupportClass.ToByteArray(value));
			Asn1Integer asn1Integer = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer == null)
			{
				throw new IOException("Decoding error");
			}
			this.partitionID = asn1Integer.intValue();
			Asn1Integer asn1Integer2 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer2 == null)
			{
				throw new IOException("Decoding error");
			}
			this.replicaState = asn1Integer2.intValue();
			Asn1Integer asn1Integer3 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer3 == null)
			{
				throw new IOException("Decoding error");
			}
			this.modificationTime = asn1Integer3.intValue();
			Asn1Integer asn1Integer4 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer4 == null)
			{
				throw new IOException("Decoding error");
			}
			this.purgeTime = asn1Integer4.intValue();
			Asn1Integer asn1Integer5 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer5 == null)
			{
				throw new IOException("Decoding error");
			}
			this.localPartitionID = asn1Integer5.intValue();
			Asn1OctetString asn1OctetString = (Asn1OctetString)lberdecoder.decode(memoryStream);
			if (asn1OctetString == null)
			{
				throw new IOException("Decoding error");
			}
			this.partitionDN = asn1OctetString.stringValue();
			if (this.partitionDN == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Integer asn1Integer6 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer6 == null)
			{
				throw new IOException("Decoding error");
			}
			this.replicaType = asn1Integer6.intValue();
			Asn1Integer asn1Integer7 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer7 == null)
			{
				throw new IOException("Decoding error");
			}
			this.flags = asn1Integer7.intValue();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000145E4 File Offset: 0x000127E4
		public virtual int getpartitionID()
		{
			return this.partitionID;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000145EC File Offset: 0x000127EC
		public virtual int getreplicaState()
		{
			return this.replicaState;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000145F4 File Offset: 0x000127F4
		public virtual int getmodificationTime()
		{
			return this.modificationTime;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000145FC File Offset: 0x000127FC
		public virtual int getpurgeTime()
		{
			return this.purgeTime;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00014604 File Offset: 0x00012804
		public virtual int getlocalPartitionID()
		{
			return this.localPartitionID;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001460C File Offset: 0x0001280C
		public virtual string getpartitionDN()
		{
			return this.partitionDN;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00014614 File Offset: 0x00012814
		public virtual int getreplicaType()
		{
			return this.replicaType;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001461C File Offset: 0x0001281C
		public virtual int getflags()
		{
			return this.flags;
		}

		// Token: 0x0400028C RID: 652
		private int partitionID;

		// Token: 0x0400028D RID: 653
		private int replicaState;

		// Token: 0x0400028E RID: 654
		private int modificationTime;

		// Token: 0x0400028F RID: 655
		private int purgeTime;

		// Token: 0x04000290 RID: 656
		private int localPartitionID;

		// Token: 0x04000291 RID: 657
		private string partitionDN;

		// Token: 0x04000292 RID: 658
		private int replicaType;

		// Token: 0x04000293 RID: 659
		private int flags;
	}
}
