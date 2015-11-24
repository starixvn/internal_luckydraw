// ----------------------------------------------------------------------
// <copyright file="DepartmentModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.23">Created.</change>
// </history>

namespace LuckyDraw.Models.Department
{
    public class DepartmentModel
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}