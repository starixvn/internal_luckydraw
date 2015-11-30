// ----------------------------------------------------------------------
// <copyright file="GetPrizeSettingByPrizeTypeCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Criteria.Prize
{
    using Fanex.Data.Repository;

    public class GetPrizeSettingByPrizeTypeCriteria : CriteriaBase
    {
        public int PrizeTypeId { get; set; }

        public override string GetSettingKey()
        {
            return "PrizeSetting_GetByPrizeType";
        }

        public override bool IsValid()
        {
            return PrizeTypeId > 0;
        }
    }
}