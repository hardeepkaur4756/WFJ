using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WFJ.Web.Models.Enums
{
    public enum UserType
    {
        [Description("None")]
        None = 0,
        [Description("System Administrator")]
        SystemAdministrator = 1,
        [Description("Prepaid Legal")]
        PrepaidLegal = 2,
        [Description("Client Administrator")]
        ClientAdministrator = 3,
        [Description("Client Manager")]
        ClientManager = 4,
        [Description("Client User")]
        ClientUser = 5,
        [Description("WFJ User")]
        WFJUser = 6,
        [Description("Other Attorney")]
        OtherAttorney = 7,
        [Description("WFJ Admin")]
        WFJAdmin = 8,
        [Description("Other User")]
        OtherUser = 9,
        [Description("Other User 2")]
        OtherUser2 = 10,
        [Description("Document Center Only")]
        DocumentCenterOnly = 11
    }
}