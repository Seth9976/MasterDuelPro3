using System;
using UnityEngine.Networking;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200024F RID: 591
	internal static class UnityWebRequestResultExtensions
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x0002DEF8 File Offset: 0x0002C0F8
		public static bool IsError(this UnityWebRequest unityWebRequest)
		{
			UnityWebRequest.Result result = unityWebRequest.result;
			return result == UnityWebRequest.Result.ConnectionError || result == UnityWebRequest.Result.DataProcessingError || result == UnityWebRequest.Result.ProtocolError;
		}
	}
}
