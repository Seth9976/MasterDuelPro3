using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000428 RID: 1064
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class StyleComplexSelector : ISerializationCallbackReceiver
	{
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x000707A0 File Offset: 0x0006E9A0
		public int specificity
		{
			get
			{
				return this.m_Specificity;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x000707B8 File Offset: 0x0006E9B8
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x000707C0 File Offset: 0x0006E9C0
		public StyleRule rule
		{
			get; [VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set;
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x000707CC File Offset: 0x0006E9CC
		public bool isSimple
		{
			get
			{
				return this.m_isSimple;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x000707E4 File Offset: 0x0006E9E4
		// (set) Token: 0x06001EEA RID: 7914 RVA: 0x000707FC File Offset: 0x0006E9FC
		public StyleSelector[] selectors
		{
			get
			{
				return this.m_Selectors;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Selectors = value;
				this.m_isSimple = this.m_Selectors.Length == 1;
			}
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x000020EA File Offset: 0x000002EA
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00070817 File Offset: 0x0006EA17
		public virtual void OnAfterDeserialize()
		{
			this.m_isSimple = this.m_Selectors.Length == 1;
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0007082C File Offset: 0x0006EA2C
		internal void CachePseudoStateMasks()
		{
			bool flag = StyleComplexSelector.s_PseudoStates == null;
			if (flag)
			{
				StyleComplexSelector.s_PseudoStates = new Dictionary<string, StyleComplexSelector.PseudoStateData>();
				StyleComplexSelector.s_PseudoStates["active"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, false);
				StyleComplexSelector.s_PseudoStates["hover"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Hover, false);
				StyleComplexSelector.s_PseudoStates["checked"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
				StyleComplexSelector.s_PseudoStates["selected"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
				StyleComplexSelector.s_PseudoStates["disabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, false);
				StyleComplexSelector.s_PseudoStates["focus"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Focus, false);
				StyleComplexSelector.s_PseudoStates["root"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Root, false);
				StyleComplexSelector.s_PseudoStates["inactive"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, true);
				StyleComplexSelector.s_PseudoStates["enabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, true);
			}
			int i = 0;
			int subCount = this.selectors.Length;
			while (i < subCount)
			{
				StyleSelector selector = this.selectors[i];
				StyleSelectorPart[] parts = selector.parts;
				PseudoStates pseudoClassMask = (PseudoStates)0;
				PseudoStates negatedPseudoClassMask = (PseudoStates)0;
				for (int j = 0; j < selector.parts.Length; j++)
				{
					bool flag2 = selector.parts[j].type == StyleSelectorType.PseudoClass;
					if (flag2)
					{
						StyleComplexSelector.PseudoStateData data;
						bool flag3 = StyleComplexSelector.s_PseudoStates.TryGetValue(parts[j].value, out data);
						if (flag3)
						{
							bool flag4 = !data.negate;
							if (flag4)
							{
								pseudoClassMask |= data.state;
							}
							else
							{
								negatedPseudoClassMask |= data.state;
							}
						}
						else
						{
							Debug.LogWarningFormat("Unknown pseudo class \"{0}\"", new object[] { parts[j].value });
						}
					}
				}
				selector.pseudoStateMask = (int)pseudoClassMask;
				selector.negatedPseudoStateMask = (int)negatedPseudoClassMask;
				i++;
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00070A2C File Offset: 0x0006EC2C
		public override string ToString()
		{
			return string.Format("[{0}]", string.Join(", ", this.m_Selectors.Select((StyleSelector x) => x.ToString()).ToArray<string>()));
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00070A84 File Offset: 0x0006EC84
		private static int StyleSelectorPartCompare(StyleSelectorPart x, StyleSelectorPart y)
		{
			bool flag = y.type < x.type;
			int num;
			if (flag)
			{
				num = -1;
			}
			else
			{
				bool flag2 = y.type > x.type;
				if (flag2)
				{
					num = 1;
				}
				else
				{
					num = y.value.CompareTo(x.value);
				}
			}
			return num;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00070ADC File Offset: 0x0006ECDC
		internal unsafe void CalculateHashes()
		{
			bool isSimple = this.isSimple;
			if (!isSimple)
			{
				for (int i = this.selectors.Length - 2; i > -1; i--)
				{
					StyleComplexSelector.m_HashList.AddRange(this.selectors[i].parts);
				}
				StyleComplexSelector.m_HashList.RemoveAll((StyleSelectorPart p) => p.type != StyleSelectorType.Class && p.type != StyleSelectorType.ID && p.type != StyleSelectorType.Type);
				StyleComplexSelector.m_HashList.Sort(new Comparison<StyleSelectorPart>(StyleComplexSelector.StyleSelectorPartCompare));
				bool isFirstEntry = true;
				StyleSelectorType lastType = StyleSelectorType.Unknown;
				string lastValue = "";
				int partIndex = 0;
				int max = Math.Min(4, StyleComplexSelector.m_HashList.Count);
				for (int j = 0; j < max; j++)
				{
					bool flag = isFirstEntry;
					if (flag)
					{
						isFirstEntry = false;
					}
					else
					{
						while (partIndex < StyleComplexSelector.m_HashList.Count && StyleComplexSelector.m_HashList[partIndex].type == lastType && StyleComplexSelector.m_HashList[partIndex].value == lastValue)
						{
							partIndex++;
						}
						bool flag2 = partIndex == StyleComplexSelector.m_HashList.Count;
						if (flag2)
						{
							break;
						}
					}
					lastType = StyleComplexSelector.m_HashList[partIndex].type;
					lastValue = StyleComplexSelector.m_HashList[partIndex].value;
					bool flag3 = lastType == StyleSelectorType.ID;
					Salt salt;
					if (flag3)
					{
						salt = Salt.IdSalt;
					}
					else
					{
						bool flag4 = lastType == StyleSelectorType.Class;
						if (flag4)
						{
							salt = Salt.ClassSalt;
						}
						else
						{
							salt = Salt.TagNameSalt;
						}
					}
					*((ref this.ancestorHashes.hashes.FixedElementField) + (IntPtr)j * 4) = lastValue.GetHashCode() * (int)salt;
				}
				StyleComplexSelector.m_HashList.Clear();
			}
		}

		// Token: 0x04000D70 RID: 3440
		[NonSerialized]
		public Hashes ancestorHashes;

		// Token: 0x04000D71 RID: 3441
		[SerializeField]
		private int m_Specificity;

		// Token: 0x04000D73 RID: 3443
		[NonSerialized]
		private bool m_isSimple;

		// Token: 0x04000D74 RID: 3444
		[SerializeField]
		private StyleSelector[] m_Selectors;

		// Token: 0x04000D75 RID: 3445
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int ruleIndex;

		// Token: 0x04000D76 RID: 3446
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[NonSerialized]
		internal StyleComplexSelector nextInTable;

		// Token: 0x04000D77 RID: 3447
		[NonSerialized]
		internal int orderInStyleSheet;

		// Token: 0x04000D78 RID: 3448
		private static Dictionary<string, StyleComplexSelector.PseudoStateData> s_PseudoStates;

		// Token: 0x04000D79 RID: 3449
		private static List<StyleSelectorPart> m_HashList = new List<StyleSelectorPart>();

		// Token: 0x02000429 RID: 1065
		private struct PseudoStateData
		{
			// Token: 0x06001EF3 RID: 7923 RVA: 0x00070CB4 File Offset: 0x0006EEB4
			public PseudoStateData(PseudoStates state, bool negate)
			{
				this.state = state;
				this.negate = negate;
			}

			// Token: 0x04000D7A RID: 3450
			public readonly PseudoStates state;

			// Token: 0x04000D7B RID: 3451
			public readonly bool negate;
		}
	}
}
