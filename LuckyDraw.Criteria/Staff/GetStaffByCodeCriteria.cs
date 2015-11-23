// ----------------------------------------------------------------------
// <copyright file="GetStaffByCodeCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.23">Created.</change>
// </history>

namespace LuckyDraw.Criteria.Staff
{
    using Fanex.Data.Repository;

    public class GetStaffByCodeCriteria : CriteriaBase
    {
        public string Code { get; set; }

        public override string GetSettingKey()
        {
            return "Staff_GetByCode";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Code);
        }
    }
}