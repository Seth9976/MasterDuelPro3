using System;
using UnityEngine.Scripting;

namespace Willow.InGameField
{
	// Token: 0x0200156B RID: 5483
	[Preserve]
	public class IntFieldEventListener : BaseFieldEventListener
	{
		// Token: 0x06009F07 RID: 40711 RVA: 0x0000216D File Offset: 0x0000036D
		[Preserve]
		private void OnEnable()
		{
		}

		// Token: 0x06009F08 RID: 40712 RVA: 0x0000216D File Offset: 0x0000036D
		[Preserve]
		private void OnDisable()
		{
		}

		// Token: 0x06009F09 RID: 40713 RVA: 0x0000216D File Offset: 0x0000036D
		[Preserve]
		private void OnDestroy()
		{
		}

		// Token: 0x06009F0A RID: 40714 RVA: 0x0000216D File Offset: 0x0000036D
		[Preserve]
		public void OnEventRaised(int value)
		{
		}

		// Token: 0x06009F0B RID: 40715 RVA: 0x0000216A File Offset: 0x0000036A
		[Preserve]
		public override BaseFieldEvent GetTargetFieldEvent()
		{
			return null;
		}

		// Token: 0x06009F0C RID: 40716 RVA: 0x0000216D File Offset: 0x0000036D
		[Preserve]
		public override void SetOrOverwriteEvent(BaseFieldEvent fieldEvent)
		{
		}

		// Token: 0x0400DE42 RID: 56898
		public IntFieldEvent target;

		// Token: 0x0400DE43 RID: 56899
		public ResponseIntEvent responseInt;

		// Token: 0x0400DE44 RID: 56900
		public ResponseStringEvent responseString;

		// Token: 0x0400DE45 RID: 56901
		public bool dontUnregisterOnDisabled;
	}
}
