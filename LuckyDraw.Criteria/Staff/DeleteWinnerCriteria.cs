// ----------------------------------------------------------------------
// <copyright file="DeleteWinnerCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.12.02">Created.</change>
// </history>

namespace LuckyDraw.Criteria.Staff
{
    using Fanex.Data.Repository;

    public class DeleteWinnerCriteria : CriteriaBase
    {
        public string StaffIds { get; set; }

        public int PrizeTypeId { get; set; }

        public int? CurrentPrize { get; set; }

        public override string GetSettingKey()
        {
            return "Winner_Delete";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(StaffIds) && PrizeTypeId > 0;
        }
    }
}