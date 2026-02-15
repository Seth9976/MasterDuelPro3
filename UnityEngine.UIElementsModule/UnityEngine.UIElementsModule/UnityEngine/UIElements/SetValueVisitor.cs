using System;
using Unity.Properties;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000171 RID: 369
	internal class SetValueVisitor<TSrcValue> : PathVisitor
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x000357A6 File Offset: 0x000339A6
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x000357AE File Offset: 0x000339AE
		public ConverterGroup group { get; set; }

		// Token: 0x06000AEE RID: 2798 RVA: 0x000357B7 File Offset: 0x000339B7
		public override void Reset()
		{
			base.Reset();
			this.Value = default(TSrcValue);
			this.group = null;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000357D8 File Offset: 0x000339D8
		protected override void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
		{
			bool isReadOnly = property.IsReadOnly;
			if (isReadOnly)
			{
				base.ReturnCode = VisitReturnCode.AccessViolation;
			}
			else
			{
				TValue local;
				bool flag = this.group != null && this.group.TryConvert<TSrcValue, TValue>(ref this.Value, out local);
				if (flag)
				{
					property.SetValue(ref container, local);
				}
				else
				{
					TValue global;
					bool flag2 = ConverterGroups.TryConvert<TSrcValue, TValue>(ref this.Value, out global);
					if (flag2)
					{
						property.SetValue(ref container, global);
					}
					else
					{
						base.ReturnCode = VisitReturnCode.InvalidCast;
					}
				}
			}
		}

		// Token: 0x04000717 RID: 1815
		public static readonly ObjectPool<SetValueVisitor<TSrcValue>> Pool = new ObjectPool<SetValueVisitor<TSrcValue>>(() => new SetValueVisitor<TSrcValue>(), delegate(SetValueVisitor<TSrcValue> v)
		{
			v.Reset();
		}, null, null, true, 10, 10000);

		// Token: 0x04000718 RID: 1816
		public TSrcValue Value;
	}
}
