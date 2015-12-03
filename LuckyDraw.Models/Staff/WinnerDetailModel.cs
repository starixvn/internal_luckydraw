// ----------------------------------------------------------------------
// <copyright file="WinnerDetailModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.12.02">Created.</change>
// </history>

namespace LuckyDraw.Models.Staff
{
    using System.Collections.Generic;
    using LuckyDraw.Models.Prize;

    public class WinnerDetailModel
    {
        public WinnerDetailModel()
        {
            Prizes = new List<PrizeModel>();
            Winners = new List<StaffModel>();
        }

        public PrizeModel Prize { get; set; }

        public IList<PrizeModel> Prizes { get; set; }

        public IList<StaffModel> Winners { get; set; }
    }
}