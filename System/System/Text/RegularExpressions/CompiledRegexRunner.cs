using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000126 RID: 294
	internal sealed class CompiledRegexRunner : RegexRunner
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x0001C790 File Offset: 0x0001A990
		public void SetDelegates(Action<RegexRunner> go, Func<RegexRunner, bool> firstChar, Action<RegexRunner> trackCount)
		{
			this._goMethod = go;
			this._findFirstCharMethod = firstChar;
			this._initTrackCountMethod = trackCount;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001C7A7 File Offset: 0x0001A9A7
		protected override void Go()
		{
			this._goMethod(this);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001C7B5 File Offset: 0x0001A9B5
		protected override bool FindFirstChar()
		{
			return this._findFirstCharMethod(this);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001C7C3 File Offset: 0x0001A9C3
		protected override void InitTrackCount()
		{
			this._initTrackCountMethod(this);
		}

		// Token: 0x040004BE RID: 1214
		private Action<RegexRunner> _goMethod;

		// Token: 0x040004BF RID: 1215
		private Func<RegexRunner, bool> _findFirstCharMethod;

		// Token: 0x040004C0 RID: 1216
		private Action<RegexRunner> _initTrackCountMethod;
	}
}
