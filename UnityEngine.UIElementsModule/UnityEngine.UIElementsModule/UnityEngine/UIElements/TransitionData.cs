using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020003B7 RID: 951
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct TransitionData : IStyleDataGroup<TransitionData>, IEquatable<TransitionData>
	{
		// Token: 0x06001C12 RID: 7186 RVA: 0x00069604 File Offset: 0x00067804
		public TransitionData Copy()
		{
			return new TransitionData
			{
				transitionDelay = new List<TimeValue>(this.transitionDelay),
				transitionDuration = new List<TimeValue>(this.transitionDuration),
				transitionProperty = new List<StylePropertyName>(this.transitionProperty),
				transitionTimingFunction = new List<EasingFunction>(this.transitionTimingFunction)
			};
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00069668 File Offset: 0x00067868
		public void CopyFrom(ref TransitionData other)
		{
			bool flag = this.transitionDelay != other.transitionDelay;
			if (flag)
			{
				this.transitionDelay.Clear();
				this.transitionDelay.AddRange(other.transitionDelay);
			}
			bool flag2 = this.transitionDuration != other.transitionDuration;
			if (flag2)
			{
				this.transitionDuration.Clear();
				this.transitionDuration.AddRange(other.transitionDuration);
			}
			bool flag3 = this.transitionProperty != other.transitionProperty;
			if (flag3)
			{
				this.transitionProperty.Clear();
				this.transitionProperty.AddRange(other.transitionProperty);
			}
			bool flag4 = this.transitionTimingFunction != other.transitionTimingFunction;
			if (flag4)
			{
				this.transitionTimingFunction.Clear();
				this.transitionTimingFunction.AddRange(other.transitionTimingFunction);
			}
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0006974C File Offset: 0x0006794C
		public static bool operator ==(TransitionData lhs, TransitionData rhs)
		{
			return lhs.transitionDelay == rhs.transitionDelay && lhs.transitionDuration == rhs.transitionDuration && lhs.transitionProperty == rhs.transitionProperty && lhs.transitionTimingFunction == rhs.transitionTimingFunction;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0006979C File Offset: 0x0006799C
		public bool Equals(TransitionData other)
		{
			return other == this;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000697BC File Offset: 0x000679BC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is TransitionData && this.Equals((TransitionData)obj);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000697F4 File Offset: 0x000679F4
		public override int GetHashCode()
		{
			int hashCode = this.transitionDelay.GetHashCode();
			hashCode = (hashCode * 397) ^ this.transitionDuration.GetHashCode();
			hashCode = (hashCode * 397) ^ this.transitionProperty.GetHashCode();
			return (hashCode * 397) ^ this.transitionTimingFunction.GetHashCode();
		}

		// Token: 0x04000C51 RID: 3153
		public List<TimeValue> transitionDelay;

		// Token: 0x04000C52 RID: 3154
		public List<TimeValue> transitionDuration;

		// Token: 0x04000C53 RID: 3155
		public List<StylePropertyName> transitionProperty;

		// Token: 0x04000C54 RID: 3156
		public List<EasingFunction> transitionTimingFunction;
	}
}
