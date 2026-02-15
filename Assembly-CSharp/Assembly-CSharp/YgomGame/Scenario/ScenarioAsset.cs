using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Scenario
{
	// Token: 0x020009A5 RID: 2469
	public class ScenarioAsset : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060047E8 RID: 18408 RVA: 0x0000216A File Offset: 0x0000036A
		public List<object> commandList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnAfterDeserialize()
		{
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x0000216A File Offset: 0x0000036A
		public static object CreateCommand()
		{
			return null;
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCommandKey(object commandData)
		{
			return null;
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCommandKey(object commandData, string key)
		{
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x000029CC File Offset: 0x00000BCC
		public static ScenarioDef.BehaviourAsyncMode GetAsyncMode(object commandData)
		{
			return ScenarioDef.BehaviourAsyncMode.None;
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAsyncMode(object commandData, ScenarioDef.BehaviourAsyncMode value)
		{
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetArgs(object commandData)
		{
			return null;
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetCommandKeyByIdx(int index)
		{
			return null;
		}

		// Token: 0x060047F2 RID: 18418 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCommandKeyByIdx(int index, string commandKey)
		{
		}

		// Token: 0x060047F3 RID: 18419 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsSupportedAsyncByIdx(int index)
		{
			return false;
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x000029CC File Offset: 0x00000BCC
		public ScenarioDef.BehaviourAsyncMode GetAsyncModeByIdx(int index)
		{
			return ScenarioDef.BehaviourAsyncMode.None;
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAsyncModeByIdx(int index, ScenarioDef.BehaviourAsyncMode value)
		{
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<string, object> GetArgsByIdx(int index)
		{
			return null;
		}

		// Token: 0x04008641 RID: 34369
		private List<object> m_CommandList;

		// Token: 0x04008642 RID: 34370
		[SerializeField]
		private string[] m_CommandJsons;
	}
}
