using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Events.Edir.EventData;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000D1 RID: 209
	public class EdirEventIntermediateResponse : LdapIntermediateResponse
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x000161E6 File Offset: 0x000143E6
		public EdirEventType EventType
		{
			get
			{
				return this.event_type;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x000161EE File Offset: 0x000143EE
		public EdirEventResultType EventResultType
		{
			get
			{
				return this.event_result_type;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000161F6 File Offset: 0x000143F6
		public BaseEdirEventData EventResponseDataObject
		{
			get
			{
				return this.event_response_data;
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000161FE File Offset: 0x000143FE
		public EdirEventIntermediateResponse(RfcLdapMessage message)
			: base(message)
		{
			this.ProcessMessage(base.getValue());
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00016213 File Offset: 0x00014413
		public EdirEventIntermediateResponse(byte[] message)
			: base(new RfcLdapMessage(new Asn1Sequence()))
		{
			this.ProcessMessage(SupportClass.ToSByteArray(message));
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00016234 File Offset: 0x00014434
		[CLSCompliant(false)]
		protected void ProcessMessage(sbyte[] returnedValue)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)new LBERDecoder().decode(returnedValue);
			this.event_type = (EdirEventType)((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			this.event_result_type = (EdirEventResultType)((Asn1Integer)asn1Sequence.get_Renamed(1)).intValue();
			if (asn1Sequence.size() > 2)
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)asn1Sequence.get_Renamed(2);
				switch (asn1Tagged.getIdentifier().Tag)
				{
				case 1:
					this.event_response_data = new EntryEventData(EdirEventDataType.EDIR_TAG_ENTRY_EVENT_DATA, asn1Tagged.taggedValue());
					return;
				case 2:
					this.event_response_data = new ValueEventData(EdirEventDataType.EDIR_TAG_VALUE_EVENT_DATA, asn1Tagged.taggedValue());
					return;
				case 3:
					this.event_response_data = new GeneralDSEventData(EdirEventDataType.EDIR_TAG_GENERAL_EVENT_DATA, asn1Tagged.taggedValue());
					return;
				case 4:
					this.event_response_data = null;
					return;
				case 5:
					this.event_response_data = new BinderyObjectEventData(EdirEventDataType.EDIR_TAG_BINDERY_EVENT_DATA, asn1Tagged.taggedValue());
					return;
				case 6:
					this.event_response_data = new SecurityEquivalenceEventData(EdirEventDataType.EDIR_TAG_DSESEV_INFO, asn1Tagged.taggedValue());
					return;
				case 7:
					this.event_response_data = new ModuleStateEventData(EdirEventDataType.EDIR_TAG_MODULE_STATE_DATA, asn1Tagged.taggedValue());
					return;
				case 8:
					this.event_response_data = new NetworkAddressEventData(EdirEventDataType.EDIR_TAG_NETWORK_ADDRESS, asn1Tagged.taggedValue());
					return;
				case 9:
					this.event_response_data = new ConnectionStateEventData(EdirEventDataType.EDIR_TAG_CONNECTION_STATE, asn1Tagged.taggedValue());
					return;
				case 10:
					this.event_response_data = new ChangeAddressEventData(EdirEventDataType.EDIR_TAG_CHANGE_SERVER_ADDRESS, asn1Tagged.taggedValue());
					return;
				case 12:
					this.event_response_data = null;
					return;
				case 14:
					this.event_response_data = new DebugEventData(EdirEventDataType.EDIR_TAG_DEBUG_EVENT_DATA, asn1Tagged.taggedValue());
					return;
				}
				throw new IOException();
			}
			this.event_response_data = null;
		}

		// Token: 0x04000461 RID: 1121
		protected EdirEventType event_type;

		// Token: 0x04000462 RID: 1122
		protected EdirEventResultType event_result_type;

		// Token: 0x04000463 RID: 1123
		protected BaseEdirEventData event_response_data;
	}
}
