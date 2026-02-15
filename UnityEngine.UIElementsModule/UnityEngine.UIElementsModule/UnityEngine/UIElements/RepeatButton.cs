using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000132 RID: 306
	public class RepeatButton : TextElement
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x0002C21F File Offset: 0x0002A41F
		public RepeatButton()
		{
			base.AddToClassList(RepeatButton.ussClassName);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0002C235 File Offset: 0x0002A435
		public RepeatButton(Action clickEvent, long delay, long interval)
			: this()
		{
			this.SetAction(clickEvent, delay, interval);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0002C249 File Offset: 0x0002A449
		public void SetAction(Action clickEvent, long delay, long interval)
		{
			this.RemoveManipulator(this.m_Clickable);
			this.m_Clickable = new Clickable(clickEvent, delay, interval);
			this.AddManipulator(this.m_Clickable);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0002C274 File Offset: 0x0002A474
		internal void AddAction(Action clickEvent)
		{
			this.m_Clickable.clicked += clickEvent;
		}

		// Token: 0x040005D0 RID: 1488
		private Clickable m_Clickable;

		// Token: 0x040005D1 RID: 1489
		public new static readonly string ussClassName = "unity-repeat-button";

		// Token: 0x02000133 RID: 307
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<RepeatButton, RepeatButton.UxmlTraits>
		{
		}

		// Token: 0x02000134 RID: 308
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x0600094D RID: 2381 RVA: 0x0002C29C File Offset: 0x0002A49C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RepeatButton repeatButton = (RepeatButton)ve;
				repeatButton.SetAction(null, this.m_Delay.GetValueFromBag(bag, cc), this.m_Interval.GetValueFromBag(bag, cc));
			}

			// Token: 0x040005D2 RID: 1490
			private UxmlLongAttributeDescription m_Delay = new UxmlLongAttributeDescription
			{
				name = "delay"
			};

			// Token: 0x040005D3 RID: 1491
			private UxmlLongAttributeDescription m_Interval = new UxmlLongAttributeDescription
			{
				name = "interval"
			};
		}
	}
}
