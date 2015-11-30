// ----------------------------------------------------------------------
// <copyright file="PrizeRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Repositories.Implements
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using LuckyDraw.Models.Prize;
    using LuckyDraw.Repositories.Interfaces;

    public class PrizeRepository : IPrizeRepository
    {
        public PrizeSettingModel GetPrizeSettingByPrizeType(ICriteria criteria)
        {
            PrizeSettingModel prizeSetting = new PrizeSettingModel();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    prizeSetting = multi.Read<PrizeSettingModel>().FirstOrDefault();

                    prizeSetting.Prizes = multi.Read<PrizeModel, PrizeTypeModel, PrizeModel>(
                    (prize, type) =>
                    {
                        prize.PrizeType = type;

                        return prize;
                    },
                    "PrizeTypeId");
                }
            }

            return prizeSetting;
        }

        public IEnumerable<PrizeModel> GetPrizeByPrizeType(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<PrizeModel, PrizeTypeModel, PrizeModel>(
                    (prize, type) =>
                    {
                        prize.PrizeType = type;

                        return prize;
                    },
                    "PrizeTypeId",
                    criteria.GetSpParams());
            }
        }
    }
}