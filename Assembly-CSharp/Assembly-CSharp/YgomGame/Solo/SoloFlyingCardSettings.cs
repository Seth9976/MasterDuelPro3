using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Solo
{
	// Token: 0x020008F8 RID: 2296
	public class SoloFlyingCardSettings : ScriptableObject
	{
		// Token: 0x06004315 RID: 17173 RVA: 0x0000216A File Offset: 0x0000036A
		public SoloFlyingCardSettings.MrkList GetMrkList(SoloFlyingCardSettings.Format format)
		{
			return null;
		}

		// Token: 0x04008193 RID: 33171
		[SerializeField]
		private SoloFlyingCardSettings.MrkList soloTutorialMrkList;

		// Token: 0x04008194 RID: 33172
		[SerializeField]
		private SoloFlyingCardSettings.MrkList soloNormalMrkList;

		// Token: 0x020008F9 RID: 2297
		[Serializable]
		public class MrkList
		{
			// Token: 0x06004317 RID: 17175 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool Contains(int mrk)
			{
				return false;
			}

			// Token: 0x06004318 RID: 17176 RVA: 0x0000216D File Offset: 0x0000036D
			public void Add(int mrk)
			{
			}

			// Token: 0x06004319 RID: 17177 RVA: 0x0000216A File Offset: 0x0000036A
			public List<int> GetListCopy()
			{
				return null;
			}

			// Token: 0x0600431A RID: 17178 RVA: 0x0000216A File Offset: 0x0000036A
			public IEnumerable<int> GetRandomMrkFromList(int quantity = 10)
			{
				return null;
			}

			// Token: 0x0600431B RID: 17179 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearAll()
			{
			}

			// Token: 0x04008195 RID: 33173
			[SerializeField]
			private List<int> mrkList;
		}

		// Token: 0x020008FA RID: 2298
		public enum Format
		{
			// Token: 0x04008197 RID: 33175
			SOLO_TUTORIAL,
			// Token: 0x04008198 RID: 33176
			SOLO_NORMAL
		}
	}
}
