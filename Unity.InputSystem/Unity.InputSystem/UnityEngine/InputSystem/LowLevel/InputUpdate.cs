using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001CB RID: 459
	internal static class InputUpdate
	{
		// Token: 0x06001123 RID: 4387 RVA: 0x000514BD File Offset: 0x0004F6BD
		internal static void OnBeforeUpdate(InputUpdateType type)
		{
			InputUpdate.s_LatestUpdateType = type;
			if (type - InputUpdateType.Dynamic <= 1 || type == InputUpdateType.Manual)
			{
				InputUpdate.s_PlayerUpdateStepCount.OnBeforeUpdate();
				InputUpdate.s_UpdateStepCount = InputUpdate.s_PlayerUpdateStepCount.value;
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000514E9 File Offset: 0x0004F6E9
		internal static void OnUpdate(InputUpdateType type)
		{
			InputUpdate.s_LatestUpdateType = type;
			if (type - InputUpdateType.Dynamic <= 1 || type == InputUpdateType.Manual)
			{
				InputUpdate.s_PlayerUpdateStepCount.OnUpdate();
				InputUpdate.s_UpdateStepCount = InputUpdate.s_PlayerUpdateStepCount.value;
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00051518 File Offset: 0x0004F718
		public static InputUpdate.SerializedState Save()
		{
			return new InputUpdate.SerializedState
			{
				lastUpdateType = InputUpdate.s_LatestUpdateType,
				playerUpdateStepCount = InputUpdate.s_PlayerUpdateStepCount
			};
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00051548 File Offset: 0x0004F748
		public static void Restore(InputUpdate.SerializedState state)
		{
			InputUpdate.s_LatestUpdateType = state.lastUpdateType;
			InputUpdate.s_PlayerUpdateStepCount = state.playerUpdateStepCount;
			InputUpdateType inputUpdateType = InputUpdate.s_LatestUpdateType;
			if (inputUpdateType - InputUpdateType.Dynamic <= 1 || inputUpdateType == InputUpdateType.Manual)
			{
				InputUpdate.s_UpdateStepCount = InputUpdate.s_PlayerUpdateStepCount.value;
				return;
			}
			InputUpdate.s_UpdateStepCount = 0U;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00051592 File Offset: 0x0004F792
		public static InputUpdateType GetUpdateTypeForPlayer(this InputUpdateType mask)
		{
			if ((mask & InputUpdateType.Manual) != InputUpdateType.None)
			{
				return InputUpdateType.Manual;
			}
			if ((mask & InputUpdateType.Dynamic) != InputUpdateType.None)
			{
				return InputUpdateType.Dynamic;
			}
			if ((mask & InputUpdateType.Fixed) != InputUpdateType.None)
			{
				return InputUpdateType.Fixed;
			}
			return InputUpdateType.None;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000515AC File Offset: 0x0004F7AC
		public static bool IsPlayerUpdate(this InputUpdateType updateType)
		{
			return updateType != InputUpdateType.Editor && updateType > InputUpdateType.None;
		}

		// Token: 0x04000A59 RID: 2649
		public static uint s_UpdateStepCount;

		// Token: 0x04000A5A RID: 2650
		public static InputUpdateType s_LatestUpdateType;

		// Token: 0x04000A5B RID: 2651
		public static InputUpdate.UpdateStepCount s_PlayerUpdateStepCount;

		// Token: 0x020001CC RID: 460
		[Serializable]
		public struct UpdateStepCount
		{
			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x06001129 RID: 4393 RVA: 0x000515B8 File Offset: 0x0004F7B8
			// (set) Token: 0x0600112A RID: 4394 RVA: 0x000515C0 File Offset: 0x0004F7C0
			public uint value { readonly get; private set; }

			// Token: 0x0600112B RID: 4395 RVA: 0x000515CC File Offset: 0x0004F7CC
			public void OnBeforeUpdate()
			{
				this.m_WasUpdated = true;
				uint value = this.value;
				this.value = value + 1U;
			}

			// Token: 0x0600112C RID: 4396 RVA: 0x000515F0 File Offset: 0x0004F7F0
			public void OnUpdate()
			{
				if (!this.m_WasUpdated)
				{
					uint value = this.value;
					this.value = value + 1U;
				}
				this.m_WasUpdated = false;
			}

			// Token: 0x04000A5C RID: 2652
			private bool m_WasUpdated;
		}

		// Token: 0x020001CD RID: 461
		[Serializable]
		public struct SerializedState
		{
			// Token: 0x04000A5E RID: 2654
			public InputUpdateType lastUpdateType;

			// Token: 0x04000A5F RID: 2655
			public InputUpdate.UpdateStepCount playerUpdateStepCount;
		}
	}
}
