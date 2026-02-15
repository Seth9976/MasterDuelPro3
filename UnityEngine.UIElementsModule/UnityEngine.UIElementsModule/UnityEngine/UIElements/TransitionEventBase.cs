using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200023B RID: 571
	[EventCategory(EventCategory.StyleTransition)]
	public abstract class TransitionEventBase<T> : EventBase<T> where T : TransitionEventBase<T>, new()
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0004324C File Offset: 0x0004144C
		public StylePropertyNameCollection stylePropertyNames { get; }

		// Token: 0x170002DD RID: 733
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x00043254 File Offset: 0x00041454
		protected double elapsedTime
		{
			[CompilerGenerated]
			set
			{
				this.<elapsedTime>k__BackingField = value;
			}
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0004325D File Offset: 0x0004145D
		protected TransitionEventBase()
		{
			this.stylePropertyNames = new StylePropertyNameCollection(new List<StylePropertyName>());
			this.LocalInit();
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0004327E File Offset: 0x0004147E
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0004328F File Offset: 0x0004148F
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles;
			this.stylePropertyNames.propertiesList.Clear();
			this.elapsedTime = 0.0;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x000432BC File Offset: 0x000414BC
		public static T GetPooled(StylePropertyName stylePropertyName, double elapsedTime)
		{
			T e = EventBase<T>.GetPooled();
			e.stylePropertyNames.propertiesList.Add(stylePropertyName);
			e.elapsedTime = elapsedTime;
			return e;
		}
	}
}
