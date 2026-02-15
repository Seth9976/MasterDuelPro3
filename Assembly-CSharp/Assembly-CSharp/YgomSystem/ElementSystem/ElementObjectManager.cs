using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.ElementSystem
{
	// Token: 0x0200077F RID: 1919
	[DisallowMultipleComponent]
	public class ElementObjectManager : MonoBehaviour
	{
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003B96 RID: 15254 RVA: 0x0000216D File Offset: 0x0000036D
		public bool error
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetElement(ElementObject element)
		{
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x0000216D File Offset: 0x0000036D
		private void ArrangementElements()
		{
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupChildrenElements()
		{
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupSerializedElements()
		{
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x0000216D File Offset: 0x0000036D
		private void Reset()
		{
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupElements()
		{
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x0000216D File Offset: 0x0000036D
		public void ApplySerializedElements()
		{
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckSameLabel()
		{
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x000F3A58 File Offset: 0x000F1C58
		public int GetElementsCount()
		{
			int count = 0;
			foreach (ElementObject elementObject in this.serializedElements)
			{
				count++;
			}
			return count;
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000F3A88 File Offset: 0x000F1C88
		public bool IsExistsLabel(string label)
		{
			ElementObject[] array = this.serializedElements;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].label == label)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x0000216A File Offset: 0x0000036A
		public SortedDictionary<string, ElementObject>.KeyCollection GetLabels()
		{
			return null;
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x000F3AC0 File Offset: 0x000F1CC0
		public GameObject GetElement(string label)
		{
			foreach (ElementObject element in this.serializedElements)
			{
				if (element.label == label)
				{
					return element.gameObject;
				}
			}
			return null;
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x000F3AFC File Offset: 0x000F1CFC
		public GameObject GetNestedElement(string label)
		{
			string[] paths = label.Split('/', StringSplitOptions.None);
			ElementObjectManager manager = this;
			for (int i = 0; i < paths.Length - 1; i++)
			{
				manager = manager.GetElement<ElementObjectManager>(paths[i]);
			}
			ElementObjectManager elementObjectManager = manager;
			string[] array = paths;
			return elementObjectManager.GetElement(array[array.Length - 1]);
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x000F3B3C File Offset: 0x000F1D3C
		public T GetElement<T>(string label) where T : global::UnityEngine.Object
		{
			foreach (ElementObject element in this.serializedElements)
			{
				if (element.label == label)
				{
					return element.GetComponent<T>();
				}
			}
			return default(T);
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x000F3B80 File Offset: 0x000F1D80
		public T GetNestedElement<T>(string label) where T : global::UnityEngine.Object
		{
			string[] paths = label.Split('/', StringSplitOptions.None);
			ElementObjectManager manager = this;
			for (int i = 0; i < paths.Length - 1; i++)
			{
				manager = manager.GetElement<ElementObjectManager>(paths[i]);
			}
			ElementObjectManager elementObjectManager = manager;
			string[] array = paths;
			return elementObjectManager.GetElement<T>(array[array.Length - 1]);
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCallback(string label, UnityAction callback)
		{
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetNestedCallback(string label, UnityAction callback)
		{
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCallbackInt(string label, UnityAction<int> callback)
		{
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCallbackFloat(string label, UnityAction<float> callback)
		{
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCallbackVector2(string label, UnityAction<Vector2> callback)
		{
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCallbackBool(string label, UnityAction<bool> callback)
		{
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCallbackString(string label, UnityAction<string> callback)
		{
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x000F3BC0 File Offset: 0x000F1DC0
		public void RegisterOnDestroyEvent(Action action)
		{
			this.OnDestroyAction = (Action)Delegate.Combine(this.OnDestroyAction, action);
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000F3BD9 File Offset: 0x000F1DD9
		private void OnDestroy()
		{
			Action onDestroyAction = this.OnDestroyAction;
			if (onDestroyAction == null)
			{
				return;
			}
			onDestroyAction();
		}

		// Token: 0x0400348F RID: 13455
		public SortedDictionary<string, ElementObject> elements;

		// Token: 0x04003490 RID: 13456
		[NonSerialized]
		public List<ElementObject> tempElements;

		// Token: 0x04003491 RID: 13457
		public Dictionary<string, int> labelCounter;

		// Token: 0x04003492 RID: 13458
		public ElementObject[] serializedElements;

		// Token: 0x04003493 RID: 13459
		private bool applied;

		// Token: 0x04003494 RID: 13460
		private Action OnDestroyAction;
	}
}
