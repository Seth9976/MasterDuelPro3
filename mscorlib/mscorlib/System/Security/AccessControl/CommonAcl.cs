using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access control list (ACL) and is the base class for the <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> and <see cref="T:System.Security.AccessControl.SystemAcl" /> classes.</summary>
	// Token: 0x020003EC RID: 1004
	public abstract class CommonAcl : GenericAcl
	{
		// Token: 0x060021E9 RID: 8681 RVA: 0x0008D598 File Offset: 0x0008B798
		internal CommonAcl(bool isContainer, bool isDS, RawAcl rawAcl)
		{
			if (rawAcl == null)
			{
				rawAcl = new RawAcl(isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, 10);
			}
			else
			{
				byte[] array = new byte[rawAcl.BinaryLength];
				rawAcl.GetBinaryForm(array, 0);
				rawAcl = new RawAcl(array, 0);
			}
			this.Init(isContainer, isDS, rawAcl);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0008D5EE File Offset: 0x0008B7EE
		internal CommonAcl(bool isContainer, bool isDS, byte revision, int capacity)
		{
			this.Init(isContainer, isDS, new RawAcl(revision, capacity));
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0008D606 File Offset: 0x0008B806
		internal CommonAcl(bool isContainer, bool isDS, int capacity)
			: this(isContainer, isDS, isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, capacity)
		{
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0008D620 File Offset: 0x0008B820
		private void Init(bool isContainer, bool isDS, RawAcl rawAcl)
		{
			this.is_container = isContainer;
			this.is_ds = isDS;
			this.raw_acl = rawAcl;
			this.CanonicalizeAndClearAefa();
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object. This length should be used before marshaling the access control list (ACL) into a binary array by using the <see cref="M:System.Security.AccessControl.CommonAcl.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</returns>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x0008D63D File Offset: 0x0008B83D
		public sealed override int BinaryLength
		{
			get
			{
				return this.raw_acl.BinaryLength;
			}
		}

		/// <summary>Gets the number of access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</summary>
		/// <returns>The number of ACEs in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</returns>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x0008D64A File Offset: 0x0008B84A
		public sealed override int Count
		{
			get
			{
				return this.raw_acl.Count;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object are in canonical order.</summary>
		/// <returns>true if the ACEs in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object are in canonical order; otherwise, false.</returns>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x0008D657 File Offset: 0x0008B857
		public bool IsCanonical
		{
			get
			{
				return this.is_canonical;
			}
		}

		/// <summary>Sets whether the <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a container. </summary>
		/// <returns>true if the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a container.</returns>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x0008D65F File Offset: 0x0008B85F
		public bool IsContainer
		{
			get
			{
				return this.is_container;
			}
		}

		/// <summary>Sets whether the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a directory object access control list (ACL).</summary>
		/// <returns>true if the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a directory object ACL.</returns>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x0008D667 File Offset: 0x0008B867
		public bool IsDS
		{
			get
			{
				return this.is_ds;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (set) Token: 0x060021F2 RID: 8690 RVA: 0x0008D66F File Offset: 0x0008B86F
		internal bool IsAefa
		{
			set
			{
				this.is_aefa = value;
			}
		}

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.CommonAcl" />.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.CommonAcl" />.</returns>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x0008D678 File Offset: 0x0008B878
		public sealed override byte Revision
		{
			get
			{
				return this.raw_acl.Revision;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.AccessControl.CommonAce" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.CommonAce" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.AccessControl.CommonAce" /> to get or set.</param>
		// Token: 0x170003DA RID: 986
		public sealed override GenericAce this[int index]
		{
			get
			{
				return CommonAcl.CopyAce(this.raw_acl[index]);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CommonAcl" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CommonAcl" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		// Token: 0x060021F6 RID: 8694 RVA: 0x0008D698 File Offset: 0x0008B898
		public sealed override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			this.raw_acl.GetBinaryForm(binaryForm, offset);
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0008D6A7 File Offset: 0x0008B8A7
		internal void RequireCanonicity()
		{
			if (!this.IsCanonical)
			{
				throw new InvalidOperationException("ACL is not canonical.");
			}
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0008D6BC File Offset: 0x0008B8BC
		internal void CanonicalizeAndClearAefa()
		{
			this.RemoveAces<GenericAce>(new CommonAcl.RemoveAcesCallback<GenericAce>(this.IsAceMeaningless));
			this.is_canonical = this.TestCanonicity();
			if (this.IsCanonical)
			{
				this.ApplyCanonicalSortToExplicitAces();
				this.MergeExplicitAces();
			}
			this.IsAefa = false;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0008D6F8 File Offset: 0x0008B8F8
		internal virtual bool IsAceMeaningless(GenericAce ace)
		{
			AceFlags aceFlags = ace.AceFlags;
			KnownAce knownAce = ace as KnownAce;
			if (knownAce != null)
			{
				if (knownAce.AccessMask == 0)
				{
					return true;
				}
				if ((aceFlags & AceFlags.InheritOnly) != AceFlags.None)
				{
					if (knownAce is ObjectAce)
					{
						return true;
					}
					if (!this.IsContainer)
					{
						return true;
					}
					if ((aceFlags & (AceFlags.ObjectInherit | AceFlags.ContainerInherit)) == AceFlags.None)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0008D748 File Offset: 0x0008B948
		private bool TestCanonicity()
		{
			AceEnumerator aceEnumerator = base.GetEnumerator();
			while (aceEnumerator.MoveNext())
			{
				if (!(aceEnumerator.Current is QualifiedAce))
				{
					return false;
				}
			}
			bool flag = false;
			aceEnumerator = base.GetEnumerator();
			while (aceEnumerator.MoveNext())
			{
				if (((QualifiedAce)aceEnumerator.Current).IsInherited)
				{
					flag = true;
				}
				else if (flag)
				{
					return false;
				}
			}
			bool flag2 = false;
			foreach (GenericAce genericAce in this)
			{
				QualifiedAce qualifiedAce = (QualifiedAce)genericAce;
				if (qualifiedAce.IsInherited)
				{
					break;
				}
				if (qualifiedAce.AceQualifier == AceQualifier.AccessAllowed)
				{
					flag2 = true;
				}
				else if (AceQualifier.AccessDenied == qualifiedAce.AceQualifier && flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0008D7E8 File Offset: 0x0008B9E8
		internal int GetCanonicalExplicitDenyAceCount()
		{
			int num = 0;
			while (num < this.Count && !this.raw_acl[num].IsInherited)
			{
				QualifiedAce qualifiedAce = this.raw_acl[num] as QualifiedAce;
				if (qualifiedAce == null || qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
				{
					break;
				}
				num++;
			}
			return num;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0008D840 File Offset: 0x0008BA40
		internal int GetCanonicalExplicitAceCount()
		{
			int num = 0;
			while (num < this.Count && !this.raw_acl[num].IsInherited)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0008D874 File Offset: 0x0008BA74
		private void MergeExplicitAces()
		{
			int num = this.GetCanonicalExplicitAceCount();
			int i = 0;
			while (i < num - 1)
			{
				GenericAce genericAce = this.MergeExplicitAcePair(this.raw_acl[i], this.raw_acl[i + 1]);
				if (null != genericAce)
				{
					this.raw_acl[i] = genericAce;
					this.raw_acl.RemoveAce(i + 1);
					num--;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0008D8E4 File Offset: 0x0008BAE4
		private GenericAce MergeExplicitAcePair(GenericAce ace1, GenericAce ace2)
		{
			QualifiedAce qualifiedAce = ace1 as QualifiedAce;
			QualifiedAce qualifiedAce2 = ace2 as QualifiedAce;
			if (!(null != qualifiedAce) || !(null != qualifiedAce2))
			{
				return null;
			}
			if (qualifiedAce.AceQualifier != qualifiedAce2.AceQualifier)
			{
				return null;
			}
			if (!(qualifiedAce.SecurityIdentifier == qualifiedAce2.SecurityIdentifier))
			{
				return null;
			}
			AceFlags aceFlags = qualifiedAce.AceFlags;
			AceFlags aceFlags2 = qualifiedAce2.AceFlags;
			int accessMask = qualifiedAce.AccessMask;
			int accessMask2 = qualifiedAce2.AccessMask;
			if (!this.IsContainer)
			{
				aceFlags &= ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.NoPropagateInherit | AceFlags.InheritOnly);
				aceFlags2 &= ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.NoPropagateInherit | AceFlags.InheritOnly);
			}
			AceFlags aceFlags3;
			int num;
			if (aceFlags != aceFlags2)
			{
				if (accessMask != accessMask2)
				{
					return null;
				}
				if ((aceFlags & ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit)) == (aceFlags2 & ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit)))
				{
					aceFlags3 = aceFlags | aceFlags2;
					num = accessMask;
				}
				else
				{
					if ((aceFlags & ~(AceFlags.SuccessfulAccess | AceFlags.FailedAccess)) != (aceFlags2 & ~(AceFlags.SuccessfulAccess | AceFlags.FailedAccess)))
					{
						return null;
					}
					aceFlags3 = aceFlags | aceFlags2;
					num = accessMask;
				}
			}
			else
			{
				aceFlags3 = aceFlags;
				num = accessMask | accessMask2;
			}
			CommonAce commonAce = ace1 as CommonAce;
			CommonAce commonAce2 = ace2 as CommonAce;
			if (null != commonAce && null != commonAce2)
			{
				return new CommonAce(aceFlags3, commonAce.AceQualifier, num, commonAce.SecurityIdentifier, commonAce.IsCallback, commonAce.GetOpaque());
			}
			ObjectAce objectAce = ace1 as ObjectAce;
			ObjectAce objectAce2 = ace2 as ObjectAce;
			if (null != objectAce && null != objectAce2)
			{
				Guid guid;
				Guid guid2;
				CommonAcl.GetObjectAceTypeGuids(objectAce, out guid, out guid2);
				Guid guid3;
				Guid guid4;
				CommonAcl.GetObjectAceTypeGuids(objectAce2, out guid3, out guid4);
				if (guid == guid3 && guid2 == guid4)
				{
					return new ObjectAce(aceFlags3, objectAce.AceQualifier, num, objectAce.SecurityIdentifier, objectAce.ObjectAceFlags, objectAce.ObjectAceType, objectAce.InheritedObjectAceType, objectAce.IsCallback, objectAce.GetOpaque());
				}
			}
			return null;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0008DA8C File Offset: 0x0008BC8C
		private static void GetObjectAceTypeGuids(ObjectAce ace, out Guid type, out Guid inheritedType)
		{
			type = Guid.Empty;
			inheritedType = Guid.Empty;
			if ((ace.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
			{
				type = ace.ObjectAceType;
			}
			if ((ace.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
			{
				inheritedType = ace.InheritedObjectAceType;
			}
		}

		// Token: 0x06002200 RID: 8704
		internal abstract void ApplyCanonicalSortToExplicitAces();

		// Token: 0x06002201 RID: 8705 RVA: 0x0008DADC File Offset: 0x0008BCDC
		internal void ApplyCanonicalSortToExplicitAces(int start, int count)
		{
			for (int i = start + 1; i < start + count; i++)
			{
				KnownAce knownAce = (KnownAce)this.raw_acl[i];
				SecurityIdentifier securityIdentifier = knownAce.SecurityIdentifier;
				int num = i;
				while (num > start && ((KnownAce)this.raw_acl[num - 1]).SecurityIdentifier.CompareTo(securityIdentifier) > 0)
				{
					this.raw_acl[num] = this.raw_acl[num - 1];
					num--;
				}
				this.raw_acl[num] = knownAce;
			}
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0008DB68 File Offset: 0x0008BD68
		internal void RemoveAces<T>(CommonAcl.RemoveAcesCallback<T> callback) where T : GenericAce
		{
			int i = 0;
			while (i < this.raw_acl.Count)
			{
				if (this.raw_acl[i] is T && callback((T)((object)this.raw_acl[i])))
				{
					this.raw_acl.RemoveAce(i);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0008DBC8 File Offset: 0x0008BDC8
		internal void AddAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			QualifiedAce qualifiedAce = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
			this.AddAce(qualifiedAce);
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0008DBEC File Offset: 0x0008BDEC
		private QualifiedAce AddAceGetQualifiedAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			return new CommonAce(this.GetAceFlags(inheritanceFlags, propagationFlags, auditFlags), aceQualifier, accessMask, sid, false, null);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0008DC04 File Offset: 0x0008BE04
		private void AddAce(QualifiedAce newAce)
		{
			this.RequireCanonicity();
			int aceInsertPosition = this.GetAceInsertPosition(newAce.AceQualifier);
			this.raw_acl.InsertAce(aceInsertPosition, CommonAcl.CopyAce(newAce));
			this.CanonicalizeAndClearAefa();
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0008DC3C File Offset: 0x0008BE3C
		private static GenericAce CopyAce(GenericAce ace)
		{
			byte[] array = new byte[ace.BinaryLength];
			ace.GetBinaryForm(array, 0);
			return GenericAce.CreateFromBinaryForm(array, 0);
		}

		// Token: 0x06002207 RID: 8711
		internal abstract int GetAceInsertPosition(AceQualifier aceQualifier);

		// Token: 0x06002208 RID: 8712 RVA: 0x0008DC64 File Offset: 0x0008BE64
		private AceFlags GetAceFlags(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			if (inheritanceFlags != InheritanceFlags.None && !this.IsContainer)
			{
				throw new ArgumentException("Flags only work with containers.", "inheritanceFlags");
			}
			if (inheritanceFlags == InheritanceFlags.None && propagationFlags != PropagationFlags.None)
			{
				throw new ArgumentException("Propagation flags need inheritance flags.", "propagationFlags");
			}
			AceFlags aceFlags = AceFlags.None;
			if ((InheritanceFlags.ContainerInherit & inheritanceFlags) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ContainerInherit;
			}
			if ((InheritanceFlags.ObjectInherit & inheritanceFlags) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ObjectInherit;
			}
			if ((PropagationFlags.InheritOnly & propagationFlags) != PropagationFlags.None)
			{
				aceFlags |= AceFlags.InheritOnly;
			}
			if ((PropagationFlags.NoPropagateInherit & propagationFlags) != PropagationFlags.None)
			{
				aceFlags |= AceFlags.NoPropagateInherit;
			}
			if ((AuditFlags.Success & auditFlags) != AuditFlags.None)
			{
				aceFlags |= AceFlags.SuccessfulAccess;
			}
			if ((AuditFlags.Failure & auditFlags) != AuditFlags.None)
			{
				aceFlags |= AceFlags.FailedAccess;
			}
			return aceFlags;
		}

		// Token: 0x04001071 RID: 4209
		private bool is_aefa;

		// Token: 0x04001072 RID: 4210
		private bool is_canonical;

		// Token: 0x04001073 RID: 4211
		private bool is_container;

		// Token: 0x04001074 RID: 4212
		private bool is_ds;

		// Token: 0x04001075 RID: 4213
		internal RawAcl raw_acl;

		// Token: 0x020003ED RID: 1005
		// (Invoke) Token: 0x0600220A RID: 8714
		internal delegate bool RemoveAcesCallback<T>(T ace);
	}
}
