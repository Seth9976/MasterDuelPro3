using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004AB RID: 1195
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Obsolete("UxmlObjectFactoryRegistry is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	internal class UxmlObjectFactoryRegistry
	{
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x0007CCE8 File Offset: 0x0007AEE8
		internal static Dictionary<string, List<IBaseUxmlObjectFactory>> factories
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				bool flag = UxmlObjectFactoryRegistry.s_Factories == null;
				if (flag)
				{
					UxmlObjectFactoryRegistry.s_Factories = new Dictionary<string, List<IBaseUxmlObjectFactory>>();
					UxmlObjectFactoryRegistry.RegisterEngineFactories();
					UxmlObjectFactoryRegistry.RegisterUserFactories();
				}
				return UxmlObjectFactoryRegistry.s_Factories;
			}
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0007CD24 File Offset: 0x0007AF24
		protected static void RegisterFactory(IBaseUxmlObjectFactory factory)
		{
			List<IBaseUxmlObjectFactory> factoryList;
			bool flag = UxmlObjectFactoryRegistry.factories.TryGetValue(factory.uxmlQualifiedName, out factoryList);
			if (flag)
			{
				foreach (IBaseUxmlObjectFactory f in factoryList)
				{
					bool flag2 = f.GetType() == factory.GetType();
					if (flag2)
					{
						throw new ArgumentException("A factory for the type " + factory.GetType().FullName + " was already registered");
					}
				}
				factoryList.Add(factory);
			}
			else
			{
				factoryList = new List<IBaseUxmlObjectFactory> { factory };
				UxmlObjectFactoryRegistry.s_Factories.Add(factory.uxmlQualifiedName, factoryList);
			}
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0007CDEC File Offset: 0x0007AFEC
		private static void RegisterEngineFactories()
		{
			IBaseUxmlObjectFactory[] objectFactories = new IBaseUxmlObjectFactory[]
			{
				new Columns.UxmlObjectFactory(),
				new Column.UxmlObjectFactory(),
				new SortColumnDescriptions.UxmlObjectFactory(),
				new SortColumnDescription.UxmlObjectFactory()
			};
			foreach (IBaseUxmlObjectFactory factory in objectFactories)
			{
				UxmlObjectFactoryRegistry.RegisterFactory(factory);
			}
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x0007CE40 File Offset: 0x0007B040
		private static void RegisterUserFactories()
		{
			HashSet<string> userAssemblies = new HashSet<string>(ScriptingRuntime.GetAllUserAssemblies());
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				bool flag = !userAssemblies.Contains(assembly.GetName().Name + ".dll") || assembly.GetName().Name == "UnityEngine.UIElementsModule";
				if (!flag)
				{
					Type[] types = assembly.GetTypes();
					foreach (Type type in types)
					{
						bool flag2 = !typeof(IBaseUxmlObjectFactory).IsAssignableFrom(type) || type.IsInterface || type.IsAbstract || type.IsGenericType;
						if (!flag2)
						{
							IBaseUxmlObjectFactory factory = (IBaseUxmlObjectFactory)Activator.CreateInstance(type);
							UxmlObjectFactoryRegistry.RegisterFactory(factory);
						}
					}
				}
			}
		}

		// Token: 0x04000F17 RID: 3863
		private static Dictionary<string, List<IBaseUxmlObjectFactory>> s_Factories;
	}
}
