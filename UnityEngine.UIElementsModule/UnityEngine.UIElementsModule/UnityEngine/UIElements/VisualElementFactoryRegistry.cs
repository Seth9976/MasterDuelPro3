using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020004B2 RID: 1202
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class VisualElementFactoryRegistry
	{
		// Token: 0x06002245 RID: 8773 RVA: 0x0007D4A8 File Offset: 0x0007B6A8
		internal static string GetMovedUIControlTypeName(Type type, MovedFromAttribute attr)
		{
			bool flag = type == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				MovedFromAttributeData data = attr.data;
				string namespaceName = (data.nameSpaceHasChanged ? data.nameSpace : type.Namespace);
				string typeName = (data.classHasChanged ? data.className : type.Name);
				string fullOldName = namespaceName + "." + typeName;
				text = fullOldName;
			}
			return text;
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x0007D518 File Offset: 0x0007B718
		internal static Dictionary<string, List<IUxmlFactory>> factories
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				bool flag = VisualElementFactoryRegistry.s_Factories == null;
				if (flag)
				{
					VisualElementFactoryRegistry.s_Factories = new Dictionary<string, List<IUxmlFactory>>();
					VisualElementFactoryRegistry.s_MovedTypesFactories = new Dictionary<string, List<IUxmlFactory>>(50);
					VisualElementFactoryRegistry.RegisterEngineFactories();
					VisualElementFactoryRegistry.RegisterUserFactories();
				}
				return VisualElementFactoryRegistry.s_Factories;
			}
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0007D560 File Offset: 0x0007B760
		protected static void RegisterFactory(IUxmlFactory factory)
		{
			List<IUxmlFactory> factoryList;
			bool flag = VisualElementFactoryRegistry.factories.TryGetValue(factory.uxmlQualifiedName, out factoryList);
			if (flag)
			{
				foreach (IUxmlFactory f in factoryList)
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
				factoryList = new List<IUxmlFactory>();
				factoryList.Add(factory);
				VisualElementFactoryRegistry.s_Factories.Add(factory.uxmlQualifiedName, factoryList);
				Type uxmlType = factory.uxmlType;
				MovedFromAttribute attr = ((uxmlType != null) ? uxmlType.GetCustomAttribute(false) : null);
				bool flag3 = attr != null && typeof(VisualElement).IsAssignableFrom(uxmlType);
				if (flag3)
				{
					string movedTypeName = VisualElementFactoryRegistry.GetMovedUIControlTypeName(uxmlType, attr);
					bool flag4 = !string.IsNullOrEmpty(movedTypeName);
					if (flag4)
					{
						VisualElementFactoryRegistry.s_MovedTypesFactories.Add(movedTypeName, factoryList);
					}
				}
			}
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0007D690 File Offset: 0x0007B890
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static bool TryGetValue(string fullTypeName, out List<IUxmlFactory> factoryList)
		{
			bool ret = VisualElementFactoryRegistry.factories.TryGetValue(fullTypeName, out factoryList);
			bool flag = !ret;
			if (flag)
			{
				ret = VisualElementFactoryRegistry.s_MovedTypesFactories.TryGetValue(fullTypeName, out factoryList);
			}
			return ret;
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x0007D6C8 File Offset: 0x0007B8C8
		private static void RegisterEngineFactories()
		{
			IUxmlFactory[] factories = new IUxmlFactory[]
			{
				new UxmlRootElementFactory(),
				new UxmlTemplateFactory(),
				new UxmlStyleFactory(),
				new UxmlAttributeOverridesFactory(),
				new Button.UxmlFactory(),
				new ToggleButtonGroup.UxmlFactory(),
				new VisualElement.UxmlFactory(),
				new IMGUIContainer.UxmlFactory(),
				new Image.UxmlFactory(),
				new Label.UxmlFactory(),
				new RepeatButton.UxmlFactory(),
				new ScrollView.UxmlFactory(),
				new Scroller.UxmlFactory(),
				new Slider.UxmlFactory(),
				new SliderInt.UxmlFactory(),
				new MinMaxSlider.UxmlFactory(),
				new GroupBox.UxmlFactory(),
				new RadioButton.UxmlFactory(),
				new RadioButtonGroup.UxmlFactory(),
				new Toggle.UxmlFactory(),
				new TextField.UxmlFactory(),
				new TemplateContainer.UxmlFactory(),
				new Box.UxmlFactory(),
				new EnumField.UxmlFactory(),
				new DropdownField.UxmlFactory(),
				new HelpBox.UxmlFactory(),
				new PopupWindow.UxmlFactory(),
				new ProgressBar.UxmlFactory(),
				new ListView.UxmlFactory(),
				new TwoPaneSplitView.UxmlFactory(),
				new TreeView.UxmlFactory(),
				new Foldout.UxmlFactory(),
				new MultiColumnListView.UxmlFactory(),
				new MultiColumnTreeView.UxmlFactory(),
				new BindableElement.UxmlFactory(),
				new TextElement.UxmlFactory(),
				new ButtonStripField.UxmlFactory(),
				new FloatField.UxmlFactory(),
				new DoubleField.UxmlFactory(),
				new Hash128Field.UxmlFactory(),
				new IntegerField.UxmlFactory(),
				new LongField.UxmlFactory(),
				new UnsignedIntegerField.UxmlFactory(),
				new UnsignedLongField.UxmlFactory(),
				new RectField.UxmlFactory(),
				new Vector2Field.UxmlFactory(),
				new RectIntField.UxmlFactory(),
				new Vector3Field.UxmlFactory(),
				new Vector4Field.UxmlFactory(),
				new Vector2IntField.UxmlFactory(),
				new Vector3IntField.UxmlFactory(),
				new BoundsField.UxmlFactory(),
				new BoundsIntField.UxmlFactory(),
				new Tab.UxmlFactory(),
				new TabView.UxmlFactory()
			};
			foreach (IUxmlFactory factory in factories)
			{
				VisualElementFactoryRegistry.RegisterFactory(factory);
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x0007D8E4 File Offset: 0x0007BAE4
		internal static void RegisterUserFactories()
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
						bool flag2 = !typeof(IUxmlFactory).IsAssignableFrom(type) || type.IsInterface || type.IsAbstract || type.IsGenericType;
						if (!flag2)
						{
							IUxmlFactory factory = (IUxmlFactory)Activator.CreateInstance(type);
							VisualElementFactoryRegistry.RegisterFactory(factory);
						}
					}
				}
			}
		}

		// Token: 0x04000F2A RID: 3882
		private static Dictionary<string, List<IUxmlFactory>> s_Factories;

		// Token: 0x04000F2B RID: 3883
		private static Dictionary<string, List<IUxmlFactory>> s_MovedTypesFactories;
	}
}
