using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Service.Model
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

    public enum FieldTypes
    {
        Number = 1,
        Date = 2,
        Text = 3,
        YesNo = 4,
        Telephone = 5,
        TextOnly = 6,
        SelectionList = 7,
        Address = 8,
        Name = 9,
        //SocialSecurityNumber = 10,
        //TablewithColumnsOnly = 11,
        //TableWithRowsAndColumns = 12,
        Checkbox = 13,
        SectionHeadingTextOnly = 14,
        MultiSelectSelectionList = 15,
        MultiSelectCheckboxes = 16,
        SectionHeader = 17,
        //PopUpDateSelection = 18,
        Currency = 19,
        //DBSelectionList = 20
    }

    public enum AccountActivity
    {
        Accounts,
        Activity
    }

    public enum ApproveStatus
    {
        Unapproved = 0,
        Approved = 1

    }
}
