using System;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000421 RID: 1057
	public struct StylePropertyName : IEquatable<StylePropertyName>
	{
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x0007058A File Offset: 0x0006E78A
		internal readonly StylePropertyId id { get; }

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x00070592 File Offset: 0x0006E792
		private readonly string name { get; }

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0007059C File Offset: 0x0006E79C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static StylePropertyId StylePropertyIdFromString(string name)
		{
			StylePropertyId id;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, out id);
			StylePropertyId stylePropertyId;
			if (flag)
			{
				stylePropertyId = id;
			}
			else
			{
				stylePropertyId = StylePropertyId.Unknown;
			}
			return stylePropertyId;
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x000705C8 File Offset: 0x0006E7C8
		internal StylePropertyName(StylePropertyId stylePropertyId)
		{
			this.id = stylePropertyId;
			this.name = null;
			string name;
			bool flag = StylePropertyUtil.s_IdToName.TryGetValue(stylePropertyId, out name);
			if (flag)
			{
				this.name = name;
			}
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00070600 File Offset: 0x0006E800
		public StylePropertyName(string name)
		{
			this.id = StylePropertyName.StylePropertyIdFromString(name);
			this.name = null;
			bool flag = this.id > StylePropertyId.Unknown;
			if (flag)
			{
				this.name = name;
			}
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00070638 File Offset: 0x0006E838
		public static bool operator ==(StylePropertyName lhs, StylePropertyName rhs)
		{
			return lhs.id == rhs.id;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0007065C File Offset: 0x0006E85C
		public static bool operator !=(StylePropertyName lhs, StylePropertyName rhs)
		{
			return lhs.id != rhs.id;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00070684 File Offset: 0x0006E884
		public static implicit operator StylePropertyName(string name)
		{
			return new StylePropertyName(name);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0007069C File Offset: 0x0006E89C
		public override int GetHashCode()
		{
			return (int)this.id;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x000706B4 File Offset: 0x0006E8B4
		public override bool Equals(object other)
		{
			return other is StylePropertyName && this.Equals((StylePropertyName)other);
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000706E0 File Offset: 0x0006E8E0
		public bool Equals(StylePropertyName other)
		{
			return this == other;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00070700 File Offset: 0x0006E900
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x02000422 RID: 1058
		internal class PropertyBag : ContainerPropertyBag<StylePropertyName>
		{
			// Token: 0x06001EDA RID: 7898 RVA: 0x00070718 File Offset: 0x0006E918
			public PropertyBag()
			{
				base.AddProperty<StylePropertyId>(new StylePropertyName.PropertyBag.IdProperty());
				base.AddProperty<string>(new StylePropertyName.PropertyBag.NameProperty());
			}

			// Token: 0x02000423 RID: 1059
			private class IdProperty : Property<StylePropertyName, StylePropertyId>
			{
				// Token: 0x17000859 RID: 2137
				// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0007073A File Offset: 0x0006E93A
				public override string Name { get; } = "id";

				// Token: 0x1700085A RID: 2138
				// (get) Token: 0x06001EDC RID: 7900 RVA: 0x00070742 File Offset: 0x0006E942
				public override bool IsReadOnly { get; } = true;

				// Token: 0x06001EDD RID: 7901 RVA: 0x0007074A File Offset: 0x0006E94A
				public override StylePropertyId GetValue(ref StylePropertyName container)
				{
					return container.id;
				}

				// Token: 0x06001EDE RID: 7902 RVA: 0x000020EA File Offset: 0x000002EA
				public override void SetValue(ref StylePropertyName container, StylePropertyId value)
				{
				}
			}

			// Token: 0x02000424 RID: 1060
			private class NameProperty : Property<StylePropertyName, string>
			{
				// Token: 0x1700085B RID: 2139
				// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x0007076D File Offset: 0x0006E96D
				public override string Name { get; } = "name";

				// Token: 0x1700085C RID: 2140
				// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x00070775 File Offset: 0x0006E975
				public override bool IsReadOnly { get; } = true;

				// Token: 0x06001EE2 RID: 7906 RVA: 0x0007077D File Offset: 0x0006E97D
				public override string GetValue(ref StylePropertyName container)
				{
					return container.name;
				}

				// Token: 0x06001EE3 RID: 7907 RVA: 0x000020EA File Offset: 0x000002EA
				public override void SetValue(ref StylePropertyName container, string value)
				{
				}
			}
		}
	}
}
