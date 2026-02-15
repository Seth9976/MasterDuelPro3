using System;

namespace Willow.InGameField
{
	// Token: 0x0200155F RID: 5471
	public class BoolFieldEventListener : BaseFieldEventListener
	{
		// Token: 0x06009ED5 RID: 40661 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06009ED6 RID: 40662 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06009ED7 RID: 40663 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnEventRaised(bool value)
		{
		}

		// Token: 0x06009ED8 RID: 40664 RVA: 0x0000216A File Offset: 0x0000036A
		public override BaseFieldEvent GetTargetFieldEvent()
		{
			return null;
		}

		// Token: 0x06009ED9 RID: 40665 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetOrOverwriteEvent(BaseFieldEvent fieldEvent)
		{
		}

		// Token: 0x0400DE30 RID: 56880
		public BoolFieldEvent target;

		// Token: 0x0400DE31 RID: 56881
		public ResponseBoolEvent responseBool;

		// Token: 0x0400DE32 RID: 56882
		public ResponseStringEvent responseString;
	}
}
