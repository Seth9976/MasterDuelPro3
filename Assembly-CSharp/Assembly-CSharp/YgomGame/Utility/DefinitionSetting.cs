using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Utility
{
	// Token: 0x02000821 RID: 2081
	public class DefinitionSetting : ScriptableObject
	{
		// Token: 0x0600402F RID: 16431 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerable<string> SelectLabels()
		{
			return null;
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x0000216A File Offset: 0x0000036A
		public DefinitionSetting.ValueContainer Get(string label, int index = 0)
		{
			return null;
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x000F38D3 File Offset: 0x000F1AD3
		public bool Get(string label, out int res)
		{
			res = 0;
			return false;
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x000F46DA File Offset: 0x000F28DA
		public bool Get(string label, out float res)
		{
			res = 0f;
			return false;
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x000F46E4 File Offset: 0x000F28E4
		public bool Get(string label, out Vector2 res)
		{
			res = default(Vector2);
			return false;
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x000F46EE File Offset: 0x000F28EE
		public bool Get(string label, out Vector3 res)
		{
			res = default(Vector3);
			return false;
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x000F46F8 File Offset: 0x000F28F8
		public bool Get(string label, out Quaternion res)
		{
			res = default(Quaternion);
			return false;
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x000F1C76 File Offset: 0x000EFE76
		public bool Get(string label, out string res)
		{
			res = null;
			return false;
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x000F1F3E File Offset: 0x000F013E
		public bool Get(string label, out Color res)
		{
			res = default(Color);
			return false;
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x000F38C6 File Offset: 0x000F1AC6
		public bool Get(string label, out bool res)
		{
			res = false;
			return false;
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x000F1C76 File Offset: 0x000EFE76
		public bool Get(string label, out List<int> res)
		{
			res = null;
			return false;
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000F1C76 File Offset: 0x000EFE76
		public bool Get(string label, out List<float> res)
		{
			res = null;
			return false;
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x000F1C76 File Offset: 0x000EFE76
		public bool GetAsValueType(string label, out object res)
		{
			res = null;
			return false;
		}

		// Token: 0x04003945 RID: 14661
		public List<DefinitionSetting.ValueContainer> list;

		// Token: 0x02000822 RID: 2082
		public enum ValueType
		{
			// Token: 0x04003947 RID: 14663
			Int,
			// Token: 0x04003948 RID: 14664
			Float,
			// Token: 0x04003949 RID: 14665
			Vector2,
			// Token: 0x0400394A RID: 14666
			Vector3,
			// Token: 0x0400394B RID: 14667
			Quaternion,
			// Token: 0x0400394C RID: 14668
			String,
			// Token: 0x0400394D RID: 14669
			Color,
			// Token: 0x0400394E RID: 14670
			Bool,
			// Token: 0x0400394F RID: 14671
			IntList,
			// Token: 0x04003950 RID: 14672
			FloatList
		}

		// Token: 0x02000823 RID: 2083
		[Serializable]
		public class ValueContainer
		{
			// Token: 0x0600403D RID: 16445 RVA: 0x0000216A File Offset: 0x0000036A
			public DefinitionSetting.ValueContainer Copy()
			{
				return null;
			}

			// Token: 0x04003951 RID: 14673
			public string label;

			// Token: 0x04003952 RID: 14674
			public List<float> floatValues;

			// Token: 0x04003953 RID: 14675
			public string stringValue;

			// Token: 0x04003954 RID: 14676
			public DefinitionSetting.ValueType valueType;
		}
	}
}
