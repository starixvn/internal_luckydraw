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

        public void SendSignalEnter()
        {
            Clients.All.getSignalEnter();
        }

        public void SendSignalEsc()
        {
            Clients.All.getSignalEsc();
        }

        public void SendSignalNumber(int number)
        {
            Clients.All.getSignalNumber(number);
        }

        public void SendSignalCharacterS()
        {
            Clients.All.getSignalCharacterS();
        }

        public void SendSignalMoveUp()
        {
            Clients.All.getSignalMoveUp();
        }

        public void SendSignalMoveDown()
        {
            Clients.All.getSignalMoveDown();
        }

        public void SendSignalDelete()
        {
            Clients.All.getSignalDelete();
        }

        public void SendSignalCtrlZ()
        {
            Clients.All.getSignalCtrlZ();
        }

        public void SendSignalCtrlY()
        {
            Clients.All.getSignalCtrlY();
        }

        public void SendSignalCtrlE()
        {
            Clients.All.getSignalCtrlE();
        }

        public void SendSignalCtrlD()
        {
            Clients.All.getSignalCtrlD();
        }
    }
}