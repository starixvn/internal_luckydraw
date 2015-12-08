// ----------------------------------------------------------------------
// <copyright file="Hubs.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.24">Created.</change>
// </history>

namespace LuckyDraw.Hubs
{
    using Microsoft.AspNet.SignalR;

    public class Hubs : Hub
    {
        public void SendStaffCode(string staffCode)
        {
            Clients.All.getStaffCode(staffCode);
        }

        public void SendMessageResponse(int grade, string message)
        {
            Clients.All.getMessageResponse(grade, message);
        }
    }
}