// ----------------------------------------------------------------------
// <copyright file="PrizeSettingModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Models.Prize
{
    using System.Collections.Generic;

    public class PrizeSettingModel
    {
        public int PrizeTypeId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public int CurrentPrize { get; set; }

        public int Multiply { get; set; }

        public int ParentTypeId { get; set; }

        public IEnumerable<PrizeModel> Prizes { get; set; }
    }
}