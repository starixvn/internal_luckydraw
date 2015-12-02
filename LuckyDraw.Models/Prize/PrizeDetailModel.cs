// ----------------------------------------------------------------------
// <copyright file="PrizeDetailModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.12.02">Created.</change>
// </history>

namespace LuckyDraw.Models.Prize
{
    using System.Collections.Generic;
    using LuckyDraw.Models.Staff;

    public class PrizeDetailModel
    {
        public PrizeDetailModel()
        {
            Prizes = new List<PrizeModel>();
            Winners = new List<StaffModel>();
        }

        public PrizeModel Prize { get; set; }

        public IList<PrizeModel> Prizes { get; set; }

        public IList<StaffModel> Winners { get; set; }
    }
}