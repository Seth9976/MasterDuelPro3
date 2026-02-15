using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001DC RID: 476
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x0600100B RID: 4107 RVA: 0x00046201 File Offset: 0x00044401
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0003731C File Offset: 0x0003551C
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00046216 File Offset: 0x00044416
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00046228 File Offset: 0x00044428
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000856 RID: 2134
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
