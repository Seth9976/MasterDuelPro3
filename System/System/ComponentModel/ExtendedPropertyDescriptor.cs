using System;
using System.Collections.Generic;

namespace System.ComponentModel
{
	// Token: 0x02000278 RID: 632
	internal sealed class ExtendedPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000F0A RID: 3850 RVA: 0x00041B9C File Offset: 0x0003FD9C
		public ExtendedPropertyDescriptor(ReflectPropertyDescriptor extenderInfo, Type receiverType, IExtenderProvider provider, Attribute[] attributes)
			: base(extenderInfo, attributes)
		{
			List<Attribute> list = new List<Attribute>(this.AttributeArray);
			list.Add(ExtenderProvidedPropertyAttribute.Create(extenderInfo, receiverType, provider));
			if (extenderInfo.IsReadOnly)
			{
				list.Add(ReadOnlyAttribute.Yes);
			}
			Attribute[] array = new Attribute[list.Count];
			list.CopyTo(array, 0);
			this.AttributeArray = array;
			this._extenderInfo = extenderInfo;
			this._provider = provider;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00041C08 File Offset: 0x0003FE08
		public override bool CanResetValue(object comp)
		{
			return this._extenderInfo.ExtenderCanResetValue(this._provider, comp);
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00041C1C File Offset: 0x0003FE1C
		public override Type ComponentType
		{
			get
			{
				return this._extenderInfo.ComponentType;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00041C29 File Offset: 0x0003FE29
		public override bool IsReadOnly
		{
			get
			{
				return this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00041C4A File Offset: 0x0003FE4A
		public override Type PropertyType
		{
			get
			{
				return this._extenderInfo.ExtenderGetType(this._provider);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x00041C60 File Offset: 0x0003FE60
		public override string DisplayName
		{
			get
			{
				string text = base.DisplayName;
				DisplayNameAttribute displayNameAttribute = this.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
				if (displayNameAttribute == null || displayNameAttribute.IsDefaultAttribute())
				{
					ISite site = MemberDescriptor.GetSite(this._provider);
					string text2 = ((site != null) ? site.Name : null);
					if (text2 != null && text2.Length > 0)
					{
						text = string.Format("{0} on {1}", text, text2);
					}
				}
				return text;
			}
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00041CCC File Offset: 0x0003FECC
		public override object GetValue(object comp)
		{
			return this._extenderInfo.ExtenderGetValue(this._provider, comp);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00041CE0 File Offset: 0x0003FEE0
		public override void ResetValue(object comp)
		{
			this._extenderInfo.ExtenderResetValue(this._provider, comp, this);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00041CF5 File Offset: 0x0003FEF5
		public override void SetValue(object component, object value)
		{
			this._extenderInfo.ExtenderSetValue(this._provider, component, value, this);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00041D0B File Offset: 0x0003FF0B
		public override bool ShouldSerializeValue(object comp)
		{
			return this._extenderInfo.ExtenderShouldSerializeValue(this._provider, comp);
		}

		// Token: 0x040009FC RID: 2556
		private readonly ReflectPropertyDescriptor _extenderInfo;

		// Token: 0x040009FD RID: 2557
		private readonly IExtenderProvider _provider;
	}
}
