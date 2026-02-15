using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Willow.InGameField
{
	// Token: 0x0200156C RID: 5484
	[CreateAssetMenu]
	[Serializable]
	public class IntParameter : ParameterAsset
	{
		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x06009F0E RID: 40718 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009F0F RID: 40719 RVA: 0x0000216D File Offset: 0x0000036D
		public int value
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x06009F10 RID: 40720 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009F11 RID: 40721 RVA: 0x0000216D File Offset: 0x0000036D
		public int runtimeValue
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x06009F12 RID: 40722 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06009F13 RID: 40723 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<int> onValueChanged
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06009F14 RID: 40724 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06009F15 RID: 40725 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnityEngine_002EISerializationCallbackReceiver_002EOnBeforeSerialize()
		{
		}

		// Token: 0x06009F16 RID: 40726 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnityEngine_002EISerializationCallbackReceiver_002EOnAfterDeserialize()
		{
		}

		// Token: 0x0400DE46 RID: 56902
		[SerializeField]
		private int m_value;

		// Token: 0x0400DE47 RID: 56903
		[NonSerialized]
		private int m_runtimeValue;
	}
}
