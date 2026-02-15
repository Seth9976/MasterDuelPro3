using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C9 RID: 713
	public struct CustomStyleProperty<T> : IEquatable<CustomStyleProperty<T>>
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x0005A1A3 File Offset: 0x000583A3
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x0005A1AB File Offset: 0x000583AB
		public string name { readonly get; private set; }

		// Token: 0x060013CC RID: 5068 RVA: 0x0005A1B4 File Offset: 0x000583B4
		public CustomStyleProperty(string propertyName)
		{
			bool flag = !string.IsNullOrEmpty(propertyName) && !propertyName.StartsWith("--");
			if (flag)
			{
				throw new ArgumentException("Custom style property \"" + propertyName + "\" must start with \"--\" prefix.");
			}
			this.name = propertyName;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0005A200 File Offset: 0x00058400
		public override bool Equals(object obj)
		{
			bool flag = !(obj is CustomStyleProperty<T>);
			return !flag && this.Equals((CustomStyleProperty<T>)obj);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0005A234 File Offset: 0x00058434
		public bool Equals(CustomStyleProperty<T> other)
		{
			return this.name == other.name;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0005A258 File Offset: 0x00058458
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}
	}
}
