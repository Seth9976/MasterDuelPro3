using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x02000262 RID: 610
	internal static class StateMachineUtility
	{
		// Token: 0x06000D9D RID: 3485 RVA: 0x0002EF4E File Offset: 0x0002D14E
		public static int GetState(IAsyncStateMachine stateMachine)
		{
			return (int)stateMachine.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).First((FieldInfo x) => x.Name.EndsWith("__state"))
				.GetValue(stateMachine);
		}
	}
}
