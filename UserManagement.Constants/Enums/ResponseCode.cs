using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Constants.Enums
{
    public enum ResponseCode : byte
    {
        [EnumMember] Success = 0,
        [EnumMember] Failed = 1,
        [EnumMember] InvalidRequest = 2,
        [EnumMember] DuplicateRow = 3,
        [EnumMember] RowNotFound = 4,
        [EnumMember] Committed = 5,
        [EnumMember] AlreadyCommitted = 6,
        [EnumMember] NotEditable = 7,
        [EnumMember] Error = 8,
        [EnumMember] Expired = 9
    }
}
