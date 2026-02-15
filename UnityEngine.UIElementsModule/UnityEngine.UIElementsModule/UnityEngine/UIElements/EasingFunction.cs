using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CC RID: 716
	public struct EasingFunction : IEquatable<EasingFunction>
	{
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0005A275 File Offset: 0x00058475
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0005A27D File Offset: 0x0005847D
		public EasingMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0005A286 File Offset: 0x00058486
		public EasingFunction(EasingMode mode)
		{
			this.m_Mode = mode;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0005A290 File Offset: 0x00058490
		public static implicit operator EasingFunction(EasingMode easingMode)
		{
			return new EasingFunction(easingMode);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0005A2A8 File Offset: 0x000584A8
		public static bool operator ==(EasingFunction lhs, EasingFunction rhs)
		{
			return lhs.m_Mode == rhs.m_Mode;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0005A2C8 File Offset: 0x000584C8
		public bool Equals(EasingFunction other)
		{
			return other == this;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0005A2E8 File Offset: 0x000584E8
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is EasingFunction)
			{
				EasingFunction other = (EasingFunction)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0005A314 File Offset: 0x00058514
		public override string ToString()
		{
			return this.m_Mode.ToString();
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0005A338 File Offset: 0x00058538
		public override int GetHashCode()
		{
			return (int)this.m_Mode;
		}

		// Token: 0x04000B43 RID: 2883
		private EasingMode m_Mode;

		// Token: 0x020002CD RID: 717
		internal class PropertyBag : ContainerPropertyBag<EasingFunction>
		{
			// Token: 0x060013E0 RID: 5088 RVA: 0x0005A350 File Offset: 0x00058550
			public PropertyBag()
			{
				base.AddProperty<EasingMode>(new EasingFunction.PropertyBag.ModeProperty());
			}

			// Token: 0x020002CE RID: 718
			private class ModeProperty : Property<EasingFunction, EasingMode>
			{
				// Token: 0x17000408 RID: 1032
				// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0005A366 File Offset: 0x00058566
				public override string Name { get; } = "mode";

				// Token: 0x17000409 RID: 1033
				// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0005A36E File Offset: 0x0005856E
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060013E3 RID: 5091 RVA: 0x0005A376 File Offset: 0x00058576
				public override EasingMode GetValue(ref EasingFunction container)
				{
					return container.mode;
				}

				// Token: 0x060013E4 RID: 5092 RVA: 0x0005A37E File Offset: 0x0005857E
				public override void SetValue(ref EasingFunction container, EasingMode value)
				{
					container.mode = value;
				}
			}
		}
	}
}
