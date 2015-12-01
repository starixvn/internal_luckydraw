// ----------------------------------------------------------------------
// <copyright file="UpdateCurrentPrizeCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.30">Created.</change>
// </history>

namespace LuckyDraw.Criteria.Prize
{
    using Fanex.Data.Repository;

    public class UpdateCurrentPrizeCriteria : CriteriaBase
    {
        public int PrizeTypeId { get; set; }

        public int CurrentPrize { get; set; }

        public override string GetSettingKey()
        {
            return "PrizeSetting_UpdateCurrentPrize";
        }

        public override bool IsValid()
        {
            return PrizeTypeId > 0 && CurrentPrize >= 0;
        }
    }
}