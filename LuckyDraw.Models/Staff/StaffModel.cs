// ----------------------------------------------------------------------
// <copyright file="StaffModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.23">Created.</change>
// </history>

namespace LuckyDraw.Models.Staff
{
    using System;

    public class StaffModel
    {
        public int StaffId { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public string Code { get; set; }

        public string BarCode { get; set; }

        public Byte[] Avatar { get; set; }
    }
}