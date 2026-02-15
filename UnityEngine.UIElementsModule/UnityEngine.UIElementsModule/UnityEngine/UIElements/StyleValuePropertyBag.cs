using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020003DE RID: 990
	internal class StyleValuePropertyBag<TContainer, TValue> : ContainerPropertyBag<TContainer> where TContainer : IStyleValue<TValue>
	{
		// Token: 0x06001D8A RID: 7562 RVA: 0x0006CBB4 File Offset: 0x0006ADB4
		public StyleValuePropertyBag()
		{
			base.AddProperty<TValue>(new StyleValuePropertyBag<TContainer, TValue>.ValueProperty());
			base.AddProperty<StyleKeyword>(new StyleValuePropertyBag<TContainer, TValue>.KeywordProperty());
		}

		// Token: 0x020003DF RID: 991
		private class ValueProperty : Property<TContainer, TValue>
		{
			// Token: 0x1700082B RID: 2091
			// (get) Token: 0x06001D8B RID: 7563 RVA: 0x0006CBD6 File Offset: 0x0006ADD6
			public override string Name { get; } = "value";

			// Token: 0x1700082C RID: 2092
			// (get) Token: 0x06001D8C RID: 7564 RVA: 0x0006CBDE File Offset: 0x0006ADDE
			public override bool IsReadOnly { get; } = false;

			// Token: 0x06001D8D RID: 7565 RVA: 0x0005F9E5 File Offset: 0x0005DBE5
			public override TValue GetValue(ref TContainer container)
			{
				return container.value;
			}

			// Token: 0x06001D8E RID: 7566 RVA: 0x0006CBE6 File Offset: 0x0006ADE6
			public override void SetValue(ref TContainer container, TValue value)
			{
				container.value = value;
			}
		}

		// Token: 0x020003E0 RID: 992
		private class KeywordProperty : Property<TContainer, StyleKeyword>
		{
			// Token: 0x1700082D RID: 2093
			// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0006CC11 File Offset: 0x0006AE11
			public override string Name { get; } = "keyword";

			// Token: 0x1700082E RID: 2094
			// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0006CC19 File Offset: 0x0006AE19
			public override bool IsReadOnly { get; } = false;

			// Token: 0x06001D92 RID: 7570 RVA: 0x0005FA1C File Offset: 0x0005DC1C
			public override StyleKeyword GetValue(ref TContainer container)
			{
				return container.keyword;
			}

			// Token: 0x06001D93 RID: 7571 RVA: 0x0006CC21 File Offset: 0x0006AE21
			public override void SetValue(ref TContainer container, StyleKeyword value)
			{
				container.keyword = value;
			}
		}
	}
}
