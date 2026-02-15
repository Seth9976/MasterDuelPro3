using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003AE RID: 942
	internal struct EventTypeSpec
	{
		// Token: 0x06001E2C RID: 7724 RVA: 0x00095B06 File Offset: 0x00093D06
		public EventTypeSpec(uint eventClass, uint eventKind)
		{
			this.eventClass = eventClass;
			this.eventKind = eventKind;
		}

		// Token: 0x04001D4D RID: 7501
		public uint eventClass;

		// Token: 0x04001D4E RID: 7502
		public uint eventKind;
	}
}
