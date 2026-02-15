using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200017D RID: 381
	public class JsonLoadSettings
	{
		// Token: 0x06000C96 RID: 3222 RVA: 0x00037FD9 File Offset: 0x000361D9
		public JsonLoadSettings()
		{
			this._lineInfoHandling = LineInfoHandling.Load;
			this._commentHandling = CommentHandling.Ignore;
			this._duplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00037FF6 File Offset: 0x000361F6
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x00037FFE File Offset: 0x000361FE
		public CommentHandling CommentHandling
		{
			get
			{
				return this._commentHandling;
			}
			set
			{
				if (value < CommentHandling.Ignore || value > CommentHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._commentHandling = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0003801A File Offset: 0x0003621A
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x00038022 File Offset: 0x00036222
		public LineInfoHandling LineInfoHandling
		{
			get
			{
				return this._lineInfoHandling;
			}
			set
			{
				if (value < LineInfoHandling.Ignore || value > LineInfoHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lineInfoHandling = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0003803E File Offset: 0x0003623E
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x00038046 File Offset: 0x00036246
		public DuplicatePropertyNameHandling DuplicatePropertyNameHandling
		{
			get
			{
				return this._duplicatePropertyNameHandling;
			}
			set
			{
				if (value < DuplicatePropertyNameHandling.Replace || value > DuplicatePropertyNameHandling.Error)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._duplicatePropertyNameHandling = value;
			}
		}

		// Token: 0x040006FC RID: 1788
		private CommentHandling _commentHandling;

		// Token: 0x040006FD RID: 1789
		private LineInfoHandling _lineInfoHandling;

		// Token: 0x040006FE RID: 1790
		private DuplicatePropertyNameHandling _duplicatePropertyNameHandling;
	}
}
