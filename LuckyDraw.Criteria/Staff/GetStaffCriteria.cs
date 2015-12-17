// ----------------------------------------------------------------------
// <copyright file="GetStaffCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.12.17">Created.</change>
// </history>

namespace LuckyDraw.Criteria.Staff
{
    using Fanex.Data.Repository;

    public class GetStaffCriteria : CriteriaBase
    {
        public string Query { get; set; }

        public override string GetSettingKey()
        {
            return "Staff_Get";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Query);
        }
    }
}