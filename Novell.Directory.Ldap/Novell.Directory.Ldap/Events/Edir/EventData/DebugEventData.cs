using System;
using System.Collections;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000DB RID: 219
	public class DebugEventData : BaseEdirEventData
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00016CE5 File Offset: 0x00014EE5
		public int DSTime
		{
			get
			{
				return this.ds_time;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00016CED File Offset: 0x00014EED
		public int MilliSeconds
		{
			get
			{
				return this.milli_seconds;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00016CF5 File Offset: 0x00014EF5
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00016CFD File Offset: 0x00014EFD
		public string FormatString
		{
			get
			{
				return this.strFormatString;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00016D05 File Offset: 0x00014F05
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00016D0D File Offset: 0x00014F0D
		public int ParameterCount
		{
			get
			{
				return this.parameter_count;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00016D15 File Offset: 0x00014F15
		public ArrayList Parameters
		{
			get
			{
				return this.parameter_collection;
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00016D20 File Offset: 0x00014F20
		public DebugEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.ds_time = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.milli_seconds = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strPerpetratorDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strFormatString = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.nVerb = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.parameter_count = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.parameter_collection = new ArrayList();
			if (this.parameter_count > 0)
			{
				Asn1Sequence asn1Sequence = (Asn1Sequence)this.decoder.decode(this.decodedData, array);
				for (int i = 0; i < this.parameter_count; i++)
				{
					this.parameter_collection.Add(new DebugParameter((Asn1Tagged)asn1Sequence.get_Renamed(i)));
				}
			}
			base.DataInitDone();
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00016E68 File Offset: 0x00015068
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DebugEventData");
			stringBuilder.AppendFormat("(Millseconds={0})", this.milli_seconds);
			stringBuilder.AppendFormat("(DSTime={0})", this.ds_time);
			stringBuilder.AppendFormat("(PerpetratorDN={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(Verb={0})", this.nVerb);
			stringBuilder.AppendFormat("(ParameterCount={0})", this.parameter_count);
			for (int i = 0; i < this.parameter_count; i++)
			{
				stringBuilder.AppendFormat("(Parameter[{0}]={1})", i, this.parameter_collection[i]);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400047E RID: 1150
		protected int ds_time;

		// Token: 0x0400047F RID: 1151
		protected int milli_seconds;

		// Token: 0x04000480 RID: 1152
		protected string strPerpetratorDN;

		// Token: 0x04000481 RID: 1153
		protected string strFormatString;

		// Token: 0x04000482 RID: 1154
		protected int nVerb;

		// Token: 0x04000483 RID: 1155
		protected int parameter_count;

		// Token: 0x04000484 RID: 1156
		protected ArrayList parameter_collection;
	}
}
