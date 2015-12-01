// ----------------------------------------------------------------------
// <copyright file="IPrizeService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Services.Interfaces
{
    using System.Collections.Generic;
    using LuckyDraw.Core.Model;
    using LuckyDraw.Criteria.Prize;
    using LuckyDraw.Models.Prize;

    public interface IPrizeService
    {
        PrizeSettingModel GetPrizeSettingByPrizeType(GetPrizeSettingByPrizeTypeCriteria criteria);

        IEnumerable<PrizeModel> GetPrizeByPrizeType(GetPrizeByPrizeTypeCriteria criteria);

        CommandResult UpdateCurrentPrize(UpdateCurrentPrizeCriteria criteria);
    }
}