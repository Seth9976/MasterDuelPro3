using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001DB RID: 475
	internal class BsonObject : BsonToken, IEnumerable<BsonProperty>, IEnumerable
	{
		// Token: 0x06001006 RID: 4102 RVA: 0x000461A7 File Offset: 0x000443A7
		public void Add(string name, BsonToken token)
		{
			this._children.Add(new BsonProperty
			{
				Name = new BsonString(name, false),
				Value = token
			});
			token.Parent = this;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00033CED File Offset: 0x00031EED
		public override BsonType Type
		{
			get
			{
				return BsonType.Object;
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x000461D4 File Offset: 0x000443D4
		public IEnumerator<BsonProperty> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x000461E6 File Offset: 0x000443E6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000855 RID: 2133
		private readonly List<BsonProperty> _children = new List<BsonProperty>();
	}
}
