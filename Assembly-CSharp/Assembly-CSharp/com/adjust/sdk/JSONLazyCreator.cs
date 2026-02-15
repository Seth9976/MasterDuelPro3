using System;

namespace com.adjust.sdk
{
	// Token: 0x02000472 RID: 1138
	internal class JSONLazyCreator : JSONNode
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060025BB RID: 9659 RVA: 0x0000216D File Offset: 0x0000036D
		public override JSONNode Item
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060025BD RID: 9661 RVA: 0x0000216D File Offset: 0x0000036D
		public override int AsInt
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x060025BF RID: 9663 RVA: 0x0000216D File Offset: 0x0000036D
		public override float AsFloat
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000F165E File Offset: 0x000EF85E
		// (set) Token: 0x060025C1 RID: 9665 RVA: 0x0000216D File Offset: 0x0000036D
		public override double AsDouble
		{
			get
			{
				return 0.0;
			}
			set
			{
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060025C3 RID: 9667 RVA: 0x0000216D File Offset: 0x0000036D
		public override bool AsBool
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x0000216A File Offset: 0x0000036A
		public override JSONArray AsArray
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x0000216A File Offset: 0x0000036A
		public override JSONClass AsObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000F166D File Offset: 0x000EF86D
		public JSONLazyCreator(JSONNode aNode)
		{
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000F166D File Offset: 0x000EF86D
		public JSONLazyCreator(JSONNode aNode, string aKey)
		{
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0000216D File Offset: 0x0000036D
		private void Set(JSONNode aVal)
		{
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Add(JSONNode aItem)
		{
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Add(string aKey, JSONNode aItem)
		{
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool operator ==(JSONLazyCreator a, object b)
		{
			return false;
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool operator !=(JSONLazyCreator a, object b)
		{
			return false;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Equals(object obj)
		{
			return false;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString()
		{
			return null;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString(string aPrefix)
		{
			return null;
		}

		// Token: 0x04002739 RID: 10041
		private JSONNode m_Node;

		// Token: 0x0400273A RID: 10042
		private string m_Key;
	}
}
