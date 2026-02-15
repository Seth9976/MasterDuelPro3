using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200047F RID: 1151
	public abstract class UxmlAttributeDescription
	{
		// Token: 0x0600219C RID: 8604 RVA: 0x0007B720 File Offset: 0x00079920
		protected UxmlAttributeDescription()
		{
			this.use = UxmlAttributeDescription.Use.Optional;
			this.restriction = null;
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x0007B73A File Offset: 0x0007993A
		// (set) Token: 0x0600219E RID: 8606 RVA: 0x0007B742 File Offset: 0x00079942
		public string name { get; set; }

		// Token: 0x17000900 RID: 2304
		// (set) Token: 0x0600219F RID: 8607 RVA: 0x0007B74C File Offset: 0x0007994C
		public IEnumerable<string> obsoleteNames
		{
			set
			{
				string[] array = value as string[];
				bool flag = array != null;
				if (flag)
				{
					this.m_ObsoleteNames = array;
				}
				else
				{
					this.m_ObsoleteNames = value.ToArray<string>();
				}
			}
		}

		// Token: 0x17000901 RID: 2305
		// (set) Token: 0x060021A0 RID: 8608 RVA: 0x0007B77E File Offset: 0x0007997E
		protected internal string type
		{
			[CompilerGenerated]
			set
			{
				this.<type>k__BackingField = value;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (set) Token: 0x060021A1 RID: 8609 RVA: 0x0007B787 File Offset: 0x00079987
		protected string typeNamespace
		{
			[CompilerGenerated]
			set
			{
				this.<typeNamespace>k__BackingField = value;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (set) Token: 0x060021A2 RID: 8610 RVA: 0x0007B790 File Offset: 0x00079990
		public UxmlAttributeDescription.Use use
		{
			[CompilerGenerated]
			set
			{
				this.<use>k__BackingField = value;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (set) Token: 0x060021A3 RID: 8611 RVA: 0x0007B799 File Offset: 0x00079999
		public UxmlTypeRestriction restriction
		{
			[CompilerGenerated]
			set
			{
				this.<restriction>k__BackingField = value;
			}
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0007B7A4 File Offset: 0x000799A4
		internal bool TryFindValueInAttributeOverrides(string elementName, CreationContext cc, List<TemplateAsset.AttributeOverride> attributeOverrides, out string value)
		{
			value = null;
			TemplateAsset.AttributeOverride overrideToApply = default(TemplateAsset.AttributeOverride);
			foreach (TemplateAsset.AttributeOverride attributeOverride in attributeOverrides)
			{
				bool flag = cc.namesPath == null;
				if (flag)
				{
					bool flag2 = attributeOverride.m_ElementName != elementName;
					if (flag2)
					{
						continue;
					}
				}
				else
				{
					bool flag3 = !attributeOverride.NamesPathMatchesElementNamesPath(cc.namesPath);
					if (flag3)
					{
						continue;
					}
				}
				bool flag4 = attributeOverride.m_AttributeName != this.name;
				if (flag4)
				{
					bool flag5 = this.m_ObsoleteNames != null;
					if (!flag5)
					{
						continue;
					}
					bool matchedObsoleteName = false;
					foreach (string obsoleteName in this.m_ObsoleteNames)
					{
						bool flag6 = attributeOverride.m_AttributeName != obsoleteName;
						if (!flag6)
						{
							matchedObsoleteName = true;
							break;
						}
					}
					bool flag7 = !matchedObsoleteName;
					if (flag7)
					{
						continue;
					}
				}
				bool flag8 = overrideToApply.m_AttributeName == null;
				if (flag8)
				{
					overrideToApply = attributeOverride;
					bool flag9 = overrideToApply.m_NamesPath == null;
					if (flag9)
					{
						break;
					}
				}
				else
				{
					bool flag10 = overrideToApply.m_NamesPath.Length < attributeOverride.m_NamesPath.Length;
					if (flag10)
					{
						overrideToApply = attributeOverride;
					}
				}
			}
			bool flag11 = overrideToApply.m_AttributeName != null;
			bool flag12;
			if (flag11)
			{
				value = overrideToApply.m_Value;
				flag12 = true;
			}
			else
			{
				flag12 = false;
			}
			return flag12;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0007B93C File Offset: 0x00079B3C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool TryGetValueFromBagAsString(IUxmlAttributes bag, CreationContext cc, out string value)
		{
			VisualTreeAsset visualTreeAsset;
			return this.TryGetValueFromBagAsString(bag, cc, out value, out visualTreeAsset);
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0007B95C File Offset: 0x00079B5C
		internal bool TryGetAttributeOverrideValueFromBagAsString(IUxmlAttributes bag, CreationContext cc, out string value, out VisualTreeAsset sourceAsset)
		{
			string elementName;
			bag.TryGetAttributeValue("name", out elementName);
			bool flag = !string.IsNullOrEmpty(elementName) && cc.attributeOverrides != null;
			if (flag)
			{
				foreach (CreationContext.AttributeOverrideRange attributeOverrideRange in cc.attributeOverrides)
				{
					bool flag2 = this.TryFindValueInAttributeOverrides(elementName, cc, attributeOverrideRange.attributeOverrides, out value);
					if (flag2)
					{
						sourceAsset = attributeOverrideRange.sourceAsset;
						return true;
					}
				}
			}
			sourceAsset = null;
			value = null;
			return false;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0007BA0C File Offset: 0x00079C0C
		internal bool ValidateName()
		{
			bool flag = this.name == null && (this.m_ObsoleteNames == null || this.m_ObsoleteNames.Length == 0);
			bool flag2;
			if (flag)
			{
				Debug.LogError("Attribute description has no name.");
				flag2 = false;
			}
			else
			{
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0007BA54 File Offset: 0x00079C54
		internal bool TryGetValueFromBagAsString(IUxmlAttributes bag, CreationContext cc, out string value, out VisualTreeAsset sourceAsset)
		{
			value = null;
			sourceAsset = null;
			bool flag = !this.ValidateName();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.TryGetAttributeOverrideValueFromBagAsString(bag, cc, out value, out sourceAsset);
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = this.name == null;
					if (flag4)
					{
						for (int i = 0; i < this.m_ObsoleteNames.Length; i++)
						{
							bool flag5 = bag.TryGetAttributeValue(this.m_ObsoleteNames[i], out value);
							if (flag5)
							{
								bool flag6 = cc.visualTreeAsset != null;
								if (flag6)
								{
								}
								sourceAsset = cc.visualTreeAsset;
								return true;
							}
						}
						flag2 = false;
					}
					else
					{
						bool flag7 = !bag.TryGetAttributeValue(this.name, out value);
						if (flag7)
						{
							bool flag8 = this.m_ObsoleteNames != null;
							if (flag8)
							{
								for (int j = 0; j < this.m_ObsoleteNames.Length; j++)
								{
									bool flag9 = bag.TryGetAttributeValue(this.m_ObsoleteNames[j], out value);
									if (flag9)
									{
										bool flag10 = cc.visualTreeAsset != null;
										if (flag10)
										{
										}
										sourceAsset = cc.visualTreeAsset;
										UxmlAsset uxmlAsset = bag as UxmlAsset;
										bool flag11 = uxmlAsset != null;
										if (flag11)
										{
											uxmlAsset.RemoveAttribute(this.m_ObsoleteNames[j]);
											uxmlAsset.SetAttribute(this.name, value);
										}
										return true;
									}
								}
							}
							flag2 = false;
						}
						else
						{
							sourceAsset = cc.visualTreeAsset;
							flag2 = true;
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x0007BBD8 File Offset: 0x00079DD8
		protected bool TryGetValueFromBag<T>(IUxmlAttributes bag, CreationContext cc, Func<string, T, T> converterFunc, T defaultValue, ref T value)
		{
			string stringValue;
			bool flag = this.TryGetValueFromBagAsString(bag, cc, out stringValue);
			bool flag3;
			if (flag)
			{
				bool flag2 = converterFunc != null;
				if (flag2)
				{
					value = converterFunc(stringValue, defaultValue);
				}
				else
				{
					value = defaultValue;
				}
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0007BC28 File Offset: 0x00079E28
		protected T GetValueFromBag<T>(IUxmlAttributes bag, CreationContext cc, Func<string, T, T> converterFunc, T defaultValue)
		{
			bool flag = converterFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("converterFunc");
			}
			string value;
			bool flag2 = this.TryGetValueFromBagAsString(bag, cc, out value);
			T t;
			if (flag2)
			{
				t = converterFunc(value, defaultValue);
			}
			else
			{
				t = defaultValue;
			}
			return t;
		}

		// Token: 0x04000EE3 RID: 3811
		private string[] m_ObsoleteNames;

		// Token: 0x02000480 RID: 1152
		public enum Use
		{
			// Token: 0x04000EE9 RID: 3817
			None,
			// Token: 0x04000EEA RID: 3818
			Optional,
			// Token: 0x04000EEB RID: 3819
			Prohibited,
			// Token: 0x04000EEC RID: 3820
			Required
		}
	}
}
