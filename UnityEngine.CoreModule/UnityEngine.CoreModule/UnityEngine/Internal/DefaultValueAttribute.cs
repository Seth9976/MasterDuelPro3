using System;

namespace UnityEngine.Internal
{
	// Token: 0x020002F8 RID: 760
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
	[Serializable]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x06001523 RID: 5411 RVA: 0x0002CC56 File Offset: 0x0002AE56
		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0002CC68 File Offset: 0x0002AE68
		public object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0002CC80 File Offset: 0x0002AE80
		public override bool Equals(object obj)
		{
			DefaultValueAttribute dva = obj as DefaultValueAttribute;
			bool flag = dva == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.DefaultValue == null;
				if (flag3)
				{
					flag2 = dva.Value == null;
				}
				else
				{
					flag2 = this.DefaultValue.Equals(dva.Value);
				}
			}
			return flag2;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0002CCD0 File Offset: 0x0002AED0
		public override int GetHashCode()
		{
			bool flag = this.DefaultValue == null;
			int num;
			if (flag)
			{
				num = base.GetHashCode();
			}
			else
			{
				num = this.DefaultValue.GetHashCode();
			}
			return num;
		}

		// Token: 0x040007F0 RID: 2032
		private object DefaultValue;
	}
}
