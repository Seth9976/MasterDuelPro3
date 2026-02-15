using System;
using System.Reflection.Emit;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000127 RID: 295
	internal sealed class CompiledRegexRunnerFactory : RegexRunnerFactory
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x0001C7D1 File Offset: 0x0001A9D1
		public CompiledRegexRunnerFactory(DynamicMethod go, DynamicMethod firstChar, DynamicMethod trackCount)
		{
			this._goMethod = go;
			this._findFirstCharMethod = firstChar;
			this._initTrackCountMethod = trackCount;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001C7F0 File Offset: 0x0001A9F0
		protected internal override RegexRunner CreateInstance()
		{
			CompiledRegexRunner compiledRegexRunner = new CompiledRegexRunner();
			compiledRegexRunner.SetDelegates((Action<RegexRunner>)this._goMethod.CreateDelegate(typeof(Action<RegexRunner>)), (Func<RegexRunner, bool>)this._findFirstCharMethod.CreateDelegate(typeof(Func<RegexRunner, bool>)), (Action<RegexRunner>)this._initTrackCountMethod.CreateDelegate(typeof(Action<RegexRunner>)));
			return compiledRegexRunner;
		}

		// Token: 0x040004C1 RID: 1217
		private readonly DynamicMethod _goMethod;

		// Token: 0x040004C2 RID: 1218
		private readonly DynamicMethod _findFirstCharMethod;

		// Token: 0x040004C3 RID: 1219
		private readonly DynamicMethod _initTrackCountMethod;
	}
}
