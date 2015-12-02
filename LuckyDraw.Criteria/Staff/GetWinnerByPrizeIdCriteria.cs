// ----------------------------------------------------------------------
// <copyright file="GetWinnerByPrizeIdCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.12.02">Created.</change>
// </history>

namespace LuckyDraw.Criteria.Staff
{
    using Fanex.Data.Repository;

    public class GetWinnerByPrizeIdCriteria : CriteriaBase
    {
        public string PrizeIds { get; set; }

        public override string GetSettingKey()
        {
            return "Winner_GetByPrizeId";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(PrizeIds);
        }
    }
}