using System;

namespace Unity.Properties
{
	// Token: 0x02000012 RID: 18
	public class DelegateProperty<TContainer, TValue> : Property<TContainer, TValue>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002645 File Offset: 0x00000845
		public override string Name { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000264D File Offset: 0x0000084D
		public override bool IsReadOnly
		{
			get
			{
				return this.m_Setter == null;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002658 File Offset: 0x00000858
		public DelegateProperty(string name, PropertyGetter<TContainer, TValue> getter, PropertySetter<TContainer, TValue> setter = null)
		{
			this.Name = name;
			if (getter == null)
			{
				throw new ArgumentException("getter");
			}
			this.m_Getter = getter;
			this.m_Setter = setter;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002688 File Offset: 0x00000888
		public override TValue GetValue(ref TContainer container)
		{
			return this.m_Getter(ref container);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026A8 File Offset: 0x000008A8
		public override void SetValue(ref TContainer container, TValue value)
		{
			bool isReadOnly = this.IsReadOnly;
			if (isReadOnly)
			{
				throw new InvalidOperationException("Property is ReadOnly.");
			}
			this.m_Setter(ref container, value);
		}

		// Token: 0x04000020 RID: 32
		private readonly PropertyGetter<TContainer, TValue> m_Getter;

		// Token: 0x04000021 RID: 33
		private readonly PropertySetter<TContainer, TValue> m_Setter;
	}
}
