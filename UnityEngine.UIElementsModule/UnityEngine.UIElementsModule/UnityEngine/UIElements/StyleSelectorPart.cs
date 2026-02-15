using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200042F RID: 1071
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal struct StyleSelectorPart
	{
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x00070E10 File Offset: 0x0006F010
		public string value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x00070E28 File Offset: 0x0006F028
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x00070E40 File Offset: 0x0006F040
		public StyleSelectorType type
		{
			get
			{
				return this.m_Type;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x00070E4C File Offset: 0x0006F04C
		public override string ToString()
		{
			return UnityString.Format("[StyleSelectorPart: value={0}, type={1}]", new object[] { this.value, this.type });
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x00070E88 File Offset: 0x0006F088
		public static StyleSelectorPart CreateClass(string className)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Class,
				m_Value = className
			};
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00070EB4 File Offset: 0x0006F0B4
		public static StyleSelectorPart CreateId(string Id)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.ID,
				m_Value = Id
			};
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00070EE0 File Offset: 0x0006F0E0
		public static StyleSelectorPart CreatePredicate(object predicate)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Predicate,
				tempData = predicate
			};
		}

		// Token: 0x04000D8D RID: 3469
		[SerializeField]
		private string m_Value;

		// Token: 0x04000D8E RID: 3470
		[SerializeField]
		private StyleSelectorType m_Type;

		// Token: 0x04000D8F RID: 3471
		internal object tempData;
	}
}
