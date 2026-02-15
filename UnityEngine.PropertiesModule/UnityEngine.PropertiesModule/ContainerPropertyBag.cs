using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000026 RID: 38
	public abstract class ContainerPropertyBag<TContainer> : PropertyBag<TContainer>, INamedProperties<TContainer>
	{
		// Token: 0x0600008F RID: 143 RVA: 0x000044C4 File Offset: 0x000026C4
		static ContainerPropertyBag()
		{
			bool flag = !TypeTraits.IsContainer(typeof(TContainer));
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Failed to create a property bag for Type=[{0}]. The type is not a valid container type.", typeof(TContainer)));
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004503 File Offset: 0x00002703
		protected void AddProperty<TValue>(Property<TContainer, TValue> property)
		{
			this.m_PropertiesList.Add(property);
			this.m_PropertiesHash.Add(property.Name, property);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004526 File Offset: 0x00002726
		public override PropertyCollection<TContainer> GetProperties()
		{
			return new PropertyCollection<TContainer>(this.m_PropertiesList);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004526 File Offset: 0x00002726
		public override PropertyCollection<TContainer> GetProperties(ref TContainer container)
		{
			return new PropertyCollection<TContainer>(this.m_PropertiesList);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004533 File Offset: 0x00002733
		public bool TryGetProperty(ref TContainer container, string name, out IProperty<TContainer> property)
		{
			return this.m_PropertiesHash.TryGetValue(name, out property);
		}

		// Token: 0x04000043 RID: 67
		private readonly List<IProperty<TContainer>> m_PropertiesList = new List<IProperty<TContainer>>();

		// Token: 0x04000044 RID: 68
		private readonly Dictionary<string, IProperty<TContainer>> m_PropertiesHash = new Dictionary<string, IProperty<TContainer>>();
	}
}
