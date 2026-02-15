using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides a read-only container for a collection of <see cref="T:System.ComponentModel.IComponent" /> objects.</summary>
	// Token: 0x0200023E RID: 574
	public class ComponentCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComponentCollection" /> class using the specified array of components.</summary>
		/// <param name="components">An array of <see cref="T:System.ComponentModel.IComponent" /> objects to initialize the collection with. </param>
		// Token: 0x06000DD4 RID: 3540 RVA: 0x0003E372 File Offset: 0x0003C572
		public ComponentCollection(IComponent[] components)
		{
			base.InnerList.AddRange(components);
		}

		/// <summary>Gets any component in the collection matching the specified name.</summary>
		/// <returns>A component with a name matching the name specified by the <paramref name="name" /> parameter, or null if the named component cannot be found in the collection.</returns>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.IComponent" /> to get. </param>
		// Token: 0x170002EB RID: 747
		public virtual IComponent this[string name]
		{
			get
			{
				if (name != null)
				{
					foreach (object obj in ((IEnumerable)base.InnerList))
					{
						IComponent component = (IComponent)obj;
						if (component != null && component.Site != null && component.Site.Name != null && string.Equals(component.Site.Name, name, StringComparison.OrdinalIgnoreCase))
						{
							return component;
						}
					}
				}
				return null;
			}
		}
	}
}
