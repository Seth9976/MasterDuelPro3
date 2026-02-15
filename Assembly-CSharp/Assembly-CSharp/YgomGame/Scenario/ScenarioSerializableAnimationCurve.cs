using System;
using UnityEngine;

namespace YgomGame.Scenario
{
	// Token: 0x020009E3 RID: 2531
	[Serializable]
	public class ScenarioSerializableAnimationCurve : IScenarioSerializableValue, ISerializationCallbackReceiver
	{
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x0000216A File Offset: 0x0000036A
		public AnimationCurve animationCurve
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06004991 RID: 18833 RVA: 0x0000216A File Offset: 0x0000036A
		public static AnimationCurve defaultCurve
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x0000216A File Offset: 0x0000036A
		public static AnimationCurve Deserialize(object jsonVal)
		{
			return null;
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x00002739 File Offset: 0x00000939
		public ScenarioSerializableAnimationCurve(AnimationCurve animationCurve)
		{
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnAfterDeserialize()
		{
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x04008772 RID: 34674
		private AnimationCurve m_AnimationCurve;

		// Token: 0x04008773 RID: 34675
		[SerializeField]
		private string[] k;
	}
}
