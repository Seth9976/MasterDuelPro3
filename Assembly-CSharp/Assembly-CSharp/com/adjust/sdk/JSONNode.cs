using System;
using System.Collections.Generic;
using System.IO;

namespace com.adjust.sdk
{
	// Token: 0x02000473 RID: 1139
	public class JSONNode
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060025D2 RID: 9682 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual JSONNode Item
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060025D6 RID: 9686 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IEnumerable<JSONNode> Childs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerable<JSONNode> DeepChilds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060025D8 RID: 9688 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060025D9 RID: 9689 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual int AsInt
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060025DA RID: 9690 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x060025DB RID: 9691 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual float AsFloat
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x000F165E File Offset: 0x000EF85E
		// (set) Token: 0x060025DD RID: 9693 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual double AsDouble
		{
			get
			{
				return 0.0;
			}
			set
			{
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060025DF RID: 9695 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual bool AsBool
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual JSONArray AsArray
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual JSONClass AsObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Add(string aKey, JSONNode aItem)
		{
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Add(JSONNode aItem)
		{
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual JSONNode Remove(string aKey)
		{
			return null;
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual JSONNode Remove(int aIndex)
		{
			return null;
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual JSONNode Remove(JSONNode aNode)
		{
			return null;
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString()
		{
			return null;
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual string ToString(string aPrefix)
		{
			return null;
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x0000216A File Offset: 0x0000036A
		public static implicit operator JSONNode(string s)
		{
			return null;
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x0000216A File Offset: 0x0000036A
		public static implicit operator string(JSONNode d)
		{
			return null;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool operator ==(JSONNode a, object b)
		{
			return false;
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool operator !=(JSONNode a, object b)
		{
			return false;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Equals(object obj)
		{
			return false;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x0000216A File Offset: 0x0000036A
		internal static string Escape(string aText)
		{
			return null;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x0000216A File Offset: 0x0000036A
		public static JSONNode Parse(string aJSON)
		{
			return null;
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Serialize(BinaryWriter aWriter)
		{
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x0000216D File Offset: 0x0000036D
		public void SaveToStream(Stream aData)
		{
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x0000216D File Offset: 0x0000036D
		public void SaveToCompressedStream(Stream aData)
		{
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x0000216D File Offset: 0x0000036D
		public void SaveToCompressedFile(string aFileName)
		{
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x0000216A File Offset: 0x0000036A
		public string SaveToCompressedBase64()
		{
			return null;
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x0000216A File Offset: 0x0000036A
		public static JSONNode Deserialize(BinaryReader aReader)
		{
			return null;
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x0000216A File Offset: 0x0000036A
		public static JSONNode LoadFromCompressedFile(string aFileName)
		{
			return null;
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x0000216A File Offset: 0x0000036A
		public static JSONNode LoadFromCompressedStream(Stream aData)
		{
			return null;
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x0000216A File Offset: 0x0000036A
		public static JSONNode LoadFromCompressedBase64(string aBase64)
		{
			return null;
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x0000216A File Offset: 0x0000036A
		public static JSONNode LoadFromStream(Stream aData)
		{
			return null;
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x0000216A File Offset: 0x0000036A
		public static JSONNode LoadFromBase64(string aBase64)
		{
			return null;
		}
	}
}
