using System;

namespace Willow.InGameField
{
	// Token: 0x02001579 RID: 5497
	public class TriggerFieldEventListener : BaseFieldEventListener
	{
		// Token: 0x06009F3F RID: 40767 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06009F40 RID: 40768 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06009F41 RID: 40769 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06009F42 RID: 40770 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnEventRaised()
		{
		}

		// Token: 0x06009F43 RID: 40771 RVA: 0x0000216A File Offset: 0x0000036A
		public override BaseFieldEvent GetTargetFieldEvent()
		{
			return null;
		}

		// Token: 0x06009F44 RID: 40772 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetOrOverwriteEvent(BaseFieldEvent fieldEvent)
		{
		}

		// Token: 0x0400DE60 RID: 56928
		public TriggerFieldEvent target;

		// Token: 0x0400DE61 RID: 56929
		public ResponseTriggerEvent responseTrigger;

		// Token: 0x0400DE62 RID: 56930
		public bool dontUnregisterOnDisabled;
	}
}
